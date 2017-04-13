using System.Xml.Linq;

namespace inSyca.foundation.framework.application
{
    public class configXml
    {
        public XDocument  configDocument { get; set; }
        public string configFileName { get; set; }
        public bool isDirty { get; set; }

        public configXml(string _configFileName)
        {
            isDirty = false;
            configFileName = _configFileName;
            configDocument = XDocument.Load(configFileName);
        }

        public string GetEventLogLevel()
        {
            foreach (XElement xNode in configDocument.Root.Elements("appSettings").Elements("add"))
                if (xNode.Attribute("key").Value.ToLower() == "eventloglevel")
                    return xNode.Attribute("value").Value;

            return string.Empty;
        }

        public string GetMailLogLevel()
        {
            foreach (XElement xNode in configDocument.Root.Elements("appSettings").Elements("add"))
                if (xNode.Attribute("key").Value.ToLower() == "mailloglevel")
                    return xNode.Attribute("value").Value;

            return string.Empty;
        }

        public string RegistryKey { get; set; }
    }
}
