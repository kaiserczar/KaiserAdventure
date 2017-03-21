using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Kaiser_Adventure.Entities.Controllers {

    /**
     * AI Movement Controller that follows a target at half the speed, constantly rotating to find it. Stops moving if within a certain distance.
     */
    class AIZombieController : Controller {

        public Character target;

        public AIZombieController(Character parent, Character target) : base(parent){
            this.target = target;
            this.parent.moveSpeed = this.parent.moveSpeed * 0.5f;
            this.parent.turningSpeed = this.parent.turningSpeed * 0.5f;
        }

        public override void DoControlUpdate(GameTime gameTime) {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // New Rotation - follow target
            Vector2 targetLoc = target.Position;
            Vector2 currentLoc = parent.Position;
            float angle = (float) Math.Atan2(targetLoc.Y - currentLoc.Y, targetLoc.X - currentLoc.X);
            this.parent.Rotation = angle;

        }
    }

}
