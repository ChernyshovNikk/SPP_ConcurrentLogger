using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerTarget loggerFile = new LoggerTarget("E:\\LoggerFile.txt");
            ILoggerTarget[] loggerTargets = new ILoggerTarget[] { loggerFile };
            int bufferLimit = 4;

            Logger logger = new Logger(bufferLimit, loggerTargets);
            
            for (int i = 0; i < 40; i++)
                logger.Log(new LoggerInformation(LogLevel.Info, "task" + (i+1) + " start"));

            logger.LoggerFlushControl();
            loggerFile.CloseFile();
        }
    }
}