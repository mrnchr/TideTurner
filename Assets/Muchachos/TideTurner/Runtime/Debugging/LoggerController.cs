using System;
using System.Collections.Generic;
using System.Linq;

namespace Muchachos.TideTurner.Runtime.Debugging
{
    public class LoggerController : ILoggerController
    {
        private const int MAX_MESSAGES = 100;
        private readonly List<string> _messages = new List<string>();

        public event Action OnLog;
        
        public void Log(object message)
        {
            if(_messages.Count >= MAX_MESSAGES)
                _messages.RemoveAt(0);
            
            _messages.Add(message.ToString());
            
            OnLog?.Invoke();
        }

        public List<string> GetMessages()
        {
            return _messages;
        }
    }
}