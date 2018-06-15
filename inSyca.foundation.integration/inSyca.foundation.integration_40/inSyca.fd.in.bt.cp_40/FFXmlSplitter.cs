using inSyca.foundation.framework;
using inSyca.foundation.integration.biztalk.components.diagnostics;
using Microsoft.BizTalk.Component;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Xml;
using System.Xml.Linq;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;

namespace inSyca.foundation.integration.biztalk.components
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_DisassemblingParser)]
    [System.Runtime.InteropServices.Guid("6118A9F0-8684-4ba2-87B4-8336D70BD4F7")]
    public class FFXmlSplitter : FFDasmComp, IBaseComponent, IDisassemblerComponent, IComponentUI, IPersistPropertyBag
    {
        static private readonly ResourceManager _resourceManager = new ResourceManager("inSyca.foundation.integration.biztalk.components.Properties.Resources", Assembly.GetExecutingAssembly());

        //Used to hold disassembled messages
        private System.Collections.Queue qOutputMsgs = null;
        private string systemPropertiesNamespace = @"http://schemas.microsoft.com/BizTalk/2003/system-properties";

        /// <summary>
        /// Default constructor
        /// </summary>
        public FFXmlSplitter()
        {
        }

        #region Properties
        /// <summary>
        /// Name of nodes to split
        /// </summary>
        private string _ChildNodeName;
        [Browsable(true)]
        public string ChildNodeName
        {
            get { return _ChildNodeName; }
            set { _ChildNodeName = value; }
        }

        /// <summary>
        /// Name of nodes to split
        /// </summary>
        private string _GroupByNodeName;
        [Browsable(true)]
        public string GroupByNodeName
        {
            get { return _GroupByNodeName; }
            set { _GroupByNodeName = value; }
        }
        #endregion Properties

        #region IBaseComponent Members

        /// <summary>
        /// Name of the component
        /// </summary>
        [Browsable(false)]
        new public string Name
        {
            get
            {
                return _resourceManager.GetString("ffXmlSplitter_name", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Version of the component
        /// </summary>
        [Browsable(false)]
        new public string Version
        {
            get
            {
                return _resourceManager.GetString("ffXmlSplitter_version", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Description of the component
        /// </summary>
        [Browsable(false)]
        new public string Description
        {
            get
            {
                return _resourceManager.GetString("ffXmlSplitter_description", CultureInfo.InvariantCulture);
            }
        }
        #endregion

        #region IComponentUI

        /// <summary>
        /// Component icon to use in integration Editor.
        /// </summary>
        [Browsable(false)]
        new public IntPtr Icon
        {
            get
            {
                return Properties.Resources.cog.Handle;
            }

        }

        #endregion

        #region IPersistPropertyBag
        /// <summary>
        /// Class GUID
        /// </summary>
        new public void GetClassID(out Guid classID)
        {
            classID = new Guid("ACC3F15A-C289-4a9d-8F8E-2A951CDC4C19");
        }

        /// <summary>
        /// Load property from property bag
        /// </summary>
        new public void Load(IPropertyBag propertyBag, int errorLog)
        {
            object _cnn = null;
            object _gbnn = null;

            try
            {
                propertyBag.Read("ChildNodeName", out _cnn, 0);
                propertyBag.Read("GroupByNodeName", out _gbnn, 0);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { propertyBag, errorLog }, ex));
            }

            if (_cnn != null)
                _ChildNodeName = (string)_cnn;
            else
                _ChildNodeName = string.Empty;

            if (_gbnn != null)
                _GroupByNodeName = (string)_gbnn;
            else
                _GroupByNodeName = string.Empty;

            base.Load(propertyBag, errorLog);
        }

        /// <summary>
        /// Write property to property bag
        /// </summary>
        new public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            object _cnn = (object)ChildNodeName;
            propertyBag.Write("ChildNodeName", ref _cnn);
            object _gbnn = (object)GroupByNodeName;
            propertyBag.Write("GroupByNodeName", ref _gbnn);

            base.Save(propertyBag, clearDirty, saveAllProperties);
        }

        #endregion

        #region IDisassemblerComponent
        /// <summary>
        /// Disassembles (breaks) message into small messages as per batch size
        /// </summary>
        new public void Disassemble(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            Log.DebugFormat("Disassemble(IPipelineContext pContext {0}, IBaseMessage pInMsg {1})", pContext, pInMsg);

            base.DocumentSpecName = this.DocumentSpecName;
            base.Disassemble(pContext, pInMsg);
        }

        /// <summary>
        /// Used to pass output messages`to next stage
        /// </summary>
        public IBaseMessage GetNext(IPipelineContext pContext)
        {
            if(qOutputMsgs == null)
            {
                qOutputMsgs = new System.Collections.Queue();

                IBaseMessage pBaseMessage = base.GetNext(pContext);

                SplitXmlMessage(pContext, pBaseMessage);

                if (qOutputMsgs.Count == 0)
                    return null;
            }

            if (qOutputMsgs.Count > 0)
            {
                Log.DebugFormat("_msgs.Count {0}", qOutputMsgs.Count);

                return ((IBaseMessage)(qOutputMsgs.Dequeue()));
            }
            else
            {
                Log.DebugFormat("Queue empty");
                return null;
            }
        }


        #endregion

        #region Helper
        private void SplitXmlMessage(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            XmlReader xmlReader = null;

            try
            {
                xmlReader = new XmlTextReader(pInMsg.BodyPart.GetOriginalDataStream());
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { pContext, pInMsg }, ex));
            }

            try
            {
                XElement xml = XElement.Load(xmlReader);

                string namespaceURI, rootElement;
                System.Collections.Generic.IEnumerable<IGrouping<string, XElement>> childNodes;

                ExtractChildNodes(xml, out namespaceURI, out rootElement, out childNodes);

                Log.DebugFormat("Disassemble(IPipelineContext pContext {0}, IBaseMessage pInMsg {1})\nnamespaceURI: {2}, rootElement: {3}, ChildNodeName: {4}, GroupByNodeName: {5}", pContext, pInMsg, namespaceURI, rootElement, ChildNodeName, GroupByNodeName);

                XElement xmlTemp = new XElement(xml);

                foreach (var childNodeGroup in childNodes)
                {
                    Log.DebugFormat("Disassemble(IPipelineContext pContext {0}, IBaseMessage pInMsg {1})\nKey: {2}", pContext, pInMsg, childNodeGroup.Key);

                    xmlTemp.RemoveNodes();

                    // Nested foreach is required to access group items. 
                    foreach (var childNode in childNodeGroup)
                        xmlTemp.Add(childNode);

                    CreateOutgoingMessage(pInMsg, pContext, xmlTemp.ToString(), namespaceURI, rootElement);

                    Log.DebugFormat("Disassemble(IPipelineContext pContext {0}, IBaseMessage pInMsg {1})\nxmlTemp: {2}", pContext, pInMsg, xmlTemp.ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { pContext, pInMsg }, ex));
            }
            finally
            {
                xmlReader = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="namespaceURI"></param>
        /// <param name="rootElement"></param>
        /// <param name="childNodes"></param>
        public void ExtractChildNodes(XElement xml, out string namespaceURI, out string rootElement, out System.Collections.Generic.IEnumerable<IGrouping<string, XElement>> childNodes)
        {
            //fetch namespace and root element
            namespaceURI = xml.Name.NamespaceName;
            rootElement = xml.Name.LocalName;

            XNamespace xmlns = xml.GetDefaultNamespace();

            XName childNodeName;
            XName groupByNodeName;

            if (string.IsNullOrEmpty(xmlns.NamespaceName))
            {
                childNodeName = ChildNodeName;
                groupByNodeName = GroupByNodeName;
            }
            else
            {
                childNodeName = xmlns + ChildNodeName;
                groupByNodeName = xmlns + GroupByNodeName;
            }
            childNodes = from s in xml.Descendants(childNodeName)
                         group s by (string)s.Element(groupByNodeName);
        }

        /// <summary>
        /// Queue outgoing messages
        /// </summary>
        private void CreateOutgoingMessage(IBaseMessage pInMsg, IPipelineContext pContext, String messageString, string namespaceURI, string rootElement)
        {
            Log.DebugFormat("CreateOutgoingMessage(IBaseMessage pInMsg {0}, IPipelineContext pContext{1}, String messageString {2}, string namespaceURI {3}, string rootElement {4})", pInMsg, pContext, messageString, namespaceURI, rootElement);

            IBaseMessage outMsg;

            try
            {
                //create outgoing message
                outMsg = pContext.GetMessageFactory().CreateMessage();
                outMsg.Context = pInMsg.Context;
                outMsg.AddPart("Body", pContext.GetMessageFactory().CreateMessagePart(), true);
                outMsg.Context.Promote("MessageType", systemPropertiesNamespace, namespaceURI + "#" + rootElement.Replace("ns0:", ""));
                byte[] bufferOutgoingMessage = System.Text.UTF8Encoding.UTF8.GetBytes(messageString);
                outMsg.BodyPart.Data = new MemoryStream(bufferOutgoingMessage);
                qOutputMsgs.Enqueue(outMsg);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { pInMsg, pContext, messageString, namespaceURI, rootElement }, ex));
            }
        }
        #endregion
    }
}
