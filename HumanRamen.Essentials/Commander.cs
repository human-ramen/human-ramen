using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanRamen.Essentials
{
    public interface ICommandHandler
    {
        void HandleCommand(string topic, string command);
    }

    public interface ICommander
    {
        void RegisterHandler(string topic, ICommandHandler handler);
        void Command(string topic, string command);
    }

    public class Commander : ICommander
    {
        private readonly Dictionary<string, List<ICommandHandler>> _topicHandlers = new Dictionary<string, List<ICommandHandler>>();

        public void RegisterHandler(string topic, ICommandHandler handler)
        {
            if (!_topicHandlers.ContainsKey(topic))
            {
                _topicHandlers.Add(topic, new List<ICommandHandler>());
            }

            _topicHandlers[topic].Add(handler);
        }

        public void Command(string topic, string command)
        {
            if (!_topicHandlers.ContainsKey(topic))
            {
                throw new ExceptionNoSuchTopic(topic);
            }

            Parallel.ForEach(_topicHandlers[topic],
                             handler => handler.HandleCommand(topic, command));
        }
    }

    public class ExceptionNoSuchTopic : Exception
    {
        public ExceptionNoSuchTopic(string topic) : base(String.Format("No such topic {0}", topic))
        {
        }
    }
}
