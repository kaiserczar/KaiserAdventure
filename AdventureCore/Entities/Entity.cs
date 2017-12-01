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

    public abstract class Entity {

        public abstract void Update(GameTime gameTime, Camera2D camera);

        public abstract void Draw(GameTime gameTime, SpriteBatch sb);

    }

}
