using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lidgren.Network;
using System.Threading;

namespace ChatTestClient {
    public partial class ClientForm : Form {

        public NetClient client;

        public string LastCommand = "";

        public ClientForm() {
            InitializeComponent();

            textBox1.Enabled = false;
            Write("CLIENT", "Enter a username, ip, and port to connect.");
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        }

        private void OnTextInputKeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == 13) {
                sendChat();
                e.Handled = true;
            }
        }

        private void sendChat() {
            NetOutgoingMessage msg = client.CreateMessage();
            msg.Write(textBox2.Text);
            msg.Write(textBox1.Text);
            client.SendMessage(msg, NetDeliveryMethod.ReliableOrdered);
            LastCommand = textBox1.Text;
            textBox1.Clear();
        }

        private void OnConnectButtonClick(object sender, EventArgs e) {
            string ipPort = textBox3.Text;
            if (textBox2.Text.Count() == 0) {
                Write("CLIENT", "You require a username to connect.");
                return;
            }
            if (ipPort.Contains(':')) {
                string ip = ipPort.Substring(0, ipPort.IndexOf(":"));
                string port = ipPort.Substring(ipPort.IndexOf(":") + 1);
                int portNum;
                if (Int32.TryParse(port, out portNum)) {
                    try {
                        NetPeerConfiguration npConfig = new NetPeerConfiguration("MyExampleName");
                        client = new NetClient(npConfig);
                        client.Start();
                        NetOutgoingMessage usernameApproval = client.CreateMessage();
                        usernameApproval.Write(textBox2.Text);
                        client.Connect(ip, portNum, usernameApproval);
                        client.RegisterReceivedCallback(new SendOrPostCallback(OnIncomingMessage));
                        textBox1.Enabled = true;
                        textBox2.Enabled = false;
                        textBox3.Enabled = false;
                        button1.Enabled = false;
                    } catch {
                        Write("CLIENT", "You messed up IP/port somehow.");
                    }
                } else {
                    Write("CLIENT", "Your port must be an integer!");
                }
            } else {
                Write("CLIENT","Your IP/port string must be separated by a colon!");
            }
        }

        public void OnIncomingMessage(object peer) {
            var msg = ((NetClient)peer).ReadMessage();
            Write(msg.ReadString(), msg.ReadString());
        }

        public void Write(string sender, string message) {
            bool isScrolled = false;
            int numRowsVisible = listBox1.ClientSize.Height / listBox1.ItemHeight;

            if (listBox1.Items.Count < numRowsVisible) { isScrolled = true; }

            if (!isScrolled) {
                if (listBox1.TopIndex >= listBox1.Items.Count - numRowsVisible) {
                    isScrolled = true;
                }
            }

            string timestamp = DateTime.Now.TimeOfDay.ToString();
            timestamp = timestamp.Substring(0, timestamp.IndexOf('.'));

            listBox1.Items.Add(timestamp + " "+sender + ": " + message);

            if (isScrolled) {
                listBox1.TopIndex = listBox1.Items.Count - 1;
            }
        }
    }
}
