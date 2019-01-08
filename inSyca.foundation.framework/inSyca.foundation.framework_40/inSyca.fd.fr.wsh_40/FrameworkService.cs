using inSyca.foundation.framework.application.service;
using inSyca.foundation.framework.data;
using inSyca.foundation.framework.diagnostics;
using Microsoft.Win32;
using System;
using System.Data;
using inSyca.foundation.framework.configuration;

namespace inSyca.foundation.framework.wsh
{
    public partial class FrameworkService : windowsServiceHost
    {
		protected override AppSchedules appSchedules
		{
			get
			{
				return Configuration.GetAppSchedules();
			}
		}

		public FrameworkService()
        {
            EvaluateRegistryKey(@"inSyca\foundation.framework", AppDomain.CurrentDomain.BaseDirectory, "foundation.framework.config");

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
            eventEntry = new dsEventEntry();
            logEntry = new dsLogEntry();

            Monitoring.MonitoringEvent += new System.EventHandler<MonitoringEventArgs>(MonitoringEvent);
            Monitoring.EventEntryEvent += new EventHandler<MonitoringEventArgs>(EventEntryEvent);

            Monitoring.invokeWatcher();
        }

        override protected void DisposeWatcher()
        {
            Monitoring.MonitoringEvent -= new System.EventHandler<MonitoringEventArgs>(MonitoringEvent);
            Monitoring.EventEntryEvent -= new EventHandler<MonitoringEventArgs>(EventEntryEvent);

            Monitoring.disposeWatcher();
        }
    }
}
