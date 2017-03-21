using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Kaiser_Adventure.Utilities;

namespace Kaiser_Adventure.Entities {

    abstract class Controller {

        protected Character parent;

        public Controller(Character parent) {
            this.parent = parent;
        }

        public abstract void DoControlUpdate(GameTime gameTime);

    }
}
