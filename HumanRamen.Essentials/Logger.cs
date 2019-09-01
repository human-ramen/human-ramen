using System;

namespace HumanRamen.Essentials
{
    public interface ILogger
    {
        void Debug(string log);
        void Info(string log);
        void Warn(string log);
    }

    public class Logger : ILogger
    {
        private string _from;

        public Logger(string from)
        {
            _from = from;
        }

        public void Debug(string log)
        {
            Console.WriteLine("DEBUG: {0} - {1}", _from, log);
        }

        public void Info(string log)
        {
            Console.WriteLine("INFO: {0} - {1}", _from, log);
        }

        public void Warn(string log)
        {
            Console.WriteLine("WARN: {0} - {1}", _from, log);
        }
    }
}
