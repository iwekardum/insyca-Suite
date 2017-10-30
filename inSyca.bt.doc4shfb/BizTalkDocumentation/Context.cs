using SandcastleBuilder.Utils.BuildEngine;
using shfb.helper;

namespace BizTalkDocumentation
{
    internal sealed class Context
    {
        private BuildProcess BuildProcess { get; set; }

        public Context(BuildProcess _buildProcess, Configuration configuration)
        {
            BuildProcess = _buildProcess;
            Configuration = configuration;

            TopicManager = new TopicManager(this);
            TopicManager.Initialize();

        }

        public void ReportProgress(string message, params object[] args)
        {
            BuildProcess.ReportProgress(message, args);
        }

        public void ReportWarning(string warningCode, string message, params object[] args)
        {
            BuildProcess.ReportWarning(warningCode, message, args);
        }

        public Configuration Configuration { get; private set; }
        public TopicManager TopicManager { get; private set; }
    }
}