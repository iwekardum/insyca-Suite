using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.XPath;

using SandcastleBuilder.Utils;
using SandcastleBuilder.Utils.BuildComponent;
using SandcastleBuilder.Utils.BuildEngine;

//using XsdDocumentation.Model;
using BizTalkDocumentation.PlugIn.Properties;
using shfb.helper;

namespace BizTalkDocumentation.PlugIn
{
    using BizTalkDocumentation;
    using SandcastleBuilder.Utils.ConceptualContent;

    [HelpFileBuilderPlugInExport(
        "BizTalk Documenter",
        Copyright = BizTalkDocumentationMetadata.Copyright,
        Description = "This plug-in creates reference documentation for a BizTalk environment.",
        IsConfigurable = true,
        RunsInPartialBuild = false,
        Version = BizTalkDocumentationMetadata.Version)]
    public sealed class BizTalkDocumentationPlugIn : IPlugIn
    {
        private ExecutionPoint[] _executionPoints;
        private BuildProcess _buildProcess;
        private BizTalkPlugInConfiguration _configuration;

        public static string GetHelpFilePath()
        {
            var asm = Assembly.GetExecutingAssembly();
            Debug.Assert(asm.Location != null);
            return Path.Combine(Path.GetDirectoryName(asm.Location), "Help.chm");
        }

        /// <summary>
        /// This read-only property returns a collection of execution points
        /// that define when the plug-in should be invoked during the build
        /// process.
        /// </summary>
        public IEnumerable<ExecutionPoint> ExecutionPoints
        {
            get { return _executionPoints ?? (_executionPoints = new[] { new ExecutionPoint(BuildStep.GenerateSharedContent, ExecutionBehaviors.Before) }); }
        }

        /// <summary>
        /// This method is used by the Sandcastle Help File Builder to let the
        /// plug-in perform its own configuration.
        /// </summary>
        /// <param name="project">A reference to the active project</param>
        /// <param name="currentConfig">The current configuration XML fragment</param>
        /// <returns>A string containing the new configuration XML fragment</returns>
        /// <remarks>The configuration data will be stored in the help file
        /// builder project.</remarks>
        public string ConfigurePlugIn(SandcastleProject project, string currentConfig)
        {
            var configuration = BizTalkPlugInConfiguration.FromXml(project, currentConfig);

            using (var dlg = new BizTalkPlugInConfigurationForm(configuration))
            {
                return (dlg.ShowDialog() == DialogResult.OK)
                        ? BizTalkPlugInConfiguration.ToXml(dlg.NewConfiguration)
                        : currentConfig;
            }
        }

        /// <summary>
        /// This method is used to initialize the plug-in at the start of the
        /// build process.
        /// </summary>
        /// <param name="buildProcess">A reference to the current build
        /// process.</param>
        /// <param name="configuration">The configuration data that the plug-in
        /// should use to initialize itself.</param>
        public void Initialize(BuildProcess buildProcess, XPathNavigator configuration)
        {
            _configuration = BizTalkPlugInConfiguration.FromXml(buildProcess.CurrentProject, configuration);
            _buildProcess = buildProcess;
            _buildProcess.ReportProgress(Resources.PlugInVersionFormatted, Resources.PlugInName, BizTalkDocumentationMetadata.Version, BizTalkDocumentationMetadata.Copyright);
        }

        /// <summary>
        /// This method is used to execute the plug-in during the build process
        /// </summary>
        /// <param name="executionContext">The current execution context</param>
        public void Execute(SandcastleBuilder.Utils.BuildComponent.ExecutionContext executionContext)
        {
            _buildProcess.ReportProgress(Resources.PlugInBuildProgress);

            var configuration = new Configuration
            {
                OutputFolderPath = _buildProcess.WorkingFolder,
                BizTalkDbInstance = _configuration.BizTalkDbInstance,
                MgmtDatabaseName = _configuration.MgmtDatabaseName,
                RulesServer = _configuration.RulesServer,
                RulesDatabase = _configuration.RulesDatabase
            };

            ContentGenerator biztalkContentGenerator = new ContentGenerator(_buildProcess, configuration);
            biztalkContentGenerator.Generate();

            var contentFileProvider = new ContentFileProvider(_buildProcess.CurrentProject);

            var contentLayoutFile = contentFileProvider.Add(BuildAction.ContentLayout, biztalkContentGenerator.ContentFile);
            contentLayoutFile.SortOrder = 1;
            _buildProcess.ConceptualContent.ContentLayoutFiles.Add(contentLayoutFile);

            foreach (var topicFileName in biztalkContentGenerator.TopicFiles)
                contentFileProvider.Add(BuildAction.None, topicFileName);

            _buildProcess.ConceptualContent.Topics.Add(new TopicCollection(contentLayoutFile));
        }

        /// <summary>
        /// This implements the Dispose() interface to properly dispose of
        /// the plug-in object.
        /// </summary>
        /// <overloads>There are two overloads for this method.</overloads>
        public void Dispose()
        {
        }

        /// <summary>
        /// This replaces the temporary project file.
        /// </summary>
        private class ContentFileProvider : IContentFileProvider
        {
            private readonly IBasePathProvider basePathProvider;

            private readonly Dictionary<BuildAction, List<ContentFile>> data = new Dictionary<BuildAction, List<ContentFile>>();

            public ContentFileProvider(IBasePathProvider basePathProvider)
            {
                this.basePathProvider = basePathProvider;
            }

            public IEnumerable<ContentFile> ContentFiles(BuildAction buildAction)
            {
                return this.data[buildAction];
            }

            public ContentFile Add(BuildAction buildAction, string fileName)
            {
                List<ContentFile> itemsForAction;
                if (!this.data.TryGetValue(buildAction, out itemsForAction))
                {
                    itemsForAction = new List<ContentFile>();
                    this.data.Add(buildAction, itemsForAction);
                }

                var contenFile = this.CreateContentFile(fileName);

                itemsForAction.Add(contenFile);
                return contenFile;
            }

            private ContentFile CreateContentFile(string fileName)
            {
                var filePath = new FilePath(fileName, this.basePathProvider);
                var contentFile = new ContentFile(filePath);
                contentFile.LinkPath = filePath; // TODO: do we need this?
                contentFile.ContentFileProvider = this;
                return contentFile;
            }
        }
    }
}