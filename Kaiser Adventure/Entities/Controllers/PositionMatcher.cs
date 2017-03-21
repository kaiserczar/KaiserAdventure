using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Kaiser_Adventure.Utilities;

namespace Kaiser_Adventure.Entities.Controllers {
    class PositionMatcher {

        public Character source;
        public List<Character> targets;
        public Camera2D camera;

        public Vector2 cameraDiffPosition;

        public PositionMatcher(Character source, Character target) {
            this.targets = new List<Character>();
            targets.Add(target);
            this.source = source;
        }

        public PositionMatcher(Character source, List<Character> targets) {
            this.source = source;
            this.targets = targets;
        }

        public PositionMatcher(Character source, Camera2D camera) {
            this.source = source;
            this.camera = camera;
            this.cameraDiffPosition = camera.Position - source.Position;
        }

        public void Syncronize() {
            if (camera == null) {
                foreach (Character c in this.targets) {
                    c.Position = source.Position;
                    c.Rotation = source.Rotation;
                }
            } else {
                camera.Position = source.Position + cameraDiffPosition;
                camera.Rotation = -source.Rotation;
            }
        }

        public void Sync() {
            Syncronize();
        }

        public void Update() {
            Syncronize();
        }

        public void Update(GameTime gameTime) {
            Syncronize();
        }

    }
}
