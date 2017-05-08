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

        public string RegistryKey { get; set; }
    }
}
