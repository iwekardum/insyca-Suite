using System.Xml.Linq;

namespace inSyca.foundation.framework.application.windowsforms
{
    public partial class uc_logging : uc_setting
    {
        protected uc_logging()
            : base()
        {
            InitializeComponent();
        }
        
        protected uc_logging(configXml _configFile)
            : base(_configFile)
        {
            InitializeComponent();
        }

        override protected bool LoadConfiguration()
        {
            if (!base.LoadConfiguration())
                return false;

            foreach (XElement xNode in xDocument.Root.Elements("appSettings").Elements("add"))
            {
                switch (xNode.Attribute("key").Value.ToLower())
                {
                    case "fulltextlogfilter":
                    case "mailloglevel":
                    case "eventloglevel":
                        propertyComponent.AddProperty(transformAppSettingsXnode(xNode));
                        break;
                }
            }

            return false;
        }
    }
}
