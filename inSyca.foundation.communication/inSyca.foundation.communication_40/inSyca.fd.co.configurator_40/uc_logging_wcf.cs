using inSyca.foundation.framework.application;
using inSyca.foundation.framework.application.windowsforms;

namespace inSyca.foundation.communication.configurator
{
    public partial class uc_logging_wcf : uc_logging
    {
        internal uc_logging_wcf(configXml _configFile)
            : base(_configFile)
        {
            InitializeComponent();
        }
    }
}
