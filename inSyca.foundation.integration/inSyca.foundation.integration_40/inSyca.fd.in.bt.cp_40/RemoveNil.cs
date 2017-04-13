using inSyca.foundation.framework;
using inSyca.foundation.integration.biztalk.components.diagnostics;
using Microsoft.BizTalk.Component.Interop;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;
using Microsoft.BizTalk.Message.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Xml;
using System.Xml.Linq;
using System.ComponentModel;
using System.Globalization;
using System.Collections;
using System.Drawing;

namespace inSyca.foundation.integration.biztalk.components
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_DisassemblingParser)]
    [ComponentCategory(CategoryTypes.CATID_AssemblingSerializer)]
    [System.Runtime.InteropServices.Guid("6178B9F0-8684-4ba2-87B4-8336D70BD4F7")]
    public class RemoveNil : IBaseComponent, IComponent, IDisassemblerComponent, IAssemblerComponent, IComponentUI, IPersistPropertyBag
    {
        static private readonly ResourceManager _resourceManager = new ResourceManager("inSyca.foundation.integration.biztalk.components.Properties.Resources", Assembly.GetExecutingAssembly());

        //Used to hold disassembled messages
        private System.Collections.Queue qOutputMsgs = new System.Collections.Queue();
        private string systemPropertiesNamespace = @"http://schemas.microsoft.com/BizTalk/2003/system-properties";

        /// <summary>
        /// Default constructor
        /// </summary>
        public RemoveNil()
        {
        }

        #region IBaseComponent Members

        /// <summary>
        /// Name of the component
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get
            {
                return _resourceManager.GetString("removeNil_name", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Version of the component
        /// </summary>
        [Browsable(false)]
        public string Version
        {
            get
            {
                return _resourceManager.GetString("removeNil_version", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Description of the component
        /// </summary>
        [Browsable(false)]
        public string Description
        {
            get
            {
                return _resourceManager.GetString("removeNil_description", CultureInfo.InvariantCulture);
            }
        }
        #endregion        /// <summary>

        #region IPersistPropertyBag Members
        /// <summary>
        /// Class GUID
        /// </summary>
        public void GetClassID(out Guid classID)
        {
            classID = new Guid("ACC3F15A-C389-4a9d-8F8E-2AB51CDC4C19");
        }

        /// <summary>
        /// InitNew
        /// </summary>
        public void InitNew()
        {
        }

        /// <summary>
        /// Load property from property bag
        /// </summary>
        public void Load(IPropertyBag propertyBag, int errorLog)
        {
        }

        /// <summary>
        /// Write property to property bag
        /// </summary>
        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
        }
        #endregion

        #region IComponentUI Members

        [Browsable(false)]
        public IntPtr Icon
        {
            get
            {
                return Properties.Resources.cog.Handle;
            }
        }


        public IEnumerator Validate(object projectSystem)
        {
            return null;
        }

        #endregion

        #region IDisassemblerComponent Members
        /// <summary>
        /// remove xsi:nil elements
        /// </summary>
        public void Disassemble(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            RemoveNilAndEmptyElements(pContext, pInMsg);
        }

        /// <summary>
        /// Used to pass output messages`to next stage
        /// </summary>
        public IBaseMessage GetNext(IPipelineContext pContext)
        {
            if (qOutputMsgs.Count > 0)
                return (IBaseMessage)qOutputMsgs.Dequeue();
            else
                return null;
        }

        public void AddDocument(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            RemoveNilAndEmptyElements(pContext, pInMsg);
        }

        #endregion

        #region IAssemblerComponent Members

        public IBaseMessage Assemble(IPipelineContext pContext)
        {
            if (qOutputMsgs.Count > 0)
            {
                IBaseMessage msg = (IBaseMessage)qOutputMsgs.Dequeue();
                return msg;
            }
            else
                return null;
        }

        #endregion

        #region IComponent Members

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            pInMsg.GetPart("Data");
            pInMsg.GetPart("Header");


            throw new NotImplementedException();
        }

        #endregion

        #region Helper

        /// <summary>
        /// Queue outgoing messages
        /// </summary>
        private void CreateOutgoingMessage(IBaseMessage pInMsg, IPipelineContext pContext, String messageString, string namespaceURI, string rootElement)
        {
            Log.DebugFormat("CreateOutgoingMessage(IBaseMessage pInMsg {0}, IPipelineContext pContext {1}, String messageString {2}, string namespaceURI {3}, string rootElement {4})", pInMsg, pContext, messageString, namespaceURI, rootElement);

            IBaseMessage outMsg;

            try
            {
                //create outgoing message
                outMsg = pContext.GetMessageFactory().CreateMessage();
                outMsg.Context = pInMsg.Context;
                outMsg.AddPart("Body", pContext.GetMessageFactory().CreateMessagePart(), true);
                outMsg.Context.Promote("MessageType", systemPropertiesNamespace, namespaceURI + "#" + rootElement);
                byte[] bufferOutgoingMessage = System.Text.UTF8Encoding.UTF8.GetBytes(messageString);
                outMsg.BodyPart.Data = new MemoryStream(bufferOutgoingMessage);
                qOutputMsgs.Enqueue(outMsg);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { pInMsg, pContext, messageString, namespaceURI, rootElement }, ex));
            }
        }

        private void RemoveNilAndEmptyElements(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            Log.DebugFormat("RemoveNilAndEmptyElements(IPipelineContext pContext {0}, IBaseMessage pInMsg {1})", pInMsg, pContext);

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
                XDocument xml = XDocument.Load(xmlReader);

                //fetch namespace and root element
                string namespaceURI = xml.Document.Root.Name.NamespaceName;
                string rootElement = xml.Document.Root.Name.LocalName;


                XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";

                IEnumerable<XElement> nills = from n in xml.Descendants()
                                              where n.Attribute(ns + "nil") != null
                                              select n;

                nills.ToList().ForEach(x => x.Remove());

                IEnumerable<XElement> empties;
                do
                {
                    empties = from n in xml.Descendants()
                              where n.IsEmpty || string.IsNullOrEmpty(n.Value)
                              select n;

                    empties.ToList().ForEach(x => x.Remove());
                }
                while (empties.Count() > 0);

                CreateOutgoingMessage(pInMsg, pContext, xml.ToString(), namespaceURI, rootElement);

                Log.DebugFormat("RemoveNilAndEmptyElements(IPipelineContext pContext {0}, IBaseMessage pInMsg {1})\nxmlTemp: {2}", pInMsg, pContext, xml.ToString());
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

        #endregion
    }
}
