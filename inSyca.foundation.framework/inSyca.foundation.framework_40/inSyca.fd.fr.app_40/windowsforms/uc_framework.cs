using System;
using System.Windows.Forms;

namespace inSyca.foundation.framework.application.windowsforms
{
    public partial class uc_framework : UserControl
    {
        public uc_framework()
        {
            InitializeComponent();
            this.tabStrip_navigation.SelectedTab = this.tab_information;
        }

        public event EventHandler<SelectedTabChangedEventArgs> NavigationChanged;

        public void AddUserControl(UserControl userControl)
        {
            panel_propertygrid.Controls.Clear();

            userControl.Dock = DockStyle.Fill;
            panel_propertygrid.Controls.Add(userControl);
        }

        private void tabStrip_Navigation_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            this.Invalidate();
            NavigationChanged?.Invoke(tabStrip_navigation, new SelectedTabChangedEventArgs(tabStrip_navigation.SelectedTab));
        }

        private void uc_main_Load(object sender, EventArgs e)
        {
            if (tabStrip_navigation.Items.Count > 0)
                tabStrip_Navigation_SelectedTabChanged(null, new SelectedTabChangedEventArgs(tabStrip_navigation.Items[0] as TabStripButton));
        }
    }
}
