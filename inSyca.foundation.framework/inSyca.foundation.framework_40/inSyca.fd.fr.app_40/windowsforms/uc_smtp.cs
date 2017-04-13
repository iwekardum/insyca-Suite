using System.Xml.Linq;

namespace inSyca.foundation.framework.application.windowsforms
{
    public partial class uc_smtp : uc_setting
    {
        protected uc_smtp()
        {
        }

        protected uc_smtp(configXml _configFile)
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
                    case "mailmessageto":
                        propertyComponent.AddProperty(transformAppSettingsXnode(xNode));
                        break;
                }
            }

            foreach (XElement xNode in xDocument.Root.Elements("system.net").Elements("mailSettings").Elements("smtp"))
            {
                propertyComponent.AddProperty(transformMailMessageFromXnode(xNode));

                foreach (XElement xChildNode in xNode.DescendantNodes())
                    foreach (XAttribute xAttribute in xChildNode.Attributes())
                        propertyComponent.AddProperty(transformMailMessageAttributesXnode(xChildNode, xAttribute));
            }

            return true;
        }
    }
}
