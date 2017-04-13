using inSyca.foundation.communication.wcf.diagnostics;
using inSyca.foundation.framework;
using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;


namespace inSyca.foundation.communication.wcf
{
    /// <summary>
    /// 
    /// </summary>
    public class BizTalkOperationBehavior : Attribute, IOperationBehavior
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
            Log.DebugFormat("AddBindingParameters(OperationDescription operationDescription {0}, BindingParameterCollection bindingParameters {1})", operationDescription, bindingParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="clientOperation"></param>
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            Log.DebugFormat("ApplyClientBehavior(OperationDescription operationDescription {0}, ClientOperation clientOperation {1})", operationDescription, clientOperation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="dispatchOperation"></param>
        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            Log.DebugFormat("OperationDescription operationDescription {0}, DispatchOperation dispatchOperation {1}", operationDescription, dispatchOperation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationDescription"></param>
        public void Validate(OperationDescription operationDescription)
        {
            Log.DebugFormat("Validate(OperationDescription operationDescription {0})", operationDescription);
        }
    }
}
