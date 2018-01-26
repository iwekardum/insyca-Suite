using shfb.helper;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System;
using SandcastleBuilder.Utils.BuildEngine;

namespace BizTalkDocumentation
{
    public sealed class ContentGenerator
    {
        private Context _context;

        public ContentGenerator(BuildProcess _buildProcess, Configuration configuration)
        {
            _context = new Context(_buildProcess, configuration);
            MediaItems = new List<MediaItem>();
            TopicFiles = new List<string>();
        }

        public string ContentFile { get; private set; }
        public string IndexFile { get; private set; }
        public string TopicsFolder { get; private set; }
        public string tokenFile { get; private set; }
        public string MediaFolder { get; private set; }
        public List<string> TopicFiles { get; private set; }
        public List<MediaItem> MediaItems { get; private set; }

        public void Generate()
        {
            TopicsFolder = Path.Combine(_context.Configuration.OutputFolderPath, "btDocTopics");
            ContentFile = Path.Combine(TopicsFolder, "btDoc.content");
            IndexFile = Path.Combine(TopicsFolder, "btDoc.index");
            tokenFile = Path.Combine(_context.Configuration.OutputFolderPath, "btDoc.tokens");
            MediaFolder = Path.Combine(_context.Configuration.OutputFolderPath, "btDocMedia");
            GenerateIndex();
            GenerateContentFile();
            GenerateTopicFiles();
            GenerateMediaFiles();
            TokenFile.GetTokenFile().Save(tokenFile);
        }

        private void GenerateIndex()
        {
            var topicIndex = new TopicIndex();
            topicIndex.Load(_context.TopicManager);
            topicIndex.Save(IndexFile);
        }

        private void GenerateTopicFiles()
        {
            Directory.CreateDirectory(TopicsFolder);

            GenerateTopicFiles(_context.TopicManager.Topics);
        }

        private void GenerateTopicFiles(IEnumerable<TopicFile> topics)
        {
            foreach (var topic in topics)
            {
                topic.FileName = GetAbsoluteFileName(TopicsFolder, topic);
                TopicFiles.Add(topic.FileName);

                topic.Save();

                GenerateTopicFiles(topic.Children);
            }
        }

        private void GenerateContentFile()
        {
            var doc = new XmlDocument();
            var rootNode = doc.CreateElement("Topics");
            doc.AppendChild(rootNode);

            GenerateContentFileElements(rootNode, _context.TopicManager.Topics);

            var directory = Path.GetDirectoryName(ContentFile);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            doc.Save(ContentFile);
        }

        private static void GenerateContentFileElements(XmlNode parentNode, IEnumerable<TopicFile> topics)
        {
            foreach (var topic in topics)
            {
                var doc = parentNode.OwnerDocument;
                var topicElement = doc.CreateElement("Topic");
                topicElement.SetAttribute("id", topic.Id);
                topicElement.SetAttribute("visible", XmlConvert.ToString(true));
                topicElement.SetAttribute("title", topic.Title);
                parentNode.AppendChild(topicElement);

                if (topic.KeywordsK.Count > 0 ||
                    topic.KeywordsF.Count > 0)
                {
                    var helpKeywordsElement = doc.CreateElement("HelpKeywords");
                    topicElement.AppendChild(helpKeywordsElement);
                    AddKeywords(helpKeywordsElement, topic.KeywordsK, "K");
                    AddKeywords(helpKeywordsElement, topic.KeywordsF, "F");
                }
                GenerateContentFileElements(topicElement, topic.Children);
            }
        }

        private void GenerateMediaFiles()
        {
            var mediaFolder = Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), "Media");
            Directory.CreateDirectory(MediaFolder);
            //foreach (var artItem in ArtItem.ArtItems)
            //{
            //    var sourceFile = Path.Combine(mediaFolder, artItem.FileName);
            //    var destinationFile = Path.Combine(MediaFolder, artItem.FileName);
            //    File.Copy(sourceFile, destinationFile);

            //    var mediaItem = new MediaItem(artItem, destinationFile);
            //    MediaItems.Add(mediaItem);
            //}
        }

        private static void AddKeywords(XmlNode helpKeywordsElement, IEnumerable<string> keywordsF, string index)
        {
            foreach (var keywordF in keywordsF)
            {
                var helpKeywordElement = helpKeywordsElement.OwnerDocument.CreateElement("HelpKeyword");
                helpKeywordElement.SetAttribute("index", index);
                helpKeywordElement.SetAttribute("term", keywordF);
                helpKeywordsElement.AppendChild(helpKeywordElement);
            }
        }

        private static string GetAbsoluteFileName(string topicsFolder, TopicFile topic)
        {
            return Path.Combine(topicsFolder, Path.ChangeExtension(topic.Id, ".aml"));
        }
    }
}