using inSyca.foundation.framework;
using inSyca.foundation.integration.biztalk.diagnostics;
using System;
using System.Management;

namespace inSyca.foundation.integration.biztalk.management
{
    public class ReceiveLocation : ControlBase
    {
        public ReceiveLocation(string ServerName, string ReceiveLocationName)
            : base(ServerName, "MSBTS_ReceiveLocation", ReceiveLocationName)
        {
        }

        public void Enable()
        {
            Log.DebugFormat("Enable()\nServername: {0}\nClassTypeWMI: {1}\nObjectNameWMI: {2}", serverName, classTypeWMI, objectNameWMI);

            try
            {
                foreach (ManagementObject MessageInstance in managementObjectSearcher.Get())
                    MessageInstance.InvokeMethod("Enable", null);
            }
            catch (Exception e)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "Error (InvokeMethod - Stop): {0}\nServername: {1}\nClassTypeWMI: {2}\nObjectNameWMI: {3}", new object[] { e.Message, serverName, classTypeWMI, objectNameWMI }));
            }
        }

        public void Disable()
        {
            Log.DebugFormat("Disable()\nServername: {0}\nClassTypeWMI: {1}\nObjectNameWMI: {2}", serverName, classTypeWMI, objectNameWMI);

            try
            {
                foreach (ManagementObject MessageInstance in managementObjectSearcher.Get())
                    MessageInstance.InvokeMethod("Disable", null);
            }
            catch (Exception e)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "Error (InvokeMethod - Stop): {0}\nServername: {1}\nClassTypeWMI: {2}\nObjectNameWMI: {3}", new object[] { e.Message, serverName, classTypeWMI, objectNameWMI }));
            }
        }
    }
}
