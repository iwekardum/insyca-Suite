using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace inSyca.foundation.framework.application.windowsforms
{

    /// <summary>
    /// Framework for running application as a tray app.
    /// </summary>
    /// <remarks>
    /// Tray app code adapted from "Creating Applications with NotifyIcon in Windows Forms", Jessica Fosler,
    /// http://windowsclient.net/articles/notifyiconapplications.aspx
    /// </remarks>
    public class FrameworkApplicationContext : ApplicationContext
    {
        //private static readonly string IconFileName = "route.ico";
        private static readonly string DefaultTooltip = "inSyca Foundation Framework";
        protected FrameworkManager frameworkManager;

        /// <summary>
        /// This class should be created and passed into Application.Run( ... )
        /// </summary>
        public FrameworkApplicationContext() 
        {
            InitializeContext();

            frameworkManager = new FrameworkManager(notifyIcon);
            //frameWorkConfigurator.BuildServerAssociations();
            if (!frameworkManager.IsDecorated) { ShowIntroForm(); }
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
//            hostManager.BuildServerAssociations();
            frameworkManager.BuildContextMenu(notifyIcon.ContextMenuStrip);
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            notifyIcon.ContextMenuStrip.Items.Add(frameworkManager.ToolStripMenuItemWithHandler("Show &Details", showDetailsItem_Click));
            notifyIcon.ContextMenuStrip.Items.Add(frameworkManager.ToolStripMenuItemWithHandler("&Help/About", showHelpItem_Click));
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            notifyIcon.ContextMenuStrip.Items.Add(frameworkManager.ToolStripMenuItemWithHandler("&Exit", exitItem_Click));
        }

        # region the child forms

        virtual protected void ShowIntroForm()
        {
        }

        virtual protected void ShowDetailsForm()
        {
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e) { ShowIntroForm();    }

        // From http://stackoverflow.com/questions/2208690/invoke-notifyicons-context-menu
        private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(notifyIcon, null);
            }
        }

        protected configurator introForm;

        // attach to context menu items
        private void showHelpItem_Click(object sender, EventArgs e)     { ShowIntroForm();    }
        private void showDetailsItem_Click(object sender, EventArgs e)  { ShowDetailsForm();  }

        // null out the forms so we know to create a new one.
        protected void mainForm_Closed(object sender, EventArgs e)        { introForm = null; }

        # endregion the child forms

        # region generic code framework

        private System.ComponentModel.IContainer components;	// a list of components to dispose when the context is disposed
        protected NotifyIcon notifyIcon;				            // the icon that sits in the system tray

        private void InitializeContext()
        {
            components = new System.ComponentModel.Container();
            notifyIcon = new NotifyIcon(components)
                             {
                                 ContextMenuStrip = new ContextMenuStrip(),
                                 Icon = Properties.Resources.icon,
                                 Text = DefaultTooltip,
                                 Visible = true
                             };
            notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            notifyIcon.MouseUp += notifyIcon_MouseUp;
        }

        /// <summary>
        /// When the application context is disposed, dispose things like the notify icon.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && components != null) { components.Dispose(); }
        }

        /// <summary>
        /// When the exit menu item is clicked, make a call to terminate the ApplicationContext.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitItem_Click(object sender, EventArgs e) 
        {
            ExitThread();
        }

        /// <summary>
        /// If we are presently showing a form, clean it up.
        /// </summary>
        protected override void ExitThreadCore()
        {
            // before we exit, let forms clean themselves up.
            if (introForm != null) { introForm.Close(); }

            notifyIcon.Visible = false; // should remove lingering tray icon
            base.ExitThreadCore();
        }

        # endregion generic code framework

    }
}
