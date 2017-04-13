using inSyca.foundation.communication.itf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace inSyca.foundation.communication.clients
{
    [ComVisible(true)]
    public class OfficeClient : Microsoft.Office.Core.IRibbonExtensibility
    {
        public OfficeClient()
        {
            officeApplicationContainer = new OfficeApplicationContainer();
        }

        public static ConfigurationChannelFactory<IOffice> officeService
        {
            get
            {
                //System.Diagnostics.EventLog.WriteEntry("inSyca.MessageBroker.Office.Client", "Get officeService", System.Diagnostics.EventLogEntryType.Information);

                var configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap { ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "foundation.communication.clients.config") }, ConfigurationUserLevel.None);

                //System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("Configuration: {0}", configuration.FilePath), System.Diagnostics.EventLogEntryType.Information);

                return new ConfigurationChannelFactory<IOffice>("OfficeTcp", configuration, null);
            }
        }

        public OfficeApplicationContainer officeApplicationContainer { get; set; }
        public DataDialogContainer dataDialogContainer { get; set; }
        public DataRibbonContainer dataRibbonContainer { get; set; }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string applicationType)
        {
            try
            {
                using (officeService)
                {
                    IOffice proxyICompany = officeService.CreateChannel();

                    dataRibbonContainer = proxyICompany.GetDataRibbonContainer(officeApplicationContainer);

                    officeService.Close();
                }
            }
            catch (System.ServiceModel.FaultException fex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", fex.Message), System.Diagnostics.EventLogEntryType.Error);
            }
            catch (InvalidOperationException oex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", oex.Message), System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", ex.Message), System.Diagnostics.EventLogEntryType.Error);
            }

            return dataRibbonContainer.ribbonData.ToString();
        }

        #endregion

        #region ActionHandlers
        public void OnRibbonAction(Microsoft.Office.Core.IRibbonControl control)
        {
            OnAction(control.Id);
        }

        private void OnButtonAction(object sender, RoutedEventArgs e)
        {
            UIElement uiElement = sender as UIElement;

            if (uiElement == null)
                return;

            OnAction(uiElement.Uid);
        }

        private void OnComboBoxAction(object sender, EventArgs e)
        {
            UIElement uiElement = sender as UIElement;

            if (uiElement == null)
                return;

            OnAction(uiElement.Uid);
        }

        private void OnAction(string strId)
        {
            XElement xControl = dataRibbonContainer.actionData.Descendants("control").FirstOrDefault(el => el.Attribute("id").Value == strId);
            IEnumerable<XElement> items = from el in xControl.Descendants("action") select el;

            foreach (var xElement in items)
            {
                switch (xElement.Value)
                {
                    case "ShowDialog":
                        ShowDialog(xElement.Name.LocalName);
                        break;

                    case "SetDocument":
                        SetDocument(xElement.Name.LocalName);
                        break;

                    case "GetDocument":
                        GetDocument(xElement.Name.LocalName);
                        break;

                    case "TransformDocument":
                        TransformDocument(xElement.Name.LocalName);
                        break;

                    case "ProcessDocumentData":
                        ProcessDocumentData(xElement.Name.LocalName);
                        break;

                    case "UpdateDialogData":
                        UpdateDialogData(xElement.Name.LocalName);
                        break;

                    default:
                        break;
                }
            }
        }

        #endregion

        #region Actions

        private void ShowDialog(string dialogID)
        {
            try
            {
                using (officeService)
                {
                    IOffice proxyICompany = officeService.CreateChannel();

                    dataDialogContainer = proxyICompany.GetDataDialogContainer(officeApplicationContainer);

                    officeService.Close();
                }
            }
            catch (System.ServiceModel.FaultException fex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", fex.Message), System.Diagnostics.EventLogEntryType.Error);
            }
            catch (InvalidOperationException oex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", oex.Message), System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", ex.Message), System.Diagnostics.EventLogEntryType.Error);
            }

            AddEventHandlers(LogicalTreeHelper.GetChildren(dataDialogContainer.window));

            dataDialogContainer.window.ShowDialog();
        }

        private void UpdateDialogData(string id)
        {
            dataDialogContainer.UpdateDataSource();

            try
            {
                using (officeService)
                {
                    IOffice proxyICompany = officeService.CreateChannel();

                    dataDialogContainer.dataSource = proxyICompany.UpdateDialogData(officeApplicationContainer, dataDialogContainer.dataSource);

                    officeService.Close();
                }
            }
            catch (System.ServiceModel.FaultException fex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", fex.Message), System.Diagnostics.EventLogEntryType.Error);
            }
            catch (InvalidOperationException oex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", oex.Message), System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", ex.Message), System.Diagnostics.EventLogEntryType.Error);
            }

            dataDialogContainer.UpdateDataProvider();
        }

        private void ProcessDocumentData(string id)
        {
            officeApplicationContainer.ProcessDocumentData(id, dataRibbonContainer.documentData);
        }

        private void SetDocument(string id)
        {
            string tempfile = officeApplicationContainer.CreateTempFile();

            using (var stream = File.Open(tempfile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] contents = File.ReadAllBytes(tempfile);

                try
                {
                    using (officeService)
                    {
                        IOffice proxyICompany = officeService.CreateChannel();

                        proxyICompany.SetDocument(officeApplicationContainer, contents);

                        officeService.Close();
                    }
                }
                catch (System.ServiceModel.FaultException fex)
                {
                    System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", fex.Message), System.Diagnostics.EventLogEntryType.Error);
                }
                catch (InvalidOperationException oex)
                {
                    System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", oex.Message), System.Diagnostics.EventLogEntryType.Error);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", ex.Message), System.Diagnostics.EventLogEntryType.Error);
                }
            }

            File.Delete(tempfile);
        }

        private void GetDocument(string id)
        {
            string tempFile = Path.GetTempFileName();

            byte[] openXmlDocument = null;

            try
            {
                using (officeService)
                {
                    IOffice proxyICompany = officeService.CreateChannel();

                    openXmlDocument = proxyICompany.GetDocument(officeApplicationContainer);

                    officeService.Close();
                }
            }
            catch (System.ServiceModel.FaultException fex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", fex.Message), System.Diagnostics.EventLogEntryType.Error);
            }
            catch (InvalidOperationException oex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", oex.Message), System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", ex.Message), System.Diagnostics.EventLogEntryType.Error);
            }

            File.WriteAllBytes(tempFile, openXmlDocument);

            officeApplicationContainer.ActivateDocument(tempFile, false);

            File.Delete(tempFile);
        }

        private void TransformDocument(string id)
        {
            byte[] contents = null;
            string tempFile = officeApplicationContainer.CreateTempFile();

            using (var stream = File.Open(tempFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                try
                {
                    using (officeService)
                    {
                        IOffice proxyICompany = officeService.CreateChannel();

                        contents = proxyICompany.TransformDocument(officeApplicationContainer, File.ReadAllBytes(tempFile));

                        officeService.Close();
                    }
                }
                catch (System.ServiceModel.FaultException fex)
                {
                    System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", fex.Message), System.Diagnostics.EventLogEntryType.Error);
                }
                catch (InvalidOperationException oex)
                {
                    System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", oex.Message), System.Diagnostics.EventLogEntryType.Error);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.EventLog.WriteEntry("HannoverLeasing.MessageBroker.Office.Client", string.Format("ServiceVersion Error: {0}", ex.Message), System.Diagnostics.EventLogEntryType.Error);
                }
            }

            File.Delete(tempFile);

            File.WriteAllBytes(tempFile, contents);

            officeApplicationContainer.ActivateDocument(tempFile, true);

            File.Delete(tempFile);
        }

        #endregion

        #region Helpers

        public void SetApplication(object officeApplication)
        {
            officeApplicationContainer.OfficeApplication = officeApplication;
        }

        private void AddEventHandlers(IEnumerable items)
        {
            foreach (UIElement item in items)
            {
                if (item.GetType() == typeof(Button))
                {
                    ((Button)item).Click += new RoutedEventHandler(OnButtonAction);
                }
                if (item.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)item).DropDownClosed += OnComboBoxAction;
                }

                else if (item.GetType() == typeof(Grid))
                    AddEventHandlers(LogicalTreeHelper.GetChildren(item));
            }
        }

        #endregion
    }
}
