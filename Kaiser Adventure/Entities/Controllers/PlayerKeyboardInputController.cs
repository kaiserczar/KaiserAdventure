using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Kaiser_Adventure.Entities.Controllers {

    class PlayerKeyboardInputController : Controller {


        public PlayerKeyboardInputController(Character parent) : base(parent) {
            this.parent.turningSpeed = (float)Math.PI;
            this.parent.moveSpeed = 1.5f;
        }

        public override void DoControlUpdate(GameTime gameTime) {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();

            // NOTE: In Monogame, angles are: 0 straight up, 2pi total circle.

            // movement
            if (keyboardState.IsKeyDown(Keys.Up)) {
                parent.Position -= new Vector2(-250 * (float)Math.Sin(parent.Rotation), 250 * (float)Math.Cos(parent.Rotation)) * deltaTime * parent.moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Down)) {
                parent.Position += new Vector2(-250 * (float)Math.Sin(parent.Rotation), 250 * (float)Math.Cos(parent.Rotation)) * deltaTime * parent.moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Left)) {
                parent.Rotation -= deltaTime * parent.turningSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Right)) {
                parent.Rotation += deltaTime * parent.turningSpeed;
            }
        }
    }
}
