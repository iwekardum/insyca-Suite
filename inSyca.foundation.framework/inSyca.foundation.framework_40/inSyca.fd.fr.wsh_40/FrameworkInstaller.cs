using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;


namespace inSyca.foundation.framework.wsh
{
    [RunInstaller(true)]
    public partial class FrameworkInstaller : Installer
    {
        public FrameworkInstaller()
        {
            InitializeComponent();
        }

        private void MessageBrokerServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            try
            {
                new ServiceController(frameworkServiceInstaller.ServiceName).Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Start Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void MessageBrokerServiceInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {
            try
            {
                ServiceController controller = ServiceController.GetServices().Where
                (s => s.ServiceName == frameworkServiceInstaller.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();

                        Log.Info("inSyca.foundation.framework.wsh stopped");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InstallException
                    (ex.Message.ToString());
            }

        }

        private void MessageBrokerServiceInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            try
            {
                ServiceController controller = ServiceController.GetServices().Where
                (s => s.ServiceName == frameworkServiceInstaller.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();

                        Log.Info("inSyca.foundation.framework.wsh stopped");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { sender, e }, ex));

                throw new InstallException
                    (ex.Message.ToString());
            }
        }
    }
}
