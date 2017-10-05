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
            bindings.ProcessBinding(selectedFile);
        }
    }
}
