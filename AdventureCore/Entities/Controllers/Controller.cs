using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AdventureCore.Utilities;

namespace AdventureCore.Entities {

    public abstract class Controller {

        protected Character parent;

        public Controller(Character parent) {
            this.parent = parent;
        }

        public abstract void DoControlUpdate(GameTime gameTime, Camera2D camera);

    }
}
