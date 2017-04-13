using System.ServiceModel;

using inSyca.foundation.communication.wcf;
using System.Runtime.Serialization;
using System.Xml;

namespace inSyca.foundation.communication.itf
{
    [ServiceContract(Namespace = "http://www.inSyca.com/IMessageBroker")]
    public interface IMessageBroker
    {
        //[OperationContract(Action = "http://inSyca.foundation.messagebroker/IMessageBroker/AssembleServiceResponse_Request", ReplyAction = "http://inSyca.foundation.messagebroker/IMessageBroker/AssembleServiceResponse_Response")]
        //[BizTalkOperationBehavior]
        //bool Assemble_ServiceResponse(BizTalkMessageWrapper inDocument, out BizTalkMessageWrapper outDocument);

        [OperationContract(Action = "http://www.inSyca.com/IMessageBroker/GetVersion")]
        service_response getVersion();

        [OperationContract(Action = "http://www.inSyca.com/IMessageBroker/LogMessage")]
        [BizTalkOperationBehavior]
        service_response logMessage(BizTalkMessageWrapper inDocument);
    }
}
