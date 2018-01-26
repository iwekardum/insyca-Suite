using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Microsoft.BizTalk.ExplorerOM;
using BizTalkDocumentation.Helpers;

namespace BizTalkDocumentation
{
    internal sealed class TopicManager : Manager
    {

        private Dictionary<BaseObject, TopicFile> _topicDictionary = new Dictionary<BaseObject, TopicFile>();
        private Dictionary<string, TopicFile> _topicUriIndex = new Dictionary<string, TopicFile>();

        public TopicManager(Context context)
            : base(context)
        {
        }

        public override void Initialize()
        {
            var bizTalkTopicBuilder = new BizTalkTopicBuilder(Context, _topicDictionary);
            bizTalkTopicBuilder.Parse();
            Topics = bizTalkTopicBuilder.GetRootTopics();

            //SetTopicIds();
            SetKeywords(Topics);
            GenerateTopicUriIndex(Topics);
        }

        public List<TopicFile> Topics { get; private set; }

        private void SetTopicIds()
        {
            var guidsInUse = new HashSet<Guid>();
            using (var md5 = HashAlgorithm.Create("MD5"))
                SetTopicIds(Topics, md5, guidsInUse);
        }

        private static void SetTopicIds(IEnumerable<TopicFile> topics, HashAlgorithm algorithm, HashSet<Guid> guidsInUse)
        {
            foreach (var topic in topics)
            {
                    var input = Encoding.UTF8.GetBytes(topic.LinkUri);
                    var output = algorithm.ComputeHash(input);
                    var guid = new Guid(output);
                    while (!guidsInUse.Add(guid))
                        guid = Guid.NewGuid();
                    topic.Id = guid.ToString();

                    SetTopicIds(topic.Children, algorithm, guidsInUse);
            }
        }

        private void SetKeywords(IEnumerable<TopicFile> topics)
        {
            foreach (var topic in topics)
            {
                switch (topic.TopicType)
                {
                    case TopicType.Application:
                    case TopicType.Assembly:
                    case TopicType.BizTalk:
                    case TopicType.Map:
                    case TopicType.Orchestration:
                    case TopicType.ReceivePort:
                    case TopicType.Schema:
                    case TopicType.SendPort:
                        AddKeywordK(topic, topic.Title);
                        break;
                }

                SetKeywords(topic.Children);
            }
        }

        private static void AddKeywordK(TopicFile topic, string keyword)
        {
            topic.KeywordsK.Add(keyword);
        }

        private static void AddKeywordF(TopicFile topic, string keyword)
        {
            topic.KeywordsF.Add(keyword);
        }

        private static void AddKeywordF(TopicFile topic, XmlQualifiedName qualifiedName)
        {
            if (string.IsNullOrEmpty(qualifiedName.Namespace))
                return;

            var keyword = string.Format("{0}#{1}", qualifiedName.Namespace, qualifiedName.Name);
            AddKeywordF(topic, keyword);
        }

        private void GenerateTopicUriIndex(IEnumerable<TopicFile> topics)
        {
            foreach (var topic in topics)
            {
                AddTopicUriToIndex(topic, topic.LinkUri);
                AddTopicUriToIndex(topic, topic.LinkIdUri);

                GenerateTopicUriIndex(topic.Children);
            }
        }

        private void AddTopicUriToIndex(TopicFile topic, string uri)
        {
            if (string.IsNullOrEmpty(uri))
                return;

            if (!_topicUriIndex.ContainsKey(uri))
                _topicUriIndex.Add(uri, topic);
        }
    }
}