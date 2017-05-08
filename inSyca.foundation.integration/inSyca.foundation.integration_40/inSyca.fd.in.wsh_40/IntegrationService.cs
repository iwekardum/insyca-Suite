using inSyca.foundation.framework.application.service;
using inSyca.foundation.framework.diagnostics;
using inSyca.foundation.framework.schedules;
using inSyca.foundation.integration.biztalk.data;
using inSyca.foundation.integration.wsh.diagnostics;
using Microsoft.Win32;
using System;
using System.Data;

namespace inSyca.foundation.integration.wsh
{
    public partial class IntegrationService : windowsServiceHost
    {
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

        // Specify what you want to happen when the Elapsed event is 
        // raised.
        override protected void OnScheduledEvent(Task task)
        {
            Log.DebugFormat("OnScheduledEvent(Task task {0})", task);

            switch (task.AppSchedule.Name)
            {
                case "inSycaTestOnce":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestOnce", task);
                    break;
                case "inSycaTestEveryMinute":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestEveryMinute", task);
                    break;
                case "inSycaTestHourly":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestHourly", task);
                    break;
                case "inSycaTestDaily":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestDaily", task);
                    break;
                case "inSycaTestWeekly":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestWeekly", task);
                    break;
                case "inSycaTestMonthly":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestMonthly", task);
                    break;
                case "inSycaTestYearly":
                    Log.InfoFormat("OnScheduledEvent(Task task {0})\ninSycaTestYearly", task);
                    break;
                default:
                    break;
            }
        }

        void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
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

    }
}
