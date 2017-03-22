using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaiser_Adventure.Utilities;
using Kaiser_Adventure.Entities.Controllers;

namespace Kaiser_Adventure.Entities {
    class PlayerCharacter : Character {

        public PlayerCharacter() : base() {
            this.controller = new PlayerMouseKeyboardInputController(this);
        }

    }
}
