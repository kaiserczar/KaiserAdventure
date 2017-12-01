using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AdventureCore.Utilities;
using AdventureCore.Entities.Controllers;
using AdventureCore.Network;

namespace AdventureCore.Entities {

    public class Character : Entity {

        public Texture2D staticImage;

        public Game main;

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }

        public Controller controller;
        public Controller secondaryController;

        public float turningSpeed = 1f;
        public float moveSpeed = 3f;

        public Character(Game main) {
            this.main = main;
            staticImage = this.main.Content.Load<Texture2D>("character");
            Position = new Vector2(this.main.GraphicsDevice.Viewport.Width / 2, this.main.GraphicsDevice.Viewport.Height / 2);
            Rotation = 0;
            
        }


        public override void Update(GameTime gameTime, Camera2D camera) {

            if (controller != null) {
                controller.DoControlUpdate(gameTime, camera);

                if (secondaryController != null) {
                    secondaryController.DoControlUpdate(gameTime, camera);
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

        public void Shoot() {

        }
    }

}
