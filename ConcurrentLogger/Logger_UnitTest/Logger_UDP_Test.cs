using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Logger_UnitTest
{
    public class Logger_UDP_Test
    {
        private UdpClient UDP_Client;
        private string serverIp;
        private int serverPort;

        public Logger_UDP_Test(string serverIP, int serverPort)
        {
            this.serverIp = serverIP;
            this.serverPort = serverPort;
        }
    }
}