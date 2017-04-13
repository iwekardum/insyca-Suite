using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using inSyca.foundation.framework.application.windowsforms;
using inSyca.foundation.framework.application;
using inSyca.foundation.framework;
using inSyca.foundation.integration.configurator.diagnostics;
using inSyca.foundation.framework.diagnostics;

namespace inSyca.foundation.integration.configurator
{
    public partial class uc_test_integration_biztalk_components : uc_test
    {
        internal uc_test_integration_biztalk_components()
        {
            InitializeComponent();
        }

        public uc_test_integration_biztalk_components(configXml _configFile)
            : base(_configFile)
        {
            InitializeComponent();

            TestSettings = Configuration.Settings;
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
