using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentLogger
{
    public class Logger : ILogger
    {
        private int bufferLimit;
        private ILoggerTarget[] loggerTarget;
        private int bufferNumber = 0;
        private List<LoggerInformation> loggerInfoList;

        public Logger(int bufferLimit, ILoggerTarget[] targets)
        {
            this.bufferLimit = bufferLimit;
            this.loggerTarget = targets;
        }

        public void Log(LoggerInformation loggerInformation)
        {
            if (loggerInfoList.Count == bufferLimit)
            {
                ThreadPool.QueueUserWorkItem(LoggersFlush, new ThreadInformation 
                {
                    threadInfoList = loggerInfoList, 
                    threadNumber = bufferNumber++
                });
            }
        }

        public void LoggersFlush(object ThreadInform)
        {
            //
        }
    }
}