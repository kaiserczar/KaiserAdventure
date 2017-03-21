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

        public Texture2D staticImage;
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Controller controller;
        public Controller secondaryController;

        public float turningSpeed;
        public float moveSpeed;

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

                if (secondaryController != null) {
                    secondaryController.DoControlUpdate(gameTime);
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb) {

            sb.Draw(staticImage,
                    Position,
                    new Rectangle(0, 0, staticImage.Width, staticImage.Height),
                    Color.White,
                    Rotation,
                    new Vector2(staticImage.Width / 2, staticImage.Height / 2),
                    1.0f,
                    SpriteEffects.None,
                    1);
        }
    }

}
