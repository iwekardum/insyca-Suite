namespace inSyca.foundation.framework.application.windowsforms
{
    partial class uc_setting
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
            this.propertySetting = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // propertySetting
            // 
            this.propertySetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertySetting.Location = new System.Drawing.Point(0, 0);
            this.propertySetting.Name = "propertyLogging";
            this.propertySetting.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertySetting.Size = new System.Drawing.Size(307, 294);
            this.propertySetting.TabIndex = 0;
            this.propertySetting.ToolbarVisible = false;
            this.propertySetting.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertySetting_PropertyValueChanged);
            // 
            // uc_setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertySetting);
            this.Name = "uc_setting";
            this.Size = new System.Drawing.Size(307, 294);
            this.Load += new System.EventHandler(this.uc_setting_Load);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.PropertyGrid propertySetting;


    }
}
