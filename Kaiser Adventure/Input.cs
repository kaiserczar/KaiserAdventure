using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AdventureCore.Network;
using AdventureCore.Utilities;
using AdventureCore.Entities;

namespace AdventureClient{

    public class Input{

        public static int counter = 0;
        public static float MoveSpeed = 1.5f;
        public GameState state;

        public Vector2 Position;
        public float Rotation;
        public Vector2 LastSentPosition { get; private set; }
        public float LastSentRotation { get; private set; }
        public bool NeedsMoveSend = false;

        public Input(GameState gs){
            state = gs;
        }

        public void Update(GameTime gameTime, Camera2D camera) {

            Position = ((TestState)state).player.Position;
            Rotation = ((TestState)state).player.Rotation;

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            // NOTE: In Monogame, angles are: 0 straight up, 2pi total circle.

            Vector2 targetLoc = camera.ScreenToWorld(mouseState.Position.X, mouseState.Position.Y);
            Vector2 currentLoc = Position;
            float currentAngle = Rotation;
            float angle = (float)Math.Atan2(targetLoc.Y - currentLoc.Y, targetLoc.X - currentLoc.X);
            Rotation = angle + (float)Math.PI / 2;

            if (keyboardState.IsKeyDown(Keys.Up)) {
                Position -= new Vector2(0, 250) * deltaTime * MoveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Down)) {
                Position += new Vector2(0, 250) * deltaTime * MoveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Left)) {
                Position -= new Vector2(250, 0) * deltaTime * MoveSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Right)) {
                Position += new Vector2(250, 0) * deltaTime * MoveSpeed;
            }

            // TODO REMOVE THIS FOR DEBUG ONLY
            Position += new Vector2(250, 0) * deltaTime * MoveSpeed * 0.1f;

            if(mouseState.LeftButton == ButtonState.Pressed | keyboardState.IsKeyDown(Keys.Space)) {
                counter++;
                ((TestState)state).player.Shoot();
            }

            if (Math.Abs(LastSentPosition.X - Position.X) >= 1 | Math.Abs(LastSentPosition.Y - Position.Y) >= 1 | Math.Abs(LastSentRotation - Rotation) >= Math.PI / 20) {
                //PlayerMovePacket.New(Position, Rotation).Send(main.Client);
                this.NeedsMoveSend = true;
            }


            

        }

        public void SentMove() {
            this.NeedsMoveSend = false;
            this.LastSentPosition = this.Position;
            this.LastSentRotation = this.Rotation;
        }
    }

}
