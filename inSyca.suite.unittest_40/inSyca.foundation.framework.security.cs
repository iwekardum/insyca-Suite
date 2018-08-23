using inSyca.foundation.framework.security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace inSyca.foundation.unittest_40
{
    [TestClass]
    public class testSecurity
    {
        [TestMethod]
        [Description("encrypt Text")]
        public void testEncrypt()
        {
            Security.ReplacePasswordCharacters("Test");
        }
    }
}
