using inSyca.foundation.communication.clients;
using inSyca.foundation.framework.application;
using inSyca.foundation.communication.configurator.diagnostics;
using inSyca.foundation.framework.diagnostics;
using inSyca.foundation.framework;

namespace inSyca.foundation.communication.configurator
{
    public partial class uc_test_clients : framework.application.windowsforms.uc_test
    {
        internal uc_test_clients(configXml _configFile)
            : base(_configFile)
        {
            InitializeComponent();
        }

        protected override void Test_Information()
        {
            Log.Info("Test_Information()");
        }

        protected override void Test_Warning()
        {
            Log.Warn("Test_Warning()");
        }

        protected override void Test_Error()
        {
            Log.Error("Test_Error()");
        }

    }
}
