using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AdventureCore.Entities;
using AdventureCore.Entities.Controllers;
using AdventureCore.Utilities;
using AdventureCore.Network;
using Lidgren.Network;
using AdventureCore;

namespace AdventureClient {

    public class TestState : GameState {

        public static TestState _;

        public SpriteBatch spriteBatch;
        public RainbowColorCycler colorCycle;
        public Camera2D camera;
        public Input input;

        public Dictionary<Player, Character> players;

        public PlayerCharacter player;
        public List<Character> npcs = new List<Character>();
        public PositionMatcher cameraLock;

        public static Texture2D cursor = Main._.Content.Load<Texture2D>("shieldCursor");
        public static Texture2D bullet = Main._.Content.Load<Texture2D>("bullet1");

        public TestState(Main main) : base(main) {
            TestState._ = this;
            input = new Input(this);



            this.colorCycle = new RainbowColorCycler(4.0);
            camera = new Camera2D(main.GraphicsDevice.Viewport);

            this.player = new PlayerCharacter(Main._);
            this.players = new Dictionary<Player, Character>();
            this.player.bulletImg = bullet; 
            this.cameraLock = new PositionMatcher(player, camera, false);

            Random rand = new Random();

            /*
            for (int i=0; i<5; i++) {
                Character npc = new Character(Main._);
                npc.Position = new Vector2(rand.Next(main.GraphicsDevice.Viewport.Width), rand.Next(main.GraphicsDevice.Viewport.Height));
                npc.controller = new AIZombieController(npc, player);
                //npc.moveSpeed = ((float)rand.NextDouble()+0.5f) * npc.moveSpeed;
                npc.moveSpeed = 1.8f * (float)rand.NextDouble();
                npcs.Add(npc);
            }
            */
        }

        public override void Update(GameTime gameTime) {

            NetIncomingMessage msg;
            while ((msg = Main.Client.ReadMessage()) != null) {
                if (msg.MessageType == NetIncomingMessageType.Data) {
                    PacketType packetType = (PacketType)msg.ReadByte();

                    switch (packetType) {
                        case PacketType.PlayerConnectPacket:
                            PlayerConnectPacket connectPacket = PlayerConnectPacket.FromMessage(msg);
                            break;
                        case PacketType.PlayerChatPacket:
                            PlayerChatPacket chatPacket = PlayerChatPacket.FromMessage(msg);
                            break;
                        case PacketType.PlayerMovePacket:
                            PlayerMovePacket movePacket = PlayerMovePacket.FromMessage(msg);
                            if (movePacket.Username == Program.UserName) {
                                player.Rotation = movePacket.Rotation;
                                player.Position = movePacket.Position;
                            } else {

                            }
                            break;
                        case PacketType.PlayerListPacket:

                            PlayerListPacket listPacket = PlayerListPacket.FromMessage(msg);
                            HandlePlayerList(listPacket.Players);
                            break;
                        default:
                            break;
                    }
                }
            }

            input.Update(gameTime, camera);
            colorCycle.Update(gameTime);
            camera.Update(gameTime);
            player.Update(gameTime, camera);
            if (input.NeedsMoveSend) {
                PlayerMovePacket.New(Program.UserName, input.Position, input.Rotation).Send(Main.Client);
                input.SentMove();
            }
            foreach(Character npc in npcs) {
                npc.Update(gameTime, camera);
            }

            foreach(Character c in players.Values.AsEnumerable()) {
                c.Update(gameTime, camera);
            }

            cameraLock.Syncronize();
            Bullet.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            int midX = main.GraphicsDevice.Viewport.Width / 2;
            int midY = main.GraphicsDevice.Viewport.Height / 2;

            this.spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());

            //this.spriteBatch.DrawString(main.font, "State "+stateNum.ToString()+": "+ counter.ToString(), new Vector2(midX, midY), colorCycle.color);
            //this.spriteBatch.DrawString(main.font, "Angle: " + npcs[0].Rotation.ToString(), new Vector2(10, 10), Color.Red);
            this.spriteBatch.DrawString(((Main)main).font, "Bullets: " + Bullet.bullets.Count, new Vector2(10, 10), Color.Red);

            foreach (Character c in npcs) {
                c.Draw(gameTime, spriteBatch);
            }

            foreach (Character c in players.Values.AsEnumerable()) {
                c.Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);

            Point mousePos = Mouse.GetState().Position;
            Vector2 mouse = camera.ScreenToWorld(mousePos.X, mousePos.Y);

            Bullet.Draw(gameTime, this.spriteBatch);

            this.spriteBatch.Draw(cursor, new Rectangle((int)mouse.X - cursor.Width/2, (int)mouse.Y - cursor.Height/2, cursor.Width, cursor.Height), Color.White);

            this.spriteBatch.End();
        }

        protected override void OnEnter() {
            this.spriteBatch = new SpriteBatch(main.GraphicsDevice);
        }

        protected override void OnExit() {
            this.spriteBatch.Dispose();
        }

        public void HandlePlayerList(List<Player> players) {
            List<Player> toBeRemoved = new List<Player>();
            foreach (KeyValuePair<Player,Character> pair in this.players) {
                if (players.Contains(pair.Key)) { // already exists in both
                    // Just assume we already have correct position. Much easier.
                    players.Remove(pair.Key); // So we can add the remaining at end.
                } else {
                    // No longer connected.
                    toBeRemoved.Add(pair.Key);
                }
            }
            foreach (Player p in toBeRemoved) {
                this.players.Remove(p);
            }
            // Loop through rest of player list to find new players.
            foreach (Player p in players) {
                if (p.Username != Program.UserName) { // Don't want ourselves in playerlist
                    Character c = new Character(Main._);
                    c.Position = p.Position;
                    c.Rotation = p.Rotation;
                    this.players.Add(p, c);
                }
            }
        }
    }
}
