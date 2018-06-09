using shfb.helper;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk maps that were documented.
    /// </summary>
    internal class MapTopic : TopicFile
    {
        Transform map;

        /// <summary>
        /// Creates a new map topic instance.
        /// </summary>
        /// <param name="basePath">The path the map topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk map names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public MapTopic(Transform _map)
        {
            map = _map;

            Title = map.FullName;
            LinkTitle = map.FullName;
            LinkUri = "#" + map.FullName;
            TopicType = TopicType.Map;
        }

        protected override void SaveTopic(MamlWriter writer)
        {
            writer.StartIntroduction();
            writer.StartParagraph();
            writer.WriteRaw(string.IsNullOrEmpty(map.Description) ? "No description was found for the schema." : map.Description);
            writer.EndParagraph();
            writer.EndIntroduction();
        }
    }
}
