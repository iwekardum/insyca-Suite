using inSyca.foundation.framework.application;

namespace inSyca.foundation.framework.configurator
{
    public partial class uc_logging : framework.application.windowsforms.uc_logging
    {
        internal uc_logging()
        { 
        }

        internal uc_logging(configXml _configFile)
            : base(_configFile)
        {
            InitializeComponent();
        }
    }
}
