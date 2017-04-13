using System;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Resources;
using System.Reflection;

namespace inSyca.foundation.framework.application.windowsforms
{
    public partial class uc_setting : UserControl
    {
        protected configXml configFile { get; set; }
        protected XDocument xDocument { get; set; }

        private PropertyComponent _propertyComponent;
        protected PropertyComponent propertyComponent
        {
            get
            {
                if (_propertyComponent == null)
                    _propertyComponent = new PropertyComponent();

                return _propertyComponent;
            }
        }

        protected uc_setting()
        {
            InitializeComponent();
        }

        protected uc_setting(configXml _configFile)
        {
            if (_configFile != null)
            {
                configFile = _configFile;
                xDocument = _configFile.configDocument;
            }

            InitializeComponent();
        }

        private void uc_setting_Load(object sender, EventArgs e)
        {
            LoadConfiguration();
            this.propertySetting.SelectedObject = propertyComponent;
            this.propertySetting.Focus();
        }

        virtual protected bool LoadConfiguration()
        {
            if (xDocument == null)
                return false;

            return true;
        }

        virtual protected void propertySetting_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            PropertyComponent.DynamicProperty dynamicProperty = e.ChangedItem.PropertyDescriptor as PropertyComponent.DynamicProperty;

            dynamicProperty.propElement.SetAttributeValue(dynamicProperty.propValueName, e.ChangedItem.Value);

            configFile.isDirty = true;
        }

        protected FoundationProperty transformAppSettingsXnode(XElement xNode)
        {
            FoundationProperty foundationProperty = new FoundationProperty();

            foundationProperty.xElement = xNode;
            foundationProperty.propValueName = xNode.Attribute("key").Value.ToString();
            foundationProperty.propName = xNode.Attribute("value").Name.ToString();
            foundationProperty.propValue = xNode.Attribute("value").Value;
            foundationProperty.propCat = string.Empty;
            foundationProperty.propType = PropertyComponent.GetValueType(xNode.Attribute("value").Value);
            foundationProperty.isExpandable = false;
            foundationProperty.isReadOnly = false;

            try
            {
                foundationProperty.propDesc = application.Properties.Resources.ResourceManager.GetString(xNode.Attribute("key").Value.ToLower()).Replace('~', '\n');
            }
            catch{}

            return foundationProperty;
        }

        protected FoundationProperty transformMailMessageFromXnode(XElement xNode)
        {
            FoundationProperty foundationProperty = new FoundationProperty();

            foundationProperty.xElement = xNode;
            foundationProperty.propValueName = "MailMessageFrom";
            foundationProperty.propName = "from";
            foundationProperty.propValue = xNode.Attribute("from").Value;
            foundationProperty.propCat = string.Empty;
            foundationProperty.propType = PropertyComponent.GetValueType(xNode.Attribute("from").Value);
            foundationProperty.isExpandable = false;
            foundationProperty.isReadOnly = false;

            try
            {
                foundationProperty.propDesc = application.Properties.Resources.ResourceManager.GetString("mailmessagefrom".ToLower()).Replace('~', '\n');
            }
            catch { }

            return foundationProperty;
        }

        protected FoundationProperty transformMailMessageAttributesXnode(XElement xNode, XAttribute xAttribute)
        {
            FoundationProperty foundationProperty = new FoundationProperty();

            foundationProperty.xElement = xNode;
            foundationProperty.propValueName = xAttribute.Name.ToString();
            foundationProperty.propName = xAttribute.Name.ToString();
            foundationProperty.propValue = xAttribute.Value;
            foundationProperty.propCat = string.Empty;
            foundationProperty.propType = PropertyComponent.GetValueType(xAttribute.Value);
            foundationProperty.isExpandable = false;
            foundationProperty.isReadOnly = false;

            try
            {
                foundationProperty.propDesc = application.Properties.Resources.ResourceManager.GetString(xAttribute.Name.ToString().ToLower()).Replace('~', '\n');
            }
            catch { }

            return foundationProperty;
        }
    }
}
