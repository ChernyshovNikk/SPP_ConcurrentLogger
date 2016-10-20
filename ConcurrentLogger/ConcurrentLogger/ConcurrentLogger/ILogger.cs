using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentLogger
{
    public interface ILogger
    {
        void Log(LoggerInformation loggerInformation);
    }

    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error
    }
}