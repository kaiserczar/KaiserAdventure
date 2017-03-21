using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Kaiser_Adventure.Utilities;
using Kaiser_Adventure.Entities.Controllers;

namespace Kaiser_Adventure.Entities {

    class Character : Entity {

        protected readonly Main main;

        protected Texture2D staticImage;
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        protected Controller controller;

        public Character() {
            main = Main._;
            staticImage = main.Content.Load<Texture2D>("character");
            Position = new Vector2(main.GraphicsDevice.Viewport.Width / 2, main.GraphicsDevice.Viewport.Height / 2);
            Rotation = 0;
            this.staticImage = main.Content.Load<Texture2D>("character");
            
        }


        public override void Update(GameTime gameTime) {
            if (controller != null) {
                controller.DoControlUpdate(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb) {
            //sb.Draw(staticImage, new Rectangle((int)Position.X - staticImage.Width / 2, (int)Position.Y - staticImage.Height /2, staticImage.Width, staticImage.Height), Color.White);

            sb.Draw(staticImage,
                    Position,
                    new Rectangle(0, 0, staticImage.Width, staticImage.Height),
                    Color.White,
                    Rotation,
                    new Vector2(staticImage.Width / 2, staticImage.Height / 2),
                    1.0f,
                    SpriteEffects.None,
                    1);

            /*
            spriteBatch.Draw(arrow,
                             new Vector2(400, 240), // Location
                             new Rectangle(0, 0, arrow.Width, arrow.Height), // Source Rectangle
                             Color.White,
                             angle,
                             new Vector2(arrow.Width / 2, arrow.Height), // Origin point
                             1.0f,
                             SpriteEffects.None,
                             1);
            */
        }
    }

}
