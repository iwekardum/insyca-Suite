using inSyca.foundation.communication.wcf.diagnostics;
using System.ServiceModel.Dispatcher;

namespace inSyca.foundation.communication.wcf
{
    /// <summary>
    /// 
    /// </summary>
    public class BizTalkParameterInspector : IParameterInspector
    {
        /// <summary>
        /// 
        /// </summary>
        public BizTalkParameterInspector()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationName"></param>
        /// <param name="outputs"></param>
        /// <param name="returnValue"></param>
        /// <param name="correlationState"></param>
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            Log.DebugFormat("AfterCall(string operationName {0}, object[] outputs {1}, object returnValue {2}, object correlationState {3})", operationName, outputs, returnValue, correlationState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationName"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public object BeforeCall(string operationName, object[] inputs)
        {
            Log.DebugFormat("BeforeCall(string operationName {0}, object[] inputs {1})", operationName, inputs);

            return null;
        }

    }
}
