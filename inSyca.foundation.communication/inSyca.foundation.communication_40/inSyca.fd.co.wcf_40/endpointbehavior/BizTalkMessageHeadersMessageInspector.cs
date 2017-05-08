using inSyca.foundation.communication.wcf.diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace inSyca.foundation.communication.wcf
{
    /// <summary>
    /// 
    /// </summary>
    public class BizTalkMessageHeadersMessageInspector : IDispatchMessageInspector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext"></param>
        /// <returns></returns>
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Log.DebugFormat("AfterReceiveRequest(ref Message request {0}, IClientChannel channel {1}, InstanceContext instanceContext {2})", request, channel, instanceContext);
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            Log.DebugFormat("BeforeSendReply(ref Message reply {0}, object correlationState {1})", reply, correlationState);
        }
    }
}
