using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentLogger
{
    public class Logger : ILogger
    {
        public Logger(int bufferLimit, ILoggerTarget[] targets)
        {
            //...
        }
        //...
    }
}
