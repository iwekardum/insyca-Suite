using shfb.helper;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk businessRules that were documented.
    /// </summary>
    internal class BusinessRuleTopic : TopicFile
    {
        Policy businessRule;

        /// <summary>
        /// Creates a new businessRule topic instance.
        /// </summary>
        /// <param name="basePath">The path the businessRule topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk businessRule names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public BusinessRuleTopic(Policy _businessRule)
        {
            businessRule = _businessRule;

            Title = businessRule.Name;
            LinkTitle = businessRule.Name;
            LinkUri = "#" + businessRule.Name;
            TopicType = TopicType.BusinessRule
                ;
        }

        protected override void SaveTopic(MamlWriter writer)
        {
            writer.StartIntroduction();
            writer.StartParagraph();
            writer.WriteRaw("This section contains documentation for all of the selected BizTalk businessRules.The BizTalk artifacts, and all documentation captured, has been pulled from the BizTalk Administration Console.For more information on any of the businessRules, please select one of the links below.");
            writer.EndParagraph();
            writer.EndIntroduction();
        }
    }
}
