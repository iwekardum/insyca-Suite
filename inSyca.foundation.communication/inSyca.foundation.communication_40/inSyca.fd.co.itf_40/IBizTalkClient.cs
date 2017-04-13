using System.ServiceModel;
using System.ServiceModel.Channels;

/// <summary>
/// Interfaces for communication
/// </summary>
namespace inSyca.foundation.communication.itf
{
    [ServiceContract(Namespace = "http://www.inSyca.com/messagebroker")]
    public interface IBizTalkClient
    {
        [OperationContract(Action = "*", ReplyAction = "*")]
        void SendToMsgBox(Message message);
    }
}
