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
            
            Console.WriteLine("Введите максимальное количество объектов в буфере: ");
            int bufferLimit = Convert.ToInt32(Console.ReadLine());

            Logger logger = new Logger(bufferLimit, loggerTargets);
            
            for (int i = 0; i < 100000; i++)
                logger.Log(new LoggerInformation(LogLevel.Info, "task" + (i+1) + " start"));

            logger.LoggerFlushControl();
            loggerFile.CloseFile();
            Console.WriteLine("-----------------");
            Console.WriteLine("Работа завершена!");
        }
    }
}