using System;
using System.Collections;
using System.ComponentModel;
using inSyca.foundation.framework;
using inSyca.foundation.integration.biztalk.components.diagnostics;
using Microsoft.BizTalk.Component.Interop;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;
using Microsoft.BizTalk.Message.Interop;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Drawing;

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

        public void GetClassID(out Guid classId)
        {
            classId = new System.Guid("48BEC85A-20EE-40ad-BFD0-319B59A0DDBC");
        }


        public void InitNew()
        {
        }


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


        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            WritePropertyBag(propertyBag, "IncomingEncoding", this.IncomingEncoding);
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
            Log.DebugFormat("Execute(IPipelineContext pipelineContext {0}, IBaseMessage inMsg {1})", pipelineContext, inMsg);

			inMsg.BodyPart.Charset = this.incomingEncoding;
			return inMsg;
		}

		#endregion


	}
}
