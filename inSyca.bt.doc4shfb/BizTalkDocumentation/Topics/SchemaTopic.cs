using shfb.helper;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk schemas that were documented.
    /// </summary>
    internal class SchemaTopic : TopicFile
    {
        Schema schema;

        /// <summary>
        /// Creates a new schema topic instance.
        /// </summary>
        /// <param name="basePath">The path the schema topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk schema names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public SchemaTopic(Schema _schema)
        {
            schema = _schema;

            Title = schema.FullName;
            LinkTitle = schema.FullName;
            LinkUri = "#" + schema.FullName;
            TopicType = TopicType.Schema;
        }
 
        protected override void SaveTopic(MamlWriter writer)
        {
            writer.StartIntroduction();
            writer.StartParagraph();
            writer.WriteRaw(string.IsNullOrEmpty(schema.Description) ? "No description was found for the schema." : schema.Description);
            writer.EndParagraph();
            writer.EndIntroduction();
        }
    }
}
