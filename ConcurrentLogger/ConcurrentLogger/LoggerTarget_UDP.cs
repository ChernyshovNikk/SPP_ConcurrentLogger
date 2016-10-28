using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConcurrentLogger
{
    public class LoggerTarget_UDP : ILoggerTarget
    {
        private UdpClient UDP_Client;
        private string clientIp, serverIp;
        private int clientPort, serverPort;

        public LoggerTarget_UDP(string clientIp, int clientPort, string serverIp, int serverPort)
        {
            this.clientIp = clientIp;
            this.clientPort = clientPort;
            this.serverIp = serverIp;
            this.serverPort = serverPort;
        }

        public bool Flush(LoggerInformation loggerInformation)
        {
            byte[] writeInfo = Encoding.Default.GetBytes(loggerInformation.GetLoggerStringMessage());
            IPEndPoint client = new IPEndPoint(IPAddress.Parse(clientIp), clientPort);
            IPEndPoint server = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

            UDP_Client = new UdpClient(client);
            UDP_Client.Send(writeInfo, writeInfo.Length, server);
            return true;
        }

        public async Task<bool> FlushAsync(LoggerInformation loggerInformation)
        {
            byte[] writeInfo = Encoding.Default.GetBytes(loggerInformation.GetLoggerStringMessage());
            IPEndPoint client = new IPEndPoint(IPAddress.Parse(clientIp), clientPort);
            IPEndPoint server = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            
            UDP_Client = new UdpClient(client);
            UDP_Client.Send(writeInfo, writeInfo.Length, server);
            return true;
        }

        public void Close()
        {
            UDP_Client.Close();
        }
    }
}
