using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentLogger
{
    public class Logger : ILogger
    {
        private int bufferLimit;
        private ILoggerTarget[] loggerTarget;

        public Logger(int bufferLimit, ILoggerTarget[] targets)
        {
            this.bufferLimit = bufferLimit;
            this.loggerTarget = targets;
        }

        public void Log(LoggerInformation loggerInformation)
        {
            //
        }
    }
}