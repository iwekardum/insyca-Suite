using inSyca.foundation.communication.service;
using inSyca.foundation.framework;
using inSyca.foundation.framework.application;
using inSyca.foundation.communication.configurator.diagnostics;
using inSyca.foundation.framework.diagnostics;

namespace inSyca.foundation.communication.configurator
{
    public partial class uc_test_service : framework.application.windowsforms.uc_test
    {
        internal uc_test_service(configXml _configFile)
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
