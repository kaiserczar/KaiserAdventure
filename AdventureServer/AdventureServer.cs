using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lidgren.Network;
using AdventureCore.Network;
using AdventureCore;

namespace AdventureServer {
    static class AdventureServer {

        public static NetServer Server;
        public static Dictionary<long, Player> clients;
        public static bool IsExiting = false;

        public static float LoopsPerSecond = 60;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main() {

            Write("SERVER", "Starting up.");

            NetPeerConfiguration config = new NetPeerConfiguration("Adventure");
            config.Port = 14242;
            config.PingInterval = 2;
            config.ConnectionTimeout = 4;
            //config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            Server = new NetServer(config);
            Server.Start();

            clients = new Dictionary<long, Player>();

            // ======================= LOOP ==========================
            while (!IsExiting) {
                DateTime start = DateTime.Now;

                #region Network Recieving
                try {
                    NetIncomingMessage msg = null;
                    while ((msg = Server.ReadMessage()) != null) {
                        MessageHandle(msg);
                    }
                } catch (Exception e){
                    Write("ERROR", e.ToString());
                }
                #endregion

                // DO LOGIC HERE

                foreach (Player p in clients.Values){
                    if (p.NeedsSendingMovement) {
                        PlayerMovePacket.New(p.Username, p.Position, p.Rotation).Send(Server);
                        p.NeedsSendingMovement = false;
                    }
                } 

                /* FOR REFERENCE
                // Broadcast the message:
                NetOutgoingMessage om = server.CreateMessage();
                om.Write(senderName);
                om.Write(text);
                server.SendMessage(om, server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
                */

                // Reenable if we want set logic loop lengths.
                double loopDuration = DateTime.Now.Subtract(start).TotalMilliseconds;
                if (loopDuration < 1000f/LoopsPerSecond) {
                    System.Threading.Thread.Sleep((int)(1000f / LoopsPerSecond - loopDuration));
                }
                
            }
        }

        public static void MessageHandle(NetIncomingMessage msg) {
            switch (msg.MessageType) {
                case NetIncomingMessageType.VerboseDebugMessage:
                case NetIncomingMessageType.DebugMessage:
                case NetIncomingMessageType.WarningMessage:
                case NetIncomingMessageType.ConnectionApproval:
                    /*
                    string username = msg.ReadString();
                    msg.SenderConnection.Approve();
                    Write("SERVER", "User connecting: " + username);
                    Write("SERVER", "    Endpoint: " + msg.SenderEndPoint);
                    Write("SERVER", "    UID: " + msg.SenderConnection.RemoteUniqueIdentifier);
                    clients.Add(msg.SenderConnection.RemoteUniqueIdentifier, username);
                    */
                    break;
                case NetIncomingMessageType.StatusChanged:
                    NetConnectionStatus msgVal = (NetConnectionStatus)msg.ReadByte();
                    switch (msgVal) {
                        case NetConnectionStatus.Disconnected:
                            Write("SERVER", "Disconnected: " + clients[msg.SenderConnection.RemoteUniqueIdentifier].Username);
                            clients.Remove(msg.SenderConnection.RemoteUniqueIdentifier);
                            if (clients.Count > 0) {
                                PlayerListPacket.New(clients.Values.ToList()).Send(Server);
                            }
                            break;
                        case NetConnectionStatus.Connected:
                            Write("SERVER", "Connection: " + msg.SenderConnection.RemoteUniqueIdentifier.ToString());
                            Write("SERVER", "Awaiting connection packet.");
                            break;
                        default:
                            break;
                    }
                    break;
                case NetIncomingMessageType.ErrorMessage:
                    Write("SERVER", msg.ReadString());
                    break;
                case NetIncomingMessageType.Data:
                    ParseDataMessage(msg);
                    break;
                default:
                    Write("SERVER", "Unhandled type: " + msg.MessageType);
                    break;
            }
        }

        public static void ParseDataMessage(NetIncomingMessage msg) {
            PacketType packetType = (PacketType)msg.ReadByte();

            switch (packetType) {
                case PacketType.PlayerConnectPacket:
                    PlayerConnectPacket connectPacket = PlayerConnectPacket.FromMessage(msg);
                    clients.Add(msg.SenderConnection.RemoteUniqueIdentifier, new Player(connectPacket.PlayerName));
                    Write("SERVER", "PlayerConnectPacket from " + connectPacket.PlayerName + " (" + msg.SenderConnection.RemoteUniqueIdentifier.ToString() + ")");
                    PlayerListPacket.New(clients.Values.ToList()).Send(Server); // tell all clients when someone joins.
                    break;
                case PacketType.PlayerChatPacket:
                    Player p = clients[msg.SenderConnection.RemoteUniqueIdentifier];
                    PlayerChatPacket chatPacket = PlayerChatPacket.FromMessage(msg);
                    Write(p.Username, chatPacket.Message);
                    break;
                case PacketType.PlayerMovePacket:
                    p = clients[msg.SenderConnection.RemoteUniqueIdentifier];
                    PlayerMovePacket movePacket = PlayerMovePacket.FromMessage(msg);
                    Write(p.Username, "MOVEPACKET x=" + movePacket.Position.X + " y=" + movePacket.Position.Y + " r=" + movePacket.Rotation);
                    p.Rotation = movePacket.Rotation;
                    p.Position = movePacket.Position;
                    p.NeedsSendingMovement = true;
                    break;
                default:
                    Write("SERVER", "Unknown packet type.");
                    break;
            }
        }

        public static void Write(string sender, string line) {
            if (sender.Length == 0) sender = "Default";

            string timestamp = DateTime.Now.TimeOfDay.ToString();
            timestamp = timestamp.Substring(0, timestamp.IndexOf('.'));

            Console.WriteLine(timestamp + " " + sender + ": " + line);
        }
    }
}
