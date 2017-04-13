using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using inSyca.foundation.framework.diagnostics;

namespace inSyca.foundation.framework.application.windowsforms
{
    public partial class uc_test : UserControl
    {
        /// <summary>
        /// The last input string (used so that we can make sure we don't echo input twice).
        /// </summary>
        private string lastInput = string.Empty;

        /// <summary>
        /// Current position that input starts at.
        /// </summary>
        int inputStart = -1;

        protected configXml configFile { get; set; }
        protected XDocument xDocument { get; set; }
        protected framework.configuration.Settings TestSettings { get; set; }

        protected uc_test()
        {
            InitializeComponent();
        }

        protected uc_test(configXml _configFile)
        {
            if (_configFile != null)
            {
                configFile = _configFile;
                xDocument = _configFile.configDocument;
            }

            InitializeComponent();

            TestSettings = Configuration.Settings;

            NavigationChanged();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (TestSettings != null)
            TestSettings.LogEventFired += new System.EventHandler<framework.diagnostics.LogEventFiredArgs>(TestSettings_LogEventFired);
            base.OnLoad(e);
        }

        protected override void OnLeave(System.EventArgs e)
        {
            TestSettings.LogEventFired -= new System.EventHandler<framework.diagnostics.LogEventFiredArgs>(TestSettings_LogEventFired);
            base.OnLeave(e);
        }


        virtual protected void TestSettings_LogEventFired(object sender, diagnostics.LogEventFiredArgs e)
        {
            Color color;

            switch ((System.Diagnostics.EventLogEntryType)sender)
            {
                case System.Diagnostics.EventLogEntryType.Error:
                    color = Color.Red;
                    break;

                case System.Diagnostics.EventLogEntryType.Warning:
                    color = Color.Yellow;
                    break;

                default:
                    color = Color.White;
                    break;
            }

            WriteOutput(e.logEntry.MessageEntry, color);
        }

        private void tb_components_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            this.Invalidate();
            pnl_component.Controls.Clear();

            NavigationChanged();
        }

        virtual protected void NavigationChanged()
        {
            if (tb_components.SelectedTab.Name == "tsb_diagnostics")
            {
                uc_test_diagnostics testDiagnostics = new uc_test_diagnostics();
                sp_main.SplitterDistance = testDiagnostics.Height + tb_components.Height;
                testDiagnostics.Dock = DockStyle.Fill;
                pnl_component.Controls.Add(testDiagnostics);
            }
        }

        protected void WriteOutput(string output, Color color)
        {
            if (string.IsNullOrEmpty(lastInput) == false &&
                (output == lastInput || output.Replace("\r\n", "") == lastInput))
                return;

            Invoke((Action)(() =>
            {
                //  Write the output.
                rtb_console.SelectionColor = color;
                rtb_console.SelectedText += output + "\r\n------------------------------------------\r\n";
                inputStart = rtb_console.SelectionStart;
            }));
        }

        virtual protected void btnTest_Click(object sender, EventArgs e)
        {
            if (tb_components.SelectedTab.Name == "tsb_diagnostics")
            {
                uc_test_diagnostics testDiagnostics = pnl_component.Controls[0] as uc_test_diagnostics;

                switch (testDiagnostics.Level())
                {
                    case uc_test_diagnostics.DiagnosticLevel.Test_Eventlog_Error:
                        Test_Error();
                        break;
                    case uc_test_diagnostics.DiagnosticLevel.Test_Eventlog_Warning:
                        Test_Warning();
                        break;
                    case uc_test_diagnostics.DiagnosticLevel.Test_Eventlog_Information:
                        Test_Information();
                        break;
                    case uc_test_diagnostics.DiagnosticLevel.Test_Maillog_Error:
                        Test_Maillog_Error();
                        break;
                    case uc_test_diagnostics.DiagnosticLevel.Test_Maillog_Warning:
                        Test_Maillog_Warning();
                        break;
                    case uc_test_diagnostics.DiagnosticLevel.Test_Maillog_Information:
                        Test_Maillog_Information();
                        break;
                }
            }
        }

        protected virtual void Test_Maillog_Information()
        {
        }

        protected virtual void Test_Maillog_Warning()
        {
        }

        protected virtual void Test_Maillog_Error()
        {
        }

        protected virtual void Test_Information()
        {
        }

        protected virtual void Test_Warning()
        {
        }

        protected virtual void Test_Error()
        {
        }
    }
}
