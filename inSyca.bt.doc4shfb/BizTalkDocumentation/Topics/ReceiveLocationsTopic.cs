using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using shfb.helper;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk applications that were documented.
    /// </summary>
    internal class ReceiveLocationsTopic : TopicFile
    {
        public ReceiveLocationsTopic()
        {
            Title = "ReceiveLocations";
            LinkTitle = "ReceiveLocations";
            LinkUri = "##ReceiveLocations";
            TopicType = TopicType.ReceiveLocations;
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
