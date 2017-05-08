using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using inSyca.foundation.framework;
using inSyca.foundation.integration.biztalk.diagnostics;

namespace inSyca.foundation.integration.biztalk.management
{
    public class HostInstance : ControlBase
    {
        /// <summary>
        /// Attention! Servername has to be computername without domain
        /// </summary>
        /// <param name="ServerName"></param>
        /// <param name="HostInstanceName"></param>
        public HostInstance(string ServerName, string HostInstanceName)
            : base(ServerName, "MSBTS_HostInstance", string.Format("Microsoft BizTalk Server {0} {1}", HostInstanceName, ServerName))
        {
        }

        public void Start()
        {
            Log.DebugFormat("Start()\nServername: {0}\nClassTypeWMI: {1}\nObjectNameWMI: {2}", serverName, classTypeWMI, objectNameWMI);

            try
            {
                foreach (ManagementObject MessageInstance in managementObjectSearcher.Get())
                    MessageInstance.InvokeMethod("Start", null);
            }
            catch (Exception e)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "Error (InvokeMethod - Stop): {0}\nServername: {1}\nClassTypeWMI: {2}\nObjectNameWMI: {3}", new object[] { e.Message, serverName, classTypeWMI, objectNameWMI }));
            }
        }

        public void Stop()
        {
            Log.DebugFormat("Stop()\nServername: {0}\nClassTypeWMI: {1}\nObjectNameWMI: {2}", serverName, classTypeWMI, objectNameWMI);

            try
            {
                foreach (ManagementObject MessageInstance in managementObjectSearcher.Get())
                    MessageInstance.InvokeMethod("Stop", null);
            }
            catch (Exception e)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "Error (InvokeMethod - Stop): {0}\nServername: {1}\nClassTypeWMI: {2}\nObjectNameWMI: {3}", new object[] { e.Message, serverName, classTypeWMI, objectNameWMI }));
            }
        }
    }
}
