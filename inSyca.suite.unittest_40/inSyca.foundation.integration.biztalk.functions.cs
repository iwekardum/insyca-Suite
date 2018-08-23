using inSyca.foundation.integration.biztalk.adapter.file.design;
using inSyca.foundation.integration.biztalk.functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Xml;

namespace inSyca.foundation.communication.unittest_40
{
    [TestClass]
    public class testFunctions
    {
        [TestMethod]
        public void testCreateLogMessage()
        {
            XmlDocument childXmlDocument = new XmlDocument();

            childXmlDocument.Load(new XmlTextReader(@"..\..\Testfiles\simple_002.xml"));

            childXmlDocument = components.xml.scripting.createLogMessage(childXmlDocument);
        }

        [TestMethod]
        public void testAdapter()
        {
            StaticAdapterManagement sam = new StaticAdapterManagement();

            sam.GetConfigSchema(Microsoft.BizTalk.Adapter.Framework.ConfigType.ReceiveHandler);
        }

        [TestMethod]
        public void testFormatNumber()
        {
            string input;
            string output;

            input = "5 kg";
            output = scripting.FormatNumber(input, "0","",string.Empty, string.Empty);
            Debug.WriteLine("FormatNumber: {0} - {1}", input, output);

            input = "5,00 kg";
            output = scripting.FormatNumber(input, "0", "", string.Empty, string.Empty);
            Debug.WriteLine("FormatNumber: {0} - {1}", input, output);

            input = "5.00 kg";
            output = scripting.FormatNumber(input, "0", "", string.Empty, string.Empty);
            Debug.WriteLine("FormatNumber: {0} - {1}", input, output);

            input = "5";
            output = scripting.FormatNumber(input, "0", "", string.Empty, string.Empty);
            Debug.WriteLine("FormatNumber: {0} - {1}", input, output);

            input = "50000000";
            output = scripting.FormatNumber(input, "0", "", string.Empty, string.Empty);
            Debug.WriteLine("FormatNumber: {0} - {1}", input, output);

            input = "Hallo -55000,008900";
            output = scripting.FormatNumber(input, "0", "", string.Empty, string.Empty);
            Debug.WriteLine("FormatNumber: {0} - {1}", input, output);
        }
    }
}
