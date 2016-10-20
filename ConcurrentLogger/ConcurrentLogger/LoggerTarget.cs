using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConcurrentLogger
{
    public class LoggerTarget : ILoggerTarget
    {
        private FileStream fileStream;

        public LoggerTarget(string fileName)
        {
            fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write);
        }

        public bool Flush(LoggerInformation loggerInformation)
        {
            byte[] writeInfo = Encoding.Default.GetBytes(loggerInformation.GetLoggerStringMessage());
            fileStream.Write(writeInfo, 0, writeInfo.Length);
            fileStream.Flush();
            return true;
        }

        public async Task<bool> FlushAsync(LoggerInformation loggerInformation)
        {
            byte[] writeInfo = Encoding.Default.GetBytes(loggerInformation.GetLoggerStringMessage());
            fileStream.Write(writeInfo, 0, writeInfo.Length);
            await fileStream.FlushAsync();
            return true;
        }
    }
}