using inSyca.foundation.framework;
using inSyca.foundation.integration.biztalk.components.diagnostics;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;

namespace inSyca.foundation.integration.biztalk.components
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_AssemblingSerializer)]
    [System.Runtime.InteropServices.Guid("a97048da-37eb-4abf-b0f1-50b55874083c")]
    public class RemoveNamespace : IAssemblerComponent, IBaseComponent, IComponentUI, IComponent, IPersistPropertyBag
    {
        //Used to hold disassembled messages
        private System.Collections.Queue qOutputMsgs = new System.Collections.Queue();
        private string systemPropertiesNamespace = @"http://schemas.microsoft.com/BizTalk/2003/system-properties";

        #region IBaseComponent
        public string Description
        {
            get
            {
                return "RemoveNamespace";
            }
        }

        public string Name
        {
            get
            {
                return "RemoveNamespace";
            }
        }

        public string Version
        {
            get
            {
                return "1.0.0.0";
            }
        }
        #endregion

        #region IComponentUI

        public IntPtr Icon
        {
            get
            {
                return new System.IntPtr();
            }
        }

        public System.Collections.IEnumerator Validate(object projectSystem)
        {
            return null;
        }

        #endregion

        #region IPersistPropertyBag



        public void GetClassID(out Guid classID)
        {
            classID = new Guid("a97048da-37eb-4abf-b0f1-50b55874083c");
        }

        public void InitNew()
        {
        }

        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            object val;

        }

        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            object val;

        }

        private static object ReadPropertyBag(IPropertyBag pb, string propName)
        {
            object val = null;
            try
            {
                pb.Read(propName, out val, 0);
            }

            catch (ArgumentException)
            {
                return val;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format(
                    "Error while reading property '{0}' from PropertyBag",
                    propName), ex);
            }
            return val;
        }

        #endregion

        #region IComponent

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helper

        private Stream TransformMessage(Stream originalStream)
        {
            Log.Debug(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { originalStream }));

            try
            {
                using (var xmlReader = new XmlTextReader(originalStream))
                {
                    XElement xElement = XElement.Load(xmlReader);

                    xElement.Descendants()
                                    .Attributes()
                                    .Where(x => x.IsNamespaceDeclaration)
                                    .Remove();

                    foreach (var descendants in xElement.Descendants())
                        descendants.Name = descendants.Name.LocalName;

                    Log.DebugFormat("TransformMessage(XElement xDocument {0})", xElement);

                    Stream newStream = new MemoryStream();
                    xElement.Save(newStream);
                    return newStream;
                }
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { originalStream }, ex));

                return null;
            }
        }
        #endregion

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
            Log.DebugFormat("Execute(IPipelineContext pContext {0}, IBaseMessage pInMsg {1})", pContext, pInMsg);

            try
            {
                if (pInMsg == null || pInMsg.BodyPart == null || pInMsg.BodyPart.Data == null)
                {
                    Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "pInMsg Error", null));
                    throw new ArgumentNullException("pInMsg Error");
                }

                Stream stream = TransformMessage(pInMsg.BodyPart.GetOriginalDataStream()); ;

                Log.DebugFormat("Execute(IPipelineContext pContext {0}, IBaseMessage pInMsg {1})\nstream.Length={2})", pContext, pInMsg, stream.Length);

                // Rewind the stream ready to read from it elsewhere
                stream.Position = 0;
                pInMsg.BodyPart.Data = stream;
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
            }

            qOutputMsgs.Enqueue(pInMsg);
        }

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
    }
}