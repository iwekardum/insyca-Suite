namespace inSyca.foundation.framework.configurator
{
    partial class uc_test_filesystem
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
            this.gb_FileSystem = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gb_FileSystem.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_FileSystem
            // 
            this.gb_FileSystem.Controls.Add(this.label1);
            this.gb_FileSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_FileSystem.Location = new System.Drawing.Point(3, 3);
            this.gb_FileSystem.Name = "gb_FileSystem";
            this.gb_FileSystem.Size = new System.Drawing.Size(210, 59);
            this.gb_FileSystem.TabIndex = 0;
            this.gb_FileSystem.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Test for the Filesystem Process";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uc_test_filesystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gb_FileSystem);
            this.Name = "uc_test_filesystem";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(216, 65);
            this.gb_FileSystem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_FileSystem;
        private System.Windows.Forms.Label label1;
    }
}
