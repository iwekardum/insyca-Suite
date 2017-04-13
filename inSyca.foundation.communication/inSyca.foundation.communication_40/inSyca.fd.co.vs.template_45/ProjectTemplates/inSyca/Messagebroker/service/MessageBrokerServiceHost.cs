namespace inSyca.messagebroker.ns
{
    public class MessageBrokerServiceHost : inSyca.foundation.communication.service.MessageBrokerServiceHost
    {
        public MessageBrokerServiceHost()
            : base(typeof(MessageBrokerService))
        {
        }
    }
}
