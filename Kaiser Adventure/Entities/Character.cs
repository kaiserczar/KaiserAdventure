using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Kaiser_Adventure.Utilities;

namespace Kaiser_Adventure.Entities {

    class Character : Entity {

        private readonly Main main;

        private Texture2D staticImage;
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }

        public Character() {
            main = Main._;
            staticImage = main.Content.Load<Texture2D>("character");
            Position = new Vector2(main.GraphicsDevice.Viewport.Width / 2, main.GraphicsDevice.Viewport.Height / 2);
            this.staticImage = main.Content.Load<Texture2D>("character");
        }


        public override void Update(GameTime gameTime) {
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb) {
            sb.Draw(staticImage, new Rectangle((int)Position.X - staticImage.Width / 2, (int)Position.Y - staticImage.Height /2, staticImage.Width, staticImage.Height), Color.White);
        }
    }

}
