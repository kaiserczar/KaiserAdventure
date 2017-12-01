using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureCore.Utilities;
using AdventureCore.Entities.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdventureCore.Entities {
    public class PlayerCharacter : Character {

        public static int counter = 0;

        public int shootDelayMS = 50;
        public Texture2D bulletImg;
        private DateTime lastShot;

        public PlayerCharacter(Game main) : base(main) {
            //this.controller = new PlayerMouseKeyboardInputController(this);
            this.lastShot = DateTime.Now.AddYears(-1);
        }

        public new void Shoot() {
            if (DateTime.Now.Subtract(lastShot).Milliseconds > shootDelayMS) {
                // Actually shoot here.
                lastShot = DateTime.Now;
                new Bullet(this, this.Position, this.Rotation, bulletImg);
            }
        }

    }
}
