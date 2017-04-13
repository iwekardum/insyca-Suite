using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using inSyca.foundation.framework;
using inSyca.foundation.integration.biztalk.components.diagnostics;
using Microsoft.BizTalk.Component.Interop;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;
using Microsoft.BizTalk.Message.Interop;
using System.Globalization;

namespace inSyca.foundation.integration.biztalk.components
{
/// <summary>
/// Implements custom pipeline component to append and/or prepend data to a stream.
/// </summary>
/// <remarks>
/// FixMag class implements pipeline component that can be used in receive and
/// send integration pipelines. The pipeline component gets a data stream, appends
/// and/or prepends user specified data to it and outputs modified stream.
///</remarks>
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    [ComponentCategory(CategoryTypes.CATID_Validate)]
    [System.Runtime.InteropServices.Guid("48BEC85A-20EE-40ad-BFD0-319B59A0DDBD")]
    public class UTF_8_wo_BOM : BaseCustomTypeDescriptor, IBaseComponent, IComponent, IPersistPropertyBag, IComponentUI
    {
        static private readonly ResourceManager _resourceManager = new ResourceManager("inSyca.foundation.integration.biztalk.components.Properties.Resources", Assembly.GetExecutingAssembly());

        private string prependData = null;
        private string appendData = null;

        /// <summary>
        /// Constructor initializes base class to allow custom names and description for component properies
        /// </summary>
        public UTF_8_wo_BOM() :
            base(_resourceManager)
        {
        }

        #region Properties
        /// <summary>
        /// Data to prepend at the beginning of a stream.
        /// </summary>	
        [
        FixMsgPropertyName("PropPrependData"),
        FixMsgDescription("DescrPrependData")
        ]
        public string PrependData
        {
            get { return prependData; }
            set { prependData = value; }
        }
        /// <summary>
        /// Data to append at the end of stream.
        /// </summary>
        [
        FixMsgPropertyName("PropAppendData"),
        FixMsgDescription("DescrAppendData")
        ]
        public string AppendData
        {
            get { return appendData; }
            set { appendData = value; }
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
                return _resourceManager.GetString("UTF_8_wo_BOM_name", CultureInfo.InvariantCulture);
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
                return _resourceManager.GetString("UTF_8_wo_BOM_version", CultureInfo.InvariantCulture);
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
                return _resourceManager.GetString("UTF_8_wo_BOM_description", CultureInfo.InvariantCulture);
            }
        }
        #endregion

        #region IPersistPropertyBag

        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classid">Class ID of the component.</param>
        public void GetClassID(out Guid classid)
        {
            classid = new System.Guid("48BEC85A-20EE-40ad-BFD0-319B59A0DDBD");
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public void InitNew()
        {
        }

        /// <summary>
        /// Loads configuration property for component.
        /// </summary>
        /// <param name="pb">Configuration property bag.</param>
        /// <param name="errlog">Error status (not used in this code).</param>
        public void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Int32 errlog)
        {
            string val = (string)ReadPropertyBag(pb, "AppendData");
            if (val != null) appendData = val;

            val = (string)ReadPropertyBag(pb, "PrependData");
            if (val != null) prependData = val;
        }

        /// <summary>
        /// Saves the current component configuration into the property bag.
        /// </summary>
        /// <param name="pb">Configuration property bag.</param>
        /// <param name="fClearDirty">Not used.</param>
        /// <param name="fSaveAllProperties">Not used.</param>
        public void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Boolean fClearDirty, Boolean fSaveAllProperties)
        {
            object val = (object)appendData;
            WritePropertyBag(pb, "AppendData", val);

            val = (object)prependData;
            WritePropertyBag(pb, "PrependData", val);
        }

        /// <summary>
        /// Reads property value from property bag.
        /// </summary>
        /// <param name="pb">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <returns>Value of the property.</returns>
        private static object ReadPropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName)
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
                throw new ApplicationException(ex.Message);
            }
            return val;
        }

        /// <summary>
        /// Writes property values into a property bag.
        /// </summary>
        /// <param name="pb">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <param name="val">Value of property.</param>
        private static void WritePropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName, object val)
        {
            try
            {
                pb.Write(propName, ref val);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }




        #endregion

        #region IComponentUI

        /// <summary>
        /// Component icon to use in integration Editor.
        /// </summary>
        [Browsable(false)]
        public IntPtr Icon
        {
            get
            {
                return Properties.Resources.cog.Handle;
            }
        }

        /// <summary>
        /// The Validate method is called by the integration Editor during the build 
        /// of a integration project.
        /// </summary>
        /// <param name="obj">Project system.</param>
        /// <returns>
        /// A list of error and/or warning messages encounter during validation
        /// of this component.
        /// </returns>
        public IEnumerator Validate(object obj)
        {
            IEnumerator enumerator = null;
            ArrayList strList = new ArrayList();

            // Validate prepend data property
            if ((prependData != null) &&
            (prependData.Length >= 64))
            {
                strList.Add(_resourceManager.GetString("ErrorPrependDataTooLong"));
            }

            // validate append data property
            if ((appendData != null) &&
            (appendData.Length >= 64))
            {
                strList.Add(_resourceManager.GetString("ErrorAppendDataTooLong"));
            }

            if (strList.Count > 0)
            {
                enumerator = strList.GetEnumerator();
            }

            return enumerator;
        }

        #endregion

        #region IComponent

        /// <summary>
        /// Implements IComponent.Execute method.
        /// </summary>
        /// <param name="pipelineContext">Pipeline context</param>
        /// <param name="inMsg">Input message.</param>
        /// <returns>Processed input message with appended or prepended data.</returns>
        /// <remarks>
        /// IComponent.Execute method is used to initiate
        /// the processing of the message in pipeline component.
        /// </remarks>
        public IBaseMessage Execute(IPipelineContext pipelineContext, IBaseMessage inMsg)
        {
            Log.DebugFormat("Execute(IPipelineContext pContext {0}, IBaseMessage pInMsg {1})", pipelineContext, inMsg);

            IBaseMessagePart bodyPart = inMsg.BodyPart;
            if (bodyPart != null)
            {
                byte[] prependByteData = new byte[] {}; //UTF8 Byte Order Mark 

                byte[] appendByteData = ConvertToBytes(appendData);
                Stream originalStrm = bodyPart.GetOriginalDataStream();
                Stream strm = null;

                if (originalStrm != null)
                {
                    strm = new FixMsgStream(originalStrm, prependByteData, appendByteData, _resourceManager);
                    bodyPart.Data = strm;
                    pipelineContext.ResourceTracker.AddResource(strm);
                }
            }

            return inMsg;
        }
        #endregion

        #region Helper
        /// <summary>
        /// Converts a string to its byte representation.
        /// </summary>
        /// <param name="str">String to be converted to byte representation.</param>
        /// <returns>Array of bytes that represents the string.</returns>
        private byte[] ConvertToBytes(string str)
        {
            byte[] data = null;

            if (str != null)
                data = UTF8Encoding.UTF8.GetBytes(str);

            return data;
        }
        #endregion
    }
}