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
        public bool doRotation;

        public Vector2 cameraDiffPosition;

        public PositionMatcher(Character source, Character target, bool doRotation) {
            this.targets = new List<Character>();
            targets.Add(target);
            this.source = source;
            this.doRotation = doRotation;
        }

        public PositionMatcher(Character source, List<Character> targets, bool doRotation) {
            this.source = source;
            this.targets = targets;
            this.doRotation = doRotation;
        }

        public PositionMatcher(Character source, Camera2D camera, bool doRotation) {
            this.source = source;
            this.camera = camera;
            this.cameraDiffPosition = camera.Position - source.Position;
            this.doRotation = doRotation;
        }

        public void Syncronize() {
            if (camera == null) {
                foreach (Character c in this.targets) {
                    c.Position = source.Position;
                    if (doRotation) {
                        c.Rotation = source.Rotation;
                    }
                }
            } else {
                camera.Position = source.Position + cameraDiffPosition;
                if (doRotation) {
                    camera.Rotation = -source.Rotation;
                }
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
