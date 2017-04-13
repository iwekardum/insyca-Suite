using inSyca.foundation.framework.configurator.diagnostics;
using inSyca.foundation.framework;
using inSyca.foundation.framework.application;
using System;
using System.Windows.Forms;

namespace inSyca.foundation.framework.configurator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Log.InfoFormat("Main(string[] args {0}\nApplication started)", args);

            if (!SingleInstance.Start()) { return; } // mutex not obtained so exit

            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                if (application.Program.Uninstall())
                    return;

                if (application.Program.UnattendedMode())
                {
                    Application.Run(new framework_configurator());

                    return;
                }

                Application.Run(new framework_configurator());
            }

            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), args, ex));
                MessageBox.Show(ex.Message, "Program Terminated Unexpectedly", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SingleInstance.Stop(); // all finished so release the mutex

            Log.InfoFormat("Main(string[] args {0}\nApplication closed)", args);
        }
    }
}
