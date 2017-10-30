using shfb.helper;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk roles that were documented.
    /// </summary>
    internal class RoleTopic : TopicFile
    {
        Role role;

        /// <summary>
        /// Creates a new role topic instance.
        /// </summary>
        /// <param name="basePath">The path the role topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk role names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public RoleTopic(Role _role)
        {
            role = _role;

            Title = role.Name;
            LinkTitle = role.Name;
            LinkUri = "#" + role.Name;
            TopicType = TopicType.Role
                ;
        }

        protected override void SaveTopic(MamlWriter writer)
        {
            writer.StartIntroduction();
            writer.StartParagraph();
            writer.WriteRaw("This section contains documentation for all of the selected BizTalk roles.The BizTalk artifacts, and all documentation captured, has been pulled from the BizTalk Administration Console.For more information on any of the roles, please select one of the links below.");
            writer.EndParagraph();
            writer.EndIntroduction();
        }
    }
}
