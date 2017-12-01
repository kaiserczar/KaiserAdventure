using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace AdventureCore.Network {

    public class PlayerListPacket {

        public List<Player> Players;

        private PlayerListPacket(List<Player> players) {
            this.Players = players;
        }

        public void Send(NetServer peer) {
            NetOutgoingMessage retVal = peer.CreateMessage();
            retVal.Write((byte)PacketType.PlayerListPacket);
            retVal.Write(Players.Count);
            foreach (Player p in Players){
                retVal.Write(p.Username);
                retVal.Write(p.Position.X);
                retVal.Write(p.Position.Y);
                retVal.Write(p.Rotation);
            }
            peer.SendMessage(retVal, peer.Connections, NetDeliveryMethod.ReliableUnordered, 0);
        }

        public static PlayerListPacket FromMessage(NetIncomingMessage msg) {
            List<Player> players = new List<Player>();
            int numP = msg.ReadInt32();
            for (int i=0; i<numP; i++) {
                players.Add(new Player(msg.ReadString(),msg.ReadFloat(),msg.ReadFloat(),msg.ReadFloat()));
            }
            PlayerListPacket retVal = new PlayerListPacket(players);
            return retVal;
        }
        public static PlayerListPacket New(List<Player> players) {
            return new PlayerListPacket(players);
        }

    }
}
