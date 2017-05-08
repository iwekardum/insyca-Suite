using inSyca.foundation.communication.wcf.diagnostics;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml.Linq;


namespace inSyca.foundation.communication.wcf
{
    public class BizTalkMessageFormatter : IDispatchMessageFormatter
    {
        //Formatter.
        private IDispatchMessageFormatter innerFormatter;
        public int LogLevel { get; set; }

        //Constructor.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formatter"></param>
        public BizTalkMessageFormatter(IDispatchMessageFormatter formatter)
        {
            innerFormatter = formatter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public void DeserializeRequest(Message message, object[] parameters)
        {
            Log.DebugFormat("DeserializeRequest(Message message {0}, object[] parameters {1})", message, parameters);

            XElement node = XElement.ReadFrom(message.GetReaderAtBodyContents()) as XElement;

            XNamespace xNamespace = XNamespace.Get("http://www.inSyca.com/messagebroker");

            if (parameters != null && parameters.GetLength(0) > 0)
            {
                BizTalkMessageWrapper bizTalkMessageWrapper = new BizTalkMessageWrapper();
                bizTalkMessageWrapper.BizTalkMessage = node.Descendants(xNamespace + "BizTalkMessage").FirstOrDefault();
                parameters[0] = bizTalkMessageWrapper;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageVersion"></param>
        /// <param name="parameters"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            Log.DebugFormat("SerializeReply(MessageVersion messageVersion {0}, object[] parameters {1}, object result {2})", messageVersion, parameters, result);

            return this.innerFormatter.SerializeReply(messageVersion, parameters, result);
        }
    }

}
