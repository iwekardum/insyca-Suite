using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using inSyca.foundation.framework;
using inSyca.foundation.integration.biztalk.diagnostics;

namespace inSyca.foundation.integration.biztalk.management
{
    public abstract class ControlBase
    {
        protected string serverName { get; set; }
        protected string classTypeWMI { get; set; }
        protected string objectNameWMI { get; set; }
       
        protected ManagementObjectSearcher managementObjectSearcher;

        public ControlBase(string ServerName, string ClassTypeWMI, string ObjectNameWMI)
        {
            serverName = ServerName;
            classTypeWMI = ClassTypeWMI;
            objectNameWMI = ObjectNameWMI;

            managementObjectSearcher = GetManagementObjectSearcher();
        }

        protected ManagementObjectSearcher GetManagementObjectSearcher()
        {
            ManagementPath managementPathBizTalk = new ManagementPath(string.Format(@"\\{0}\root\MicrosoftBizTalkServer", serverName));

            ConnectionOptions connectionOptionsBizTalk = new ConnectionOptions();

            connectionOptionsBizTalk.Authentication = AuthenticationLevel.Packet;
            connectionOptionsBizTalk.Timeout = new TimeSpan(0, 5, 0, 0);
            connectionOptionsBizTalk.EnablePrivileges = true;

            ManagementScope managementScopeBizTalk = new ManagementScope(managementPathBizTalk, connectionOptionsBizTalk);

            WqlObjectQuery wqlObjectQuery = new WqlObjectQuery(string.Format("Select * from {0} where Name = '{1}'", classTypeWMI, objectNameWMI));

            Log.DebugFormat("GetManagementObjectSearcher()\nWqlObjectQuery: {0}", wqlObjectQuery.QueryString);

            return new ManagementObjectSearcher(managementScopeBizTalk, wqlObjectQuery);
        }
    }
}
