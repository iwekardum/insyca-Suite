using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using inSyca.foundation.communication.wcf;

namespace inSyca.foundation.communication.service
{
    public class MessageBrokerServiceHost : ServiceHost
    {
        public MessageBrokerServiceHost(Type serviceType) : base(serviceType)
        {
            foreach (ServiceEndpoint se in this.Description.Endpoints)
                foreach (OperationDescription od in se.Contract.Operations)
                    if (od.Behaviors.Find<BizTalkOperationBehavior>() != null)
                    {
                        od.Behaviors.Add(new BizTalkMessageFormatterBehavior());
                    }
        }
    }
}
