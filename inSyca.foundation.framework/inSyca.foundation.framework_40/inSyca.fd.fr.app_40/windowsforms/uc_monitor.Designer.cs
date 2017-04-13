namespace inSyca.foundation.framework.application.windowsforms
{
    partial class uc_monitor
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
            this.sc_monitor = new System.Windows.Forms.SplitContainer();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.rtb_monitoring = new System.Windows.Forms.RichTextBox();
            this.btn_tasklist = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sc_monitor)).BeginInit();
            this.sc_monitor.Panel1.SuspendLayout();
            this.sc_monitor.Panel2.SuspendLayout();
            this.sc_monitor.SuspendLayout();
            this.SuspendLayout();
            // 
            // sc_monitor
            // 
            this.sc_monitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_monitor.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sc_monitor.Location = new System.Drawing.Point(0, 0);
            this.sc_monitor.Name = "sc_monitor";
            // 
            // sc_monitor.Panel1
            // 
            this.sc_monitor.Panel1.Controls.Add(this.btn_tasklist);
            this.sc_monitor.Panel1.Controls.Add(this.btn_Clear);
            this.sc_monitor.Panel1.Controls.Add(this.btn_Stop);
            this.sc_monitor.Panel1.Controls.Add(this.btn_Start);
            // 
            // sc_monitor.Panel2
            // 
            this.sc_monitor.Panel2.Controls.Add(this.rtb_monitoring);
            this.sc_monitor.Size = new System.Drawing.Size(481, 384);
            this.sc_monitor.SplitterDistance = 80;
            this.sc_monitor.TabIndex = 1;
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(3, 61);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 23);
            this.btn_Clear.TabIndex = 2;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(3, 32);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(75, 23);
            this.btn_Stop.TabIndex = 1;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(3, 3);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // rtb_monitoring
            // 
            this.rtb_monitoring.BackColor = System.Drawing.Color.Black;
            this.rtb_monitoring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_monitoring.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_monitoring.ForeColor = System.Drawing.Color.White;
            this.rtb_monitoring.HideSelection = false;
            this.rtb_monitoring.Location = new System.Drawing.Point(0, 0);
            this.rtb_monitoring.Name = "rtb_monitoring";
            this.rtb_monitoring.ReadOnly = true;
            this.rtb_monitoring.Size = new System.Drawing.Size(397, 384);
            this.rtb_monitoring.TabIndex = 0;
            this.rtb_monitoring.Text = "";
            // 
            // btn_tasklist
            // 
            this.btn_tasklist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_tasklist.Location = new System.Drawing.Point(3, 358);
            this.btn_tasklist.Name = "btn_tasklist";
            this.btn_tasklist.Size = new System.Drawing.Size(75, 23);
            this.btn_tasklist.TabIndex = 3;
            this.btn_tasklist.Text = "Tasklist";
            this.btn_tasklist.UseVisualStyleBackColor = true;
            this.btn_tasklist.Click += new System.EventHandler(this.btn_tasklist_Click);
            // 
            // uc_monitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sc_monitor);
            this.Name = "uc_monitor";
            this.Size = new System.Drawing.Size(481, 384);
            this.sc_monitor.Panel1.ResumeLayout(false);
            this.sc_monitor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sc_monitor)).EndInit();
            this.sc_monitor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer sc_monitor;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.RichTextBox rtb_monitoring;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btn_tasklist;

    }
}
