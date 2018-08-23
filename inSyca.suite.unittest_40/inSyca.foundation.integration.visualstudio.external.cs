using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using inSyca.foundation.integration.visualstudio.external;
using System.IO;

namespace inSyca.foundation.unittest_40
{
    /// <summary>
    /// Summary description for TestExtensions
    /// </summary>
    [TestClass]
    public class testExtensions
    {
        [TestMethod]
        public void testProcessBindings()
        {
            FileInfo selectedFile = new FileInfo(@"..\..\Testfiles\binding_001.xml");

            bindings.ProcessBinding(selectedFile);
        }
    }
}
