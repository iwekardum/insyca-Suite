namespace inSyca.foundation.framework.configurator
{
    partial class framework_configurator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(framework_configurator));
            this.tab_control = new System.Windows.Forms.TabControl();
            this.tab_framework = new System.Windows.Forms.TabPage();
            this.uc_framework = new inSyca.foundation.framework.application.windowsforms.uc_framework();
            this.tab_framework_monitoring = new System.Windows.Forms.TabPage();
            this.sb_monitoring = new System.Windows.Forms.SplitContainer();
            this.uc_monitor = new inSyca.foundation.framework.application.windowsforms.uc_monitor();
            ((System.ComponentModel.ISupportInitialize)(this.sb_main)).BeginInit();
            this.sb_main.Panel1.SuspendLayout();
            this.sb_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).BeginInit();
            this.tab_control.SuspendLayout();
            this.tab_framework.SuspendLayout();
            this.tab_framework_monitoring.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sb_monitoring)).BeginInit();
            this.sb_monitoring.Panel2.SuspendLayout();
            this.sb_monitoring.SuspendLayout();
            this.SuspendLayout();
            // 
            // sb_main
            // 
            // 
            // sb_main.Panel1
            // 
            this.sb_main.Panel1.Controls.Add(this.tab_control);
            // 
            // tab_control
            // 
            resources.ApplyResources(this.tab_control, "tab_control");
            this.tab_control.Controls.Add(this.tab_framework);
            this.tab_control.Controls.Add(this.tab_framework_monitoring);
            this.tab_control.Name = "tab_control";
            this.tab_control.SelectedIndex = 0;
            // 
            // tab_framework
            // 
            this.tab_framework.Controls.Add(this.uc_framework);
            resources.ApplyResources(this.tab_framework, "tab_framework");
            this.tab_framework.Name = "tab_framework";
            this.tab_framework.UseVisualStyleBackColor = true;
            // 
            // uc_framework
            // 
            resources.ApplyResources(this.uc_framework, "uc_framework");
            this.uc_framework.Name = "uc_framework";
            this.uc_framework.NavigationChanged += new System.EventHandler<inSyca.foundation.framework.application.windowsforms.SelectedTabChangedEventArgs>(this.uc_framework_NavigationChanged);
            // 
            // tab_framework_monitoring
            // 
            this.tab_framework_monitoring.Controls.Add(this.sb_monitoring);
            resources.ApplyResources(this.tab_framework_monitoring, "tab_framework_monitoring");
            this.tab_framework_monitoring.Name = "tab_framework_monitoring";
            this.tab_framework_monitoring.UseVisualStyleBackColor = true;
            // 
            // sb_monitoring
            // 
            resources.ApplyResources(this.sb_monitoring, "sb_monitoring");
            this.sb_monitoring.Name = "sb_monitoring";
            this.sb_monitoring.Panel1Collapsed = true;
            // 
            // sb_monitoring.Panel2
            // 
            this.sb_monitoring.Panel2.Controls.Add(this.uc_monitor);
            // 
            // uc_monitoring
            // 
            resources.ApplyResources(this.uc_monitor, "uc_monitoring");
            this.uc_monitor.Name = "uc_monitoring";
            this.uc_monitor.UserControlEventFired += new System.EventHandler<inSyca.foundation.framework.diagnostics.UserControlEventFiredArgs>(this.uc_monitoring_UserControlEventFired);
            // 
            // framework_configurator
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "framework_configurator";
            this.sb_main.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sb_main)).EndInit();
            this.sb_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).EndInit();
            this.tab_control.ResumeLayout(false);
            this.tab_framework.ResumeLayout(false);
            this.tab_framework_monitoring.ResumeLayout(false);
            this.sb_monitoring.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sb_monitoring)).EndInit();
            this.sb_monitoring.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab_control;
        private System.Windows.Forms.TabPage tab_framework;
        protected framework.application.windowsforms.uc_framework uc_framework;
        private System.Windows.Forms.TabPage tab_framework_monitoring;
        private System.Windows.Forms.SplitContainer sb_monitoring;
        private framework.application.windowsforms.uc_monitor uc_monitor;



    }
}