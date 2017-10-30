using shfb.helper;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk adapters that were documented.
    /// </summary>
    internal class AdapterTopic : TopicFile
    {
        ProtocolType adapter;

        /// <summary>
        /// Creates a new adapter topic instance.
        /// </summary>
        /// <param name="basePath">The path the adapter topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk adapter names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public AdapterTopic(ProtocolType _adapter)
        {
            adapter = _adapter;

            Title = adapter.Name;
            LinkTitle = adapter.Name;
            LinkUri = "#" + adapter.Name;
            TopicType = TopicType.Adapter;
        }

        protected override void SaveTopic(MamlWriter writer)
        {
            writer.StartIntroduction();
            writer.StartParagraph();
            writer.WriteRaw("This section contains documentation for all of the selected BizTalk adapters.The BizTalk artifacts, and all documentation captured, has been pulled from the BizTalk Administration Console.For more information on any of the adapters, please select one of the links below.");
            writer.EndParagraph();
            writer.EndIntroduction();
        }
    }
}
