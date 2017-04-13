using inSyca.foundation.framework;
using inSyca.foundation.framework.diagnostics;
using inSyca.foundation.integration.biztalk.components.diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace inSyca.foundation.integration.biztalk.components
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
