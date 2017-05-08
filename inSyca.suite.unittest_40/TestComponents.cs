using inSyca.foundation.framework.xml;
using inSyca.foundation.integration.biztalk.test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace inSyca.foundation.unittest_40
{
    [TestClass]
    public class TestComponents
    {

        [TestMethod]
        public void removeNilAndEmpty()
        {
            XElement xmldocument = XElement.Load(@"..\..\Testfiles\simple_002.xml");

            xElement.RemoveNilElements(xmldocument);
            xElement.RemoveEmptyElements(xmldocument);

            Console.Write(xmldocument);
        }

        [TestMethod]
        public void bizTalkTestEnvironment()
        {
            BizTalkTestEnvironment btEnvironment = new BizTalkTestEnvironment();

        }
    }
}
