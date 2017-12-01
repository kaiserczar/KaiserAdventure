using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdventureCore.Entities {
    public class Bullet {

        public static List<Bullet> bullets = new List<Bullet>();

        private Character shooter;
        public Vector2 Position;
        public float Rotation;
        public float moveSpeed;
        public Texture2D img;

        public Bullet(Character shooter, Vector2 Position, float Rotation, Texture2D img) {
            this.shooter = shooter;
            this.Position = Position;
            this.Rotation = Rotation;
            this.moveSpeed = 3f;
            this.img = img;
            bullets.Add(this);
        }

        public static void Update(GameTime gameTime) {
            for (int i = bullets.Count-1; i >=0; i--) {
                Bullet bullet = bullets[i];
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                bullet.Position -= new Vector2(-250 * (float)Math.Sin(bullet.Rotation), 250 * (float)Math.Cos(bullet.Rotation)) * deltaTime * bullet.moveSpeed;

                if (Math.Sqrt(Math.Pow(bullet.Position.X - bullet.shooter.Position.X,2) + Math.Pow(bullet.Position.Y - bullet.shooter.Position.Y, 2)) > 1000) {
                    bullets.RemoveAt(i);
                }
            }
        }

        public static void Draw(GameTime gameTime, SpriteBatch sb) {
            foreach (Bullet bullet in bullets) {
                bullet.Draw(sb);
            }
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(img,
                    Position,
                    new Rectangle(0, 0, img.Width, img.Height),
                    Color.White,
                    Rotation,
                    new Vector2(img.Width / 2, img.Height / 2),
                    1.0f,
                    SpriteEffects.None,
                    1);
        }

    }
}
