using inSyca.foundation.framework.configuration;
using inSyca.foundation.framework.diagnostics;
using System;

namespace inSyca.foundation.framework
{
    public class testing
    {
        public static void logging(EventHandler<LogEventFiredArgs> eventHandler, System.Reflection.MethodBase methodBase, int eventLogLevel, int mailLogLevel, System.Diagnostics.EventLogEntryType eventLogEntryType)
        {
            framework.configuration.Settings settings = Configuration.Settings;

            settings.LogEventFired -= eventHandler;
            settings.LogEventFired += eventHandler;

            switch (eventLogEntryType)
            {
                case System.Diagnostics.EventLogEntryType.Error:
                    Log.Error(new LogEntry(methodBase, null));
                    break;
                case System.Diagnostics.EventLogEntryType.Information:
                    Log.Info(new LogEntry(methodBase, null));
                    break;
                case System.Diagnostics.EventLogEntryType.Warning:
                    Log.Warn(new LogEntry(methodBase, null));
                    break;
                default:
                    break;
            }
        }
    }
}
