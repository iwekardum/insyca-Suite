﻿using System.Collections.Generic;
using System.Windows.Forms;
using inSyca.foundation.framework.application;
using inSyca.foundation.framework.application.windowsforms;
using inSyca.foundation.framework.diagnostics;

namespace inSyca.foundation.communication.configurator
{
    public partial class uc_information_wcf : uc_info
    {
        internal uc_information_wcf(configXml _configFile, List<Information> _information)
            : base(_configFile, _information)
        {
            InitializeComponent();
        }

        internal uc_information_wcf(configXml _configFile, Information _information)
            : base(_configFile, _information)
        {
            InitializeComponent();
        }

        override protected void LoadConfiguration()
        {
            base.LoadConfiguration();

            propertyComponent.AddProperty("Foundation Framework Version", information[0].Version, string.Empty, "inSyca Foundation", typeof(string), true, false);
            propertyComponent.AddProperty("Foundation Communication WCF Version", information[1].Version, string.Empty, "inSyca Foundation", typeof(string), true, false);
            propertyComponent.AddProperty("Configurator Version", Application.ProductVersion, string.Empty, "inSyca Foundation", typeof(string), true, false);
        }
    }
}
