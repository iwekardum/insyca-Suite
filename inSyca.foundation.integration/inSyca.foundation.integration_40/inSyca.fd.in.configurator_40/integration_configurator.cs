using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using inSyca.foundation.framework.diagnostics;
using System;
using inSyca.foundation.integration.biztalk.data;
using System.Data;

namespace inSyca.foundation.integration.configurator
{
    public partial class integration_configurator : framework.application.windowsforms.configurator
    {
        const string in_title = "inSyca Foundation Integration\nConfigurator for .NET 4.0";
        const string in_bt = "foundation.integration.biztalk.config";
        const string in_bt_fx = "foundation.integration.biztalk.functions.config";
        const string in_bt_cp = "foundation.integration.biztalk.components.config";
        const string in_bt_ap = "foundation.integration.biztalk.adapter.config";
        const string reg_in_bt = @"SOFTWARE\inSyca\foundation.integration.biztalk";
        const string reg_in_bt_fx = @"SOFTWARE\inSyca\foundation.integration.biztalk.functions";
        const string reg_in_bt_cp = @"SOFTWARE\inSyca\foundation.integration.biztalk.components";
        const string reg_in_bt_ap = @"SOFTWARE\inSyca\foundation.integration.biztalk.adapter";

        public integration_configurator()
            : base(in_title)
        {
            EvaluateRegistryKey(@"inSyca\foundation.integration.biztalk", AppDomain.CurrentDomain.BaseDirectory, "foundation.integration.biztalk.config");
            EvaluateRegistryKey(@"inSyca\foundation.integration.biztalk.functions", AppDomain.CurrentDomain.BaseDirectory, "foundation.integration.biztalk.functions.config");
            EvaluateRegistryKey(@"inSyca\foundation.integration.biztalk.components", AppDomain.CurrentDomain.BaseDirectory, "foundation.integration.biztalk.components.config");
            EvaluateRegistryKey(@"inSyca\foundation.integration.biztalk.adapter", AppDomain.CurrentDomain.BaseDirectory, "foundation.integration.biztalk.adapter.config");

            InitializeComponent();

            configFiles[in_bt].RegistryKey = reg_in_bt;
            configFiles[in_bt_fx].RegistryKey = reg_in_bt_fx;
            configFiles[in_bt_cp].RegistryKey = reg_in_bt_cp;
            configFiles[in_bt_ap].RegistryKey = reg_in_bt_ap;

            uc_integration_biztalk_functions.tab_monitoring.Visible = false;
            uc_integration_biztalk_components.tab_monitoring.Visible = false;
            uc_integration_biztalk_adapter.tab_monitoring.Visible = false;
        }

        private void uc_integration_biztalk_functions_NavigationChanged(object sender, framework.application.windowsforms.SelectedTabChangedEventArgs e)
        {
            UserControl control = null;

            if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_logging_name)
                control = new uc_logging_integration_biztalk_functions(configFiles[in_bt_fx]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_smtp_name)
                control = new uc_smtp_integration_biztalk_functions(configFiles[in_bt_fx]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_information_name)
                control = new uc_information_integration_biztalk_functions(configFiles[in_bt_fx], new List<framework.diagnostics.Information> { new framework.diagnostics.Information(), new inSyca.foundation.integration.biztalk.functions.diagnostics.Information() });
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_test_name)
                control = new uc_test_integration_biztalk_functions(configFiles[in_bt_fx]);
            else
                return;

            control.Dock = DockStyle.Fill;
            uc_integration_biztalk_functions.AddUserControl(control);
        }

        private void uc_integration_biztalk_components_NavigationChanged(object sender, framework.application.windowsforms.SelectedTabChangedEventArgs e)
        {
            UserControl control = null;

            if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_logging_name)
                control = new uc_logging_integration_biztalk_components(configFiles[in_bt_cp]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_smtp_name)
                control = new uc_smtp_integration_biztalk_components(configFiles[in_bt_cp]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_information_name)
                control = new uc_information_integration_biztalk_components(configFiles[in_bt_cp], new List<framework.diagnostics.Information> { new framework.diagnostics.Information(), new inSyca.foundation.integration.biztalk.components.diagnostics.Information() });
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_test_name)
                control = new uc_test_integration_biztalk_components(configFiles[in_bt_cp]);
            else
                return;

            control.Dock = DockStyle.Fill;
            uc_integration_biztalk_components.AddUserControl(control);
        }

        private void uc_integration_biztalk_adapter_NavigationChanged(object sender, framework.application.windowsforms.SelectedTabChangedEventArgs e)
        {
            UserControl control = null;

            if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_logging_name)
                control = new uc_logging_integration_biztalk_adapter(configFiles[in_bt_ap]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_smtp_name)
                control = new uc_smtp_integration_biztalk_adapter(configFiles[in_bt_ap]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_information_name)
                control = new uc_information_integration_biztalk_adapter(configFiles[in_bt_ap], new List<framework.diagnostics.Information> { new framework.diagnostics.Information(), new inSyca.foundation.integration.biztalk.adapter.diagnostics.Information() });
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_test_name)
                control = new uc_test_integration_biztalk_adapter(configFiles[in_bt_ap]);
            else
                return;

            control.Dock = DockStyle.Fill;
            uc_integration_biztalk_adapter.AddUserControl(control);
        }

        private void uc_integration_biztalk_NavigationChanged(object sender, framework.application.windowsforms.SelectedTabChangedEventArgs e)
        {
            UserControl control = null;

            if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_logging_name)
                control = new uc_logging_integration_biztalk(configFiles[in_bt]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_smtp_name)
                control = new uc_smtp_integration_biztalk(configFiles[in_bt]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_information_name)
                control = new uc_information_integration_biztalk(configFiles[in_bt], new List<framework.diagnostics.Information> { new framework.diagnostics.Information(), new inSyca.foundation.integration.biztalk.diagnostics.Information() });
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_monitoring_name)
                control = new uc_monitoring_integration_biztalk(configFiles[in_bt]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_test_name)
                control = new uc_test_integration_biztalk(configFiles[in_bt]);
            else
                return;

            control.Dock = DockStyle.Fill;
            uc_integration_biztalk.AddUserControl(control);
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

            integration.biztalk.diagnostics.Monitoring.MonitoringEvent += new System.EventHandler<MonitoringEventArgs>(MonitoringEvent);
            integration.biztalk.diagnostics.Monitoring.EventEntryEvent += new EventHandler<MonitoringEventArgs>(EventEntryEvent);

            integration.biztalk.diagnostics.Monitoring.invokeWatcher();
        }

        override protected void DisposeWatcher()
        {
            integration.biztalk.diagnostics.Monitoring.MonitoringEvent -= new System.EventHandler<MonitoringEventArgs>(MonitoringEvent);
            integration.biztalk.diagnostics.Monitoring.EventEntryEvent -= new EventHandler<MonitoringEventArgs>(EventEntryEvent);

            integration.biztalk.diagnostics.Monitoring.disposeWatcher();

            uc_monitor.WriteOutput("----------------------------------------\n", Color.Red);
            uc_monitor.WriteOutput("MONITORING STOPPED\n", Color.Red);
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
            }

            logEntry.AcceptChanges();
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
