using shfb.helper;
using Microsoft.BizTalk.ExplorerOM;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk hosts that were documented.
    /// </summary>
    internal class HostTopic : TopicFile
    {
        Host host;

        /// <summary>
        /// Creates a new host topic instance.
        /// </summary>
        /// <param name="basePath">The path the host topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk host names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public HostTopic(Host _host)
        {
            host = _host;

            Title = host.Name;
            LinkTitle = host.Name;
            LinkUri = "#" + host.Name;
            TopicType = TopicType.Host;
        }

        protected override void SaveTopic(MamlWriter writer)
        {
            writer.StartIntroduction();
            writer.StartParagraph();
            writer.WriteRaw("This section contains documentation for all of the selected BizTalk hosts.The BizTalk artifacts, and all documentation captured, has been pulled from the BizTalk Administration Console.For more information on any of the hosts, please select one of the links below.");
            writer.EndParagraph();
            writer.EndIntroduction();
        }
    }
}
