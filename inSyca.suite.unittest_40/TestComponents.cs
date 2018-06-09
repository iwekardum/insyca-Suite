using inSyca.foundation.framework.xml;
using inSyca.foundation.integration.biztalk.test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            StreamReader sr = new StreamReader(@"..\..\Testfiles\simple_001.csv");
            string messageString = sr.ReadToEnd();

            //messageString = messageString.TrimEnd('\r', '\n');
            //messageString = messageString + Environment.NewLine;

            messageString = Regex.Replace(messageString, @"\r\n+", "\r\n");

            messageString = Regex.Replace(messageString, "\".*?\"", "");

            byte[] byteArray = Encoding.UTF8.GetBytes(messageString);

            File.WriteAllBytes(@"..\..\Testfiles\output.csv", byteArray);
        }
    }
}
