namespace inSyca.foundation.framework.application.windowsforms
{
    partial class uc_framework
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_framework));
            this.tabStrip_navigation = new inSyca.foundation.framework.application.windowsforms.TabStrip();
            this.tab_logging = new inSyca.foundation.framework.application.windowsforms.TabStripButton();
            this.tab_smtp = new inSyca.foundation.framework.application.windowsforms.TabStripButton();
            this.tab_test = new inSyca.foundation.framework.application.windowsforms.TabStripButton();
            this.tab_information = new inSyca.foundation.framework.application.windowsforms.TabStripButton();
            this.tab_monitoring = new inSyca.foundation.framework.application.windowsforms.TabStripButton();
            this.panel_propertygrid = new System.Windows.Forms.Panel();
            this.tabStrip_navigation.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabStrip_navigation
            // 
            this.tabStrip_navigation.AutoSize = false;
            this.tabStrip_navigation.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabStrip_navigation.FlipButtons = false;
            this.tabStrip_navigation.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tabStrip_navigation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tab_logging,
            this.tab_monitoring,
            this.tab_smtp,
            this.tab_test,
            this.tab_information});
            this.tabStrip_navigation.Location = new System.Drawing.Point(0, 0);
            this.tabStrip_navigation.Name = "tabStrip_navigation";
            this.tabStrip_navigation.RenderStyle = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.tabStrip_navigation.SelectedTab = this.tab_monitoring;
            this.tabStrip_navigation.Size = new System.Drawing.Size(200, 455);
            this.tabStrip_navigation.TabIndex = 25;
            this.tabStrip_navigation.Text = "tabStrip_navigation";
            this.tabStrip_navigation.UseVisualStyles = false;
            this.tabStrip_navigation.SelectedTabChanged += new System.EventHandler<inSyca.foundation.framework.application.windowsforms.SelectedTabChangedEventArgs>(this.tabStrip_Navigation_SelectedTabChanged);
            // 
            // tab_logging
            // 
            this.tab_logging.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tab_logging.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tab_logging.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tab_logging.IsSelected = false;
            this.tab_logging.Margin = new System.Windows.Forms.Padding(0);
            this.tab_logging.Name = "tab_logging";
            this.tab_logging.Padding = new System.Windows.Forms.Padding(0);
            this.tab_logging.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tab_logging.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tab_logging.Size = new System.Drawing.Size(199, 29);
            this.tab_logging.Text = global::inSyca.foundation.framework.application.Properties.Resources.tab_logging_text;
            this.tab_logging.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tab_smtp
            // 
            this.tab_smtp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tab_smtp.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tab_smtp.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tab_smtp.IsSelected = false;
            this.tab_smtp.Margin = new System.Windows.Forms.Padding(0);
            this.tab_smtp.Name = "tab_smtp";
            this.tab_smtp.Padding = new System.Windows.Forms.Padding(0);
            this.tab_smtp.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tab_smtp.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tab_smtp.Size = new System.Drawing.Size(199, 29);
            this.tab_smtp.Text = global::inSyca.foundation.framework.application.Properties.Resources.tab_smtp_text;
            this.tab_smtp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tab_test
            // 
            this.tab_test.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tab_test.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tab_test.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tab_test.IsSelected = false;
            this.tab_test.Margin = new System.Windows.Forms.Padding(0);
            this.tab_test.Name = "tab_test";
            this.tab_test.Padding = new System.Windows.Forms.Padding(0);
            this.tab_test.SelectedFont = new System.Drawing.Font("Segoe UI", 9F);
            this.tab_test.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tab_test.Size = new System.Drawing.Size(199, 29);
            this.tab_test.Text = global::inSyca.foundation.framework.application.Properties.Resources.tab_test_text;
            this.tab_test.ToolTipText = "Test";
            // 
            // tab_information
            // 
            this.tab_information.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tab_information.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tab_information.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tab_information.IsSelected = false;
            this.tab_information.Margin = new System.Windows.Forms.Padding(0);
            this.tab_information.Name = "tab_information";
            this.tab_information.Padding = new System.Windows.Forms.Padding(0);
            this.tab_information.SelectedFont = new System.Drawing.Font("Segoe UI", 9F);
            this.tab_information.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tab_information.Size = new System.Drawing.Size(199, 29);
            this.tab_information.Text = global::inSyca.foundation.framework.application.Properties.Resources.tab_information_text;
            // 
            // tab_monitoring
            // 
            this.tab_monitoring.Checked = true;
            this.tab_monitoring.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tab_monitoring.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tab_monitoring.Image = ((System.Drawing.Image)(resources.GetObject("tab_monitoring.Image")));
            this.tab_monitoring.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tab_monitoring.IsSelected = true;
            this.tab_monitoring.Margin = new System.Windows.Forms.Padding(0);
            this.tab_monitoring.Name = "tab_monitoring";
            this.tab_monitoring.Padding = new System.Windows.Forms.Padding(0);
            this.tab_monitoring.SelectedFont = new System.Drawing.Font("Segoe UI", 9F);
            this.tab_monitoring.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tab_monitoring.Size = new System.Drawing.Size(199, 29);
            this.tab_monitoring.Text = global::inSyca.foundation.framework.application.Properties.Resources.tab_monitoring_text;
            // 
            // panel_propertygrid
            // 
            this.panel_propertygrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_propertygrid.BackColor = System.Drawing.Color.White;
            this.panel_propertygrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_propertygrid.Location = new System.Drawing.Point(203, 1);
            this.panel_propertygrid.Name = "panel_propertygrid";
            this.panel_propertygrid.Size = new System.Drawing.Size(337, 452);
            this.panel_propertygrid.TabIndex = 26;
            // 
            // uc_framework
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_propertygrid);
            this.Controls.Add(this.tabStrip_navigation);
            this.Name = "uc_framework";
            this.Size = new System.Drawing.Size(543, 455);
            this.Load += new System.EventHandler(this.uc_main_Load);
            this.tabStrip_navigation.ResumeLayout(false);
            this.tabStrip_navigation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabStrip tabStrip_navigation;
        private System.Windows.Forms.Panel panel_propertygrid;
        public TabStripButton tab_logging;
        public TabStripButton tab_smtp;
        public TabStripButton tab_information;
        public TabStripButton tab_test;
        public TabStripButton tab_monitoring;
    }
}
