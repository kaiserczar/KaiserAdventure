using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AdventureCore.Entities.Physics {

    public abstract class Collider {

        public abstract bool IsColliding(Vector2 loc);

    }
}
