using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentLogger
{
    public class LoggerInformation
    {
        private LogLevel loggerLevel;
        private string loggerMessage;
        private string loggerDateAndTime;

        public LoggerInformation(LogLevel level, string message)
        {
            this.loggerLevel = level;
            this.loggerMessage = message;
            this.loggerDateAndTime = DateTime.Now.ToString();
        }

        public string GetLoggerStringMessage()
        {
            return String.Format("[{0}] {1} {2}\r\n", loggerDateAndTime, loggerLevel, loggerMessage);
        }
    }
}