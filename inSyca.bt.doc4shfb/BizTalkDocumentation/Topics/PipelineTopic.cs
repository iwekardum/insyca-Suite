using shfb.helper;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk pipelines that were documented.
    /// </summary>
    internal class PipelineTopic : TopicFile
    {
        Pipeline pipeline;

        /// <summary>
        /// Creates a new pipeline topic instance.
        /// </summary>
        /// <param name="basePath">The path the pipeline topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk pipeline names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public PipelineTopic(Pipeline _pipeline)
        {
            pipeline = _pipeline;

            Title = pipeline.FullName;
            LinkTitle = pipeline.FullName;
            LinkUri = "#" + pipeline.FullName;
            TopicType = TopicType.Pipeline;
        }

        protected override void SaveTopic(MamlWriter writer)
        {
            writer.StartIntroduction();
            writer.StartParagraph();
            writer.WriteRaw("This section contains documentation for all of the selected BizTalk pipelines.The BizTalk artifacts, and all documentation captured, has been pulled from the BizTalk Administration Console.For more information on any of the pipelines, please select one of the links below.");
            writer.EndParagraph();
            writer.EndIntroduction();
        }
    }
}
