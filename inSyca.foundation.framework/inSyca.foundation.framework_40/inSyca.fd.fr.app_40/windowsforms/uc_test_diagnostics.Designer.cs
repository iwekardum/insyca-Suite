namespace inSyca.foundation.framework.application.windowsforms
{
    partial class uc_test_diagnostics
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
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.gb_logtype = new System.Windows.Forms.GroupBox();
            this.rb_maillog = new System.Windows.Forms.RadioButton();
            this.rb_eventlog = new System.Windows.Forms.RadioButton();
            this.gb_loglevel = new System.Windows.Forms.GroupBox();
            this.rb_error = new System.Windows.Forms.RadioButton();
            this.rb_warning = new System.Windows.Forms.RadioButton();
            this.rb_information = new System.Windows.Forms.RadioButton();
            this.tlp_main.SuspendLayout();
            this.gb_logtype.SuspendLayout();
            this.gb_loglevel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            this.tlp_main.AutoSize = true;
            this.tlp_main.ColumnCount = 2;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_main.Controls.Add(this.gb_logtype, 0, 0);
            this.tlp_main.Controls.Add(this.gb_loglevel, 1, 0);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 1;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tlp_main.Size = new System.Drawing.Size(384, 94);
            this.tlp_main.TabIndex = 2;
            // 
            // gb_logtype
            // 
            this.gb_logtype.Controls.Add(this.rb_maillog);
            this.gb_logtype.Controls.Add(this.rb_eventlog);
            this.gb_logtype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_logtype.Location = new System.Drawing.Point(3, 3);
            this.gb_logtype.Name = "gb_logtype";
            this.gb_logtype.Size = new System.Drawing.Size(186, 88);
            this.gb_logtype.TabIndex = 0;
            this.gb_logtype.TabStop = false;
            this.gb_logtype.Text = "LogType";
            // 
            // rb_maillog
            // 
            this.rb_maillog.AutoSize = true;
            this.rb_maillog.Location = new System.Drawing.Point(7, 43);
            this.rb_maillog.Name = "rb_maillog";
            this.rb_maillog.Size = new System.Drawing.Size(58, 17);
            this.rb_maillog.TabIndex = 1;
            this.rb_maillog.TabStop = true;
            this.rb_maillog.Text = "Maillog";
            this.rb_maillog.UseVisualStyleBackColor = true;
            // 
            // rb_eventlog
            // 
            this.rb_eventlog.AutoSize = true;
            this.rb_eventlog.Checked = true;
            this.rb_eventlog.Location = new System.Drawing.Point(7, 20);
            this.rb_eventlog.Name = "rb_eventlog";
            this.rb_eventlog.Size = new System.Drawing.Size(67, 17);
            this.rb_eventlog.TabIndex = 0;
            this.rb_eventlog.TabStop = true;
            this.rb_eventlog.Text = "Eventlog";
            this.rb_eventlog.UseVisualStyleBackColor = true;
            // 
            // gb_loglevel
            // 
            this.gb_loglevel.Controls.Add(this.rb_error);
            this.gb_loglevel.Controls.Add(this.rb_warning);
            this.gb_loglevel.Controls.Add(this.rb_information);
            this.gb_loglevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_loglevel.Location = new System.Drawing.Point(195, 3);
            this.gb_loglevel.Name = "gb_loglevel";
            this.gb_loglevel.Size = new System.Drawing.Size(186, 88);
            this.gb_loglevel.TabIndex = 1;
            this.gb_loglevel.TabStop = false;
            this.gb_loglevel.Text = "Log Level";
            // 
            // rb_error
            // 
            this.rb_error.AutoSize = true;
            this.rb_error.Location = new System.Drawing.Point(7, 65);
            this.rb_error.Name = "rb_error";
            this.rb_error.Size = new System.Drawing.Size(47, 17);
            this.rb_error.TabIndex = 4;
            this.rb_error.TabStop = true;
            this.rb_error.Text = "Error";
            this.rb_error.UseVisualStyleBackColor = true;
            // 
            // rb_warning
            // 
            this.rb_warning.AutoSize = true;
            this.rb_warning.Location = new System.Drawing.Point(7, 42);
            this.rb_warning.Name = "rb_warning";
            this.rb_warning.Size = new System.Drawing.Size(65, 17);
            this.rb_warning.TabIndex = 3;
            this.rb_warning.TabStop = true;
            this.rb_warning.Text = "Warning";
            this.rb_warning.UseVisualStyleBackColor = true;
            // 
            // rb_information
            // 
            this.rb_information.AutoSize = true;
            this.rb_information.Checked = true;
            this.rb_information.Location = new System.Drawing.Point(7, 19);
            this.rb_information.Name = "rb_information";
            this.rb_information.Size = new System.Drawing.Size(77, 17);
            this.rb_information.TabIndex = 2;
            this.rb_information.TabStop = true;
            this.rb_information.Text = "Information";
            this.rb_information.UseVisualStyleBackColor = true;
            // 
            // uc_test_diagnostics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp_main);
            this.Name = "uc_test_diagnostics";
            this.Size = new System.Drawing.Size(384, 94);
            this.tlp_main.ResumeLayout(false);
            this.gb_logtype.ResumeLayout(false);
            this.gb_logtype.PerformLayout();
            this.gb_loglevel.ResumeLayout(false);
            this.gb_loglevel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.TableLayoutPanel tlp_main;
        protected System.Windows.Forms.GroupBox gb_logtype;
        protected System.Windows.Forms.RadioButton rb_maillog;
        protected System.Windows.Forms.RadioButton rb_eventlog;
        protected System.Windows.Forms.GroupBox gb_loglevel;
        protected System.Windows.Forms.RadioButton rb_error;
        protected System.Windows.Forms.RadioButton rb_warning;
        protected System.Windows.Forms.RadioButton rb_information;
    }
}
