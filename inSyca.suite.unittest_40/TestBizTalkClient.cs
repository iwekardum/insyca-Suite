using inSyca.foundation.communication.clients;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Linq;

namespace inSyca.foundation.unittest_40
{
    [TestClass]
    public class TestBizTalkClient
    {
        [TestMethod]
        [Description("send generic message to BizTalk")]
        public void send_generic_message()
        {
            XmlReader xmlrdr = XElement.Parse("<ns0:message xmlns:ns0='http://inSyca.foundation.messagebroker/testmessage'><id>69</id><trackingid>223FCA9C-EB79-46F5-86F0-5AA6CA66553B</trackingid></ns0:message>").CreateReader();
            Message message = Message.CreateMessage(MessageVersion.Default, "*", xmlrdr);

            BizTalkClient btClient = new BizTalkClient();
            btClient.SendToMsgBox(message);
        }
    }
}
