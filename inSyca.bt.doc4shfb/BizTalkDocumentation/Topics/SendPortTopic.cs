using shfb.helper;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk sendPorts that were documented.
    /// </summary>
    internal class SendPortTopic : TopicFile
    {
        SendPort sendPort;

        /// <summary>
        /// Creates a new sendPort topic instance.
        /// </summary>
        /// <param name="basePath">The path the sendPort topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk sendPort names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public SendPortTopic(SendPort _sendPort)
        {
            sendPort = _sendPort;

            Title = sendPort.Name;
            LinkTitle = sendPort.Name;
            LinkUri = "#" + sendPort.Name;
            TopicType = TopicType.SendPort;
        }

        protected override void SaveTopic(MamlWriter writer)
        {
            writer.StartIntroduction();
            writer.StartParagraph();
            writer.WriteRaw("This section contains documentation for all of the selected BizTalk sendPorts.The BizTalk artifacts, and all documentation captured, has been pulled from the BizTalk Administration Console.For more information on any of the sendPorts, please select one of the links below.");
            writer.EndParagraph();
            writer.EndIntroduction();
        }
    }
}
