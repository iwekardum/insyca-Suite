using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace inSyca.foundation.integration.biztalk.functions
{
	[Serializable]
	public sealed class soapxmldocument
	{
        public soapxmldocument()
		{
		}


        public static string ToXmlString(XmlDocument xmlDoc)
        {
			return xmlDoc.OuterXml;
        }
	}
}
