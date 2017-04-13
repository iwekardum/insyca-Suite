using inSyca.foundation.framework;
using inSyca.foundation.framework.application;
using inSyca.foundation.framework.configurator.diagnostics;
using System.Drawing;
using System.Windows.Forms;
using inSyca.foundation.framework.diagnostics;

namespace inSyca.foundation.framework.configurator
{
    public partial class uc_test : framework.application.windowsforms.uc_test
    {
        internal uc_test()
        {
            InitializeComponent();
        }

        internal uc_test(configXml _configFile)
            : base(_configFile)
        {
            InitializeComponent();
        }

        override protected void NavigationChanged()
        {
            if (tb_components.SelectedTab.Name == "tsb_FileSystem")
            {
                uc_test_filesystem testFileSystem = new uc_test_filesystem();
                sp_main.SplitterDistance = testFileSystem.Height + tb_components.Height;
                testFileSystem.Dock = DockStyle.Fill;
                pnl_component.Controls.Add(testFileSystem);
            }
            else
            {
                base.NavigationChanged();
            }
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

        protected override void btnTest_Click(object sender, System.EventArgs e)
        {
            if (tb_components.SelectedTab.Name == "tsb_FileSystem")
            {
                inSyca.foundation.framework.io.FileSystem fileSystem = new framework.io.FileSystem();
                fileSystem.Process();
            }
            else
                base.btnTest_Click(sender, e);
        }
    }
}
