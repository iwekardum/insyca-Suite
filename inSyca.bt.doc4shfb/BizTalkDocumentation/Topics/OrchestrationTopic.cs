using shfb.helper;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk orchestrations that were documented.
    /// </summary>
    internal class OrchestrationTopic : TopicFile
    {
        BtsOrchestration orchestration;

        /// <summary>
        /// Creates a new orchestration topic instance.
        /// </summary>
        /// <param name="basePath">The path the orchestration topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk orchestration names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public OrchestrationTopic(BtsOrchestration _orchestration)
        {
            orchestration = _orchestration;

            Title = orchestration.FullName;
            LinkTitle = orchestration.FullName;
            LinkUri = "#" + orchestration.FullName;
            TopicType = TopicType.Orchestration;
        }

        protected override void SaveTopic(MamlWriter writer)
        {
            writer.StartIntroduction();
            writer.StartParagraph();
            writer.WriteRaw(!string.IsNullOrEmpty(orchestration.Description) ? orchestration.Description : "No description was available from the orchestration.");
            writer.EndParagraph();
            writer.EndIntroduction();
        }
    }
}
