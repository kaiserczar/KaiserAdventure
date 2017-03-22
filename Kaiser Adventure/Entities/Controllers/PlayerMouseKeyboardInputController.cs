using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Kaiser_Adventure.Entities.Controllers {

    class PlayerMouseKeyboardInputController : Controller {

        public PlayerMouseKeyboardInputController(Character parent) : base(parent) {
            this.parent.moveSpeed = 1.5f;
        }

        public override void DoControlUpdate(GameTime gameTime) {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            // NOTE: In Monogame, angles are: 0 straight up, 2pi total circle.

            // Rotation
            Vector2 targetLoc = TestState._.camera.ScreenToWorld(mouseState.Position.X, mouseState.Position.Y);
            Vector2 currentLoc = parent.Position;
            float angle = (float)Math.Atan2(targetLoc.Y - currentLoc.Y, targetLoc.X - currentLoc.X);
            this.parent.Rotation = angle + (float)Math.PI / 2;

            // movement
            //if (keyboardState.IsKeyDown(Keys.Up)) {
            //    parent.Position -= new Vector2(-250 * (float)Math.Sin(parent.Rotation), 250 * (float)Math.Cos(parent.Rotation)) * deltaTime * parent.moveSpeed;
            //}

            //if (keyboardState.IsKeyDown(Keys.Down)) {
            //    parent.Position += new Vector2(-250 * (float)Math.Sin(parent.Rotation), 250 * (float)Math.Cos(parent.Rotation)) * deltaTime * parent.moveSpeed;
            //}
            // movement
            if (keyboardState.IsKeyDown(Keys.Up)) {
                parent.Position -= new Vector2(0, 250) * deltaTime * parent.moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Down)) {
                parent.Position += new Vector2(0, 250) * deltaTime * parent.moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Left)) {
                parent.Position -= new Vector2(250, 0) * deltaTime * parent.moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Right)) {
                parent.Position += new Vector2(250, 0) * deltaTime * parent.moveSpeed;
            }
        }
    }

}
