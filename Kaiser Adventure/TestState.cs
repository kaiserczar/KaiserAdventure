using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Kaiser_Adventure.Utilities;
using Kaiser_Adventure.Entities;
using Kaiser_Adventure.Entities.Controllers;

namespace Kaiser_Adventure {

    class TestState : GameState {

        public static TestState _;

        private SpriteBatch spriteBatch;
        private RainbowColorCycler colorCycle;
        public Camera2D camera;

        private Character player;
        private List<Character> npcs = new List<Character>();
        private PositionMatcher cameraLock;

        private Texture2D cursor;

        public TestState(Main main) : base(main) {
            TestState._ = this;

            this.colorCycle = new RainbowColorCycler(4.0);
            camera = new Camera2D(main.GraphicsDevice.Viewport);

            this.player = new PlayerCharacter();
            this.cameraLock = new PositionMatcher(player, camera, false);

            this.cursor = main.Content.Load<Texture2D>("shieldCursor");

            Random rand = new Random();

            for (int i=0; i<5; i++) {
                Character npc = new Character();
                npc.Position = new Vector2(rand.Next(main.GraphicsDevice.Viewport.Width), rand.Next(main.GraphicsDevice.Viewport.Height));
                npc.controller = new AIZombieController(npc, player);
                npcs.Add(npc);
            }
        }

        public override void Update(GameTime gameTime) {
            colorCycle.Update(gameTime);
            camera.Update(gameTime);
            player.Update(gameTime);
            foreach(Character npc in npcs) {
                npc.Update(gameTime);
            }
            cameraLock.Syncronize();
        }

        public override void Draw(GameTime gameTime) {
            int midX = main.GraphicsDevice.Viewport.Width / 2;
            int midY = main.GraphicsDevice.Viewport.Height / 2;

            this.spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());

            //this.spriteBatch.DrawString(main.font, "State "+stateNum.ToString()+": "+ counter.ToString(), new Vector2(midX, midY), colorCycle.color);
            this.spriteBatch.DrawString(main.font, "Angle: " + npcs[0].Rotation.ToString(), new Vector2(10, 10), Color.Red);

            foreach (Character c in npcs) {
                c.Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);

            Point mousePos = Mouse.GetState().Position;
            Vector2 mouse = camera.ScreenToWorld(mousePos.X, mousePos.Y);

            this.spriteBatch.Draw(cursor, new Rectangle((int)mouse.X - cursor.Width/2, (int)mouse.Y - cursor.Height/2, cursor.Width, cursor.Height), Color.White);

            this.spriteBatch.End();
        }

        protected override void OnEnter() {
            this.spriteBatch = new SpriteBatch(main.GraphicsDevice);
        }

        protected override void OnExit() {
            this.spriteBatch.Dispose();
        }
    }
}
