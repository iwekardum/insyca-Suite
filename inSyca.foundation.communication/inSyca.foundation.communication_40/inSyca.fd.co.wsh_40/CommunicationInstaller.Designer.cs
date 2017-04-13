namespace inSyca.foundation.communication.wsh
{
    partial class CommunicationInstaller
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
            this.CommunicationServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.CommunicationServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // CommunicationProcessInstaller
            // 
            this.CommunicationServiceProcessInstaller.Password = null;
            this.CommunicationServiceProcessInstaller.Username = null;
            // 
            // CommunicationInstaller
            // 
            this.CommunicationServiceInstaller.Description = "Monitoring of WMI events";
            this.CommunicationServiceInstaller.DisplayName = "inSyca Foundation Communication Service";
            this.CommunicationServiceInstaller.ServiceName = Program.serviceName;
            this.CommunicationServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.CommunicationServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.MessageBrokerServiceInstaller_AfterInstall);
            this.CommunicationServiceInstaller.BeforeInstall += new System.Configuration.Install.InstallEventHandler(this.MessageBrokerServiceInstaller_BeforeInstall);
            this.CommunicationServiceInstaller.BeforeUninstall += new System.Configuration.Install.InstallEventHandler(this.MessageBrokerServiceInstaller_BeforeUninstall);
            // 
            // CommunicationInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.CommunicationServiceProcessInstaller,
            this.CommunicationServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller CommunicationServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller CommunicationServiceInstaller;
    }
}