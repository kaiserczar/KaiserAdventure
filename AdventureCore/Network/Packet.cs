using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureCore.Network {
    
    public enum PacketType : byte {
        PlayerChatPacket,
        PlayerConnectPacket,
        PlayerMovePacket,
        PlayerListPacket
    }
}
