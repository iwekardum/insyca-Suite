using inSyca.foundation.framework;
using inSyca.foundation.integration.ssis.components.diagnostics;
using System;
using System.IO;
using System.Messaging;
using System.Text;

namespace inSyca.foundation.integration.ssis.components
{
    internal static class MSMQHelper
    {
        internal static void SendToMessageQueue(string MSMQName, string MSMQEntryLabel, int rowsProcessed, MemoryStream memoryStream)
        {
            Log.DebugFormat("SendToMessageQueue(string MSMQName {0}, string MSMQEntryLabel {1}, int rowsProcessed {2}, MemoryStream memoryStream {3})", MSMQName, MSMQEntryLabel, rowsProcessed, memoryStream);

            if (rowsProcessed < 1)
                return;

            Message msg = new Message();

            try
            {
                msg.Body = Encoding.Unicode.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { MSMQName, MSMQEntryLabel, rowsProcessed, memoryStream }, ex));
            }

            msg.Formatter = new ActiveXMessageFormatter();

            MessageQueue queue = new MessageQueue(MSMQName);

            try
            {
                Log.DebugFormat("SendToMessageQueue(string MSMQName {0}, string MSMQEntryLabel {1}, int rowsProcessed {2}, MemoryStream memoryStream {3})", MSMQName, MSMQEntryLabel, rowsProcessed, memoryStream);

                queue.Send(msg, string.Format("{0} @:{1} - Records processed: {2}", MSMQEntryLabel, DateTime.Now.ToString(), rowsProcessed), MessageQueueTransactionType.Single);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { MSMQName, MSMQEntryLabel, rowsProcessed, memoryStream }, ex));
            }
            finally
            {
                queue.Close();
            }
        }
    }
}
