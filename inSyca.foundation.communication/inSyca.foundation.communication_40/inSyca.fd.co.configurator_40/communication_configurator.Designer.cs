namespace inSyca.foundation.communication.configurator
{
    partial class communication_configurator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(communication_configurator));
            this.tab_main = new System.Windows.Forms.TabControl();
            this.tab_communication_client = new System.Windows.Forms.TabPage();
            this.uc_communication_clients = new inSyca.foundation.framework.application.windowsforms.uc_framework();
            this.tab_communication_components = new System.Windows.Forms.TabPage();
            this.uc_communication_components = new inSyca.foundation.framework.application.windowsforms.uc_framework();
            this.tab_communication_service = new System.Windows.Forms.TabPage();
            this.uc_communication_service = new inSyca.foundation.framework.application.windowsforms.uc_framework();
            this.tab_communication_wcf = new System.Windows.Forms.TabPage();
            this.uc_communication_wcf = new inSyca.foundation.framework.application.windowsforms.uc_framework();
            this.tab_main.SuspendLayout();
            this.tab_communication_client.SuspendLayout();
            this.tab_communication_components.SuspendLayout();
            this.tab_communication_service.SuspendLayout();
            this.tab_communication_wcf.SuspendLayout();
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
            // 
            // tabMain
            // 
            resources.ApplyResources(this.tab_main, "tabMain");
            this.tab_main.Controls.Add(this.tab_communication_client);
            this.tab_main.Controls.Add(this.tab_communication_components);
            this.tab_main.Controls.Add(this.tab_communication_service);
            this.tab_main.Controls.Add(this.tab_communication_wcf);
            this.tab_main.Name = "tab_control";
            this.tab_main.SelectedIndex = 0;
            // 
            // tab_communication_client
            // 
            this.tab_communication_client.Controls.Add(this.uc_communication_clients);
            resources.ApplyResources(this.tab_communication_client, "tab_communication_client");
            this.tab_communication_client.Name = "tab_communication_client";
            this.tab_communication_client.UseVisualStyleBackColor = true;
            // 
            // uc_communication_clients
            // 
            resources.ApplyResources(this.uc_communication_clients, "uc_communication_clients");
            this.uc_communication_clients.Name = "uc_communication_clients";
            this.uc_communication_clients.NavigationChanged += new System.EventHandler<inSyca.foundation.framework.application.windowsforms.SelectedTabChangedEventArgs>(this.uc_communication_client_NavigationChanged);
            // 
            // tab_communication_components
            // 
            this.tab_communication_components.Controls.Add(this.uc_communication_components);
            resources.ApplyResources(this.tab_communication_components, "tab_communication_components");
            this.tab_communication_components.Name = "tab_communication_components";
            this.tab_communication_components.UseVisualStyleBackColor = true;
            // 
            // uc_communication_components
            // 
            resources.ApplyResources(this.uc_communication_components, "uc_communication_components");
            this.uc_communication_components.Name = "uc_communication_components";
            this.uc_communication_components.NavigationChanged += new System.EventHandler<inSyca.foundation.framework.application.windowsforms.SelectedTabChangedEventArgs>(this.uc_communication_components_NavigationChanged);
            // 
            // tab_communication_service
            // 
            this.tab_communication_service.Controls.Add(this.uc_communication_service);
            resources.ApplyResources(this.tab_communication_service, "tab_communication_service");
            this.tab_communication_service.Name = "tab_communication_service";
            this.tab_communication_service.UseVisualStyleBackColor = true;
            // 
            // uc_communication_service
            // 
            resources.ApplyResources(this.uc_communication_service, "uc_communication_service");
            this.uc_communication_service.Name = "uc_communication_service";
            this.uc_communication_service.NavigationChanged += new System.EventHandler<inSyca.foundation.framework.application.windowsforms.SelectedTabChangedEventArgs>(this.uc_communication_service_NavigationChanged);
            // 
            // tab_communication_wcf
            // 
            this.tab_communication_wcf.Controls.Add(this.uc_communication_wcf);
            resources.ApplyResources(this.tab_communication_wcf, "tab_communication_wcf");
            this.tab_communication_wcf.Name = "tab_communication_wcf";
            this.tab_communication_wcf.UseVisualStyleBackColor = true;
            // 
            // uc_communication_wcf
            // 
            resources.ApplyResources(this.uc_communication_wcf, "uc_communication_wcf");
            this.uc_communication_wcf.Name = "uc_communication_wcf";
            this.uc_communication_wcf.NavigationChanged += new System.EventHandler<inSyca.foundation.framework.application.windowsforms.SelectedTabChangedEventArgs>(this.uc_communication_wcf_NavigationChanged);
            // 
            // communication_configurator
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "communication_configurator";
            this.tab_main.ResumeLayout(false);
            this.tab_communication_client.ResumeLayout(false);
            this.tab_communication_components.ResumeLayout(false);
            this.tab_communication_service.ResumeLayout(false);
            this.tab_communication_wcf.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab_main;
        private System.Windows.Forms.TabPage tab_communication_client;
        private framework.application.windowsforms.uc_framework uc_communication_clients;
        private System.Windows.Forms.TabPage tab_communication_components;
        private framework.application.windowsforms.uc_framework uc_communication_components;
        private System.Windows.Forms.TabPage tab_communication_service;
        private framework.application.windowsforms.uc_framework uc_communication_service;
        private System.Windows.Forms.TabPage tab_communication_wcf;
        private framework.application.windowsforms.uc_framework uc_communication_wcf;

    }
}