 namespace inSyca.foundation.framework.application.windowsforms
{
    partial class uc_info
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        protected System.ComponentModel.IContainer components = null;

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
        protected void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_info));
            this.propertyInformation = new System.Windows.Forms.PropertyGrid();
            this.pb_logo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // propertyInformation
            // 
            resources.ApplyResources(this.propertyInformation, "propertyInformation");
            this.propertyInformation.Name = "propertyInformation";
            this.propertyInformation.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyInformation.ToolbarVisible = false;
            // 
            // pb_logo
            // 
            this.pb_logo.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pb_logo, "pb_logo");
            this.pb_logo.ErrorImage = global::inSyca.foundation.framework.application.Properties.Resources.company;
            this.pb_logo.Image = global::inSyca.foundation.framework.application.Properties.Resources.company;
            this.pb_logo.InitialImage = global::inSyca.foundation.framework.application.Properties.Resources.logo;
            this.pb_logo.Name = "pb_logo";
            this.pb_logo.TabStop = false;
            // 
            // uc_info
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pb_logo);
            this.Controls.Add(this.propertyInformation);
            this.Name = "uc_info";
            this.Load += new System.EventHandler(this.uc_info_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyInformation;
        public System.Windows.Forms.PictureBox pb_logo;
    }
}
