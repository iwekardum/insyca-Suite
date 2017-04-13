namespace inSyca.foundation.integration.wsh
{
	partial class IntegrationInstaller
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
            this.integrationServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.integrationServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // integrationServiceProcessInstaller
            // 
            this.integrationServiceProcessInstaller.Password = null;
            this.integrationServiceProcessInstaller.Username = null;
            // 
            // integrationServiceInstaller
            // 
            this.integrationServiceInstaller.Description = "Monitoring for BizTalk events";
            this.integrationServiceInstaller.DisplayName = "inSyca Foundation Integration Service";
            this.integrationServiceInstaller.ServiceName = "Foundation Integration";
            this.integrationServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.integrationServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.MessageBrokerServiceInstaller_AfterInstall);
            this.integrationServiceInstaller.BeforeInstall += new System.Configuration.Install.InstallEventHandler(this.MessageBrokerServiceInstaller_BeforeInstall);
            this.integrationServiceInstaller.BeforeUninstall += new System.Configuration.Install.InstallEventHandler(this.MessageBrokerServiceInstaller_BeforeUninstall);
            // 
            // IntegrationInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.integrationServiceProcessInstaller,
            this.integrationServiceInstaller});

        }

		#endregion

        private System.ServiceProcess.ServiceProcessInstaller integrationServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller integrationServiceInstaller;
	}
}