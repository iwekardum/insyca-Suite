using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace inSyca.foundation.framework.configuration
{
    public static class RegistrySettings
    {
        public static void SetRegistryLogLevel(string eventLogLevel, string mailLogLevel, string subKey)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(subKey, true);

            registryKey.SetValue("EventLogLevel", eventLogLevel);
            registryKey.SetValue("MailLogLevel", mailLogLevel);
        }
    }
}
