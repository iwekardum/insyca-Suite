namespace inSyca.messagebroker.ns
{
    partial class MessageBrokerInstaller
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
            this.messageBrokerServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.MessageBrokerServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // messageBrokerServiceProcessInstaller
            // 
            this.messageBrokerServiceProcessInstaller.Password = null;
            this.messageBrokerServiceProcessInstaller.Username = null;
            // 
            // MessageBrokerServiceInstaller
            // 
            this.MessageBrokerServiceInstaller.Description = "inSyca.messagebroker.root.ns";
            this.MessageBrokerServiceInstaller.DisplayName = "inSyca.messagebroker.root.ns";
            this.MessageBrokerServiceInstaller.ServiceName = Program.serviceName;
            this.MessageBrokerServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.MessageBrokerServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.MessageBrokerServiceInstaller_AfterInstall);
            this.MessageBrokerServiceInstaller.BeforeInstall += new System.Configuration.Install.InstallEventHandler(this.MessageBrokerServiceInstaller_BeforeInstall);
            this.MessageBrokerServiceInstaller.BeforeUninstall += new System.Configuration.Install.InstallEventHandler(this.MessageBrokerServiceInstaller_BeforeUninstall);
            // 
            // MessageBrokerInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.messageBrokerServiceProcessInstaller,
            this.MessageBrokerServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller messageBrokerServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller MessageBrokerServiceInstaller;
    }
}