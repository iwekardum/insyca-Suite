using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using inSyca.foundation.framework.application;
using System.Reflection;
using System.Diagnostics;

namespace inSyca.foundation.framework.configurator
{
    public partial class uc_monitoring : foundation.framework.application.windowsforms.uc_monitoring
    {
        internal uc_monitoring()
        {
            InitializeComponent();
        }

        internal uc_monitoring(configXml _configFile)
            : base(_configFile)
        {
            InitializeComponent();

            ServiceName = "Foundation Framework";
            ServiceAssemblyName = Assembly.GetAssembly(typeof(inSyca.foundation.framework.wsh.FrameworkService)).Location;
        }
    }
}
