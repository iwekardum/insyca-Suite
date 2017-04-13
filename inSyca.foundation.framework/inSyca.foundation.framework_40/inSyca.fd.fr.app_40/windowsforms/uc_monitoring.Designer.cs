namespace inSyca.foundation.framework.application.windowsforms
{
    partial class uc_monitoring
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        new protected System.ComponentModel.IContainer components = null;

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
        new protected void InitializeComponent()
        {
            this.sc_monitoring = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_installService = new System.Windows.Forms.Button();
            this.btn_uninstallService = new System.Windows.Forms.Button();
            this.btn_startService = new System.Windows.Forms.Button();
            this.btn_stopService = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sc_monitoring)).BeginInit();
            this.sc_monitoring.Panel1.SuspendLayout();
            this.sc_monitoring.Panel2.SuspendLayout();
            this.sc_monitoring.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertySetting
            // 
            this.propertySetting.Size = new System.Drawing.Size(586, 420);
            // 
            // sc_monitoring
            // 
            this.sc_monitoring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_monitoring.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.sc_monitoring.IsSplitterFixed = true;
            this.sc_monitoring.Location = new System.Drawing.Point(0, 0);
            this.sc_monitoring.Name = "sc_monitoring";
            this.sc_monitoring.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sc_monitoring.Panel1
            // 
            this.sc_monitoring.Panel1.Controls.Add(this.propertySetting);
            // 
            // sc_monitoring.Panel2
            // 
            this.sc_monitoring.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.sc_monitoring.Size = new System.Drawing.Size(586, 456);
            this.sc_monitoring.SplitterDistance = 420;
            this.sc_monitoring.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btn_installService, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_uninstallService, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_startService, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_stopService, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(586, 32);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_installService
            // 
            this.btn_installService.BackColor = System.Drawing.Color.White;
            this.btn_installService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_installService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_installService.Location = new System.Drawing.Point(3, 3);
            this.btn_installService.Name = "btn_installService";
            this.btn_installService.Size = new System.Drawing.Size(140, 26);
            this.btn_installService.TabIndex = 0;
            this.btn_installService.Text = "Install Service";
            this.btn_installService.UseVisualStyleBackColor = false;
            this.btn_installService.Click += new System.EventHandler(this.btn_installService_Click);
            // 
            // btn_uninstallService
            // 
            this.btn_uninstallService.BackColor = System.Drawing.Color.White;
            this.btn_uninstallService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_uninstallService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_uninstallService.Location = new System.Drawing.Point(149, 3);
            this.btn_uninstallService.Name = "btn_uninstallService";
            this.btn_uninstallService.Size = new System.Drawing.Size(140, 26);
            this.btn_uninstallService.TabIndex = 3;
            this.btn_uninstallService.Text = "Uninstall Service";
            this.btn_uninstallService.UseVisualStyleBackColor = false;
            this.btn_uninstallService.Click += new System.EventHandler(this.btn_uninstallService_Click);
            // 
            // btn_startService
            // 
            this.btn_startService.BackColor = System.Drawing.Color.White;
            this.btn_startService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_startService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_startService.Location = new System.Drawing.Point(295, 3);
            this.btn_startService.Name = "btn_startService";
            this.btn_startService.Size = new System.Drawing.Size(140, 26);
            this.btn_startService.TabIndex = 2;
            this.btn_startService.Text = "Start Service";
            this.btn_startService.UseVisualStyleBackColor = false;
            this.btn_startService.Click += new System.EventHandler(this.btn_startService_Click);
            // 
            // btn_stopService
            // 
            this.btn_stopService.BackColor = System.Drawing.Color.White;
            this.btn_stopService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_stopService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_stopService.Location = new System.Drawing.Point(441, 3);
            this.btn_stopService.Name = "btn_stopService";
            this.btn_stopService.Size = new System.Drawing.Size(142, 26);
            this.btn_stopService.TabIndex = 1;
            this.btn_stopService.Text = "Stop Service";
            this.btn_stopService.UseVisualStyleBackColor = false;
            this.btn_stopService.Click += new System.EventHandler(this.btn_stopService_Click);
            // 
            // uc_monitoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sc_monitoring);
            this.Name = "uc_monitoring";
            this.Size = new System.Drawing.Size(586, 456);
            this.sc_monitoring.Panel1.ResumeLayout(false);
            this.sc_monitoring.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sc_monitoring)).EndInit();
            this.sc_monitoring.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer sc_monitoring;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        protected System.Windows.Forms.Button btn_installService;
        protected System.Windows.Forms.Button btn_uninstallService;
        protected System.Windows.Forms.Button btn_startService;
        protected System.Windows.Forms.Button btn_stopService;
    }
}
