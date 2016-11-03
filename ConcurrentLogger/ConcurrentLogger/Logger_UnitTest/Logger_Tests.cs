using System;
using System.Text;
using System.IO;
using ConcurrentLogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logger_UnitTest
{
    [TestClass]
    public class Logger_Tests
    {
        [TestMethod]
        public void LoggerFile_Test()
        {
            int bufferLimit = 3, loggersCount = 1000;
            int count = 0, i = 0;
            string[] allMessages = new string[1000]; 

            LoggerTarget loggerFile = new LoggerTarget("E:\\Test_Logger_File.txt");
            ILoggerTarget[] loggerTargets = new ILoggerTarget[] { loggerFile };
            Logger logger = new Logger(bufferLimit, loggerTargets);

            for (i = 0; i < loggersCount; i++)
                logger.Log(new LoggerInformation(LogLevel.Info, "task" + (i + 1)));

            for (i = 0; i < loggersCount; i++)
                allMessages[i] = LogLevel.Info + " task" + (i + 1);

            logger.LoggerFlushControl();
            loggerFile.CloseFile();

            string[] fileText = File.ReadAllLines("E:\\Test_Logger_File.txt");
            i = 0;
            foreach (string line in fileText)
            {
                if (line.EndsWith(allMessages[i]))
                    count++;
                i++;
            }
            Assert.AreEqual(count, 1000);
        }


        [TestMethod]
        public void LoggerUDP_Test()
        {
            int bufferLimit = 7, loggersCount = 10000;
            string clientIP = "127.0.0.1";
            int clientPort = 8000;
            string serverIP = "127.0.0.1";
            int serverPort = 8888;

            Logger_UDP_Test server = new Logger_UDP_Test(serverIP, serverPort, loggersCount);
            server.ReceiveMessages();
            LoggerTarget_UDP loggerUDP = new LoggerTarget_UDP(clientIP, clientPort, serverIP, serverPort);
            ILoggerTarget[] loggerTargets = new ILoggerTarget[] { loggerUDP };
            Logger logger = new Logger(bufferLimit, loggerTargets);
            
            for (int i = 0; i < loggersCount; i++)
                logger.Log(new LoggerInformation(LogLevel.Info, "task" + (i + 1)));

            logger.LoggerFlushControl();
            server.Close();
            Assert.AreEqual(Logger_UDP_Test.udp_count, 10000);
        }
    }
}