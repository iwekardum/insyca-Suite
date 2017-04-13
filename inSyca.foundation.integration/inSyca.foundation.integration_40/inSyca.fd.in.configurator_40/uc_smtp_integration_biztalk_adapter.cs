using System.Xml.Linq;

using inSyca.foundation.framework.application;
using inSyca.foundation.framework.application.windowsforms;

namespace inSyca.foundation.integration.configurator
{
    public partial class uc_smtp_integration_biztalk_adapter : uc_smtp
    {
        internal uc_smtp_integration_biztalk_adapter(configXml _configFile)
            : base(_configFile)
        {
            InitializeComponent();
        }
    }
}
