using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace ChatTestServer {
    class Program {
        static void Main(string[] args) {

            NetPeerConfiguration config = new NetPeerConfiguration("MyExampleName");
            config.Port = 14242;
            config.PingInterval = 2;
            config.ConnectionTimeout = 4;
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            NetServer server = new NetServer(config);
            server.Start();

            Dictionary<long,string> clients = new Dictionary<long,string>();

            NetIncomingMessage msg;
            while (true) {
                while ((msg = server.ReadMessage()) != null) {
                    switch (msg.MessageType) {
                        case NetIncomingMessageType.VerboseDebugMessage:
                        case NetIncomingMessageType.DebugMessage:
                        case NetIncomingMessageType.WarningMessage:
                        case NetIncomingMessageType.ConnectionApproval:
                            string username = msg.ReadString();
                            msg.SenderConnection.Approve();
                            Console.WriteLine("User connecting: " + username);
                            Console.WriteLine("    Endpoint: " + msg.SenderEndPoint);
                            Console.WriteLine("    UID: " + msg.SenderConnection.RemoteUniqueIdentifier);
                            clients.Add(msg.SenderConnection.RemoteUniqueIdentifier, username);
                            break;
                        case NetIncomingMessageType.StatusChanged:
                            NetConnectionStatus msgVal = (NetConnectionStatus) msg.ReadByte();
                            switch (msgVal) {
                                case NetConnectionStatus.Disconnected:
                                    Console.WriteLine("Disconnected: " + clients[msg.SenderConnection.RemoteUniqueIdentifier]);
                                    clients.Remove(msg.SenderConnection.RemoteUniqueIdentifier);
                                    break;
                                case NetConnectionStatus.Connected:
                                    Console.WriteLine("Connection: " + clients[msg.SenderConnection.RemoteUniqueIdentifier]);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case NetIncomingMessageType.ErrorMessage:
                            Console.WriteLine(msg.ReadString());
                            break;
                        case NetIncomingMessageType.Data:
                            string senderName = msg.ReadString();
                            string text = msg.ReadString();
                            Console.WriteLine(senderName + ": " + text);
                            Console.WriteLine("    UID: " + msg.SenderConnection.RemoteUniqueIdentifier);
                            // Broadcast the message:
                            NetOutgoingMessage om = server.CreateMessage();
                            om.Write(senderName);
                            om.Write(text);
                            server.SendMessage(om, server.Connections, NetDeliveryMethod.ReliableOrdered,0);
                            break;
                        default:
                            Console.WriteLine("Unhandled type: " + msg.MessageType);
                            break;
                    }
                    server.Recycle(msg);
                }
            }

        }
    }
}
