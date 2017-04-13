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
            XDocument xml = XDocument.Load(@"..\..\Testfiles\simple.xml");

            //fetch namespace and root element
            string namespaceURI = xml.Document.Root.Name.NamespaceName;
            string rootElement = xml.Document.Root.Name.LocalName;

            XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";

            IEnumerable<XElement> nills = from n in xml.Descendants()
                                          where n.Attribute(ns + "nil") != null
                                          select n;

            nills.ToList().ForEach(x => x.Remove());

            IEnumerable<XElement> empties;
            do
            {
                empties = from n in xml.Descendants()
                          where n.IsEmpty || string.IsNullOrEmpty(n.Value)
                          select n;

                empties.ToList().ForEach(x => x.Remove());
            }
            while (empties.Count() > 0);

            Console.Write(xml.ToString());
        }

        [TestMethod]
        public void bizTalkTestEnvironment()
        {
            BizTalkTestEnvironment btEnvironment = new BizTalkTestEnvironment();

        }
    }
}
