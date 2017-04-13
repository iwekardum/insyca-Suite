using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace inSyca.foundation.integration.visualstudio.external.dialog
{

    internal partial class dlgGAC : Form
    {
        internal event EventHandler refreshEvent;

        internal AssemblyDescription SelectedAssemblyDescription { get; set; }
        internal BindingList<AssemblyDescription> AssemblyBindingList { get; set; }

        internal dlgGAC(BindingList<AssemblyDescription> assemblyBindingList)
        {
            InitializeComponent();

            assemblyGridView.AutoGenerateColumns = false;
            AssemblyBindingList = assemblyBindingList;
            assemblyGridView.DataSource = AssemblyBindingList;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            SelectedAssemblyDescription = assemblyGridView.SelectedRows[0].DataBoundItem as AssemblyDescription;
            DialogResult = DialogResult.OK;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            this.Close();
        }

        private void assemblyGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedAssemblyDescription = assemblyGridView.SelectedRows[0].DataBoundItem as AssemblyDescription;
            DialogResult = DialogResult.OK;
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                assemblyGridView.DataSource = new BindingList<AssemblyDescription>(AssemblyBindingList.Where(m => m.Name.ToLower().Contains(searchTextBox.Text.ToLower()) || m.PublicKeyToken.Contains(searchTextBox.Text)).ToList<AssemblyDescription>());
            }
            catch (Exception ex)
            {
                new ToolTip().SetToolTip(searchTextBox, ex.Message);
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            AssemblyBindingList.RemoveAt(0);
            AssemblyBindingList.RemoveAt(1);
            AssemblyBindingList.RemoveAt(2);
            AssemblyBindingList.RemoveAt(3);
            AssemblyBindingList.RemoveAt(4);
            AssemblyBindingList.RemoveAt(5);
            if (refreshEvent != null)
                refreshEvent(this, null);

            assemblyGridView.DataSource = AssemblyBindingList;
        }
    }

    public class WaterMarkTextBox : TextBox
    {
        private Font oldFont = null;
        private Boolean waterMarkTextEnabled = false;

        #region Attributes
        private Color _waterMarkColor = Color.Gray;
        public Color WaterMarkColor
        {
            get { return _waterMarkColor; }
            set
            {
                _waterMarkColor = value; Invalidate();
            }
        }

        private string _waterMarkText = "Water Mark";
        public string WaterMarkText
        {
            get { return _waterMarkText; }
            set { _waterMarkText = value; Invalidate(); }
        }
        #endregion

        //Default constructor
        public WaterMarkTextBox()
        {
            JoinEvents(true);
        }

        //Override OnCreateControl ... thanks to  "lpgray .. codeproject guy"
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            WaterMark_Toggel(null, null);
        }

        //Override OnPaint
        protected override void OnPaint(PaintEventArgs args)
        {
            // Use the same font that was defined in base class
            System.Drawing.Font drawFont = new System.Drawing.Font(Font.FontFamily,
                Font.Size, Font.Style, Font.Unit);
            //Create new brush with gray color or 
            SolidBrush drawBrush = new SolidBrush(WaterMarkColor);//use Water mark color
            //Draw Text or WaterMark
            args.Graphics.DrawString((waterMarkTextEnabled ? WaterMarkText : Text),
                drawFont, drawBrush, new PointF(0.0F, 0.0F));
            base.OnPaint(args);
        }

        private void JoinEvents(Boolean join)
        {
            if (join)
            {
                this.TextChanged += new System.EventHandler(this.WaterMark_Toggel);
                this.LostFocus += new System.EventHandler(this.WaterMark_Toggel);
                this.FontChanged += new System.EventHandler(this.WaterMark_FontChanged);
                //No one of the above events will start immeddiatlly 
                //TextBox control still in constructing, so,
                //Font object (for example) couldn't be catched from within
                //WaterMark_Toggle
                //So, call WaterMark_Toggel through OnCreateControl after TextBox
                //is totally created
                //No doupt, it will be only one time call

                //Old solution uses Timer.Tick event to check Create property
            }
        }

        private void WaterMark_Toggel(object sender, EventArgs args)
        {
            if (this.Text.Length <= 0)
                EnableWaterMark();
            else
                DisbaleWaterMark();
        }

        private void EnableWaterMark()
        {
            //Save current font until returning the UserPaint style to false (NOTE:
            //It is a try and error advice)
            oldFont = new System.Drawing.Font(Font.FontFamily, Font.Size, Font.Style,
               Font.Unit);
            //Enable OnPaint event handler
            this.SetStyle(ControlStyles.UserPaint, true);
            this.waterMarkTextEnabled = true;
            //Triger OnPaint immediatly
            Refresh();
        }

        private void DisbaleWaterMark()
        {
            //Disbale OnPaint event handler
            this.waterMarkTextEnabled = false;
            this.SetStyle(ControlStyles.UserPaint, false);
            //Return back oldFont if existed
            if (oldFont != null)
                this.Font = new System.Drawing.Font(oldFont.FontFamily, oldFont.Size,
                    oldFont.Style, oldFont.Unit);
        }

        private void WaterMark_FontChanged(object sender, EventArgs args)
        {
            if (waterMarkTextEnabled)
            {
                oldFont = new System.Drawing.Font(Font.FontFamily, Font.Size, Font.Style,
                    Font.Unit);
                Refresh();
            }
        }
    }
}
