using inSyca.foundation.framework;
using System;
using System.Data;
using System.ServiceModel;
using System.Threading;
using System.Xml;
using System.Xml.Linq;  
using inSyca.foundation.communication.wcf;
using inSyca.foundation.communication.itf;
using inSyca.foundation.communication.components.xml;
using System.ServiceModel.Channels;
using inSyca.foundation.communication.clients;

namespace inSyca.messagebroker.ns
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Info("inSyca.messagebroker.ns started...(Warm-up)");

            Thread.Sleep(5000);

            Log.Info("inSyca.messagebroker.ns ready");

            ChannelFactory<IMessageBroker> factoryIMessageBroker = new ChannelFactory<IMessageBroker>("MessageBrokerTcp");
            IMessageBroker proxyIMessageBroker = factoryIMessageBroker.CreateChannel();

            ChannelFactory<inSyca.messagebroker.root.ns.interfaces.ICompany> factoryICompany = new ChannelFactory<inSyca.messagebroker.root.ns.interfaces.ICompany>("CompanyTcp");
            inSyca.messagebroker.root.ns.interfaces.ICompany proxyICompany = factoryICompany.CreateChannel();

            bool bChoice = false;

            do
            {
                Console.WriteLine("++++++++++++++++++++++++++++++++++");
                Console.WriteLine("0 -> getVersion");
                Console.WriteLine("1 -> logMessage");
                Console.WriteLine("2 -> setSQLTable");
                Console.WriteLine("3 -> sendToBizTalk");
                Console.WriteLine("press any other key to end client");

                string strChoice = Console.ReadLine();
                Console.WriteLine("++++++++++++++++++++++++++++++++++");

                Console.ForegroundColor = ConsoleColor.Red;

                switch (strChoice)
                {
                    case "0":
                        getVersion(proxyIMessageBroker);
                        bChoice = true;
                        break;
                    case "1":
                        logMessage(proxyIMessageBroker);
                        bChoice = true;
                        break;
                    case "2":
                        setSQLTable(proxyICompany);
                        bChoice = true;
                        break;
                    case "3":
                        sendToBizTalk();
                        bChoice = true;
                        break;
                    default:
                        bChoice = false;
                        break;
                }

                Console.ForegroundColor = ConsoleColor.White;

            } while (bChoice);

            ((IClientChannel)proxyIMessageBroker).Close();
            factoryIMessageBroker.Close();

            ((IClientChannel)proxyICompany).Close();
            factoryICompany.Close();

            Console.ReadLine();
        }

        private static void getVersion(IMessageBroker proxy)
        {
            Console.WriteLine("get version dataset...");
            Console.WriteLine();
            Console.WriteLine("--- RESPONSE ---");

            service_response serviceResponse = proxy.getVersion();

            Console.WriteLine("timestamp_started: {0}", serviceResponse.response.Rows[0][0]);
            Console.WriteLine("timestamp_finished: {0}", serviceResponse.response.Rows[0][1]);
            Console.WriteLine("status: {0}", serviceResponse.response.Rows[0][2]);
            Console.WriteLine("message: {0}", serviceResponse.response.Rows[0][3]);

            Console.WriteLine("--- RESPONSE ---");
        }

        private static void logMessage(IMessageBroker proxy)
        {
            BizTalkMessageWrapper inDocument = new BizTalkMessageWrapper();
            inDocument.BizTalkMessage = XElement.Load(@"..\..\..\inSyca.messagebroker.root.ns.unittest\testFiles\untypedBizTalkMessage.xml");

            Console.WriteLine("log message...");
            Console.WriteLine();
            Console.WriteLine("--- RESPONSE ---");

            service_response serviceResponse = proxy.logMessage(inDocument);

            Console.WriteLine("timestamp_started: {0}", serviceResponse.response.Rows[0][0]);
            Console.WriteLine("timestamp_finished: {0}", serviceResponse.response.Rows[0][1]);
            Console.WriteLine("status: {0}", serviceResponse.response.Rows[0][2]);
            Console.WriteLine("message: {0}", serviceResponse.response.Rows[0][3]);

            Console.WriteLine("--- RESPONSE ---");
        }

        private static void setSQLTable(inSyca.messagebroker.root.ns.interfaces.ICompany proxy)
        {
            BizTalkMessageWrapper inDocument = new BizTalkMessageWrapper();
            BizTalkMessageWrapper outDocument;

            inDocument.BizTalkMessage = XElement.Load(@"..\..\..\inSyca.messagebroker.root.ns.unittest\testFiles\typedBizTalkMessage.xml");

            Console.WriteLine("set SQLTable dataset...");
            Console.WriteLine();
            Console.WriteLine("--- RESPONSE ---");

            proxy.setSQLTable(inDocument, out outDocument);

            Console.WriteLine();
            Console.WriteLine("+++ formatted +++");

            DataSet ds = new DataSet();
            ds.ReadXml(new System.IO.StringReader(outDocument.BizTalkMessage.ToString()));

            Console.WriteLine("timestamp_started: {0}", outDocument.BizTalkDataSet.Tables[0].Rows[0][0]);
            Console.WriteLine("timestamp_finished: {0}", ds.Tables[0].Rows[0][1]);
            Console.WriteLine("status: {0}", ds.Tables[0].Rows[0][2]);
            Console.WriteLine("message: {0}", ds.Tables[0].Rows[0][3]);

            Console.WriteLine("+++ formatted +++");
            Console.WriteLine();
            Console.WriteLine("+++ native XML +++");

            Console.WriteLine(outDocument.BizTalkMessage.ToString());

            Console.WriteLine("+++ native XML +++");
            Console.WriteLine();
            Console.WriteLine("--- RESPONSE ---");
        }

        private static void sendToBizTalk()
        {
            Console.WriteLine("send to BizTalk...");
            Console.WriteLine();

            using (BizTalkClient btClient = new BizTalkClient())
            {
                try
                {
                    XmlReader xmlrdr = XElement.Load(@"..\..\..\inSyca.messagebroker.root.ns.unittest\testFiles\untypedBizTalkMessage.xml").CreateReader();
                    Message message = Message.CreateMessage(MessageVersion.Default, "*", xmlrdr);
                    btClient.SendToMsgBox(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("--- ERROR ---");
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
            Console.WriteLine("--- Message sent ---");
        }
    }
}
