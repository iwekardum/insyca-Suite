using shfb.helper;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk sendPortGroups that were documented.
    /// </summary>
    internal class SendPortGroupTopic : TopicFile
    {
        SendPortGroup sendPortGroup;

        /// <summary>
        /// Creates a new sendPortGroup topic instance.
        /// </summary>
        /// <param name="basePath">The path the sendPortGroup topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk sendPortGroup names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public SendPortGroupTopic(SendPortGroup _sendPortGroup)
        {
            sendPortGroup = _sendPortGroup;

            Title = sendPortGroup.Name;
            LinkTitle = sendPortGroup.Name;
            LinkUri = "#" + sendPortGroup.Name;
            TopicType = TopicType.SendPortGroup;
        }
        
        protected override void SaveTopic(MamlWriter writer)
        {
            writer.StartIntroduction();
            writer.StartParagraph();
            writer.WriteRaw("This section contains documentation for all of the selected BizTalk sendPortGroups.The BizTalk artifacts, and all documentation captured, has been pulled from the BizTalk Administration Console.For more information on any of the sendPortGroups, please select one of the links below.");
            writer.EndParagraph();
            writer.EndIntroduction();
        }
    }
}
