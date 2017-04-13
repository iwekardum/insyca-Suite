using inSyca.foundation.framework.application.windowsforms;
using inSyca.foundation.framework.data;
using inSyca.foundation.framework.diagnostics;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace inSyca.foundation.framework.configurator
{
    public partial class framework_configurator : framework.application.windowsforms.configurator
    {
        const string fr_title = "inSyca Foundation Framework \nConfigurator for .NET 4.0";
        const string fr_lib = "foundation.framework.config";
        const string reg_fr_lib = @"SOFTWARE\inSyca\foundation.framework";

        public framework_configurator()
            : base(fr_title)
        {
            EvaluateRegistryKey(@"inSyca\foundation.framework", AppDomain.CurrentDomain.BaseDirectory, fr_lib);

            InitializeComponent();
            
            configFiles[fr_lib].RegistryKey = reg_fr_lib;
        }

        private void uc_framework_NavigationChanged(object sender, SelectedTabChangedEventArgs e)
        {
            UserControl control = null;

            if (e.SelectedTab.Name == application.Properties.Resources.tab_logging_name)
                control = new uc_logging(configFiles[fr_lib]);
            else if (e.SelectedTab.Name == application.Properties.Resources.tab_smtp_name)
                control = new uc_smtp(configFiles[fr_lib]);
            else if (e.SelectedTab.Name == application.Properties.Resources.tab_information_name)
                control = new uc_information(configFiles[fr_lib], new Information());
            else if (e.SelectedTab.Name == application.Properties.Resources.tab_test_name)
                control = new uc_test(configFiles[fr_lib]);
            else if (e.SelectedTab.Name == application.Properties.Resources.tab_monitoring_name)
                control = new uc_monitoring(configFiles[fr_lib]);
            else
                return;

            uc_framework.AddUserControl(control);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            StopMonitoring();
            base.OnClosing(e);
        }

        #region Monitoring

        override protected void InvokeWatcher()
        {
            uc_monitor.WriteOutput("----------------------------------------", Color.Green);
            uc_monitor.WriteOutput("\nMONITORING STARTED\n", Color.Green);
            uc_monitor.WriteOutput("----------------------------------------\n", Color.Green);

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

            uc_monitor.WriteOutput("----------------------------------------", Color.Red);
            uc_monitor.WriteOutput("\nMONITORING STOPPED\n", Color.Red);
            uc_monitor.WriteOutput("----------------------------------------\n", Color.Red);
        }

        override protected void WriteOutput(string message)
        {
            Color color = Color.White;
            uc_monitor.WriteOutput(message + "\n", color);
        }

        override protected void ImportLogEntryRow(DataRow dataRow)
        {
            Color color = Color.White;

            dsLogEntry.dtLogEntryRow logEntryRow = (dsLogEntry.dtLogEntryRow)dataRow;

            logEntry.Tables["dtLogEntry"].ImportRow(logEntryRow);

            switch (logEntryRow.eventtype)
            {
                default:
                    color = Color.White;
                    break;
            }

            uc_monitor.WriteOutput("----------------------------------------\n", color);
            uc_monitor.WriteOutput(logEntryRow.eventtype + "\n", color);
            uc_monitor.WriteOutput("----------------------------------------\n", color);

            foreach (dsLogEntry.dtManagementBaseObjectRow managementBaseObjectRow in logEntryRow.GetChildRows("dtLogEntry_dtManagementBaseObject"))
            {
                logEntry.Tables["dtManagementBaseObject"].ImportRow(managementBaseObjectRow);
                uc_monitor.WriteOutput(managementBaseObjectRow.classname + "\n", color);

                foreach (dsLogEntry.dtPropertiesRow propertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtProperties"))
                {
                    logEntry.Tables["dtProperties"].ImportRow(propertiesRow);
                    uc_monitor.WriteOutput(string.Format("{0}\tValue: {1}\n", propertiesRow.name.PadRight(20, ' '), propertiesRow.value), color);
                }
                foreach (dsLogEntry.dtQualifiersRow qualifiersRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtQualifiers"))
                {
                    logEntry.Tables["dtQualifiers"].ImportRow(qualifiersRow);
                    uc_monitor.WriteOutput(string.Format("{0}\tValue: {1}\n", qualifiersRow.name.PadRight(20, ' '), qualifiersRow.value), color);
                }
                foreach (dsLogEntry.dtSystemPropertiesRow systemPropertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtSystemProperties"))
                {
                    logEntry.Tables["dtSystemProperties"].ImportRow(systemPropertiesRow);
                    uc_monitor.WriteOutput(string.Format("{0}\tValue: {1}\n", systemPropertiesRow.name.PadRight(20, ' '), systemPropertiesRow.value), color);
                }

                logEntry.AcceptChanges();
            }
        }

        override protected void ImportEventEntryRow(DataRow dataRow)
        {
            dsEventEntry.dtEventEntryRow eventEntryRow = (dsEventEntry.dtEventEntryRow)dataRow;

            eventEntry.Tables["dtEventEntry"].ImportRow(eventEntryRow);
            eventEntry.AcceptChanges();
        }

        #endregion
    }
}
