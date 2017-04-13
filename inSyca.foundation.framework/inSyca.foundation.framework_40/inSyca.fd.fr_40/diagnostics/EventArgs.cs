using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace inSyca.foundation.framework.diagnostics
{
    public class UserControlEventFiredArgs : EventArgs
    {
        public enum function { Start = 1, Stop = 2 };

        public readonly function fx;

        public UserControlEventFiredArgs(function _fx)
        {
            fx = _fx;
        }
    }

    public class MonitoringEventArgs : EventArgs
    {
        public readonly System.Data.DataRow Row;
        public readonly string Message;

        public MonitoringEventArgs(string _Message)
        {
            Message = _Message;
        }

        public MonitoringEventArgs(System.Data.DataRow _Row)
        {
            Row = _Row;
        }
    }

    public class LogEventFiredArgs : EventArgs
    {
        public readonly LogEntry logEntry;

        public LogEventFiredArgs(LogEntry _logEntry)
        {
            logEntry = _logEntry;
        }
    }

}
