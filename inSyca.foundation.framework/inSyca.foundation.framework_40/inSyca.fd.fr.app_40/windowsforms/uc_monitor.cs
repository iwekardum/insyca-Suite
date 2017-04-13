using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using inSyca.foundation.framework.diagnostics;
using System.Diagnostics;

namespace inSyca.foundation.framework.application.windowsforms
{
    public partial class uc_monitor : UserControl
    {
        /// <summary>
        /// The last input string (used so that we can make sure we don't echo input twice).
        /// </summary>
        private string lastInput = string.Empty;

        /// <summary>
        /// Current position that input starts at.
        /// </summary>
        int inputStart = -1;

        public uc_monitor()
        {
            InitializeComponent();
        }

        public event EventHandler<UserControlEventFiredArgs> UserControlEventFired;

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (UserControlEventFired != null)
                UserControlEventFired(rtb_monitoring, new UserControlEventFiredArgs(UserControlEventFiredArgs.function.Start));
        }

        public void WriteOutput(string output, Color color)
        {
            if (string.IsNullOrEmpty(lastInput) == false &&
                (output == lastInput || output.Replace("\r\n", "") == lastInput))
                return;

            Invoke((Action)(() =>
            {
                if (rtb_monitoring.Text.Length > 1)
                    rtb_monitoring.Select(rtb_monitoring.Text.Length - 1, 0);
                //  Write the output.
                rtb_monitoring.SelectionColor = color;
                rtb_monitoring.SelectedText += output;
                inputStart = rtb_monitoring.SelectionStart;
            }));
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            if (UserControlEventFired != null)
                UserControlEventFired(rtb_monitoring, new UserControlEventFiredArgs(UserControlEventFiredArgs.function.Stop));
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            rtb_monitoring.Text = string.Empty;
        }

        private void btn_tasklist_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = new System.Diagnostics.ProcessStartInfo("tasklist", "/svc");
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;

            using (var process = Process.Start(p.StartInfo))
            {
                var standardOutput = new StringBuilder();

                // read chunk-wise while process is running.
                while (!process.HasExited)
                {
                    standardOutput.Append(process.StandardOutput.ReadToEnd());
                }

                // make sure not to miss out on any remaindings.
                standardOutput.Append(process.StandardOutput.ReadToEnd());

                WriteOutput(standardOutput.ToString(), Color.White);
            }
        }
    }
}
