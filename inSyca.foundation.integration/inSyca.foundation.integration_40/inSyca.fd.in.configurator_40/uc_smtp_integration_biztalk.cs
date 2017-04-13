using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using inSyca.foundation.framework.application.windowsforms;
using inSyca.foundation.framework.application;

namespace inSyca.foundation.integration.configurator
{
    public partial class uc_smtp_integration_biztalk : uc_smtp
    {
        internal uc_smtp_integration_biztalk(configXml _configFile)
            : base(_configFile)
        {
            InitializeComponent();
        }
    }
}
