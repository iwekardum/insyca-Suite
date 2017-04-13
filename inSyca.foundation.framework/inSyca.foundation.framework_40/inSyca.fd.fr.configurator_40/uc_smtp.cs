using inSyca.foundation.framework.application;

namespace inSyca.foundation.framework.configurator
{
    public partial class uc_smtp : framework.application.windowsforms.uc_smtp
    {
        internal uc_smtp()
        { 
        }

        internal uc_smtp(configXml _configFile)
            : base(_configFile)
        {
            InitializeComponent();
        }
    }
}
