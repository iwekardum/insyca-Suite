using inSyca.foundation.communication.service;
using inSyca.foundation.framework;
using System;
using System.ServiceModel;
using System.ServiceProcess;

namespace inSyca.foundation.communication.wsh
{
    public partial class CommunicationService : ServiceBase
    {
        public ServiceHost messageBrokerServiceHost = null;

        public CommunicationService()
        {
            this.ServiceName = Program.serviceName;
        }

        protected override void OnStart(string[] args)
        {
            if (messageBrokerServiceHost != null)
            {
                Log.Info("inSyca.messagebroker.ns already initialized - close and re-initialize");

                messageBrokerServiceHost.Close();
            }

            // Create a ServiceHost
            messageBrokerServiceHost = new MessageBrokerServiceHost(typeof(MessageBrokerService));

            try
            {
                // Open the ServiceHostBase to create listeners and start
                // listening for messages.
                messageBrokerServiceHost.Open();

                Log.Info("inSyca.messagebroker.ns ready");
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), args, ex));
                throw ex;
            }
        }

        protected override void OnStop()
        {
            if (messageBrokerServiceHost != null)
            {
                try
                {
                    messageBrokerServiceHost.Close();

                    Log.Info("inSyca.messagebroker.ns closed");
                }
                catch (Exception ex)
                {
                    Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
                    throw ex;
                }

                messageBrokerServiceHost = null;
            }
        }
    }
}
