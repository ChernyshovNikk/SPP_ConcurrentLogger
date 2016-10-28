using System;
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
            int bufferLimit = 3, loggersCount = 10000;
            LoggerTarget loggerFile = new LoggerTarget("E:\\Test_Logger_File.txt");
            ILoggerTarget[] loggerTargets = new ILoggerTarget[] { loggerFile };
            Logger logger = new Logger(bufferLimit, loggerTargets);

            for (int i = 0; i < loggersCount; i++)
                logger.Log(new LoggerInformation(LogLevel.Info, "task" + (i + 1) + " start"));

            logger.LoggerFlushControl();
            loggerFile.CloseFile();
        }
        

        [TestMethod]
        public void LoggerUDP_Test()
        {
            int bufferLimit = 3, loggersCount = 10000;
            string clientIP = "127.0.0.1"; 
            int clientPort = 8000;
            string serverIP = "127.0.0.1";
            int serverPort = 8888;
            LoggerTarget_UDP loggerUDP = new LoggerTarget_UDP(clientIP, clientPort, serverIP, serverPort);
            ILoggerTarget[] loggerTargets = new ILoggerTarget[] { loggerUDP };
            Logger logger = new Logger(bufferLimit, loggerTargets);

            for (int i = 0; i < loggersCount; i++)
                logger.Log(new LoggerInformation(LogLevel.Info, "task" + (i + 1) + " start"));

            logger.LoggerFlushControl();
            loggerUDP.Close();
        }
    }
}