namespace inSyca.foundation.framework.application.windowsforms
{
    partial class uc_test
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
            this.sp_main = new System.Windows.Forms.SplitContainer();
            this.pnl_component = new System.Windows.Forms.Panel();
            this.tb_components = new inSyca.foundation.framework.application.windowsforms.TabStrip();
            this.tsb_diagnostics = new inSyca.foundation.framework.application.windowsforms.TabStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnTest = new System.Windows.Forms.Button();
            this.rtb_console = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sp_main)).BeginInit();
            this.sp_main.Panel1.SuspendLayout();
            this.sp_main.Panel2.SuspendLayout();
            this.sp_main.SuspendLayout();
            this.tb_components.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sp_main
            // 
            this.sp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp_main.Location = new System.Drawing.Point(0, 0);
            this.sp_main.Name = "sp_main";
            this.sp_main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sp_main.Panel1
            // 
            this.sp_main.Panel1.Controls.Add(this.pnl_component);
            this.sp_main.Panel1.Controls.Add(this.tb_components);
            // 
            // sp_main.Panel2
            // 
            this.sp_main.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.sp_main.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.sp_main.Size = new System.Drawing.Size(433, 428);
            this.sp_main.SplitterDistance = 123;
            this.sp_main.TabIndex = 0;
            // 
            // pnl_component
            // 
            this.pnl_component.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_component.Location = new System.Drawing.Point(0, 25);
            this.pnl_component.Name = "pnl_component";
            this.pnl_component.Size = new System.Drawing.Size(433, 98);
            this.pnl_component.TabIndex = 1;
            // 
            // tb_components
            // 
            this.tb_components.AutoSize = false;
            this.tb_components.BackColor = System.Drawing.Color.White;
            this.tb_components.FlipButtons = false;
            this.tb_components.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tb_components.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_diagnostics});
            this.tb_components.Location = new System.Drawing.Point(0, 0);
            this.tb_components.Name = "tb_components";
            this.tb_components.RenderStyle = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.tb_components.SelectedTab = this.tsb_diagnostics;
            this.tb_components.Size = new System.Drawing.Size(433, 25);
            this.tb_components.TabIndex = 0;
            this.tb_components.Text = "tb_components";
            this.tb_components.UseVisualStyles = false;
            this.tb_components.SelectedTabChanged += new System.EventHandler<inSyca.foundation.framework.application.windowsforms.SelectedTabChangedEventArgs>(this.tb_components_SelectedTabChanged);
            // 
            // tsb_diagnostics
            // 
            this.tsb_diagnostics.Checked = true;
            this.tsb_diagnostics.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb_diagnostics.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tsb_diagnostics.IsSelected = true;
            this.tsb_diagnostics.Margin = new System.Windows.Forms.Padding(0);
            this.tsb_diagnostics.Name = "tsb_diagnostics";
            this.tsb_diagnostics.Padding = new System.Windows.Forms.Padding(0);
            this.tsb_diagnostics.SelectedFont = new System.Drawing.Font("Segoe UI", 9F);
            this.tsb_diagnostics.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tsb_diagnostics.Size = new System.Drawing.Size(72, 25);
            this.tsb_diagnostics.Text = "Diagnostics";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnTest, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtb_console, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(423, 291);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnTest.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTest.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTest.Location = new System.Drawing.Point(3, 3);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(417, 29);
            this.btnTest.TabIndex = 9;
            this.btnTest.Text = "Test";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // rtb_console
            // 
            this.rtb_console.BackColor = System.Drawing.Color.Black;
            this.rtb_console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_console.ForeColor = System.Drawing.Color.White;
            this.rtb_console.HideSelection = false;
            this.rtb_console.Location = new System.Drawing.Point(3, 38);
            this.rtb_console.Name = "rtb_console";
            this.rtb_console.Size = new System.Drawing.Size(417, 250);
            this.rtb_console.TabIndex = 10;
            this.rtb_console.Text = "";
            // 
            // uc_test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sp_main);
            this.Name = "uc_test";
            this.Size = new System.Drawing.Size(433, 428);
            this.sp_main.Panel1.ResumeLayout(false);
            this.sp_main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sp_main)).EndInit();
            this.sp_main.ResumeLayout(false);
            this.tb_components.ResumeLayout(false);
            this.tb_components.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.SplitContainer sp_main;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Button btnTest;
        public System.Windows.Forms.RichTextBox rtb_console;
        private TabStripButton tsb_diagnostics;
        public TabStrip tb_components;
        public System.Windows.Forms.Panel pnl_component;
    }
}
