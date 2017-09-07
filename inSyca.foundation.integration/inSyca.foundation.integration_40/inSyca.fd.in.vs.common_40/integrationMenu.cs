//------------------------------------------------------------------------------
// <copyright file="integrationMenu.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using EnvDTE80;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;
using System.Runtime.InteropServices;
using inSyca.foundation.integration.visualstudio.external;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace inSyca.foundation.integration.visualstudio.template
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class integrationMenu
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int BTDFCommandId = 0x0100;
        public const int GACCommandId = 0x0101;
        public const int ODXCommandId = 0x0102;
        public const int fixODXCommandId = 0x0103;
        public const int ExplorerCommandId = 0x0104;
        public const int BindingCommandId = 0x0105;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid toolsBTDFMenuCommandSet = new Guid("21cb661d-d1d7-4710-9ae8-1caf27c86f63");
        public static readonly Guid projectContextMenuCommandSet = new Guid("D08CBFD1-246A-477E-80CA-D7500F49953A");
        public static readonly Guid itemContextMenuCommandSet = new Guid("6DC8CB70-8B6F-4C36-B751-420717624321");
        public static readonly Guid referenceContextMenuCommandSet = new Guid("494E9F57-14A7-43B9-8921-0E6B39B94969");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;
        private const string orchestrationExtension = ".odx";
        private const string bizTalkProjectExtension = ".btproj";
        private const string bindingFileName = "PortBindingsMaster.xml";

        public btdf BizTalkDeploymentFrameworkHelper { get; set; }
        public gac GACHelper { get; set; }

        public FileInfo selectedFile { get; set; }

        private DTE2 _dte2
        {
            get
            {
                return (DTE2)this.ServiceProvider.GetService(typeof(DTE));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="integrationMenu"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private integrationMenu(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var btdfMenuCommandID = new CommandID(toolsBTDFMenuCommandSet, BTDFCommandId);
                var btdfMenuItem = new OleMenuCommand(this.ToolsMenuItemCallback, btdfMenuCommandID);
                commandService.AddCommand(btdfMenuItem);

                var odxContextMenuCommandID = new CommandID(itemContextMenuCommandSet, ODXCommandId);
                var odxContextMenuItem = new OleMenuCommand(this.ContextMenuItemCallback, odxContextMenuCommandID);
                odxContextMenuItem.BeforeQueryStatus += ItemContextMenuItem_BeforeQueryStatus;
                commandService.AddCommand(odxContextMenuItem);

                var bindingContextMenuCommandID = new CommandID(itemContextMenuCommandSet, BindingCommandId);
                var bindingContextMenuItem = new OleMenuCommand(this.ContextMenuItemCallback, bindingContextMenuCommandID);
                bindingContextMenuItem.BeforeQueryStatus += ItemContextMenuItem_BeforeQueryStatus;
                commandService.AddCommand(bindingContextMenuItem);

                var explorerContextMenuCommandID = new CommandID(itemContextMenuCommandSet, ExplorerCommandId);
                var explorerContextMenuItem = new OleMenuCommand(this.ContextMenuItemCallback,explorerContextMenuCommandID);
                commandService.AddCommand(explorerContextMenuItem);

                var fixContextMenuCommandID = new CommandID(projectContextMenuCommandSet, fixODXCommandId);
                var fixContextMenuItem = new OleMenuCommand(this.ContextMenuItemCallback, fixContextMenuCommandID);
                fixContextMenuItem.BeforeQueryStatus += ItemContextMenuItem_BeforeQueryStatus;
                commandService.AddCommand(fixContextMenuItem);

                var gacContextMenuCommandID = new CommandID(projectContextMenuCommandSet, GACCommandId);
                var gacContextMenuItem = new OleMenuCommand(this.ContextMenuItemCallback, gacContextMenuCommandID);
                commandService.AddCommand(gacContextMenuItem);

                var referenceContextMenuCommandID = new CommandID(referenceContextMenuCommandSet, GACCommandId);
                var referenceContextMenuItem = new OleMenuCommand(this.ContextMenuItemCallback, referenceContextMenuCommandID);
                commandService.AddCommand(referenceContextMenuItem);
            }

            BizTalkDeploymentFrameworkHelper = new btdf();
            GACHelper = new gac();

            GACHelper.LoadAssemblies();
        }

        private void ItemContextMenuItem_BeforeQueryStatus(object sender, EventArgs e)
        {
            OleMenuCommand menuCommand = sender as OleMenuCommand;

            setSelectedItem();

            if (menuCommand != null && menuCommand.CommandID.ID == ODXCommandId)
                if(selectedFile.Extension == orchestrationExtension)
                    menuCommand.Visible = true;
                else
                    menuCommand.Visible = false;

            if (menuCommand != null && menuCommand.CommandID.ID == BindingCommandId)
                if (selectedFile.Name == bindingFileName)
                    menuCommand.Visible = true;
                else
                    menuCommand.Visible = false;

            if (menuCommand != null && menuCommand.CommandID.ID == fixODXCommandId)
                if(selectedFile.Extension == bizTalkProjectExtension)
                    menuCommand.Visible = true;
                else
                    menuCommand.Visible = false;
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static integrationMenu Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new integrationMenu(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void ToolsMenuItemCallback(object sender, EventArgs e)
        {
            DTE2 _dte2 = (DTE2)this.ServiceProvider.GetService(typeof(DTE));
            //            _dte2.ToolWindows.OutputWindow
            BizTalkDeploymentFrameworkHelper.solutionFileName = _dte2.Solution.FileName;
            BizTalkDeploymentFrameworkHelper.configurationName = _dte2.Solution.SolutionBuild.ActiveConfiguration.Name;
            BizTalkDeploymentFrameworkHelper.eventActivateOutputWindow += BizTalkDeploymentFrameworkHelper_eventActivateOutputWindow;
            BizTalkDeploymentFrameworkHelper.eventWriteToOutputWindow += BizTalkDeploymentFrameworkHelper_eventWriteToOutputWindow;
            BizTalkDeploymentFrameworkHelper.Execute();
        }

        private void BizTalkDeploymentFrameworkHelper_eventActivateOutputWindow(object sender, OutputWindowEventArgs e)
        {
            OutputWindow ow = _dte2.ToolWindows.OutputWindow;
            OutputWindowPane owP = GetOutputWindowPane(e.Name);

            owP.Clear();
            owP.Activate();
            ow.Parent.Activate();
        }

        private void BizTalkDeploymentFrameworkHelper_eventWriteToOutputWindow(object sender, OutputWindowEventArgs e)
        {
            OutputWindowPane owP = GetOutputWindowPane(e.Name);
            owP.OutputString(e.Text);
            owP.OutputString("\r\n");
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void ContextMenuItemCallback(object sender, EventArgs e)
        {
            OleMenuCommand menuCommand = sender as OleMenuCommand;

            setSelectedItem();

            if (menuCommand != null && menuCommand.CommandID.ID == GACCommandId)
            {
                string selectedAssembly;
                GACHelper.ShowDialog(out selectedAssembly);
                AddReference(selectedAssembly);
            }
            else if (menuCommand != null && menuCommand.CommandID.ID == BindingCommandId && selectedFile.Name == bindingFileName)
            {
                ProcessBinding(selectedFile);
            }
            else if (menuCommand != null && menuCommand.CommandID.ID == ODXCommandId && selectedFile.Extension == orchestrationExtension)
            {
                File.WriteAllText(selectedFile.FullName, File.ReadAllText(selectedFile.FullName).Replace("\"Internal\"", "\"Public\"").Replace("internal ", "public "));
            }
            else if (menuCommand != null && menuCommand.CommandID.ID == fixODXCommandId && selectedFile.Extension == bizTalkProjectExtension)
            {
                File.WriteAllText(selectedFile.FullName, File.ReadAllText(selectedFile.FullName).Replace("<SubType>Designer</SubType>", string.Empty));
            }
            else if (menuCommand != null && menuCommand.CommandID.ID == ExplorerCommandId)
            {
                System.Diagnostics.Process.Start(selectedFile.DirectoryName);
            }

        }

        private OutputWindowPane GetOutputWindowPane(string name)
        {
            OutputWindow ow = _dte2.ToolWindows.OutputWindow;

            foreach (OutputWindowPane owp in ow.OutputWindowPanes)
            {
                if (string.Compare(owp.Name, name, true) == 0)
                {
                    return owp;
                }
            }

            return ow.OutputWindowPanes.Add(name);
        }

        private Project GetSelectedProject()
        {
            object selectedObject = null;
            IntPtr pHierarchyPointer, pSelectionContainer;
            IVsMultiItemSelect multiItemSelect;
            uint projectItemId;

            IVsMonitorSelection monitorSelection = (IVsMonitorSelection)Package.GetGlobalService(typeof(SVsShellMonitorSelection));

            int hr = monitorSelection.GetCurrentSelection(out pHierarchyPointer, out projectItemId, out multiItemSelect, out pSelectionContainer);

            if (hr == 0)
            {
                if (multiItemSelect == null)
                {
                    IVsHierarchy selectedHierarchy = Marshal.GetTypedObjectForIUnknown(pHierarchyPointer, typeof(IVsHierarchy)) as IVsHierarchy;

                    ErrorHandler.ThrowOnFailure(selectedHierarchy.GetProperty((uint)VSConstants.VSITEMID_ROOT, (int)__VSHPROPID.VSHPROPID_ExtObject, out selectedObject));
                }
            }
            return selectedObject as Project;
        }

        private void setSelectedItem()
        {
            string fileName = string.Empty;
            IntPtr pHierarchyPointer, pSelectionContainer;
            IVsMultiItemSelect multiItemSelect;
            uint projectItemId;

            IVsMonitorSelection monitorSelection = (IVsMonitorSelection)Package.GetGlobalService(typeof(SVsShellMonitorSelection));

            int hr = monitorSelection.GetCurrentSelection(out pHierarchyPointer, out projectItemId, out multiItemSelect, out pSelectionContainer);

            if (hr == 0)
            {
                if (multiItemSelect == null)
                {
                    IVsProject vsProj = (IVsProject)Marshal.GetObjectForIUnknown(pHierarchyPointer);

                    hr = vsProj.GetMkDocument(projectItemId, out fileName);
                }
            }

            if (string.IsNullOrEmpty(fileName))
                return;

            selectedFile = new FileInfo(fileName);
        }

        private void AddReference(String referenceStrIdentity)
        {
            if (string.IsNullOrEmpty(referenceStrIdentity))
                return;

            Project project = GetSelectedProject();

            if (project != null && project.Object is VSLangProj.VSProject)
            {
                VSLangProj.VSProject vsproject = (VSLangProj.VSProject)project.Object;

                vsproject.References.Add(referenceStrIdentity);
            }
        }

        private void ProcessBinding(FileInfo selectedFile)
        {
            XDocument xBinding = XDocument.Load(selectedFile.FullName);

            Dictionary<string, XElement> list = new Dictionary<string, XElement>();

            var schemas = xBinding.Descendants("Schema");

            foreach (var schema in schemas)
            {
                schema.Attribute("AlwaysTrackAllProperties").Value = "${bt_schema_AlwaysTrackAllProperties}";
            }

            var services = xBinding.Descendants("Service");

            foreach (var service in services)
            {
                service.Attribute("State").Value = "${bt_orchestration_State}";
                service.Attribute("TrackingOption").Value = "${bt_orchestration_TrackingOption}";
            }

            var hosts = xBinding.Descendants("Host");

            foreach (var host in hosts)
            {
                XAttribute ntGroupName = host.Attribute("NTGroupName");

                if (ntGroupName != null)
                    ntGroupName.Remove();
            }

            ProcessSendPorts(xBinding, list);

            ProcessReceivePorts(xBinding, list);

            xBinding.Save(selectedFile.FullName);

            File.WriteAllLines(Path.Combine(selectedFile.DirectoryName, "settings.csv"), list.Select(s => String.Format(@"{0};=""{1}""", s.Key, s.Value.Value)));
        }

        private static void ProcessReceivePorts(XDocument xBinding, Dictionary<string, XElement> list)
        {
            var receivePorts = xBinding.Descendants("ReceivePort").OrderBy(e => e.Descendants("ReceiveLocation").FirstOrDefault().Attribute("Name").Value);

            foreach (var receivePort in receivePorts)
            {
                receivePort.SetElementValue("Tracking", "${bt_rp_Tracking}");

                var receiveLocations = receivePort.Descendants("ReceiveLocation");

                foreach (var receiveLocation in receiveLocations)
                {
                    XElement receiveLocationTransportType = receiveLocation.Descendants("ReceiveLocationTransportType").FirstOrDefault();
                    XElement address = receiveLocation.Element("Address");

                    XElement receiveLocationTransportTypeData = receiveLocation.Descendants("ReceiveLocationTransportTypeData").FirstOrDefault();
                    XElement customProps = XElement.Parse(receiveLocationTransportTypeData.Value);

                    string receiveLocationTransportTypeName = receiveLocationTransportType.Attribute("Name").Value;
                    string receiveLocationName = receiveLocation.Attribute("Name").Value;

                    list.Add(string.Format("--- {0} ---", receiveLocationName.ToUpper()), new XElement("new_sendport"));
                    list.Add(string.Format("{0}_address", receiveLocationName), new XElement(address));

                    switch (receiveLocationTransportTypeName)
                    {
                        case "FILE":
                            {
                                ReceiveLocation_File(list, address, receiveLocationTransportTypeData, customProps, receiveLocationName);
                            }
                            break;
                        case "FTP":
                            {
                                ReceiveLocation_FTP(list, address, receiveLocationTransportTypeData, customProps, receiveLocationName);
                            }
                            break;

                        case "Windows SharePoint Services":
                            {
                                ReceiveLocation_Sharepoint(list, address, receiveLocationTransportTypeData, customProps, receiveLocationName);
                            }
                            break;

                        case "WCF-Custom":
                        case "WCF-CustomIsolated":
                            {
                                ReceiveLocation_WCF_Custom(receiveLocation, receiveLocationTransportTypeData, customProps, receiveLocationName);
                            }
                            break;
                        case "SB-Messaging":
                            {
                                ReceiveLocation_SB(list, address, receiveLocationTransportTypeData, customProps, receiveLocationName);
                            }
                            break;
                        default:
                            {
                                address.Value = string.Format("${{{0}_address}}", receiveLocationName);
                            }
                            break;
                    }
                }

            }
        }

        private static void ProcessSendPorts(XDocument xBinding, Dictionary<string, XElement> list)
        {
            var sendPorts = xBinding.Descendants("SendPort").OrderBy(e => e.Attribute("Name").Value); ;

            foreach (var sendPort in sendPorts)
            {
                sendPort.SetElementValue("Tracking", "${bt_sp_Tracking}");

                XElement Filter = sendPort.Descendants("Filter").FirstOrDefault();

                if (Filter != null && !string.IsNullOrEmpty(Filter.Value))
                    Filter.Value = Filter.Value.Replace("\n", string.Empty).Trim();

                XElement primaryTransport = sendPort.Descendants("PrimaryTransport").FirstOrDefault();
                XElement address = primaryTransport.Element("Address");

                XElement transportType = primaryTransport.Descendants("TransportType").FirstOrDefault();
                XElement transportTypeData = primaryTransport.Descendants("TransportTypeData").FirstOrDefault();

                XElement customProps = XElement.Parse(transportTypeData.Value);

                string transportTypeName = transportType.Attribute("Name").Value;
                string sendPortName = sendPort.Attribute("Name").Value;

                list.Add(string.Format("--- {0} ---", sendPortName.ToUpper()), new XElement("new_sendport"));
                list.Add(string.Format("{0}_address", sendPortName), new XElement(address));

                switch (transportTypeName)
                {
                    case "FILE":
                        {
                            SendPort_File(list, address, transportTypeData, customProps, sendPortName);
                        }
                        break;
                    case "FTP":
                        {
                            SendPort_FTP(list, address, transportTypeData, customProps, sendPortName);
                        }
                        break;
                    case "Windows SharePoint Services":
                        {
                            SendPort_Sharepoint(list, address, transportTypeData, customProps, sendPortName);
                        }
                        break;
                    case "WCF-Custom":
                    case "WCF-CustomIsolated":
                        {
                            SendPort_WCF_Custom(address, transportTypeData, customProps, sendPortName);
                        }
                        break;
                    case "SB-Messaging":
                        {
                            SendPort_SB(list, address, transportTypeData, customProps, sendPortName);
                        }
                        break;
                    default:
                        address.Value = string.Format("${{{0}_address}}", sendPortName);
                        break;
                }
            }
        }

        private static void SendPort_SB(Dictionary<string, XElement> list, XElement address, XElement transportTypeData, XElement customProps, string sendPortName)
        {
            address.Value = string.Format("${{{0}_address}}", sendPortName);

            customProps.SetElementValue("SessionIdleTimeout", "${sb_sessionIdleTimeout}");
            customProps.SetElementValue("BatchFlushInterval", "${sb_batchFlushInterval}");
            customProps.SetElementValue("OpenTimeout", "${sb_openTimeout}");
            customProps.SetElementValue("CloseTimeout", "${sb_closeTimeout}");
            customProps.SetElementValue("SendTimeout", "${sb_sendTimeout}");
            customProps.SetElementValue("MaxReceivedMessageSize", "${sb_maxReceivedMessageSize}");

            XElement SharedAccessKeyName = customProps.Element("SharedAccessKeyName");
            XElement SharedAccessKey = customProps.Element("SharedAccessKey");

            list.Add(string.Format("{0}_sharedAccessKeyName", sendPortName), new XElement(SharedAccessKeyName));
            list.Add(string.Format("{0}_sharedAccessKey", sendPortName), new XElement(SharedAccessKey));

            SharedAccessKeyName.Value = string.Format("${{{0}_sharedAccessKeyName}}", sendPortName);
            SharedAccessKey.Value = string.Format("${{{0}_sharedAccessKey}}", sendPortName);

            transportTypeData.Value = customProps.ToString();
        }

        private static void ReceiveLocation_SB(Dictionary<string, XElement> list, XElement address, XElement receiveLocationTransportTypeData, XElement customProps, string receiveLocationName)
        {
            address.Value = string.Format("${{{0}_address}}", receiveLocationName);

            customProps.SetElementValue("SessionIdleTimeout", "${sb_sessionIdleTimeout}");
            customProps.SetElementValue("BatchFlushInterval", "${sb_batchFlushInterval}");
            customProps.SetElementValue("OpenTimeout", "${sb_openTimeout}");
            customProps.SetElementValue("CloseTimeout", "${sb_closeTimeout}");
            customProps.SetElementValue("SendTimeout", "${sb_sendTimeout}");
            customProps.SetElementValue("MaxReceivedMessageSize", "${sb_maxReceivedMessageSize}");

            XElement SharedAccessKeyName = customProps.Element("SharedAccessKeyName");
            XElement SharedAccessKey = customProps.Element("SharedAccessKey");

            list.Add(string.Format("{0}_sharedAccessKeyName", receiveLocationName), new XElement(SharedAccessKeyName));
            list.Add(string.Format("{0}_sharedAccessKey", receiveLocationName), new XElement(SharedAccessKey));

            SharedAccessKeyName.Value = string.Format("${{{0}_sharedAccessKeyName}}", receiveLocationName);
            SharedAccessKey.Value = string.Format("${{{0}_sharedAccessKey}}", receiveLocationName);

            receiveLocationTransportTypeData.Value = customProps.ToString();
        }

        private static void ReceiveLocation_WCF_Custom(XElement receiveLocation, XElement receiveLocationTransportTypeData, XElement customProps, string receiveLocationName)
        {
            receiveLocation.SetElementValue("Address", string.Format("${{{0}_address}}", receiveLocationName));

            XElement bindingConfiguration = customProps.Descendants("BindingConfiguration").FirstOrDefault();
            XElement binding = XElement.Parse(bindingConfiguration.Value);

            binding.SetAttributeValue("maxReceivedMessageSize", "${wcf_maxReceivedMessageSize}");
            binding.SetAttributeValue("maxBufferPoolSize", "${wcf_maxBufferPoolSize}");
            binding.SetAttributeValue("maxBufferSize", "${wcf_maxBufferSize}");
            binding.SetAttributeValue("closeTimeout", "${wcf_closeTimeout}");
            binding.SetAttributeValue("openTimeout", "${wcf_openTimeout}");
            binding.SetAttributeValue("receiveTimeout", "${wcf_receiveTimeout}");
            binding.SetAttributeValue("sendTimeout", "${wcf_sendTimeout}");

            bindingConfiguration.Value = binding.ToString();
            receiveLocationTransportTypeData.Value = customProps.ToString();
        }

        private static void ReceiveLocation_Sharepoint(Dictionary<string, XElement> list, XElement address, XElement receiveLocationTransportTypeData, XElement customProps, string receiveLocationName)
        {
            address.Value = string.Format("${{{0}_uri}}", receiveLocationName);

            XElement adapterConfig = customProps.Descendants("AdapterConfig").FirstOrDefault();
            XElement config = XElement.Parse(adapterConfig.Value);
            XElement SiteUrl = config.Element("SiteUrl");
            XElement WssLocation = config.Element("WssLocation");
            XElement ViewName = config.Element("ViewName");
            XElement uri = config.Element("uri");

            list.Add(string.Format("{0}_siteurl", receiveLocationName), new XElement(SiteUrl));
            list.Add(string.Format("{0}_wssLocation", receiveLocationName), new XElement(WssLocation));
            list.Add(string.Format("{0}_viewname", receiveLocationName), new XElement(ViewName));
            list.Add(string.Format("{0}_uri", receiveLocationName), new XElement(uri));

            SiteUrl.Value = string.Format("${{{0}_siteurl}}", receiveLocationName);
            WssLocation.Value = string.Format("${{{0}_wssLocation}}", receiveLocationName);
            ViewName.Value = string.Format("${{{0}_viewname}}", receiveLocationName);
            uri.Value = string.Format("${{{0}_uri}}", receiveLocationName);

            adapterConfig.Value = config.ToString();
            receiveLocationTransportTypeData.Value = customProps.ToString();
        }

        private static void ReceiveLocation_FTP(Dictionary<string, XElement> list, XElement address, XElement receiveLocationTransportTypeData, XElement customProps, string receiveLocationName)
        {
            address.Value = string.Format("ftp://${{{0}_serveraddress}}:${{{0}_serverport}}/${{{0}_targetfolder}}/${{{0}_filemask}}", receiveLocationName);

            XElement adapterConfig = customProps.Descendants("AdapterConfig").FirstOrDefault();
            XElement config = XElement.Parse(adapterConfig.Value);
            XElement serverAddress = config.Element("serverAddress");
            XElement serverPort = config.Element("serverPort");
            XElement fileMask = config.Element("fileMask");
            XElement targetFolder = config.Element("targetFolder");

            list[string.Format("{0}_address", receiveLocationName)].Value = list[string.Format("{0}_address", receiveLocationName)].Value.Replace(fileMask.Value, "");

            list.Add(string.Format("{0}_serveraddress", receiveLocationName), new XElement(serverAddress));
            list.Add(string.Format("{0}_serverport", receiveLocationName), new XElement(serverPort));
            list.Add(string.Format("{0}_filemask", receiveLocationName), new XElement(fileMask));
            list.Add(string.Format("{0}_targetfolder", receiveLocationName), new XElement(targetFolder));

            config.SetElementValue("uri", string.Format("ftp://${{{0}_serveraddress}}:${{{0}_serverPort}}/${{{0}_targetFolder}}/${{{0}_filemask}}", receiveLocationName));
            config.SetElementValue("serverAddress", string.Format("${{{0}_serveraddress}}", receiveLocationName));
            config.SetElementValue("serverPort", string.Format("${{{0}_serverport}}", receiveLocationName));
            config.SetElementValue("fileMask", string.Format("${{{0}_filemask}}", receiveLocationName));
            config.SetElementValue("targetFolder", string.Format("${{{0}_targetfolder}}", receiveLocationName));

            adapterConfig.Value = config.ToString();
            receiveLocationTransportTypeData.Value = customProps.ToString();
        }


        private static void SendPort_WCF_Custom(XElement address, XElement transportTypeData, XElement customProps, string sendPortName)
        {
            address.Value = string.Format("${{{0}_address}}", sendPortName);

            XElement bindingConfiguration = customProps.Descendants("BindingConfiguration").FirstOrDefault();
            XElement binding = XElement.Parse(bindingConfiguration.Value);

            binding.SetAttributeValue("maxReceivedMessageSize", "${wcf_maxReceivedMessageSize}");
            binding.SetAttributeValue("maxBufferPoolSize", "${wcf_maxBufferPoolSize}");
            binding.SetAttributeValue("maxBufferSize", "${wcf_maxBufferSize}");
            binding.SetAttributeValue("closeTimeout", "${wcf_closeTimeout}");
            binding.SetAttributeValue("openTimeout", "${wcf_openTimeout}");
            binding.SetAttributeValue("receiveTimeout", "${wcf_receiveTimeout}");
            binding.SetAttributeValue("sendTimeout", "${wcf_sendTimeout}");

            bindingConfiguration.Value = binding.ToString();
            transportTypeData.Value = customProps.ToString();
        }

        private static void SendPort_Sharepoint(Dictionary<string, XElement> list, XElement address, XElement transportTypeData, XElement customProps, string sendPortName)
        {
            address.Value = string.Format("${{{0}_uri}}", sendPortName);

            XElement adapterConfig = customProps.Descendants("AdapterConfig").FirstOrDefault();
            XElement config = XElement.Parse(adapterConfig.Value);
            XElement SiteUrl = config.Element("SiteUrl");
            XElement WssLocation = config.Element("WssLocation");
            XElement FileName = config.Element("FileName");
            XElement uri = config.Element("uri");

            list.Add(string.Format("{0}_siteurl", sendPortName), new XElement(SiteUrl));
            list.Add(string.Format("{0}_wssLocation", sendPortName), new XElement(WssLocation));
            list.Add(string.Format("{0}_filename", sendPortName), new XElement(FileName));
            list.Add(string.Format("{0}_uri", sendPortName), new XElement(uri));

            SiteUrl.Value = string.Format("${{{0}_siteurl}}", sendPortName);
            WssLocation.Value = string.Format("${{{0}_wssLocation}}", sendPortName);
            FileName.Value = string.Format("${{{0}_filename}}", sendPortName);
            uri.Value = string.Format("${{{0}_uri}}", sendPortName);

            adapterConfig.Value = config.ToString();
            transportTypeData.Value = customProps.ToString();
        }

        private static void SendPort_FTP(Dictionary<string, XElement> list, XElement address, XElement transportTypeData, XElement customProps, string sendPortName)
        {
            address.Value = string.Format("ftp://${{{0}_serveraddress}}:${{{0}_serverport}}/${{{0}_targetfolder}}/${{{0}_targetFileName}}", sendPortName);

            XElement adapterConfig = customProps.Descendants("AdapterConfig").FirstOrDefault();
            XElement config = XElement.Parse(adapterConfig.Value);
            XElement uri = config.Element("uri");
            XElement serverAddress = config.Element("serverAddress");
            XElement serverPort = config.Element("serverPort");
            XElement targetFileName = config.Element("targetFileName");
            XElement targetFolder = config.Element("targetFolder");

            list.Add(string.Format("{0}_serveraddress", sendPortName), new XElement(serverAddress));
            list.Add(string.Format("{0}_serverport", sendPortName), new XElement(serverPort));
            list.Add(string.Format("{0}_targetfilename", sendPortName), new XElement(targetFileName));
            list.Add(string.Format("{0}_targetfolder", sendPortName), new XElement(targetFolder));

            list[string.Format("{0}_address", sendPortName)].Value = list[string.Format("{0}_address", sendPortName)].Value.Replace(targetFileName.Value, "");

            uri.Value = string.Format("ftp://${{{0}_serveraddress}}:${{{0}_serverPort}}/${{{0}_targetFolder}}/${{{0}_targetfilename}}", sendPortName);
            serverAddress.Value = string.Format("${{{0}_serveraddress}}", sendPortName);
            serverPort.Value = string.Format("${{{0}_serverport}}", sendPortName);
            targetFileName.Value = string.Format("${{{0}_targetfilename}}", sendPortName);
            targetFolder.Value = string.Format("${{{0}_targetfolder}}", sendPortName);

            adapterConfig.Value = config.ToString();
            transportTypeData.Value = customProps.ToString();
        }

        private static void ReceiveLocation_File(Dictionary<string, XElement> list, XElement address, XElement receiveLocationTransportTypeData, XElement customProps, string receiveLocationName)
        {
            address.Value = string.Format("${{{0}_address}}${{{0}_filemask}}", receiveLocationName);

            XElement FileMask = customProps.Element("FileMask");

            list[string.Format("{0}_address", receiveLocationName)].Value = list[string.Format("{0}_address", receiveLocationName)].Value.Replace(FileMask.Value, "");

            list.Add(string.Format("{0}_filemask", receiveLocationName), new XElement(FileMask));

            FileMask.Value = string.Format("${{{0}_filemask}}", receiveLocationName);

            receiveLocationTransportTypeData.Value = customProps.ToString();
        }

        private static void SendPort_File(Dictionary<string, XElement> list, XElement address, XElement transportTypeData, XElement customProps, string sendPortName)
        {
            address.Value = string.Format("${{{0}_address}}${{{0}_fileName}}", sendPortName);

            XElement fileName = customProps.Element("FileName");

            list[string.Format("{0}_address", sendPortName)].Value = list[string.Format("{0}_address", sendPortName)].Value.Replace(fileName.Value, "");

            list.Add(string.Format("{0}_fileName", sendPortName), new XElement(fileName));

            fileName.Value = string.Format("${{{0}_fileName}}", sendPortName);

            transportTypeData.Value = customProps.ToString();
        }
    }
}
