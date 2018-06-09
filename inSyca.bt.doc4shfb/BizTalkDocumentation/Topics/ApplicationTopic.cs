using shfb.helper;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk applications that were documented.
    /// </summary>
    internal class ApplicationTopic : TopicFile
    {
        Application application;

        /// <summary>
        /// Creates a new application topic instance.
        /// </summary>
        /// <param name="basePath">The path the application topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk application names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public ApplicationTopic(Application _application)
        {
            application = _application;

            Title = application.Name;
            LinkTitle = application.Name;
            LinkUri = "#" + application.Name;
            TopicType = TopicType.Application;
            tokenId = CleanAndPrep(application.Name);
            TokenFile.GetTokenFile().AddTopicToken(tokenId, Id);
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
