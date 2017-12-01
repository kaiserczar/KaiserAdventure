using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdventureCore
{

    public class Player
    {

        public string Username;
        public Vector2 Position;
        public float Rotation;

        // These only make sense on either client or server, but whatever. Too lazy for two objects when one makes sense.
        public long ConnectionID;
        public bool NeedsSendingMovement;
        // TODO: Move PlayerCharacter to Core y/n?

        public Player(string user) {
            Username = user;
            Position = new Vector2(200, 200);
            Rotation = 0;
        }

        public Player(string user, Vector2 pos, float rot) {
            Username = user;
            Position = pos;
            Rotation = rot;
        }

        public Player(string user, float x, float y, float rot) {
            Username = user;
            Position = new Vector2(x, y);
            Rotation = rot;
        }

        public override bool Equals(object obj) {
            if (obj is Player) {
                return this.Username == ((Player)obj).Username;
            } else {
                return false;
            }
        }

    }
}
