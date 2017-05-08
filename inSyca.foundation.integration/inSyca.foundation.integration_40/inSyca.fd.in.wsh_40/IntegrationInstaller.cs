using inSyca.foundation.framework;
using inSyca.foundation.integration.wsh.diagnostics;
using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace inSyca.foundation.integration.wsh
{
    [RunInstaller(true)]
	public partial class IntegrationInstaller: Installer
	{
		public IntegrationInstaller()
		{
			InitializeComponent();
		}

        private void MessageBrokerServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            new ServiceController(integrationServiceInstaller.ServiceName).Start();
        }

        private void MessageBrokerServiceInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {
            try
            {
                ServiceController controller = ServiceController.GetServices().Where
                (s => s.ServiceName == integrationServiceInstaller.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();

                        Log.Warn("inSyca.foundation.integration.wsh stopped");
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
                (s => s.ServiceName == integrationServiceInstaller.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();

                        Log.Warn("inSyca.foundation.integration.wsh stopped");
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
