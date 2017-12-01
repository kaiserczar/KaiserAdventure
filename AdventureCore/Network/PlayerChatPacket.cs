using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace AdventureCore.Network {
    public class PlayerChatPacket {

        public string Message;
        public string Username;

        private PlayerChatPacket(string user, string message) {
            this.Message = message;
            this.Username = user;
        }

        public void Send(NetServer peer) {
            NetOutgoingMessage retVal = peer.CreateMessage();
            retVal.Write((byte)PacketType.PlayerChatPacket);
            retVal.Write(Message);
            peer.SendMessage(retVal, peer.Connections, NetDeliveryMethod.ReliableOrdered, 0);
        }

        public void Send(NetClient peer) {
            NetOutgoingMessage retVal = peer.CreateMessage();
            retVal.Write((byte)PacketType.PlayerChatPacket);
            retVal.Write(Message);
            peer.SendMessage(retVal, NetDeliveryMethod.ReliableOrdered);
        }

        public static PlayerChatPacket FromMessage(NetIncomingMessage msg) {
            return new PlayerChatPacket(msg.ReadString(), msg.ReadString());
        }

        public static PlayerChatPacket New(string user, string message) {
            return new PlayerChatPacket(user, message);
        }

    }
}
