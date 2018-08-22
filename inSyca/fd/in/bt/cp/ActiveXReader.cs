/// Pipeline component Built by Dipesh Avlani (http://integrationexperts.wordpress.com). Free to use in your projects.
///
/// <summary>
/// The purpose of this custom pipeline is to decode the xlsx files and convert it to xml. This component also adds a namespace.
/// A regular expressions are used to ensure blank cells are handled in the excel 2007 format.     
/// NamespaceBase:      -   The new namespace to be inserted. 
/// NamespacePrefix:    -   The namespace prefix to use.
/// RootNodeName:       -   Name of the root node.
/// IsFirstRowHeader:   -   Flag to indicate if the first row represents column names.
/// 
/// Note:   
/// This solution is based on the VirtualStream supported in BizTalk Server 2006 and up. The caching is set to to disk. The default location is for this file is 'C:\Documents and Settings\<BTSHostInstanceName>\Local Settings\Temp'. For performance reasons this location should be moved to a non OS-Drive. Make sure that BizTalk Host Instance account has full control of this folder. 
/// </summary>

using inSyca.foundation.integration.biztalk.components.diagnostics;
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

		private string incomingEncoding;

		public ActiveXMessageReader()
		{
			this.incomingEncoding = "utf-16";
		}

        #region Properties

        [Description("Encoding name")]
		[DisplayName("Encoding name")]
		[DefaultValue("utf-16")]
		public string IncomingEncoding
		{
			get
			{
				return this.incomingEncoding;
			}

			set
			{
				this.incomingEncoding = value;
			}
		}

		#endregion public members

        #region IBaseComponent Members

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
        #endregion

        #region IPersistPropertyBag Members

        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classid">
        /// Class ID of the component
        /// </param>
		public void GetClassID(out Guid classId)
        {
            classId = new System.Guid("48BEC85A-20EE-40ad-BFD0-319B59A0DDBC");
        }

        /// <summary>
        /// not implemented
        /// </summary>
        public void InitNew()
        {
        }

        /// <summary>
        /// Loads configuration properties for the component
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="errlog">Error status</param>
        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            try
            {
                this.IncomingEncoding = (string)ReadPropertyBag(propertyBag, "IncomingEncoding");
            }
            catch
            {
                this.IncomingEncoding = "utf-16";
            }
        }


        /// <summary>
        /// Saves the current component configuration into the property bag
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="fClearDirty">not used</param>
        /// <param name="fSaveAllProperties">not used</param>
        public virtual void Save(IPropertyBag pb, bool fClearDirty, bool fSaveAllProperties)
        {
            WritePropertyBag(pb, "IncomingEncoding", this.IncomingEncoding);
        }


        /// <summary>
        /// Reads property value from property bag
        /// </summary>
        /// <param name="pb">Property bag</param>
        /// <param name="propName">Name of property</param>
        /// <returns>Value of the property</returns>
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
            Log.DebugFormat("Execute(IPipelineContext pipelineContext {0}, IBaseMessage inMsg {1})", pipelineContext, inMsg);

			inMsg.BodyPart.Charset = this.incomingEncoding;
			return inMsg;
		}

		#endregion


	}
}
