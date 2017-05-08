using inSyca.foundation.communication.wcf.diagnostics;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace inSyca.foundation.communication.wcf
{
    /// <summary>
    /// 
    /// </summary>
    public class BizTalkMessageInspector : IDispatchMessageInspector
    {
        public BizTalkMessageInspector()
        {
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Log.DebugFormat("AfterReceiveRequest(ref Message request {0}, IClientChannel channel {1}, InstanceContext instanceContext {2})", request, channel, instanceContext);

            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            request = buffer.CreateMessage();

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            Log.DebugFormat("BeforeSendReply(ref Message reply {0}, object correlationState {1})", reply, correlationState);

            MessageBuffer buffer = reply.CreateBufferedCopy(Int32.MaxValue);
            reply = buffer.CreateMessage();

        }
    }

}
