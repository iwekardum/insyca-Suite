namespace inSyca.foundation.integration.configurator
{
    partial class integration_configurator
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
            this.tab_main = new System.Windows.Forms.TabControl();
            this.tab_integration_biztalk = new System.Windows.Forms.TabPage();
            this.uc_integration_biztalk = new inSyca.foundation.framework.application.windowsforms.uc_framework();
            this.tab_integration_biztalk_functions = new System.Windows.Forms.TabPage();
            this.uc_integration_biztalk_functions = new inSyca.foundation.framework.application.windowsforms.uc_framework();
            this.tab_integration_biztalk_components = new System.Windows.Forms.TabPage();
            this.uc_integration_biztalk_components = new inSyca.foundation.framework.application.windowsforms.uc_framework();
            this.tab_integration_biztalk_adapter = new System.Windows.Forms.TabPage();
            this.uc_integration_biztalk_adapter = new inSyca.foundation.framework.application.windowsforms.uc_framework();
            this.tab_biztalk_monitoring = new System.Windows.Forms.TabPage();
            this.uc_monitor = new inSyca.foundation.framework.application.windowsforms.uc_monitor();
            ((System.ComponentModel.ISupportInitialize)(this.sb_main)).BeginInit();
            this.sb_main.Panel1.SuspendLayout();
            this.sb_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).BeginInit();
            this.tab_main.SuspendLayout();
            this.tab_integration_biztalk.SuspendLayout();
            this.tab_integration_biztalk_functions.SuspendLayout();
            this.tab_integration_biztalk_components.SuspendLayout();
            this.tab_integration_biztalk_adapter.SuspendLayout();
            this.tab_biztalk_monitoring.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitBody
            // 
            // 
            // splitBody.Panel1
            // 
            this.sb_main.Panel1.Controls.Add(this.tab_main);
            // 
            // tab_control
            // 
            this.tab_main.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tab_main.Controls.Add(this.tab_integration_biztalk);
            this.tab_main.Controls.Add(this.tab_integration_biztalk_functions);
            this.tab_main.Controls.Add(this.tab_integration_biztalk_components);
            this.tab_main.Controls.Add(this.tab_integration_biztalk_adapter);
            this.tab_main.Controls.Add(this.tab_biztalk_monitoring);
            this.tab_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_main.Location = new System.Drawing.Point(5, 5);
            this.tab_main.Name = "tab_control";
            this.tab_main.SelectedIndex = 0;
            this.tab_main.Size = new System.Drawing.Size(784, 560);
            this.tab_main.TabIndex = 0;
            // 
            // tab_integration_biztalk
            // 
            this.tab_integration_biztalk.Controls.Add(this.uc_integration_biztalk);
            this.tab_integration_biztalk.Location = new System.Drawing.Point(4, 25);
            this.tab_integration_biztalk.Name = "tab_integration_biztalk";
            this.tab_integration_biztalk.Padding = new System.Windows.Forms.Padding(3);
            this.tab_integration_biztalk.Size = new System.Drawing.Size(776, 531);
            this.tab_integration_biztalk.TabIndex = 2;
            this.tab_integration_biztalk.Text = "Integration.BizTalk";
            this.tab_integration_biztalk.UseVisualStyleBackColor = true;
            // 
            // uc_integration_biztalk
            // 
            this.uc_integration_biztalk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_integration_biztalk.Location = new System.Drawing.Point(3, 3);
            this.uc_integration_biztalk.Name = "uc_integration_biztalk";
            this.uc_integration_biztalk.Size = new System.Drawing.Size(770, 525);
            this.uc_integration_biztalk.TabIndex = 2;
            this.uc_integration_biztalk.NavigationChanged += new System.EventHandler<inSyca.foundation.framework.application.windowsforms.SelectedTabChangedEventArgs>(this.uc_integration_biztalk_NavigationChanged);
            // 
            // tab_integration_biztalk_functions
            // 
            this.tab_integration_biztalk_functions.Controls.Add(this.uc_integration_biztalk_functions);
            this.tab_integration_biztalk_functions.Location = new System.Drawing.Point(4, 25);
            this.tab_integration_biztalk_functions.Name = "tab_integration_biztalk_functions";
            this.tab_integration_biztalk_functions.Padding = new System.Windows.Forms.Padding(3);
            this.tab_integration_biztalk_functions.Size = new System.Drawing.Size(776, 531);
            this.tab_integration_biztalk_functions.TabIndex = 0;
            this.tab_integration_biztalk_functions.Text = "Integration.BizTalk.Functions";
            this.tab_integration_biztalk_functions.UseVisualStyleBackColor = true;
            // 
            // uc_integration_biztalk_functions
            // 
            this.uc_integration_biztalk_functions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_integration_biztalk_functions.Location = new System.Drawing.Point(3, 3);
            this.uc_integration_biztalk_functions.Name = "uc_integration_biztalk_functions";
            this.uc_integration_biztalk_functions.Size = new System.Drawing.Size(770, 525);
            this.uc_integration_biztalk_functions.TabIndex = 0;
            this.uc_integration_biztalk_functions.NavigationChanged += new System.EventHandler<inSyca.foundation.framework.application.windowsforms.SelectedTabChangedEventArgs>(this.uc_integration_biztalk_functions_NavigationChanged);
            // 
            // tab_integration_biztalk_components
            // 
            this.tab_integration_biztalk_components.Controls.Add(this.uc_integration_biztalk_components);
            this.tab_integration_biztalk_components.Location = new System.Drawing.Point(4, 25);
            this.tab_integration_biztalk_components.Name = "tab_integration_biztalk_components";
            this.tab_integration_biztalk_components.Padding = new System.Windows.Forms.Padding(3);
            this.tab_integration_biztalk_components.Size = new System.Drawing.Size(776, 531);
            this.tab_integration_biztalk_components.TabIndex = 1;
            this.tab_integration_biztalk_components.Text = "Integration.BizTalk.Components";
            this.tab_integration_biztalk_components.UseVisualStyleBackColor = true;
            // 
            // uc_integration_biztalk_components
            // 
            this.uc_integration_biztalk_components.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_integration_biztalk_components.Location = new System.Drawing.Point(3, 3);
            this.uc_integration_biztalk_components.Name = "uc_integration_biztalk_components";
            this.uc_integration_biztalk_components.Size = new System.Drawing.Size(770, 525);
            this.uc_integration_biztalk_components.TabIndex = 0;
            this.uc_integration_biztalk_components.NavigationChanged += new System.EventHandler<inSyca.foundation.framework.application.windowsforms.SelectedTabChangedEventArgs>(this.uc_integration_biztalk_components_NavigationChanged);
            // 
            // tab_integration_biztalk_adapter
            // 
            this.tab_integration_biztalk_adapter.Controls.Add(this.uc_integration_biztalk_adapter);
            this.tab_integration_biztalk_adapter.Location = new System.Drawing.Point(4, 25);
            this.tab_integration_biztalk_adapter.Name = "tab_integration_biztalk_adapter";
            this.tab_integration_biztalk_adapter.Padding = new System.Windows.Forms.Padding(3);
            this.tab_integration_biztalk_adapter.Size = new System.Drawing.Size(776, 531);
            this.tab_integration_biztalk_adapter.TabIndex = 1;
            this.tab_integration_biztalk_adapter.Text = "Integration.BizTalk.Adapter";
            this.tab_integration_biztalk_adapter.UseVisualStyleBackColor = true;
            // 
            // uc_integration_biztalk_adapter
            // 
            this.uc_integration_biztalk_adapter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_integration_biztalk_adapter.Location = new System.Drawing.Point(3, 3);
            this.uc_integration_biztalk_adapter.Name = "uc_integration_biztalk_adapter";
            this.uc_integration_biztalk_adapter.Size = new System.Drawing.Size(770, 525);
            this.uc_integration_biztalk_adapter.TabIndex = 0;
            this.uc_integration_biztalk_adapter.NavigationChanged += new System.EventHandler<inSyca.foundation.framework.application.windowsforms.SelectedTabChangedEventArgs>(this.uc_integration_biztalk_adapter_NavigationChanged);
            // 
            // tab_biztalk_monitoring
            // 
            this.tab_biztalk_monitoring.Controls.Add(this.uc_monitor);
            this.tab_biztalk_monitoring.Location = new System.Drawing.Point(4, 25);
            this.tab_biztalk_monitoring.Name = "tab_biztalk_monitoring";
            this.tab_biztalk_monitoring.Padding = new System.Windows.Forms.Padding(3);
            this.tab_biztalk_monitoring.Size = new System.Drawing.Size(776, 531);
            this.tab_biztalk_monitoring.TabIndex = 3;
            this.tab_biztalk_monitoring.Text = "Integration.BizTalk.Monitor";
            this.tab_biztalk_monitoring.UseVisualStyleBackColor = true;
            // 
            // uc_monitoring
            // 
            this.uc_monitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_monitor.Location = new System.Drawing.Point(3, 3);
            this.uc_monitor.Name = "uc_monitoring";
            this.uc_monitor.Size = new System.Drawing.Size(770, 525);
            this.uc_monitor.TabIndex = 0;
            this.uc_monitor.UserControlEventFired += new System.EventHandler<inSyca.foundation.framework.diagnostics.UserControlEventFiredArgs>(this.uc_monitoring_UserControlEventFired);
            // 
            // integration_configurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 676);
            this.Name = "integration_configurator";
            this.Text = "inSyca Foundation Integration";
            this.sb_main.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sb_main)).EndInit();
            this.sb_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).EndInit();
            this.tab_main.ResumeLayout(false);
            this.tab_integration_biztalk.ResumeLayout(false);
            this.tab_integration_biztalk_functions.ResumeLayout(false);
            this.tab_integration_biztalk_components.ResumeLayout(false);
            this.tab_integration_biztalk_adapter.ResumeLayout(false);
            this.tab_biztalk_monitoring.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab_main;
        private System.Windows.Forms.TabPage tab_integration_biztalk_functions;
        private System.Windows.Forms.TabPage tab_integration_biztalk_components;
        private System.Windows.Forms.TabPage tab_integration_biztalk_adapter;
        private framework.application.windowsforms.uc_framework uc_integration_biztalk_functions;
        private framework.application.windowsforms.uc_framework uc_integration_biztalk_components;
        private framework.application.windowsforms.uc_framework uc_integration_biztalk_adapter;
        private System.Windows.Forms.TabPage tab_integration_biztalk;
        private framework.application.windowsforms.uc_framework uc_integration_biztalk;
        private System.Windows.Forms.TabPage tab_biztalk_monitoring;
        private framework.application.windowsforms.uc_monitor uc_monitor;
    }
}