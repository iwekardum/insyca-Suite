using shfb.helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace BizTalkDocumentation
{
    /// <summary>
    /// The base class for all topic files.
    /// </summary>
    internal abstract class TopicFile: IObservable<TopicFile>, IDisposable
    {
        /// <summary>
        /// The loop result from the <see cref="Parallel"/> operation performed in each topic.
        /// </summary>
        protected ParallelLoopResult loopResult;
        /// <summary>
        /// The topic observer.
        /// </summary>
        /// 
        protected IObserver<TopicFile> observer;
        /// <summary>
        /// The XLink namespace.
        /// </summary>
        protected static XNamespace xlink = "http://www.w3.org/1999/xlink";
        /// <summary>
        /// The xmlns namespace.
        /// </summary>
        protected static XNamespace xmlns = "http://ddue.schemas.microsoft.com/authoring/2003/5";
        /// <summary>
        /// The application name.
        /// </summary>
        protected string appName;
        /// <summary>
        /// The path to the document.
        /// </summary>
        protected string topicRelativePath;
        /// <summary>
        /// Indicates the topic is ready to be saved.
        /// </summary>
        public bool ReadyToSave;
        /// <summary>
        /// Indicates whether a <see cref="Parallel"/> task is being performed.
        /// </summary>
        public bool UsingParallel;
        /// <summary>
        /// The topic's token ID.
        /// </summary>
        protected string tokenId;

        private readonly Stopwatch watch;

        /// <summary>
        /// The topic ID.
        /// </summary>
        public string Id { get; set; }
        public string Title { get; set; }
        public string LinkUri { get; set; }
        public string LinkIdUri { get; set; }
        public string LinkTitle { get; set; }
        public TopicType TopicType { get; set; }
        public string Namespace { get; set; }
        //public XmlSchemaObject SchemaObject { get; set; }
        public List<TopicFile> Children { get; private set; }
        public List<string> KeywordsK { get; private set; }
        public List<String> KeywordsF { get; private set; }
        public string FileName { get; set; }

        /// <summary>
        /// Creates a new <see cref="TopicFile"/> instance.
        /// </summary>
        public TopicFile()
        {
            Children = new List<TopicFile>();
            KeywordsK = new List<string>();
            KeywordsF = new List<string>();

            //Id = Guid.NewGuid().ToString();
            //var topic = new XElement("topic");
            //topic.SetAttributeValue("Id", Id);
            //topic.SetAttributeValue("revision", "1");
            //doc = new XDocument(
            //    new XElement("topic",
            //                 new XAttribute("Id", Id),
            //                 new XAttribute("revision", "1")));

            watch = new Stopwatch();
        }

        protected abstract void SaveTopic(MamlWriter writer);
        /// <summary>
        /// Save the topic file to disk.
        /// </summary>
        public void Save()
        {
            TimerStart();

            using (var stream = File.Create(FileName))
            using (var writer = new MamlWriter(stream))
            {
                writer.StartTopic(Id);
                SaveTopic(writer);
                writer.EndTopic();
            }

            TimerStop();
        }

        /// <summary>
        /// Start a stopwatch timer. 
        /// </summary>
        protected void TimerStart()
        {
            watch.Start();
            PrintLine("Started {0}", tokenId);
        }

        /// <summary>
        /// Stop the stopwatch timer.
        /// </summary>
        protected void TimerStop()
        {
            watch.Stop();
            PrintLine("{0} completed in {1} seconds.", tokenId, watch.Elapsed.TotalSeconds);
            watch.Reset();
        }

        /// <summary>
        /// Perform a trace operation.
        /// </summary>
        /// <param name="message">The format message.</param>
        /// <param name="args">The optional format arguments.</param>
        protected static void PrintLine(string message, params object[] args)
        {
            if (args == null || args.Length < 1)
            {
                Trace.WriteLine(message);
                return;
            }
            Trace.WriteLine(string.Format(message, args));
        }

        #region Implementation of IObservable<TopicFile>

        /// <summary>
        /// Subscribes an observer to the observable sequence.
        /// </summary>
        public IDisposable Subscribe(IObserver<TopicFile> observer)
        {
            this.observer = observer;
            return this;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (!UsingParallel) return;
            do
            {
                PrintLine("Blocking Dispose() call for {0} while waiting for parallel loop to complete.",tokenId);
                Thread.Sleep(100);
            } while (!loopResult.IsCompleted);
            return;
        }

        #endregion
    }
}