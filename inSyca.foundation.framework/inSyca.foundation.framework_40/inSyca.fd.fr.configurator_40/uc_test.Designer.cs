using inSyca.foundation.framework.application.windowsforms;
namespace inSyca.foundation.framework.configurator
{
    partial class uc_test
    {

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
        new private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.sp_main)).BeginInit();
            this.sp_main.SuspendLayout();
            this.SuspendLayout();
            this.tsb_FileSystem = new inSyca.foundation.framework.application.windowsforms.TabStripButton();
            // 
            // tsb_FileSystem
            // 
            this.tsb_FileSystem.Checked = false;
            this.tsb_FileSystem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_FileSystem.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tsb_FileSystem.IsSelected = true;
            this.tsb_FileSystem.Margin = new System.Windows.Forms.Padding(0);
            this.tsb_FileSystem.Name = "tsb_FileSystem";
            this.tsb_FileSystem.Padding = new System.Windows.Forms.Padding(0);
            this.tsb_FileSystem.SelectedFont = new System.Drawing.Font("Segoe UI", 9F);
            this.tsb_FileSystem.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tsb_FileSystem.Size = new System.Drawing.Size(67, 25);
            this.tsb_FileSystem.Text = "FileSystem";
            this.tb_components.Items.Add(this.tsb_FileSystem);
            this.tb_components.SelectedTab = tb_components.Items[0] as TabStripButton;
            // 
            // uc_test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "uc_test";
            ((System.ComponentModel.ISupportInitialize)(this.sp_main)).EndInit();
            this.sp_main.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private TabStripButton tsb_FileSystem;

    }
}
