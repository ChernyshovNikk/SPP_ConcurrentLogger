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
        private ILoggerTarget[] loggerTargets;
        private int buffersCount = 0;
        private List<LoggerInformation> loggerInfoList;
        private object threadLocker = new object();
        private int count = 0;

        public Logger(int bufferLimit, ILoggerTarget[] targets)
        {
            this.bufferLimit = bufferLimit;
            this.loggerTargets = targets;
            loggerInfoList = new List<LoggerInformation>();
        }

        public void Log(LoggerInformation loggerInformation)
        {
            if (loggerInfoList.Count == bufferLimit)
            {
                ThreadPool.QueueUserWorkItem(LoggersFlush, new ThreadInformation
                {
                    threadInfoList = loggerInfoList,
                    threadNumber = buffersCount++
                });
                loggerInfoList = new List<LoggerInformation>();
            }
            loggerInfoList.Add(loggerInformation);
        }

        public void LoggersFlush(object ThreadInform)
        {
            var threadInform = (ThreadInformation)ThreadInform;
            var loggerList = threadInform.threadInfoList;
            lock (threadLocker)
            {
                while (threadInform.threadNumber != count)
                    Monitor.Wait(threadLocker);
                foreach (ILoggerTarget currentTarget in loggerTargets)
                    foreach (LoggerInformation currentLogger in loggerList)
                        currentTarget.Flush(currentLogger);
                count++;
                Monitor.PulseAll(threadLocker);
            }
        }

        public void LoggerFlushControl()
        {
            ThreadPool.QueueUserWorkItem(LoggersFlush, new ThreadInformation
            {
                threadInfoList = loggerInfoList,
                threadNumber = buffersCount++
            });
            lock (threadLocker)
            {
                while (buffersCount != count)
                    Monitor.Wait(threadLocker);
            }
        }
    }
}