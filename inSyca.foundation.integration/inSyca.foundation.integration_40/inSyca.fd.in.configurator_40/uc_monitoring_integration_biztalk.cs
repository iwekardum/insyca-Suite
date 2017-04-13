using System.Xml.Linq;
using inSyca.foundation.framework.application;
using System.Reflection;

namespace inSyca.foundation.integration.configurator
{
    public partial class uc_monitoring_integration_biztalk : foundation.framework.application.windowsforms.uc_monitoring
    {
        internal uc_monitoring_integration_biztalk()
        {
            InitializeComponent();
        }

        internal uc_monitoring_integration_biztalk(configXml _configFile)
            : base(_configFile)
        {
            InitializeComponent();
            ServiceName = "Foundation Integration";
            ServiceAssemblyName = Assembly.GetAssembly(typeof(inSyca.foundation.integration.wsh.IntegrationService)).Location;
        }
    }
}
