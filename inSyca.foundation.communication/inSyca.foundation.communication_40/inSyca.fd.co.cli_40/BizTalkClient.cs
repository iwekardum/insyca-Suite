using inSyca.foundation.communication.clients.diagnostics;
using inSyca.foundation.communication.itf;
using inSyca.foundation.framework;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

/// <summary>
/// Namespace for ready to use clients with the communication library
/// </summary>
namespace inSyca.foundation.communication.clients
{
    /// <summary>
    /// Ready to use BizTalk client class
    /// </summary>
    public class BizTalkClient : ClientBase<IBizTalkClient>, IBizTalkClient, IDisposable
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BizTalkClient()
        {
            Log.Debug("BizTalkClient()");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        virtual public void SendToMsgBox(Message message)
        {
            Log.DebugFormat("SendToMsgBox(Message message {0})", message);

            try
            {
                base.Channel.SendToMsgBox(message);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { message }, ex));
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        virtual public void Dispose()
        {
            Log.Debug("Dispose()");
        }
    }
}
