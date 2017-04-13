using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using inSyca.foundation.communication.wcf.diagnostics;
using inSyca.foundation.framework;
using System.ServiceModel.Channels;

namespace inSyca.foundation.communication.wcf
{
    public class BizTalkParameterBehavior : IEndpointBehavior
    {
        internal BizTalkParameterBehavior()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="clientRuntime"></param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint,ClientRuntime clientRuntime)
        {
            foreach (ClientOperation clientOperation in clientRuntime.Operations)
                clientOperation.ParameterInspectors.Add(new BizTalkParameterInspector());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="endpointDispatcher"></param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            foreach (DispatchOperation dispatchOperation in endpointDispatcher.DispatchRuntime.Operations)
                dispatchOperation.ParameterInspectors.Add(new BizTalkParameterInspector());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceEndpoint"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
        {
            Log.DebugFormat("AddBindingParameters(ServiceEndpoint serviceEndpoint {0}, BindingParameterCollection bindingParameters {1})", serviceEndpoint, bindingParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceEndpoint"></param>
        public void Validate(ServiceEndpoint serviceEndpoint)
        {
            Log.DebugFormat("Validate(ServiceEndpoint serviceEndpoint {0})", serviceEndpoint);
        }
    }
}
