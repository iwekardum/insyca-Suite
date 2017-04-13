
using inSyca.foundation.framework.application;
using inSyca.foundation.framework.application.windowsforms;

namespace inSyca.foundation.communication.configurator
{
    public partial class uc_smtp_components : uc_smtp
    {
        internal uc_smtp_components(configXml _configFile)
            : base(_configFile)
        {
            InitializeComponent();
        }
    }
}
