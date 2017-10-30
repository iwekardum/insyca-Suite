using shfb.helper;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk receivePorts that were documented.
    /// </summary>
    internal class ReceivePortTopic : TopicFile
    {
        ReceivePort receivePort;

        /// <summary>
        /// Creates a new receivePort topic instance.
        /// </summary>
        /// <param name="basePath">The path the receivePort topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk receivePort names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public ReceivePortTopic(ReceivePort _receivePort)
        {
            receivePort = _receivePort;

            Title = receivePort.Name;
            LinkTitle = receivePort.Name;
            LinkUri = "#" + receivePort.Name;
            TopicType = TopicType.ReceivePort;
        }

        protected override void SaveTopic(MamlWriter writer)
        {
            writer.StartIntroduction();
            writer.StartParagraph();
            writer.WriteRaw("This section contains documentation for all of the selected BizTalk receivePorts.The BizTalk artifacts, and all documentation captured, has been pulled from the BizTalk Administration Console.For more information on any of the receivePorts, please select one of the links below.");
            writer.EndParagraph();
            writer.EndIntroduction();
        }
    }
}
