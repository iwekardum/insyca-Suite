using inSyca.foundation.framework.xml;
using inSyca.foundation.integration.biztalk.components;
using inSyca.foundation.integration.biztalk.test;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.PipelineResources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace inSyca.foundation.unittest_40
{
    [TestClass]
    public class testComponents
    {

        [TestMethod]
        public void testRemoveNilAndEmpty()
        {
            XElement xmldocument = XElement.Load(@"..\..\Testfiles\simple_002.xml");

            xElement.RemoveNilElements(xmldocument);
            xElement.RemoveEmptyElements(xmldocument);

            Console.Write(xmldocument);
        }

        [TestMethod]
        public void testXmlSplitter()
        {
            XElement xml = XElement.Load(@"..\..\Testfiles\simple_002.xml");

            string namespaceURI, rootElement;
            System.Collections.Generic.IEnumerable<IGrouping<string, XElement>> childNodes;

            XmlSplitter xmlSplitter = new XmlSplitter();

            xmlSplitter.ChildNodeName = "food";
            xmlSplitter.GroupByNodeName = "name";

            xmlSplitter.ExtractChildNodes(xml, out namespaceURI, out rootElement, out childNodes);

        }

        [TestMethod]
        public void testActiveXReader()
        {
            XElement xml = XElement.Load(@"..\..\Testfiles\simple_002.xml");

            ActiveXMessageReader amr = new ActiveXMessageReader();

            amr.IncomingEncoding = "utf-16";

            IPipelineContext pipelineContext = new PipelineContext();
            IBaseMessage inMsg = new BaseMessage();

            IBaseMessage resultMessage = amr.Execute(pipelineContext, inMsg);
        }
    }

    public class PipelineContext : IPipelineContext
    {

        public int ComponentIndex
        {
            get { throw new NotImplementedException(); }
        }

        public IDocumentSpec GetDocumentSpecByName(string DocSpecName)
        {
            throw new NotImplementedException();
        }

        public IDocumentSpec GetDocumentSpecByType(string DocType)
        {
            throw new NotImplementedException();
        }

        public Microsoft.BizTalk.Bam.EventObservation.EventStream GetEventStream()
        {
            throw new NotImplementedException();
        }

        public string GetGroupSigningCertificate()
        {
            throw new NotImplementedException();
        }

        public IBaseMessageFactory GetMessageFactory()
        {
            throw new NotImplementedException();
        }

        public Guid PipelineID
        {
            get { throw new NotImplementedException(); }
        }

        public string PipelineName
        {
            get { throw new NotImplementedException(); }
        }

        public IResourceTracker ResourceTracker
        {
            get { throw new NotImplementedException(); }
        }

        public Guid StageID
        {
            get { throw new NotImplementedException(); }
        }

        public int StageIndex
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class BaseMessage : IBaseMessage
    {
        public void AddPart(string partName, IBaseMessagePart part, bool bBody)
        {
            throw new NotImplementedException();
        }

        public IBaseMessagePart BodyPart
        {
            get { throw new NotImplementedException(); }
        }

        public string BodyPartName
        {
            get { throw new NotImplementedException(); }
        }

        public IBaseMessageContext Context
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Exception GetErrorInfo()
        {
            throw new NotImplementedException();
        }

        public IBaseMessagePart GetPart(string partName)
        {
            throw new NotImplementedException();
        }

        public IBaseMessagePart GetPartByIndex(int index, out string partName)
        {
            throw new NotImplementedException();
        }

        public void GetSize(out ulong lSize, out bool fImplemented)
        {
            throw new NotImplementedException();
        }

        public bool IsMutable
        {
            get { throw new NotImplementedException(); }
        }

        public Guid MessageID
        {
            get { throw new NotImplementedException(); }
        }

        public int PartCount
        {
            get { throw new NotImplementedException(); }
        }

        public void RemovePart(string partName)
        {
            throw new NotImplementedException();
        }

        public void SetErrorInfo(Exception errInfo)
        {
            throw new NotImplementedException();
        }
    }
}
