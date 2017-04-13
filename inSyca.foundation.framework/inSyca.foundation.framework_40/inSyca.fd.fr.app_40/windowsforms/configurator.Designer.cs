using System.Windows.Forms;

namespace inSyca.foundation.framework.application.windowsforms
{
    partial class configurator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(configurator));
            this.sb_main = new System.Windows.Forms.SplitContainer();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel_header = new System.Windows.Forms.Panel();
            this.pb_logo = new System.Windows.Forms.PictureBox();
            this.rtb_heading = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sb_main)).BeginInit();
            this.sb_main.Panel2.SuspendLayout();
            this.sb_main.SuspendLayout();
            this.panel_header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // splitBody
            // 
            resources.ApplyResources(this.sb_main, "splitBody");
            this.sb_main.Name = "splitBody";
            // 
            // splitBody.Panel1
            // 
            resources.ApplyResources(this.sb_main.Panel1, "splitBody.Panel1");
            // 
            // splitBody.Panel2
            // 
            resources.ApplyResources(this.sb_main.Panel2, "splitBody.Panel2");
            this.sb_main.Panel2.Controls.Add(this.btnSave);
            this.sb_main.Panel2.Controls.Add(this.btnClose);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel_header
            // 
            resources.ApplyResources(this.panel_header, "panel_header");
            this.panel_header.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.panel_header.BackColor = System.Drawing.Color.White;
            this.panel_header.Controls.Add(this.pb_logo);
            this.panel_header.Controls.Add(this.rtb_heading);
            this.panel_header.Name = "panel_header";
            // 
            // pb_logo
            // 
            resources.ApplyResources(this.pb_logo, "pb_logo");
            this.pb_logo.ErrorImage = global::inSyca.foundation.framework.application.Properties.Resources.logo;
            this.pb_logo.Image = global::inSyca.foundation.framework.application.Properties.Resources.logo;
            this.pb_logo.InitialImage = global::inSyca.foundation.framework.application.Properties.Resources.logo;
            this.pb_logo.Name = "pb_logo";
            this.pb_logo.TabStop = false;
            // 
            // rtb_heading
            // 
            resources.ApplyResources(this.rtb_heading, "rtb_heading");
            this.rtb_heading.BackColor = System.Drawing.Color.White;
            this.rtb_heading.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_heading.Name = "rtb_heading";
            this.rtb_heading.ReadOnly = true;
            // 
            // configurator
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.sb_main);
            this.Controls.Add(this.panel_header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "configurator";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.sb_main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sb_main)).EndInit();
            this.sb_main.ResumeLayout(false);
            this.panel_header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel_header;
        private RichTextBox rtb_heading;
        protected SplitContainer sb_main;
        private Button btnClose;
        private Button btnSave;
        protected PictureBox pb_logo;

    }
}