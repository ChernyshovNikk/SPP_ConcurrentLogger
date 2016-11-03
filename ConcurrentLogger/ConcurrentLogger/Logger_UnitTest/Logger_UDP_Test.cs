using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ConcurrentLogger;

namespace Logger_UnitTest
{
    public class Logger_UDP_Test
    {
        public static int udp_count = 0;
        private UdpClient UDP_Client;
        private string serverIp;
        private int serverPort;
        private Task receiver;
        string[] receiveMessages = new string[10000];
        int k = 0;
        bool isReceive = false;

        public Logger_UDP_Test(string serverIP, int serverPort, int messagesCount)
        {
            this.serverIp = serverIP;
            this.serverPort = serverPort;
            for (int i = 0; i < messagesCount; i++)
                receiveMessages[i] = LogLevel.Info + " task" + (i + 1) + "\r\n";
        }

        public void ReceiveMessages()
        {
            IPEndPoint server = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            UDP_Client = new UdpClient(server);
            isReceive = true;
            receiver = Task.Run(() => ReceiveAndControl());
        }

        public void ReceiveAndControl()
        {
            while (isReceive)
            {
                IPEndPoint client = null;
                var receiveBytes = UDP_Client.Receive(ref client);
                string receiveMessage = Encoding.Default.GetString(receiveBytes);

                int index = receiveMessage.IndexOf("]") + 2;
                string analyzeString = receiveMessage.Substring(index, receiveMessage.Length - index);
                if (receiveMessages[k] == analyzeString)
                    udp_count++;
                k++;
            }
        }

        public void Close()
        {
            UDP_Client.Close();
        }
    }
}