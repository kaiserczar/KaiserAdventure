using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using AdventureCore.Network;

namespace AdventureCore.Network {
    public class PlayerConnectPacket{

        public string PlayerName;

        private PlayerConnectPacket(string PlayerName) {
            this.PlayerName = PlayerName;
        }

        public void Send(NetServer peer) {
            NetOutgoingMessage retVal = peer.CreateMessage();
            retVal.Write((byte)PacketType.PlayerConnectPacket);
            retVal.Write(PlayerName);
            peer.SendMessage(retVal,peer.Connections,NetDeliveryMethod.ReliableOrdered,0);
        }

        public void Send(NetClient peer) {
            NetOutgoingMessage retVal = peer.CreateMessage();
            retVal.Write((byte)PacketType.PlayerConnectPacket);
            retVal.Write(PlayerName);
            peer.SendMessage(retVal, NetDeliveryMethod.ReliableOrdered);
        }

        public static PlayerConnectPacket FromMessage(NetIncomingMessage msg) {
            PlayerConnectPacket retVal = new PlayerConnectPacket(msg.ReadString());
            return retVal;
        }

        public static PlayerConnectPacket New(string playerName) {
            return new PlayerConnectPacket(playerName);
        }

    }
}
