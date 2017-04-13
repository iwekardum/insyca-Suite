using inSyca.foundation.communication.wcf;
using System.ServiceModel;

namespace inSyca.messagebroker.root.ns.interfaces
{
    //Interface for the masterdata logistics service 
    //collecting data from BizTalk and transfer to Inubit
    [ServiceContract(Namespace = "http://www.company.com/ICompany")]
    public interface ICompany
    {
        [OperationContract]
        [BizTalkOperationBehavior]
        bool setSQLTable(BizTalkMessageWrapper inDocument, out BizTalkMessageWrapper outDocument);

        [OperationContract]
        void TestScheduledEvent(string scheduleName);

        [OperationContract]
        void sendToBizTalkMessageBox();
    }
}
