using System.Data;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace inSyca.foundation.communication.wcf
{
    [DataContract(Namespace = "http://www.inSyca.com/messagebroker")]
    public class BizTalkMessageWrapper
    {
        [DataMember]
        public XElement BizTalkMessage { get; set; }

        public DataSet BizTalkDataSet
        {
            get
            {
                DataSet bizTalkDataSet = new DataSet();

                bizTalkDataSet.ReadXml(BizTalkMessage.CreateReader());

                return bizTalkDataSet;
            }
        }
    }
}
