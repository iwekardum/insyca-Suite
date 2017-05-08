using System;
using System.IO;
using System.Reflection;

namespace inSyca.foundation.framework.diagnostics
{
    public class LogSourceAttribute : Attribute
    {
        internal string configFileName;

        public LogSourceAttribute(Type T)
        {
            PropertyInfo ConfigFileInfo = T.GetProperty("ConfigFileInfo", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            configFileName = ConfigFileInfo.GetValue(null, null) as string;
        }

        internal log4net.ILog InitLogger(Type T, ref bool logInitialized)
        {
            try
            {
                log4net.Repository.ILoggerRepository repo = log4net.LogManager.CreateRepository(T.Module.Name);
                log4net.Config.XmlConfigurator.ConfigureAndWatch(repo, new FileInfo(configFileName));
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("inSyca.foundation.framework", string.Format("InitLogger(Type T, ref bool logInitialized)\nError: {0}", ex.Message), System.Diagnostics.EventLogEntryType.Warning);
            }

            logInitialized = true;

            return log4net.LogManager.GetLogger(T.Module.Name, T);
        }
    }
}
