using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Kaiser_Adventure.Utilities;
using Kaiser_Adventure.Entities;

namespace Kaiser_Adventure {
    
    public class Main : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public SpriteFont font;

        public static Main _;

        public Main() {
            _ = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize() {

            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            GameState baseState = new TestState(this, 1);
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
