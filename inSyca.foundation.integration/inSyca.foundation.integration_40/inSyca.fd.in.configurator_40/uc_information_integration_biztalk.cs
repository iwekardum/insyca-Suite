using inSyca.foundation.framework.application;
using inSyca.foundation.framework.application.windowsforms;
using inSyca.foundation.framework.diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;

namespace inSyca.foundation.integration.configurator
{
    public partial class uc_information_integration_biztalk : uc_info
    {
        internal uc_information_integration_biztalk(configXml _configFile, List<IInformation> _information)
            : base(_configFile, _information)
        {
            InitializeComponent();
        }

        internal uc_information_integration_biztalk(configXml _configFile, IInformation _information)
            : base(_configFile, _information)
        {
            InitializeComponent();
        }

        override protected void LoadConfiguration()
        {
            GetInstalledPrograms(new string[] { "Microsoft SQL", "Microsoft BizTalk" });

            base.LoadConfiguration();

            propertyComponent.AddProperty("Foundation Framework Version", information[0].Version, string.Empty, "inSyca Foundation", typeof(string), true, false);
            propertyComponent.AddProperty("Foundation Integration BizTalk Version", information[1].Version, string.Empty, "inSyca Foundation", typeof(string), true, false);
            propertyComponent.AddProperty("Location", configFile.configFileName, string.Empty, "inSyca Foundation", typeof(string), true, false);
            propertyComponent.AddProperty("Configurator Version", Application.ProductVersion, string.Empty, "inSyca Foundation", typeof(string), true, false);

        }
    }
}
