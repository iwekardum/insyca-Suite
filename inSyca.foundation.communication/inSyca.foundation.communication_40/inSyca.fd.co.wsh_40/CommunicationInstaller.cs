using inSyca.foundation.framework;
using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;


namespace inSyca.foundation.communication.wsh
{
    [RunInstaller(true)]
    public partial class CommunicationInstaller : System.Configuration.Install.Installer
    {
        public CommunicationInstaller()
        {
            InitializeComponent();
        }

        private void MessageBrokerServiceInstaller_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {
            try
            {
                new ServiceController(CommunicationServiceInstaller.ServiceName).Start();

                Log.Info("inSyca.foundation.communication.wsh started");

            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { sender, e }, ex));
            }
        }

        private void MessageBrokerServiceInstaller_BeforeInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {
            try
            {
                ServiceController controller = ServiceController.GetServices().Where
                (s => s.ServiceName == CommunicationServiceInstaller.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();

                        Log.Info("inSyca.foundation.communication.wsh stopped");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { sender, e }, ex));

                throw new System.Configuration.Install.InstallException
                    (ex.Message.ToString());
            }

        }

        private void MessageBrokerServiceInstaller_BeforeUninstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {
            try
            {
                ServiceController controller = ServiceController.GetServices().Where
                (s => s.ServiceName == CommunicationServiceInstaller.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();

                        Log.Info("inSyca.foundation.communication.wsh stopped");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { sender, e }, ex));

                throw new System.Configuration.Install.InstallException
                    (ex.Message.ToString());
            }
        }
    }
}
