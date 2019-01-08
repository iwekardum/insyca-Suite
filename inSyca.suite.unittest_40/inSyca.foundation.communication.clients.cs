using inSyca.foundation.communication.clients;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Linq;

namespace inSyca.foundation.unittest_40
{
    [TestClass]
    public class testBizTalkClient
    {
        [TestMethod]
        [Description("send generic message to BizTalk")]
        public void testSendToMsgBox()
        {
            XmlReader xmlrdr = XElement.Load(@"..\..\Testfiles\simple_001.xml").CreateReader();
			Message message = Message.CreateMessage(MessageVersion.Default, "*", xmlrdr);

            BizTalkClient btClient = new BizTalkClient();
            btClient.SendToMsgBox(message);
        }
    }
}
