using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using inSyca.foundation.framework;
using System.Management;
using inSyca.foundation.integration.biztalk.diagnostics;

namespace inSyca.foundation.integration.biztalk.management
{
    public class SendPort : ControlBase
    {
        public SendPort(string ServerName, string SendPortName)
            : base(ServerName, "MSBTS_SendPort", SendPortName)
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

        public void Enlist()
        {
            Log.DebugFormat("Enlist()\nServername: {0}\nClassTypeWMI: {1}\nObjectNameWMI: {2}", serverName, classTypeWMI, objectNameWMI);

            try
            {
                foreach (ManagementObject MessageInstance in managementObjectSearcher.Get())
                    MessageInstance.InvokeMethod("Enlist", null);
            }
            catch (Exception e)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "Error (InvokeMethod - Stop): {0}\nServername: {1}\nClassTypeWMI: {2}\nObjectNameWMI: {3}", new object[] { e.Message, serverName, classTypeWMI, objectNameWMI }));
            }
        }

        public void Unenlist()
        {
            Log.DebugFormat("Unenlist()\nServername: {0}\nClassTypeWMI: {1}\nObjectNameWMI: {2}", serverName, classTypeWMI, objectNameWMI);

            try
            {
                foreach (ManagementObject MessageInstance in managementObjectSearcher.Get())
                    MessageInstance.InvokeMethod("Unenlist", null);
            }
            catch (Exception e)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "Error (InvokeMethod - Stop): {0}\nServername: {1}\nClassTypeWMI: {2}\nObjectNameWMI: {3}", new object[] { e.Message, serverName, classTypeWMI, objectNameWMI }));
            }
        }
    }
}
