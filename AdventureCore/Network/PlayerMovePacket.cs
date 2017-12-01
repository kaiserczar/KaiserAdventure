using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdventureCore.Network {
    public class PlayerMovePacket {

        public string Username;
        public Vector2 Position;
        public float Rotation;

        private PlayerMovePacket(string user,  Vector2 Position, float Rotation) {
            this.Username = user;
            this.Position = Position;
            this.Rotation = Rotation;
        }

        private PlayerMovePacket(string user, float x, float y, float Rotation) {
            this.Username = user;
            this.Position = new Vector2(x, y);
            this.Rotation = Rotation;
        }

        public void Send(NetServer peer) {
            NetOutgoingMessage retVal = peer.CreateMessage();
            retVal.Write((byte)PacketType.PlayerMovePacket);
            retVal.Write(Username);
            retVal.Write(Position.X);
            retVal.Write(Position.Y);
            retVal.Write(Rotation);
            peer.SendMessage(retVal, peer.Connections, NetDeliveryMethod.Unreliable, 0);
        }

        public void Send(NetClient peer) {
            NetOutgoingMessage retVal = peer.CreateMessage();
            retVal.Write((byte)PacketType.PlayerMovePacket);
            retVal.Write(Username);
            retVal.Write(Position.X);
            retVal.Write(Position.Y);
            retVal.Write(Rotation);
            peer.SendMessage(retVal, NetDeliveryMethod.Unreliable);
        }

        public static PlayerMovePacket FromMessage(NetIncomingMessage msg) {
            PlayerMovePacket retVal = new PlayerMovePacket(msg.ReadString(), msg.ReadFloat(),msg.ReadFloat(),msg.ReadFloat());
            return retVal;
        }

        public static PlayerMovePacket New(string user, float x, float y, float rotation) {
            return new PlayerMovePacket(user, x, y, rotation);
        }

        public static PlayerMovePacket New(string user, Vector2 position, float rotation) {
            return new PlayerMovePacket(user, position, rotation);
        }
    }
}
