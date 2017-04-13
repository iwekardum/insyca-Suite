using inSyca.foundation.framework;
using System.Linq;
using System.ComponentModel;
using System.ServiceProcess;
using System;

namespace inSyca.messagebroker.ns
{
    [RunInstaller(true)]
    public partial class MessageBrokerInstaller : System.Configuration.Install.Installer
    {
        public MessageBrokerInstaller()
        {
            InitializeComponent();
        }

        private void MessageBrokerServiceInstaller_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {
            try
            {
                new ServiceController(MessageBrokerServiceInstaller.ServiceName).Start();

                Log.Info("inSyca.messagebroker.ns started");

            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
            }
        }

        private void MessageBrokerServiceInstaller_BeforeInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {
            try
            {
                ServiceController controller = ServiceController.GetServices().Where
                (s => s.ServiceName == MessageBrokerServiceInstaller.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();

                        Log.Info("inSyca.messagebroker.ns stopped");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));

                throw new System.Configuration.Install.InstallException
                    (ex.Message.ToString());
            }

        }

        private void MessageBrokerServiceInstaller_BeforeUninstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {
            try
            {
                ServiceController controller = ServiceController.GetServices().Where
                (s => s.ServiceName == MessageBrokerServiceInstaller.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();

                        Log.Info("inSyca.messagebroker.ns stopped");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));

                throw new System.Configuration.Install.InstallException
                    (ex.Message.ToString());
            }
        }

    }
}

