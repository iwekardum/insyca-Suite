using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace inSyca.foundation.communication.components.xml
{
    public class scripting
    {
        static public XmlDocument createLogMessage(XmlDocument childXmlDocument)
        {
            XmlDocument parentXmlDocument = new XmlDocument();

            parentXmlDocument.LoadXml("<ns0:logMessage xmlns:ns0='http://www.inSyca.com/IMessageBroker' xmlns:ns2='http://schemas.datacontract.org/2004/07/System.Xml' xmlns:ns1='http://www.inSyca.com/messagebroker'><ns0:inDocument><ns1:BizTalkMessage><ns2:XmlElement></ns2:XmlElement></ns1:BizTalkMessage></ns0:inDocument></ns0:logMessage>");

            XmlNode importedDocument = parentXmlDocument.ImportNode(childXmlDocument.DocumentElement, true);

            XmlNode xmlNode = parentXmlDocument.SelectSingleNode("//*[local-name()='XmlElement']");
            xmlNode.AppendChild(importedDocument);

            return parentXmlDocument;

        }
    }
}
