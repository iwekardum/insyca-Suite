using System.Net.Configuration;
using System;
using System.Runtime.CompilerServices;
using inSyca.foundation.framework.diagnostics;
using System.IO;

namespace inSyca.foundation.framework.configuration
{
    [RegistryKeySource(@"SOFTWARE\inSyca\foundation.framework")]
    internal class Configuration : ConfigurationBase<Configuration>
    {
    }

    public class Settings
    {
        public Settings()
        {
        }

        public Settings(Settings _settings)
        {
            LogEventFired += _settings.FireLogEvent;
        }

        public event EventHandler<LogEventFiredArgs> LogEventFired;

        public void FireLogEvent(object sender, LogEventFiredArgs logEventFiredArgs)
        {
            if (LogEventFired != null)
                LogEventFired(sender, logEventFiredArgs);
        }
    }
}