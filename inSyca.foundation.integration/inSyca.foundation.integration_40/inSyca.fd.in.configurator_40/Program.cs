using inSyca.foundation.integration.configurator.diagnostics;
using inSyca.foundation.framework;
using inSyca.foundation.framework.application;
using System;
using System.Windows.Forms;

namespace inSyca.foundation.integration.configurator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string logString = "Main(string[] args)\nArguments:\n";

            if (args.Length < 1)
                logString += "none";
            else
                foreach (var argument in args)
                    logString += string.Format("{0}\n", argument);

            Log.InfoFormat("inSyca.foundation.integration.configurator started\n{0}", logString);

            if (!SingleInstance.Start()) { return; } // mutex not obtained so exit

            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                if (framework.application.Program.Uninstall())
                    return;

                if (framework.application.Program.UnattendedMode())
                {
                    Application.Run(new integration_configurator());

                    return;
                }

                Application.Run(new integration_configurator());
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Program Terminated Unexpectedly", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), args, ex));
            }

            SingleInstance.Stop(); // all finished so release the mutex

            Log.InfoFormat("inSyca.foundation.integration.configurator stopped\n{0}", logString);
        }
    }
}
