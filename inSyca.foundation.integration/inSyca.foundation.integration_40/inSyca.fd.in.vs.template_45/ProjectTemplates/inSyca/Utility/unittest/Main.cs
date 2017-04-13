using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace inSyca.utility.ns
{
    [TestClass]
    public class Main
    {
        [TestMethod]
        public void SampleTest()
        {
            string strInput = "";
            string strOutput = "";

            strOutput = scripting.transformStatus(strInput);

            Assert.AreEqual("1", strOutput);
            Assert.AreNotEqual("9", strOutput);
        }
    }
}
