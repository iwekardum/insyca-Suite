///
///
///
///
///
///
///
///
///
///
///
///
///

using inSyca.foundation.integration.biztalk.components.diagnostics;
using Microsoft.BizTalk.Component;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;

namespace inSyca.foundation.integration.biztalk.components
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Decoder)]
    [System.Runtime.InteropServices.Guid("48BEC85A-20EE-40ad-BFD0-319B59A0DDBC")]
    public class ActiveXMessageReader : IBaseComponent, IComponent, IPersistPropertyBag, IComponentUI, IProbeMessage
    {
        static private readonly ResourceManager _resourceManager = new ResourceManager("inSyca.foundation.integration.biztalk.components.Properties.Resources", Assembly.GetExecutingAssembly());

        static string IncomingEncodingLabel = "IncomingEncoding";

        #region Properties

        [Description("IncomingEncoding")]
        [DisplayName("IncomingEncoding")]
        [DefaultValue("utf-16")]
        public string IncomingEncoding { get; set; }

        #endregion

        #region IBaseComponent Members

        /// <summary>
        /// Description of the component
        /// </summary>
        [Browsable(false)]
        public string Description
        {
            get
            {
                return _resourceManager.GetString("activeXMessageReader_description", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Name of the component
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get
            {
                return _resourceManager.GetString("activeXMessageReader_name", CultureInfo.InvariantCulture);
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
                return _resourceManager.GetString("activeXMessageReader_version", CultureInfo.InvariantCulture);
            }
        }

        #endregion

        #region IPersistPropertyBag Members

        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classID">
        /// Class ID of the component
        /// </param>
        public void GetClassID(out Guid classID)
        {
            classID = new System.Guid("48BEC85A-20EE-40ad-BFD0-319B59A0DDBC");

            Log.DebugFormat("GetClassID(out Guid {0})", classID);
        }

        /// <summary>
        /// not implemented
        /// </summary>
        public void InitNew()
        {
            Log.Debug("InitNew()");
        }

        /// <summary>
        /// Loads configuration properties for the component
        /// </summary>
        /// <param name="propertyBag">Configuration property bag</param>
        /// <param name="errorLog">Error status</param>
        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            Log.DebugFormat("Load(IPropertyBag propertyBag {0} , int errorLog {1})", propertyBag, errorLog);

            using (DisposableObjectWrapper wrapper = new DisposableObjectWrapper(propertyBag))
            {
                object val = null;

                val = PropertyHelper.ReadPropertyBag(propertyBag, IncomingEncodingLabel);

                if (val != null)
                    IncomingEncoding = ((string)(val));
                else
                    this.IncomingEncoding = "utf-16";

            }

            Log.DebugFormat("Load IncomingEncoding {0}", IncomingEncoding);
        }

        /// <summary>
        /// Saves the current component configuration into the property bag
        /// </summary>
        /// <param name="propertyBag">Configuration property bag</param>
        /// <param name="clearDirty">not used</param>
        /// <param name="saveAllProperties">not used</param>
        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            Log.DebugFormat("Save(IPropertyBag propertyBag {0}, bool clearDirty {1}, bool saveAllProperties {2})", propertyBag, clearDirty, saveAllProperties);

            using (DisposableObjectWrapper wrapper = new DisposableObjectWrapper(propertyBag))
            {
                object val = null;

                val = IncomingEncoding;
                propertyBag.Write(IncomingEncodingLabel, val);
            }

            Log.DebugFormat("Save IncomingEncodingLabel {0}", IncomingEncodingLabel);
        }

        #endregion

        #region IComponentUI members
        /// <summary>
        /// Component icon to use in BizTalk Editor
        /// </summary>
        public IntPtr Icon
        {
            get
            {
                return Properties.Resources.cog.Handle;
            }
        }

        /// <summary>
        /// The Validate method is called by the BizTalk Editor during the build 
        /// of a BizTalk project.
        /// </summary>
        /// <param name="obj">An Object containing the configuration properties.</param>
        /// <returns>The IEnumerator enables the caller to enumerate through a collection of strings containing error messages. These error messages appear as compiler error messages. To report successful property validation, the method should return an empty enumerator.</returns>
        public IEnumerator Validate(object obj)
        {
            // example implementation:
            // ArrayList errorList = new ArrayList();
            // errorList.Add("This is a compiler error");
            // return errorList.GetEnumerator();
            return null;
        }
        #endregion

        #region IComponent Members

        /// <summary>
        /// Implements IComponent.Execute method.
        /// </summary>
        /// <param name="pipelineContext">Pipeline context</param>
        /// <param name="inMsg">Input message</param>
        /// <returns>Original input message</returns>
        /// <remarks>
        /// IComponent.Execute method is used to initiate
        /// the processing of the message in this pipeline component.
        /// </remarks>
        public IBaseMessage Execute(IPipelineContext pipelineContext, IBaseMessage inMsg)
        {
            Log.DebugFormat("Execute(IPipelineContext pipelineContext {0}, IBaseMessage inMsg {1})", pipelineContext, inMsg);

            inMsg.BodyPart.Charset = IncomingEncoding;
            return inMsg;
        }

        #endregion

        #region IProbeMessage Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelineContext"></param>
        /// <param name="inMsg"></param>
        /// <returns></returns>
        public bool Probe(IPipelineContext pipelineContext, IBaseMessage inMsg)
        {
            return null != inMsg;
        }

        #endregion
    }
}