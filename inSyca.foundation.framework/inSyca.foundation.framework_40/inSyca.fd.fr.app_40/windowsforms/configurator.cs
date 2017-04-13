using inSyca.foundation.framework.diagnostics;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace inSyca.foundation.framework.application.windowsforms
{
    abstract public partial class configurator : Form
    {
        protected decimal maxLogRecords { get; set; }

        protected DataSet eventEntry { get; set; }
        protected DataSet logEntry { get; set; }

        abstract protected void InvokeWatcher();
        abstract protected void DisposeWatcher();

        abstract protected void WriteOutput(string message);
        abstract protected void ImportLogEntryRow(DataRow dataRow);
        abstract protected void ImportEventEntryRow(DataRow dataRow);

        const string config_extension = "*.config";
        const string config_backup = "{0}.{1:yyyyMMdd_HHmmss}";

        protected bool isDirty { get; set; }
        protected string configFilename { get; set; }

        protected Dictionary<string, configXml> configFiles { get; set; }

        public configurator()
        {
            InitializeComponent();
        }

        public configurator(string headingText)
        {
            InitializeComponent();

            this.rtb_heading.Text = headingText;

            configFiles = new Dictionary<string, configXml>();

            try
            {
                string dir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                string[] configFileNames = System.IO.Directory.GetFiles(dir, config_extension);

                foreach (string configFileName in configFileNames)
                {
                    configXml configFile = new configXml(configFileName);
                    configFiles.Add(Path.GetFileName(configFileName), configFile);
                }

                if (configFileNames.Length < 1)
                    MessageBox.Show(string.Format(application.Properties.Resources.mb_error_1, configFilename), application.Properties.Resources.mb_caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(application.Properties.Resources.mb_error_2, ex.Message), application.Properties.Resources.mb_caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected void EvaluateRegistryKey(string keyName, string configDir, string configFile)
        {
            string registryKeyValue = string.Format(@"SOFTWARE\{0}\", keyName);

            RegistryKey baseRegistryKey32 = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
            RegistryKey baseRegistryKey64 = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey registryKey32 = baseRegistryKey32.OpenSubKey(registryKeyValue);
            RegistryKey registryKey64 = baseRegistryKey64.OpenSubKey(registryKeyValue);

            if (registryKey32 == null || registryKey64 == null)
                throw new Exception("RegistryKeys missing - Please reinstall software");
        }

        /// <summary>
        /// Check the state of IsDirty before closing the form and prompt the
        /// user to save their changes if IsDirty == true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        virtual protected void btnClose_Click(object sender, System.EventArgs e)
        {
            if (this.isDirty == true)
            {
                DialogResult result = MessageBox.Show(application.Properties.Resources.mb_save, application.Properties.Resources.mb_caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    Save();
                else if (result == DialogResult.No)
                    this.Close();
            }
            else
                this.Close();
        }

        /// <summary>
        /// Save the state of the property grid back to the Xml configuration file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            try
            {
                foreach (var configFile in configFiles)
                {
                    if (configFile.Value.isDirty)
                    {
                        File.Move(configFile.Value.configFileName, string.Format(config_backup, configFile.Value.configFileName, DateTime.Now));
                        configuration.RegistrySettings.SetRegistryLogLevel(configFile.Value.GetEventLogLevel(), configFile.Value.GetMailLogLevel(), configFile.Value.RegistryKey);
                        configFile.Value.configDocument.Save(configFile.Value.configFileName);
                    }
                }

                MessageBox.Show(application.Properties.Resources.mb_saved, application.Properties.Resources.mb_caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                isDirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(application.Properties.Resources.mb_error_save, ex.Message), application.Properties.Resources.mb_caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected void uc_monitoring_UserControlEventFired(object sender, UserControlEventFiredArgs e)
        {
            switch (e.fx)
            {
                case UserControlEventFiredArgs.function.Start:
                    StartMonitoring();
                    break;
                case UserControlEventFiredArgs.function.Stop:
                    StopMonitoring();
                    break;
                default:
                    break;
            }
        }

        protected void StartMonitoring()
        {
            InvokeWatcher();
        }

        protected void StopMonitoring()
        {
            DisposeWatcher();
        }

        protected void EventEntryEvent(object sender, MonitoringEventArgs e)
        {
            if (e.Row != null)
            {
                ImportEventEntryRow(e.Row);
                eventEntry.AcceptChanges();
            }
        }

        protected void MonitoringEvent(object sender, MonitoringEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Message))
                WriteOutput(e.Message);
            else if (e.Row != null)
            {
                ImportLogEntryRow(e.Row);

                logEntry.AcceptChanges();
            }
        }
    }
}
