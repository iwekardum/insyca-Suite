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
    internal class SettingsTopic : TopicFile
    {
        /// <summary>
        /// Creates a new application topic instance.
        /// </summary>
        /// <param name="basePath">The path the application topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk application names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public SettingsTopic()
        {
            Title = "Plattform Settings";
            LinkTitle = "Plattform Settings";
            LinkUri = "##Settings";
            TopicType = TopicType.Settings;
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
