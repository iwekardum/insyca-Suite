using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using inSyca.foundation.framework.data;
using inSyca.foundation.framework.application.windowsforms;
using System.Drawing;
using System;

namespace inSyca.foundation.communication.configurator
{
    public partial class communication_configurator : framework.application.windowsforms.configurator
    {
        const string co_title   = "inSyca Foundation Communication\nConfigurator";
        const string co_cli     = "foundation.communication.clients.config";
        const string co_cp      = "foundation.communication.components.config";
        const string co_svc     = "foundation.communication.service.config";
        const string co_wcf     = "foundation.communication.wcf.config";
        const string reg_co_cli = @"SOFTWARE\inSyca\foundation.communication.clients";
        const string reg_co_cp  = @"SOFTWARE\inSyca\foundation.communication.components";
        const string reg_co_svc = @"SOFTWARE\inSyca\foundation.communication.service";
        const string reg_co_wcf = @"SOFTWARE\inSyca\foundation.communication.wcf";

        public communication_configurator()
            : base(co_title)
        {
            EvaluateRegistryKey(@"inSyca\foundation.communication.clients", AppDomain.CurrentDomain.BaseDirectory, "foundation.communication.clients.config");
            EvaluateRegistryKey(@"inSyca\foundation.communication.components", AppDomain.CurrentDomain.BaseDirectory, "foundation.communication.components.config");
            EvaluateRegistryKey(@"inSyca\foundation.communication.service", AppDomain.CurrentDomain.BaseDirectory, "foundation.communication.service.config");
            EvaluateRegistryKey(@"inSyca\foundation.communication.wcf", AppDomain.CurrentDomain.BaseDirectory, "foundation.communication.wcf.config");
            EvaluateRegistryKey(@"inSyca\configurator.communication", AppDomain.CurrentDomain.BaseDirectory, "configurator.communication.exe.config");

            InitializeComponent();

            configFiles[co_cli].RegistryKey = reg_co_cli;
            configFiles[co_cp].RegistryKey  = reg_co_cp;
            configFiles[co_svc].RegistryKey = reg_co_svc;
            configFiles[co_wcf].RegistryKey = reg_co_wcf;

            uc_communication_clients.tab_monitoring.Visible = false;
            uc_communication_components.tab_monitoring.Visible = false;
            uc_communication_service.tab_monitoring.Visible = false;
            uc_communication_wcf.tab_monitoring.Visible = false;
        }

        private void uc_communication_client_NavigationChanged(object sender, framework.application.windowsforms.SelectedTabChangedEventArgs e)
        {
            UserControl control = null;

            if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_logging_name)
                control = new uc_logging_clients(configFiles[co_cli]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_smtp_name)
                control = new uc_smtp_client(configFiles[co_cli]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_information_name)
                control = new uc_information_clients(configFiles[co_cli], new List<framework.diagnostics.Information> { new framework.diagnostics.Information(), new framework.diagnostics.Information() });
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_test_name)
                control = new uc_test_clients(configFiles[co_cp]);
            else
                return;

            control.Dock = DockStyle.Fill;
            uc_communication_clients.AddUserControl(control);
        }

        private void uc_communication_components_NavigationChanged(object sender, framework.application.windowsforms.SelectedTabChangedEventArgs e)
        {
            UserControl control = null;

            if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_logging_name)
                control = new uc_logging_components(configFiles[co_cp]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_smtp_name)
                control = new uc_smtp_components(configFiles[co_cp]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_information_name)
                control = new uc_information_components(configFiles[co_cp], new List<framework.diagnostics.Information> { new framework.diagnostics.Information(), new framework.diagnostics.Information() });
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_test_name)
                control = new uc_test_components(configFiles[co_cp]);
            else
                return;

            control.Dock = DockStyle.Fill;
            uc_communication_components.AddUserControl(control);
        }

        private void uc_communication_wcf_NavigationChanged(object sender, framework.application.windowsforms.SelectedTabChangedEventArgs e)
        {
            UserControl control = null;

            if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_logging_name)
                control = new uc_logging_wcf(configFiles[co_wcf]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_smtp_name)
                control = new uc_smtp_wcf(configFiles[co_wcf]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_information_name)
                control = new uc_information_wcf(configFiles[co_wcf], new List<framework.diagnostics.Information> { new framework.diagnostics.Information(), new framework.diagnostics.Information() });
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_test_name)
                control = new uc_test_wcf(configFiles[co_wcf]);
            else
                return;

            control.Dock = DockStyle.Fill;
            uc_communication_wcf.AddUserControl(control);
        }

        private void uc_communication_service_NavigationChanged(object sender, framework.application.windowsforms.SelectedTabChangedEventArgs e)
        {
            UserControl control = null;

            if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_logging_name)
                control = new uc_logging_service(configFiles[co_svc]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_smtp_name)
                control = new uc_smtp_service(configFiles[co_svc]);
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_information_name)
                control = new uc_information_service(configFiles[co_svc], new List<framework.diagnostics.Information> { new framework.diagnostics.Information(), new framework.diagnostics.Information() });
            else if (e.SelectedTab.Name == framework.application.Properties.Resources.tab_test_name)
                control = new uc_test_service(configFiles[co_svc]);
            else
                return;

            control.Dock = DockStyle.Fill;
            uc_communication_service.AddUserControl(control);
        }

        #region Monitoring

        override protected void InvokeWatcher()
        {
            //uc_monitor.WriteOutput("----------------------------------------", Color.Green);
            //uc_monitor.WriteOutput("\nMONITORING STARTED\n", Color.Green);
            //uc_monitor.WriteOutput("----------------------------------------\n", Color.Green);

            //eventEntry = new dsEventEntry();
            //logEntry = new dsLogEntry();

            //integration.biztalk.diagnostics.Monitoring.MonitoringEvent += new System.EventHandler<MonitoringEventArgs>(MonitoringEvent);
            //integration.biztalk.diagnostics.Monitoring.EventEntryEvent += new EventHandler<MonitoringEventArgs>(EventEntryEvent);

            //integration.biztalk.diagnostics.Monitoring.invokeWatcher();
        }

        override protected void DisposeWatcher()
        {
            //integration.biztalk.diagnostics.Monitoring.MonitoringEvent -= new System.EventHandler<MonitoringEventArgs>(MonitoringEvent);
            //integration.biztalk.diagnostics.Monitoring.EventEntryEvent -= new EventHandler<MonitoringEventArgs>(EventEntryEvent);

            //integration.biztalk.diagnostics.Monitoring.disposeWatcher();

            //uc_monitor.WriteOutput("----------------------------------------\n", Color.Red);
            //uc_monitor.WriteOutput("MONITORING STOPPED\n", Color.Red);
            //uc_monitor.WriteOutput("----------------------------------------\n", Color.Red);
        }

        override protected void WriteOutput(string message)
        {
            //Color color = Color.White;
            //uc_monitor.WriteOutput(message + "\n", color);
        }

        override protected void ImportLogEntryRow(DataRow dataRow)
        {
            //Color color = Color.White;

            //dsLogEntry.dtLogEntryRow logEntryRow = (dsLogEntry.dtLogEntryRow)dataRow;

            //logEntry.Tables["dtLogEntry"].ImportRow(logEntryRow);

            //switch (logEntryRow.eventtype)
            //{
            //    default:
            //        color = Color.White;
            //        break;
            //}

            //uc_monitor.WriteOutput("----------------------------------------\n", color);
            //uc_monitor.WriteOutput(logEntryRow.eventtype + "\n", color);
            //uc_monitor.WriteOutput("----------------------------------------\n", color);

            //foreach (dsLogEntry.dtManagementBaseObjectRow managementBaseObjectRow in logEntryRow.GetChildRows("dtLogEntry_dtManagementBaseObject"))
            //{
            //    logEntry.Tables["dtManagementBaseObject"].ImportRow(managementBaseObjectRow);
            //    uc_monitor.WriteOutput(managementBaseObjectRow.classname + "\n", color);

            //    foreach (dsLogEntry.dtPropertiesRow propertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtProperties"))
            //    {
            //        logEntry.Tables["dtProperties"].ImportRow(propertiesRow);
            //        uc_monitor.WriteOutput(string.Format("{0}\tValue: {1}\n", propertiesRow.name.PadRight(20, ' '), propertiesRow.value), color);
            //    }
            //    foreach (dsLogEntry.dtQualifiersRow qualifiersRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtQualifiers"))
            //    {
            //        logEntry.Tables["dtQualifiers"].ImportRow(qualifiersRow);
            //        uc_monitor.WriteOutput(string.Format("{0}\tValue: {1}\n", qualifiersRow.name.PadRight(20, ' '), qualifiersRow.value), color);
            //    }
            //    foreach (dsLogEntry.dtSystemPropertiesRow systemPropertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtSystemProperties"))
            //    {
            //        logEntry.Tables["dtSystemProperties"].ImportRow(systemPropertiesRow);
            //        uc_monitor.WriteOutput(string.Format("{0}\tValue: {1}\n", systemPropertiesRow.name.PadRight(20, ' '), systemPropertiesRow.value), color);
            //    }
            //}

            //logEntry.AcceptChanges();
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
