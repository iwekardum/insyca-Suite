using inSyca.foundation.framework.diagnostics;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Windows.Forms;


namespace inSyca.foundation.framework.application.windowsforms
{
    public partial class uc_info : UserControl
    {
        protected configXml configFile { get; set; }
        protected List<IInformation> information { get; set; }
        protected PropertyComponent propertyComponent { get; set; }
        string[] uninstallKeys = new string[] { @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall" };
        protected Dictionary<string, Dictionary<string, string>> applications { get; set; }

        protected uc_info()
        {
            InitializeComponent();
        }

        protected uc_info(configXml _configFile, List<IInformation> _information)
        {
            configFile = _configFile;
            information = _information;
            InitializeComponent();
        }

        protected uc_info(configXml _configFile, IInformation _information)
        {
            configFile = _configFile;
            information = new List<IInformation>();
            information.Add(_information);
            InitializeComponent();
        }

        private void uc_info_Load(object sender, EventArgs e)
        {
            LoadConfiguration();
            this.propertyInformation.SelectedObject = propertyComponent;
            this.propertyInformation.Focus();
        }

        virtual protected void LoadConfiguration()
        {
            propertyComponent = new PropertyComponent();

            propertyComponent.AddProperty(Properties.Resources.info_user, SystemInformation.UserName, string.Empty, Properties.Resources.info_sys_caption, typeof(string), true, false);
            propertyComponent.AddProperty(Properties.Resources.info_computername, SystemInformation.ComputerName, string.Empty, Properties.Resources.info_sys_caption, typeof(string), true, false);
            propertyComponent.AddProperty(Properties.Resources.info_domain, SystemInformation.UserDomainName, string.Empty, Properties.Resources.info_sys_caption, typeof(string), true, false);
            propertyComponent.AddProperty(Properties.Resources.info_netbios, Environment.MachineName, string.Empty, Properties.Resources.info_sys_caption, typeof(string), true, false);
            propertyComponent.AddProperty(Properties.Resources.info_culture, CultureInfo.CurrentCulture.Name, string.Empty, Properties.Resources.info_sys_caption, typeof(string), true, false);
            propertyComponent.AddProperty(Properties.Resources.info_uiculture, CultureInfo.CurrentUICulture.Name, string.Empty, Properties.Resources.info_sys_caption, typeof(string), true, false);

            propertyComponent.AddProperty(Properties.Resources.info_is64, Environment.Is64BitOperatingSystem, string.Empty, Properties.Resources.info_os_caption, typeof(string), true, false);
            propertyComponent.AddProperty(Properties.Resources.info_os, Environment.OSVersion.Platform.ToString(), string.Empty, Properties.Resources.info_os_caption, typeof(string), true, false);
            propertyComponent.AddProperty(Properties.Resources.info_version, string.Format("{0}.{1}", Environment.OSVersion.Version.Major, Environment.OSVersion.Version.Minor), string.Empty, Properties.Resources.info_os_caption, typeof(string), true, false);
            propertyComponent.AddProperty(Properties.Resources.info_build, Environment.OSVersion.Version.Build.ToString(), string.Empty, Properties.Resources.info_os_caption, typeof(string), true, false);
            propertyComponent.AddProperty(Properties.Resources.info_revision, Environment.OSVersion.Version.Revision.ToString(), string.Empty, Properties.Resources.info_os_caption, typeof(string), true, false);

            propertyComponent.AddProperty(Properties.Resources.info_clrversion, string.Format("{0}.{1}", Environment.Version.Major, Environment.Version.Minor), string.Empty, Properties.Resources.info_dotnet_caption, typeof(string), true, false);
            propertyComponent.AddProperty(Properties.Resources.info_clrbuild, Environment.Version.Build.ToString(), string.Empty, Properties.Resources.info_dotnet_caption, typeof(string), true, false);
            propertyComponent.AddProperty(Properties.Resources.info_clrrevision, Environment.Version.Revision.ToString(), string.Empty, Properties.Resources.info_dotnet_caption, typeof(string), true, false);

            propertyComponent.AddProperty(Properties.Resources.info_workingset, string.Format("{0} MB", System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024), string.Empty, Properties.Resources.info_memory_caption, typeof(string), true, false);

            propertyComponent.AddProperty(Properties.Resources.info_connection, SystemInformation.Network.ToString(), string.Empty, Properties.Resources.info_network_caption, typeof(string), true, false);

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            propertyComponent.AddProperty(Properties.Resources.info_hostname, host.HostName, string.Empty, Properties.Resources.info_network_caption, typeof(string), true, false);
            foreach (IPAddress ip in host.AddressList)
                if (ip.IsIPv6LinkLocal || ip.IsIPv6Multicast || ip.IsIPv6SiteLocal || ip.IsIPv6Teredo)
                    propertyComponent.AddProperty(Properties.Resources.info_ipv6, ip.ToString(), string.Empty, Properties.Resources.info_network_caption, typeof(string), true, false);
                else
                    propertyComponent.AddProperty(Properties.Resources.info_ipv4, ip.ToString(), string.Empty, Properties.Resources.info_network_caption, typeof(string), true, false);

            if (applications != null)
            {
                foreach (string strApplicationKey in applications.Keys)
                {
                    Dictionary<string, string> appValues;
                    applications.TryGetValue(strApplicationKey, out appValues);

                    foreach (string strValueKey in appValues.Keys)
                    {
                        string strValue;
                        appValues.TryGetValue(strValueKey, out strValue);

                        propertyComponent.AddProperty(strValueKey, strValue, string.Empty, strApplicationKey, typeof(string), true, false);
                    }
                }
            }
        }

        protected void GetInstalledPrograms(string[] subKeyNames)
        {
            applications = new Dictionary<string, Dictionary<string, string>>();

            foreach (var uninstallKey in uninstallKeys)
            {
                using (RegistryKey rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(uninstallKey))
                {
                    foreach (string skName in rk.GetSubKeyNames())
                    {
                        foreach (var subKeyName in subKeyNames)
                        {
                            if (skName.Contains(subKeyName))
                            {
                                using (RegistryKey sk = rk.OpenSubKey(skName))
                                {
                                    try
                                    {
                                        Dictionary<string, string> values = new Dictionary<string, string>();

                                        foreach (string valueName in sk.GetValueNames())
                                            if (valueName.Contains("Display") && !valueName.Contains("Icon"))
                                                values.Add(valueName, sk.GetValue(valueName).ToString());
                                        applications.Add(skName, values);
                                    }
                                    catch (Exception ex)
                                    {
                                        string msg = ex.Message;
                                    }
                                }
                            }
                        }
                    }
                }
                using (RegistryKey rk = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(uninstallKey))
                {
                    foreach (string skName in rk.GetSubKeyNames())
                    {
                        foreach (var subKeyName in subKeyNames)
                        {
                            if (skName.Contains(subKeyName))
                            {
                                using (RegistryKey sk = rk.OpenSubKey(skName))
                                {
                                    try
                                    {
                                        Dictionary<string, string> values = new Dictionary<string, string>();

                                        foreach (string valueName in sk.GetValueNames())
                                            if (valueName.Contains("Display") && !valueName.Contains("Icon"))
                                                values.Add(valueName, sk.GetValue(valueName).ToString());
                                        applications.Add(skName, values);
                                    }
                                    catch (Exception ex)
                                    {
                                        string msg = ex.Message;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
