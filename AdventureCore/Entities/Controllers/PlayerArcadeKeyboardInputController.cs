using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AdventureCore.Utilities;

namespace AdventureCore.Entities.Controllers {
    public class PlayerArcadeKeyboardInputController : Controller{

        public float turningSpeed;
        public float moveSpeed;

        public PlayerArcadeKeyboardInputController(Character parent) : base(parent) {
            turningSpeed = 2 * (float)Math.PI;
            moveSpeed = 1.5f;
        }

        public override void DoControlUpdate(GameTime gameTime, Camera2D camera) {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            
            // rotation
            if (keyboardState.IsKeyDown(Keys.Q)) {
                parent.Rotation -= deltaTime * turningSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.W)) {
                parent.Rotation += deltaTime * turningSpeed;
            }

            // movement
            if (keyboardState.IsKeyDown(Keys.Up)) {
                parent.Position -= new Vector2(0, 250) * deltaTime * moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Down)) {
                parent.Position += new Vector2(0, 250) * deltaTime * moveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Left)) {
                parent.Position -= new Vector2(250, 0) * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.Right)) {
                parent.Position += new Vector2(250, 0) * deltaTime;
            }
        }
    }
}
