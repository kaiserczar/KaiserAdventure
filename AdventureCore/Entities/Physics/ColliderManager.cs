using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureCore.Utilities;

namespace AdventureCore.Entities.Physics {

    public class ColliderManager {

        public GameState currentState;
        public List<Collider> managedColliders;

        public ColliderManager(GameState state) {

        }

    }
}
