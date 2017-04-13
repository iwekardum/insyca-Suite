namespace inSyca.foundation.framework.wsh
{
    partial class FrameworkInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.frameworkServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.frameworkServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // frameworkServiceProcessInstaller
            // 
            this.frameworkServiceProcessInstaller.Password = null;
            this.frameworkServiceProcessInstaller.Username = null;
            // 
            // frameworkServiceInstaller
            // 
            this.frameworkServiceInstaller.Description = "Monitoring of WMI events";
            this.frameworkServiceInstaller.DisplayName = "inSyca Foundation Framework Service";
            this.frameworkServiceInstaller.ServiceName = "Foundation Framework";
            this.frameworkServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.frameworkServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.MessageBrokerServiceInstaller_AfterInstall);
            this.frameworkServiceInstaller.BeforeInstall += new System.Configuration.Install.InstallEventHandler(this.MessageBrokerServiceInstaller_BeforeInstall);
            this.frameworkServiceInstaller.BeforeUninstall += new System.Configuration.Install.InstallEventHandler(this.MessageBrokerServiceInstaller_BeforeUninstall);
            // 
            // FrameworkInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.frameworkServiceProcessInstaller,
            this.frameworkServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller frameworkServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller frameworkServiceInstaller;
    }
}