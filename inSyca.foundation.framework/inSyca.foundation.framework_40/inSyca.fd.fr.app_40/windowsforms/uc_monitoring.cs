using System;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Resources;
using System.Reflection;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Management;

namespace inSyca.foundation.framework.application.windowsforms
{
    public partial class uc_monitoring : uc_setting
    {
        protected string ServiceName { get; set; }
        protected string ServiceAssemblyName { get; set; }
        protected ServiceController service;
        protected TimeSpan timeout;

        protected uc_monitoring()
            : base()
        {
            InitializeComponent();
        }

        protected uc_monitoring(configXml _configFile)
            : base(_configFile)
        {
            InitializeComponent();
        }

        override protected bool LoadConfiguration()
        {
            if (!base.LoadConfiguration())
                return false;

            foreach (XElement xNode in xDocument.Root.Elements("appSettings").Elements("add"))
            {
                switch (xNode.Attribute("key").Value.ToLower())
                {
                    case "logservernames":
                    case "maxlogrecords":
                    case "applicationsource":
                    case "systemsource":
                    case "logpath":
                        propertyComponent.AddProperty(transformAppSettingsXnode(xNode));
                        break;
                }
            }

            timeout = TimeSpan.FromMilliseconds(20000);
            service = new ServiceController(ServiceName);

            SetServiceStatus();

            return true;
        }

        private void SetServiceStatus()
        {
            btn_startService.Enabled = true;
            btn_installService.Enabled = true;
            btn_uninstallService.Enabled = true;
            btn_stopService.Enabled = true;

            try
            {
                if (service.Status == ServiceControllerStatus.Running)
                {
                    btn_startService.Enabled = false;
                    btn_installService.Enabled = false;
                    return;
                }
                else if (service.Status == ServiceControllerStatus.Stopped)
                {
                    btn_stopService.Enabled = false;
                    btn_installService.Enabled = false;
                    return;
                }

                btn_uninstallService.Enabled = false;
                btn_startService.Enabled = false;
                btn_stopService.Enabled = false;
            }
            catch (InvalidOperationException)
            {
                btn_uninstallService.Enabled = false;
                btn_startService.Enabled = false;
                btn_stopService.Enabled = false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        void btn_stopService_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Stop Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                SetServiceStatus();
            }

            MessageBox.Show(string.Format("Service  '{0}' stopped!", ServiceName), "Stop Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void btn_startService_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Start Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                SetServiceStatus();
            }

            MessageBox.Show(string.Format("Service  '{0}' started!", ServiceName), "Start Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void btn_uninstallService_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { "/u", ServiceAssemblyName });

                bool serviceExists = true;

                do
                {
                    string wmiQuery = string.Format("SELECT * FROM Win32_Service WHERE Name='{0}'", ServiceName);
                    var searcher = new ManagementObjectSearcher(wmiQuery);
                    var results = searcher.Get();

                    if (results.Count <= 0)
                        serviceExists = false;

                    foreach (ManagementObject mbo in results)
                    {
                        if (mbo["StartMode"].ToString() == "Disabled")
                        {
                            service.Close();
                            service.Dispose();
                            service = new ServiceController(ServiceName);
                            break;
                        }
                    }

                } while (serviceExists);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Uninstall Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                SetServiceStatus();
            }

            MessageBox.Show(string.Format("Service  '{0}' uninstalled!", ServiceName), "Uninstall Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void btn_installService_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { ServiceAssemblyName });
                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMilliseconds(5000));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Install Service", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                SetServiceStatus();
            }

            MessageBox.Show(string.Format("Service  '{0}' installed!", ServiceName), "Install Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
