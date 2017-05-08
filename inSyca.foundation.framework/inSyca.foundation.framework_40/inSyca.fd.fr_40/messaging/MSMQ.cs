using inSyca.foundation.framework.diagnostics;
using System;
using System.IO;
using System.Messaging;

namespace inSyca.foundation.framework.messaging
{
    public class MSMQ
    {
        #region constructor(s)

        /// <summary>
        /// No instance of this class allowed.
        /// </summary>
        private MSMQ() { }

        #endregion constructor(s)

        #region send messages

        /// <summary>
        /// Sends the given Message object to a MessageQueue with the given path.
        /// </summary>
        /// <param name="QueuePath">MessageQueues path to send to</param>
        /// <param name="aMessage"><see cref="System.Messaging.Message"/></param>
        public static void SendMessage(string QueuePath, Message aMessage)
        {
            Log.DebugFormat("SendMessage(string QueuePath {0}, Message aMessage {1})", QueuePath, aMessage);

            MessageQueue mq = new MessageQueue();

            try
            {
                mq.Path = QueuePath;
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { QueuePath, aMessage }, ex));
            }

            try
            {
                if (mq.Transactional)
                {
                    MessageQueueTransaction mqt = new MessageQueueTransaction();

                    mqt.Begin();
                    mq.Send(aMessage, mqt);
                    mqt.Commit();
                }
                else
                {
                    mq.Send(aMessage);
                }
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { QueuePath, aMessage }, ex));
            }
        }


        /// <summary>
        /// Creates a Message object with the given content 
        /// and sends it to a Queue with the given path.
        /// </summary>
        /// <param name="QueuePath">MessageQueues path to send to</param>
        /// <param name="MessageBody">Message content</param>
        public static void SendMessage(string QueuePath, string MessageBody)
        {
            Message msg = CreateMessage(MessageBody);
            SendMessage(QueuePath, msg);
        }


        /// <summary>
        /// Creates a Message object with the given content and label 
        /// and sends it to a Queue with the given path.
        /// </summary>
        /// <param name="QueuePath">MessageQueues path to send to</param>
        /// <param name="MessageBody">Message content</param>
        /// <param name="MessageLabel">Descriptive label text</param>
        public static void SendMessage(string QueuePath, string MessageBody, string MessageLabel)
        {
            Message msg = CreateMessage(MessageBody, MessageLabel);
            SendMessage(QueuePath, msg);
        }

        #endregion send messages

        #region create messages

        /// <summary>
        /// Creates and returns a Message object with the given parameters
        /// </summary>
        /// <param name="MessageBody">The content of the Message</param>
        /// <param name="MessageLabel">Describes the Message</param>
        /// <returns>A Message object filled with given parameters</returns>
        public static Message CreateMessage(string MessageBody, string MessageLabel)
        {
            Message result = new Message();

            StreamWriter bodyWriter = new StreamWriter(result.BodyStream);
            bodyWriter.Write(MessageBody);
            bodyWriter.Flush();
            result.Priority = MessagePriority.Normal;
            result.Label = MessageLabel;

            return result;
        }


        /// <summary>
        /// Creates and returns a Message object with the given parameters
        /// </summary>
        /// <param name="MessageBody">The content of the Message</param>
        /// <returns>A Message object filled with given parameters</returns>
        public static Message CreateMessage(string MessageBody)
        {
            Message result = new Message();

            StreamWriter bodyWriter = new StreamWriter(result.BodyStream);
            bodyWriter.Write(MessageBody);
            bodyWriter.Flush();
            result.Priority = MessagePriority.Normal;

            return result;
        }

        #endregion create messages
    }

    //public class Message : System.Messaging.Message
    //{

    //}
}
