using inSyca.foundation.integration.biztalk.test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace inSyca.esb.ns
{
    [TestClass]
    public class testcase : baseTestCase
    {
        //Success Case
        [TestMethod]
        [Description("send mp_sp_trigger_in from en1 to en2")]
        public void mp_sp_trigger_in_001_success()
        {
            BizTalkTestCase.prepareDefaultTest(bizTalkTestEnvironment);

            BizTalkTestCase.createTxtTestFile(bizTalkTestEnvironment, eApplications.entity_1.ToString(), "message.xml", @"receive\message.xml");

            BizTalkTestCase.fileRead(bizTalkTestEnvironment, eApplications.entity_2.ToString(), "send", "*.xml", 1, 30000);
            //BizTalkTestCase.fileRead(bizTalkTestEnvironment, "entity_2", "archive", @"*request*.xml", 1, 30000);
            //BizTalkTestCase.fileRead(bizTalkTestEnvironment, "entity_2", "archive", @"*response*.xml", 1, 30000);

            BizTalkTestCase.runTest(bizTalkTestEnvironment);
        }
    }
}
