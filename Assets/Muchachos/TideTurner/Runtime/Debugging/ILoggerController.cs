using System;
using System.Collections.Generic;

namespace Muchachos.TideTurner.Runtime.Debugging
{
    public interface ILoggerController
    {
        event Action OnLog;
        void Log(object message);
        List<string> GetMessages();
    }
}