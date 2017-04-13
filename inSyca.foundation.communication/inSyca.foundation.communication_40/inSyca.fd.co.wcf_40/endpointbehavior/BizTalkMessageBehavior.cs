using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using inSyca.foundation.communication.wcf.diagnostics;
using inSyca.foundation.framework;

namespace inSyca.foundation.communication.wcf
{
    /// <summary>
    /// 
    /// </summary>
    public class BizTalkMessageBehavior : IEndpointBehavior
    {
        internal BizTalkMessageBehavior()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="clientRuntime"></param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            throw new Exception("Behavior not supported on the consumer side!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="endpointDispatcher"></param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            Log.DebugFormat("ApplyDispatchBehavior(ServiceEndpoint endpoint {0}, EndpointDispatcher endpointDispatcher {1})", endpoint, endpointDispatcher);

            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(
                new BizTalkMessageInspector());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            Log.DebugFormat("AddBindingParameters(ServiceEndpoint endpoint {0}, BindingParameterCollection bindingParameters {1})", endpoint, bindingParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        public void Validate(ServiceEndpoint endpoint)
        {
            Log.DebugFormat("Validate(ServiceEndpoint endpoint {0})", endpoint);
        }
    }

}
