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
    internal class BizTalkTopic : TopicFile
    {
        public BizTalkTopic()
        {
            Title = "BizTalk Documentation";
            LinkTitle = "BizTalk Documentation";
            LinkUri = "##BizTalk";
            TopicType = TopicType.BizTalk;
        }

        protected override void SaveTopic(MamlWriter writer)
        {
            writer.StartIntroduction();
            writer.StartParagraph();
            writer.WriteRaw("This section contains documentation for all of the selected BizTalk applications.The BizTalk artifacts, and all documentation captured, has been pulled from the BizTalk Administration Console.For more information on any of the applications, please select one of the links below.");
            writer.EndParagraph();
            writer.EndIntroduction();
        }
    }
}
