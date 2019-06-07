using inSyca.foundation.framework;
using inSyca.foundation.framework.application.service;
using inSyca.foundation.framework.diagnostics;
using inSyca.foundation.integration.biztalk.data;
using inSyca.foundation.integration.service;
using inSyca.foundation.integration.wsh.diagnostics;
using Microsoft.Win32;
using System;
using System.Data;
using System.ServiceModel;

namespace inSyca.foundation.integration.wsh
{
	public partial class IntegrationService : windowsServiceHost
    {
		public ServiceHost trackingServiceHost = null;

		override protected framework.configuration.AppSchedules appSchedules
        {
            get
            {
                return Configuration.GetAppSchedules();
            }
        }

        public IntegrationService()
        {
            EvaluateRegistryKey(@"inSyca\foundation.integration.biztalk", AppDomain.CurrentDomain.BaseDirectory, "foundation.integration.biztalk.config");
            EvaluateRegistryKey(@"inSyca\foundation.integration.biztalk.functions", AppDomain.CurrentDomain.BaseDirectory, "foundation.integration.biztalk.functions.config");
            EvaluateRegistryKey(@"inSyca\foundation.integration.biztalk.components", AppDomain.CurrentDomain.BaseDirectory, "foundation.integration.biztalk.components.config");

            this.ServiceName = Program.serviceName;

            SystemEvents.SessionEnding += SystemEvents_SessionEnding;
        }

		protected void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
		{
			if (e.Reason == SessionEndReasons.SystemShutdown)
				DisposeWatcher();
		}

		override protected void ImportLogEntryRow(DataRow dataRow)
        {
            dsLogEntry.dtLogEntryRow logEntryRow = (dsLogEntry.dtLogEntryRow)dataRow;

            logEntry.Tables["dtLogEntry"].ImportRow(logEntryRow);

            foreach (dsLogEntry.dtManagementBaseObjectRow managementBaseObjectRow in logEntryRow.GetChildRows("dtLogEntry_dtManagementBaseObject"))
            {
                logEntry.Tables["dtManagementBaseObject"].ImportRow(managementBaseObjectRow);

                foreach (dsLogEntry.dtPropertiesRow propertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtProperties"))
                {
                    logEntry.Tables["dtProperties"].ImportRow(propertiesRow);
                }
                foreach (dsLogEntry.dtQualifiersRow qualifiersRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtQualifiers"))
                {
                    logEntry.Tables["dtQualifiers"].ImportRow(qualifiersRow);
                }
                foreach (dsLogEntry.dtSystemPropertiesRow systemPropertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtSystemProperties"))
                {
                    logEntry.Tables["dtSystemProperties"].ImportRow(systemPropertiesRow);
                }
            }
        }

        override protected void ImportEventEntryRow(DataRow dataRow)
        {
            dsEventEntry.dtEventEntryRow eventEntryRow = (dsEventEntry.dtEventEntryRow)dataRow;

            eventEntry.Tables["dtEventEntry"].ImportRow(eventEntryRow);
        }

        override protected void InvokeWatcher()
        {
			if (Configuration.GetAppSettingsValue("ActivateWatcher").Equals(false))
				return;

			eventEntry = new dsEventEntry();
			logEntry = new dsLogEntry();

			biztalk.diagnostics.Monitoring.MonitoringEvent += new EventHandler<MonitoringEventArgs>(MonitoringEvent);
			biztalk.diagnostics.Monitoring.EventEntryEvent += new EventHandler<MonitoringEventArgs>(EventEntryEvent);

			biztalk.diagnostics.Monitoring.invokeWatcher();
		}

        override protected void DisposeWatcher()
        {
			biztalk.diagnostics.Monitoring.MonitoringEvent -= new EventHandler<MonitoringEventArgs>(MonitoringEvent);
			biztalk.diagnostics.Monitoring.EventEntryEvent -= new EventHandler<MonitoringEventArgs>(EventEntryEvent);

			biztalk.diagnostics.Monitoring.disposeWatcher();
		}

		protected override void OnStart(string[] args)
		{
			base.OnStart(args);

			if (trackingServiceHost != null)
			{
				Log.Info("inSyca.trackingmonitor.ns already initialized - close and re-initialize");

				trackingServiceHost.Close();
			}

			// Create a ServiceHost
			trackingServiceHost = new TrackingServiceHost(typeof(TrackingMonitorService));

			try
			{
				// Open the ServiceHostBase to create listeners and start
				// listening for messages.
				trackingServiceHost.Open();

				Log.Info("inSyca.trackingmonitor.ns ready");
			}
			catch (Exception ex)
			{
				Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), args, ex));
				throw ex;
			}

            

        }

		protected override void OnStop()
		{
			if (trackingServiceHost != null)
			{
				try
				{
					trackingServiceHost.Close();

					Log.Info("inSyca.trackingmonitor.ns closed");
				}
				catch (Exception ex)
				{
					Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
					throw ex;
				}

				trackingServiceHost = null;
			}

			base.OnStop();
		}
	}
}
