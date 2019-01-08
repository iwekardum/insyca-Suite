using inSyca.foundation.communication.service;
using inSyca.foundation.framework;
using inSyca.foundation.framework.application.service;
using System;
using System.ServiceModel;
using System.ServiceProcess;
using System.Data;
using Microsoft.Win32;
using inSyca.foundation.framework.configuration;

namespace inSyca.foundation.communication.wsh
{
    public partial class CommunicationService : windowsServiceHost
	{
        public ServiceHost messageBrokerServiceHost = null;

		protected override AppSchedules appSchedules
		{
			get
			{
				return Configuration.GetAppSchedules();
			}
		}

		public CommunicationService()
        {
			//EvaluateRegistryKey(@"inSyca\foundation.integration.biztalk", AppDomain.CurrentDomain.BaseDirectory, "foundation.integration.biztalk.config");
			//EvaluateRegistryKey(@"inSyca\foundation.integration.biztalk.functions", AppDomain.CurrentDomain.BaseDirectory, "foundation.integration.biztalk.functions.config");
			//EvaluateRegistryKey(@"inSyca\foundation.integration.biztalk.components", AppDomain.CurrentDomain.BaseDirectory, "foundation.integration.biztalk.components.config");

			this.ServiceName = Program.serviceName;

			SystemEvents.SessionEnding += SystemEvents_SessionEnding;
		}

		protected void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
		{
			if (e.Reason == SessionEndReasons.SystemShutdown)
				DisposeWatcher();
		}

		protected override void DisposeWatcher()
		{
		}

		protected override void ImportEventEntryRow(DataRow dataRow)
		{
		}

		protected override void ImportLogEntryRow(DataRow dataRow)
		{
		}

		protected override void InvokeWatcher()
		{
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
