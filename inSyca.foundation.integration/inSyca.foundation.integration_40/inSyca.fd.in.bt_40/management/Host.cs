using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using inSyca.foundation.framework;
using inSyca.foundation.integration.biztalk.diagnostics;

namespace inSyca.foundation.integration.biztalk.management
{
    /// <summary>
    /// 
    /// </summary>
    public class Host : ControlBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServerName"></param>
        /// <param name="HostName"></param>
        public Host(string ServerName, string HostName)
            : base(ServerName, "MSBTS_Host", HostName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            Log.DebugFormat("Start()\nServername: {0}\nClassTypeWMI: {1}\nObjectNameWMI: {2}", serverName, classTypeWMI, objectNameWMI );

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

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            Log.DebugFormat("Stop()\nServername: {0}\nClassTypeWMI: {1}\nObjectNameWMI: {2}", serverName, classTypeWMI, objectNameWMI );

            try
            {
                foreach (ManagementObject MessageInstance in managementObjectSearcher.Get())
                    MessageInstance.InvokeMethod("Stop", null);
            }
            catch (Exception e)
            {
                biztalk.diagnostics.Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "Error (InvokeMethod - Stop): {0}\nServername: {1}\nClassTypeWMI: {2}\nObjectNameWMI: {3}", new object[] { e.Message, serverName, classTypeWMI, objectNameWMI }));
            }
        }
    }
}
