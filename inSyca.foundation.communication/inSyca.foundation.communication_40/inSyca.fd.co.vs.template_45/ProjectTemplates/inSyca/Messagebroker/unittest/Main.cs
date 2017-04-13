using inSyca.foundation.communication.clients;
using inSyca.foundation.communication.itf;
using inSyca.foundation.communication.wcf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

using inSyca.messagebroker.root.ns.csh;

namespace inSyca.messagebroker.ns
{
    [TestClass]
    public class Main
    {
        [TestMethod]
        [Description("Send Dataset Message to Messagebroker Service")]
        public void setSQL()
        {
            Program csh = new Program();

            csh.Start();

            Trace.WriteLine("The service is ready.");

            ChannelFactory<IMessageBroker> factoryIMessageBroker = new ChannelFactory<IMessageBroker>("MessageBrokerTcp");
            IMessageBroker proxyIMessageBroker = factoryIMessageBroker.CreateChannel();

            ChannelFactory<inSyca.messagebroker.root.ns.interfaces.ICompany> factoryICompany = new ChannelFactory<inSyca.messagebroker.root.ns.interfaces.ICompany>("CompanyTcp");
            inSyca.messagebroker.root.ns.interfaces.ICompany proxyICompany = factoryICompany.CreateChannel();

            BizTalkMessageWrapper inDocument = new BizTalkMessageWrapper();
            BizTalkMessageWrapper outDocument;

            inDocument.BizTalkMessage = XElement.Load(@"..\..\testFiles\typedBizTalkMessage.xml");

            Trace.WriteLine(proxyICompany.setSQLTable(inDocument, out outDocument));

            Trace.WriteLine(outDocument.BizTalkMessage.ToString());

            ((IClientChannel)proxyIMessageBroker).Close();
            factoryIMessageBroker.Close();

            ((IClientChannel)proxyICompany).Close();
            factoryICompany.Close();

            Thread.Sleep(5000);

            csh.Stop();
        }

        [TestMethod]
        [Description("Send Message to BizTalk")]
        public void sendToBizTalkMessageBox()
        {
            using (BizTalkClient btClient = new BizTalkClient())
            {
                try
                {
                    XmlReader xmlrdr = XElement.Load(@"..\..\testFiles\untypedBizTalkMessage.xml").CreateReader();
                    Message message = Message.CreateMessage(MessageVersion.Default, "*", xmlrdr);

                    btClient.SendToMsgBox(message);
                }
                catch (Exception ex)
                {
                    Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
                }
            }
        }
    }
}
