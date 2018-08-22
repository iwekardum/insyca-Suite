using inSyca.foundation.framework;
using inSyca.foundation.integration.biztalk.components.diagnostics;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Xml;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;

namespace inSyca.foundation.integration.biztalk.components
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    [ComponentCategory(CategoryTypes.CATID_Decoder)]
    [ComponentCategory(CategoryTypes.CATID_Encoder)]
    [ComponentCategory(CategoryTypes.CATID_Validate)]
    [System.Runtime.InteropServices.Guid("48BEC85A-20EE-40ad-BFD0-319B59A0DDBD")]
    public class XmlMultiPartAssembler : IBaseComponent, IComponent, IPersistPropertyBag, IComponentUI
    {
        static private readonly ResourceManager _resourceManager = new ResourceManager("inSyca.foundation.integration.biztalk.components.Properties.Resources", Assembly.GetExecutingAssembly());

        private string rootNodeName = "RootNodeName";

        public XmlMultiPartAssembler()
        {
        }

        #region Properties
        /// <summary>
        /// Location of Xsl transform file.
        /// </summary>
        public string RootNodeName
        {
            get { return rootNodeName; }
            set { rootNodeName = value; }
        }
        #endregion

        #region IBaseComponent Members

        /// <summary>
        /// Name of the component
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get
            {
                return _resourceManager.GetString("xmlMultiPartAssembler_name", CultureInfo.InvariantCulture);
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
                return _resourceManager.GetString("xmlMultiPartAssembler_version", CultureInfo.InvariantCulture);
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
                return _resourceManager.GetString("xmlMultiPartAssembler_description", CultureInfo.InvariantCulture);
            }
        }
        #endregion

        #region IPersistPropertyBag Members

        public void GetClassID(out Guid classId)
        {
            classId = new System.Guid("48BEC85A-20EE-40ad-BFD0-319B59A0DDBD");
        }


        public void InitNew()
        {
        }

        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            string val = (string)ReadPropertyBag(propertyBag, "RootNodeName");
            if (val != null) rootNodeName = val;
        }


        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            object val = (object)rootNodeName;
            WritePropertyBag(propertyBag, "RootNodeName", val);
        }


        private static object ReadPropertyBag(IPropertyBag propertyBag, string propName)
        {
            object val = null;

            try
            {
                propertyBag.Read(propName, out val, 0);
            }
            catch (ArgumentException)
            {
                return val;
            }
            catch (Exception)
            {
                throw;
            }

            return val;
        }


        private static void WritePropertyBag(IPropertyBag propertyBag, string propName, object val)
        {
            try
            {
                propertyBag.Write(propName, ref val);
            }
            catch (Exception)
            {
                throw;
            }
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

        #region IProbeMessage Members

        public bool Probe(IPipelineContext pipelineContext, IBaseMessage inMsg)
        {
            return null != inMsg;
        }

        #endregion

        #region IComponent Members

        public IBaseMessage Execute(IPipelineContext pipelineContext, IBaseMessage inMsg)
        {
            Log.DebugFormat("Execute(IPipelineContext pContext {0}, IBaseMessage pInMsg {1})", pipelineContext, inMsg);

            try
            {
                inMsg.BodyPart.Data = AssembleMessage(inMsg);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { pipelineContext, inMsg }, ex));
            }

            return inMsg;
        }

        private Stream AssembleMessage(IBaseMessage inMsg)
        {
            Log.DebugFormat("AssembleMessage(IBaseMessage inMsg {0})", inMsg);

            MemoryStream ms = new MemoryStream();

            XmlWriter _Writer = XmlWriter.Create(ms);

            _Writer.WriteStartDocument();
            _Writer.WriteStartElement(rootNodeName);

            string strPartname;

            for (int i = 0; i < inMsg.PartCount; i++)
            {
                IBaseMessagePart _BaseMessagePart = inMsg.GetPartByIndex(i, out strPartname);

                XmlReader reader = new XmlTextReader(_BaseMessagePart.Data);

                _Writer.WriteNode(reader, true);
            }

            _Writer.WriteEndElement();
            _Writer.WriteEndDocument();

            _Writer.Flush();

            ms.Position = 0;

            return ms;
        }

        #endregion
    }
}
