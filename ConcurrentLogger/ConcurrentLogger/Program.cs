using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentLogger
{
    public class Program
    {
        static void Main(string[] args)
        {
            int bufferLimit = 4;
            ILoggerTarget[] loggerTarget = new ILoggerTarget[] {new LoggerTarget("LogerFile.txt")};
            Logger logger = new Logger(bufferLimit, loggerTarget);
            for (int i = 0; i < 20; i++)
                logger.Log(new LoggerInformation(LogLevel.Info, "Task " + i));
        }
    }
}