using System.Collections.Generic;

namespace BizTalkDocumentation
{
    internal class TopicBuilder
    {
        private Stack<List<TopicFile>> _topicStack = new Stack<List<TopicFile>>();
        private Stack<string> _topicUriStack = new Stack<string>();

        public TopicBuilder()
        {
        }

        public List<TopicFile> GetRootTopics()
        {
            return _topicStack.Count == 0
                       ? new List<TopicFile>()
                       : _topicStack.Pop();
        }

        protected TopicFile AddTopic(TopicFile topic)
        {
            if (_topicStack.Count == 0)
            {
                var root = new List<TopicFile>();
                _topicStack.Push(root);
            }

            _topicStack.Peek().Add(topic);

            return topic;
        }
    }
}