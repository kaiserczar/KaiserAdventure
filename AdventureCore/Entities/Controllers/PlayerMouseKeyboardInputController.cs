using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AdventureCore.Network;
using AdventureCore.Utilities;

namespace AdventureCore.Entities.Controllers {

    public class PlayerMouseKeyboardInputController : Controller {

        public static int counter = 0;

        public PlayerMouseKeyboardInputController(Character parent) : base(parent) {
            this.parent.moveSpeed = 1.5f;
        }

        public override void DoControlUpdate(GameTime gameTime, Camera2D camera) {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            // NOTE: In Monogame, angles are: 0 straight up, 2pi total circle.

            Vector2 targetLoc = camera.ScreenToWorld(mouseState.Position.X, mouseState.Position.Y);
            Vector2 currentLoc = parent.Position;
            float currentAngle = parent.Rotation;
            float angle = (float)Math.Atan2(targetLoc.Y - currentLoc.Y, targetLoc.X - currentLoc.X);
            this.parent.Rotation = angle + (float)Math.PI / 2;

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

            if(mouseState.LeftButton == ButtonState.Pressed & parent is PlayerCharacter) {
                counter++;
                ((PlayerCharacter)parent).Shoot();
            }

        }
    }

}
