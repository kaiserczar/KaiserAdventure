using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AdventureCore.Utilities;
using AdventureCore.Entities;
using Lidgren.Network;
using AdventureCore.Network;

namespace AdventureClient {
    
    public class Main : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public SpriteFont font;

        public static Main _;
        public static NetClient Client;

        public Main() {
            _ = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            NetPeerConfiguration npConfig = new NetPeerConfiguration("Adventure");
            Client = new NetClient(npConfig);
            Client.Start();
            Client.Connect("174.105.224.148", 14242);
        }
        
        protected override void Initialize() {

            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            PlayerConnectPacket.New(Program.UserName).Send(Client);

            GameState baseState = new TestState(this);
            GameState.Push(baseState);

            base.Initialize();
        }
        
        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Score");
        }
        protected override void UnloadContent() {
        }
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            GameState.ManagerUpdate(this, gameTime);

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.DarkGreen);
            
            GameState.ManagerDraw(this, gameTime);

            base.Draw(gameTime);
        }
    }
}
