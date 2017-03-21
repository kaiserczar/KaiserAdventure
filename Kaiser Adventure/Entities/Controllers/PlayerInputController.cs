using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Kaiser_Adventure.Entities.Controllers {

    class PlayerInputController : Controller {

        public float turningSpeed;
        public float moveSpeed;

        public PlayerInputController(Character parent) : base(parent) {
            turningSpeed = (float)Math.PI;
            moveSpeed = 1.5f;
        }

        public override void DoControlUpdate(GameTime gameTime) {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();

            /*
            // rotation
            if (keyboardState.IsKeyDown(Keys.Q)) {
                parent.Rotation -= deltaTime * turningSpeed;
                //camera.Rotation -= deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.W)) {
                parent.Rotation += deltaTime * turningSpeed;
                //camera.Rotation += deltaTime;
            }
            */

            // NOTE: In Monogame, angles are: 0 straight up, 2pi total circle.

            // movement
            if (keyboardState.IsKeyDown(Keys.Up)) {
                parent.Position -= new Vector2(-250 * (float)Math.Sin(parent.Rotation), 250 * (float)Math.Cos(parent.Rotation)) * deltaTime * moveSpeed;
                //camera.Position -= new Vector2(0, 250) * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.Down)) {
                parent.Position += new Vector2(-250 * (float)Math.Sin(parent.Rotation), 250 * (float)Math.Cos(parent.Rotation)) * deltaTime * moveSpeed;
                //camera.Position += new Vector2(0, 250) * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.Left)) {
                parent.Rotation -= deltaTime * turningSpeed;
                //parent.Position -= new Vector2(250, 0) * deltaTime;
                //camera.Position -= new Vector2(250, 0) * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.Right)) {
                parent.Rotation += deltaTime * turningSpeed;
                //parent.Position += new Vector2(250, 0) * deltaTime;
                //camera.Position += new Vector2(250, 0) * deltaTime;
            }
        }
    }
}
