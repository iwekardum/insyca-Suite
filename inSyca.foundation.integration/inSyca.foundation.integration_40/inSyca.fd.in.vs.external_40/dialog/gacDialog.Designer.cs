namespace inSyca.foundation.integration.visualstudio.external.dialog
{
    partial class dlgGAC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgGAC));
            this.assemblyGridView = new System.Windows.Forms.DataGridView();
            this.Assemblyname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Culture = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PublicKeyToken = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cancel = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.searchTextBox = new inSyca.foundation.integration.visualstudio.external.dialog.WaterMarkTextBox();
            this.refresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.assemblyGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // assemblyGridView
            // 
            this.assemblyGridView.AllowUserToAddRows = false;
            this.assemblyGridView.AllowUserToDeleteRows = false;
            this.assemblyGridView.AllowUserToOrderColumns = true;
            this.assemblyGridView.AllowUserToResizeRows = false;
            this.assemblyGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.assemblyGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Assemblyname,
            this.Version,
            this.Culture,
            this.Type,
            this.PublicKeyToken});
            this.assemblyGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.assemblyGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.assemblyGridView.Location = new System.Drawing.Point(0, 0);
            this.assemblyGridView.MultiSelect = false;
            this.assemblyGridView.Name = "assemblyGridView";
            this.assemblyGridView.ReadOnly = true;
            this.assemblyGridView.RowHeadersVisible = false;
            this.assemblyGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.assemblyGridView.Size = new System.Drawing.Size(703, 263);
            this.assemblyGridView.TabIndex = 0;
            this.assemblyGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.assemblyGridView_CellContentClick);
            // 
            // Assemblyname
            // 
            this.Assemblyname.DataPropertyName = "Name";
            this.Assemblyname.FillWeight = 400F;
            this.Assemblyname.HeaderText = "Assembly Name";
            this.Assemblyname.Name = "Assemblyname";
            this.Assemblyname.ReadOnly = true;
            this.Assemblyname.Width = 400;
            // 
            // Version
            // 
            this.Version.DataPropertyName = "Version";
            this.Version.FillWeight = 60F;
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.Width = 60;
            // 
            // Culture
            // 
            this.Culture.DataPropertyName = "Culture";
            this.Culture.FillWeight = 60F;
            this.Culture.HeaderText = "Culture";
            this.Culture.Name = "Culture";
            this.Culture.ReadOnly = true;
            this.Culture.Width = 60;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "ProcessorArchitecture";
            this.Type.FillWeight = 60F;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 60;
            // 
            // PublicKeyToken
            // 
            this.PublicKeyToken.DataPropertyName = "PublicKeyToken";
            this.PublicKeyToken.FillWeight = 120F;
            this.PublicKeyToken.HeaderText = "Public Key Token";
            this.PublicKeyToken.Name = "PublicKeyToken";
            this.PublicKeyToken.ReadOnly = true;
            this.PublicKeyToken.Width = 120;
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(616, 269);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(535, 269);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 2;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(13, 272);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(435, 20);
            this.searchTextBox.TabIndex = 3;
            this.searchTextBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.searchTextBox.WaterMarkText = "Filter by Assembly Name or Public Key Token";
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(454, 269);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(75, 23);
            this.refresh.TabIndex = 4;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // dlgGAC
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(703, 304);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.assemblyGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgGAC";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add GAC reference";
            ((System.ComponentModel.ISupportInitialize)(this.assemblyGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView assemblyGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Assemblyname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn Culture;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn PublicKeyToken;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button ok;
        private WaterMarkTextBox searchTextBox;
        private System.Windows.Forms.Button refresh;
    }
}