using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;

namespace inSyca.foundation.communication.unittest_40
{
    [TestClass]
    public class TestScriptingFunctions
    {
        [TestMethod]
        public void Test_createLogMessage()
        {
            XmlDocument childXmlDocument = new XmlDocument();

            childXmlDocument.Load(new XmlTextReader(@"C:\Temp\iata_10.xml"));

            childXmlDocument = inSyca.foundation.communication.components.xml.scripting.createLogMessage(childXmlDocument);
        }
    }
}
