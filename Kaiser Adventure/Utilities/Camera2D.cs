using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Kaiser_Adventure.Utilities {

    public class Camera2D {
        private readonly Viewport _viewport;

        public Camera2D(Viewport viewport) {
            _viewport = viewport;

            Rotation = 0;
            Zoom = 1;
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            Position = Vector2.Zero;
        }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }
        public Vector2 Origin { get; set; }

        public Matrix GetViewMatrix() {
            return
                Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public void Update(GameTime gameTime) {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();

            // rotation
            if (keyboardState.IsKeyDown(Keys.Q))
                Rotation -= deltaTime;

            if (keyboardState.IsKeyDown(Keys.W))
                Rotation += deltaTime;

            // movement
            if (keyboardState.IsKeyDown(Keys.Up))
                Position -= new Vector2(0, 250) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Down))
                Position += new Vector2(0, 250) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Left))
                Position -= new Vector2(250, 0) * deltaTime;

            if (keyboardState.IsKeyDown(Keys.Right))
                Position += new Vector2(250, 0) * deltaTime;
        }

    }
}
