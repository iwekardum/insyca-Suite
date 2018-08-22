using inSyca.foundation.framework;
using inSyca.foundation.framework.configuration;
using inSyca.foundation.integration.biztalk.components.diagnostics;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Xsl;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;


namespace inSyca.foundation.integration.biztalk.components
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Decoder)]
    [Guid("410689B8-9B77-43DF-9804-A815CF8AC985")]
    public class Substitute : IBaseComponent, IComponent, IPersistPropertyBag, IComponentUI
    {
        static private readonly ResourceManager _resourceManager = new ResourceManager("inSyca.foundation.integration.biztalk.components.Properties.Resources", Assembly.GetExecutingAssembly());

        static string configFilePathName        = "ConfigFilePath";
        static string xmlElementLowercaseName   = "XmlElementLowercase";

        #region Properties
        [System.ComponentModel.Description("Path to Configuration File: " + "\r\n" + "There is a sample of this file.")]
        public string ConfigFilePath { get; set; }

        [System.ComponentModel.Description("Lowercase all XML element nodes")]
        public bool XmlElementLowercase { get; set; }

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
                return _resourceManager.GetString("substitute_name", CultureInfo.InvariantCulture);
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
                return _resourceManager.GetString("substitute_version", CultureInfo.InvariantCulture);
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
                return _resourceManager.GetString("substitute_description", CultureInfo.InvariantCulture);
            }
        }
        #endregion

        #region IPersistPropertyBag Members

        void IPersistPropertyBag.GetClassID(out Guid classID)
        {
            classID = new Guid("410689B8-9B77-43DF-9804-A815CF8AC985");
        }

        void IPersistPropertyBag.InitNew()
        {

        }

        void IPersistPropertyBag.Load(IPropertyBag pb, int errorLog)
        {
            object val = null;
            val = ReadPropertyBag(pb, configFilePathName);
            if ((val != null))
            {
                ConfigFilePath = ((string)(val));
            }

            val = ReadPropertyBag(pb, xmlElementLowercaseName);
            if ((val != null))
            {
                XmlElementLowercase = ((bool)(val));
            }
        }

        void IPersistPropertyBag.Save(IPropertyBag pb, bool clearDirty, bool saveAllProperties)
        {
            WritePropertyBag(pb, configFilePathName, ConfigFilePath);
            WritePropertyBag(pb, xmlElementLowercaseName, XmlElementLowercase);
        }
        #endregion

        #region IComponentUI Members

        IntPtr IComponentUI.Icon
        {
            get
            {
                return Properties.Resources.cog.Handle;
            }
        }

        System.Collections.IEnumerator IComponentUI.Validate(object projectSystem)
        {
            return null;
        }

        #endregion

        #region IComponent Members

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            Log.DebugFormat("Execute(IPipelineContext pContext {0}, IBaseMessage pInMsg {1})", pContext, pInMsg);

            try
            {
                if (pInMsg == null || pInMsg.BodyPart == null || pInMsg.BodyPart.Data == null)
                {
                    Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "pInMsg Error", null));
                    throw new ArgumentNullException("pInMsg Error");
                }

                var bodyPart = pInMsg.BodyPart;

                var inboundStream = bodyPart.GetOriginalDataStream();

                MemoryStream stream = SubstituteStream(inboundStream);

                Log.DebugFormat("Execute(IPipelineContext pContext {0}, IBaseMessage pInMsg {1})\nstream.Length={2})", pContext, pInMsg, stream.Length);

                stream.Seek(0, SeekOrigin.Begin);
                pInMsg.BodyPart.Data = stream;
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
            }

            return pInMsg;
        }

        #endregion

        #region Helper

        private MemoryStream SubstituteStream(Stream inputStream)
        {
            string messageBody = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(ConfigFilePath))
                {
                    RegistryKey regKey = null;

                    regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\inSyca\foundation.integration.biztalk.components", RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.ReadKey);

                    if (regKey != null)
                        ConfigFilePath = (string)regKey.GetValue("SubstituteConfiguration");
                }

                Parameters configParameters = new Parameters();
                configParameters.Initialize(ConfigFilePath);

                StreamReader reader = new StreamReader(inputStream);
                messageBody = reader.ReadToEnd();

                // simple replacement:
                foreach (Parameter p in configParameters.SimpleParameter)
                {
                    if (p.Search != null || p.ReplaceTo != null) // check
                        if (p.Search.Length > 0) // check
                        {
                            Log.DebugFormat("SubstituteStream(Stream inputStream {0})\nSimple - p.Search={1}\np.ReplaceTo={2}", inputStream, p.Search, p.ReplaceTo );
                            messageBody = messageBody.Replace(p.Search, p.ReplaceTo);
                        }
                }

                // RegEx replacement:
                foreach (Parameter p in configParameters.RegExParameter)
                {
                    Regex rx = null;
                    if (p.Search != null || p.ReplaceTo != null) // check
                        if (p.Search.Length > 0) // check
                        {
                            Log.DebugFormat("SubstituteStream(Stream inputStream {0})\nRegex - p.Search={1}\np.ReplaceTo={2}", inputStream, p.Search, p.ReplaceTo);
                            rx = new Regex(p.Search);
                            messageBody = rx.Replace(messageBody, p.ReplaceTo);
                        }
                }

                Log.DebugFormat("SubstituteStream(Stream inputStream {0})\nmessageBody= {1}", inputStream, messageBody);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
            }

            MemoryStream ms;

            if (XmlElementLowercase)
            {
                ms = new MemoryStream();
                try
                {
                    string sXslt = string.Concat(new string[]{
                    "<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>",
                    "<xsl:output method='xml' indent='yes'/>",
                    "<xsl:variable name='lowercase' select='abcdefghijklmnopqrstuvwxyz'/>",
                    "<xsl:variable name='uppercase' select='ABCDEFGHIJKLMNOPQRSTUVWXYZ' />",
                    "<xsl:template match='*'>",
                    "<xsl:element name='{ translate(local-name(), $uppercase, $lowercase)}' namespace='{namespace-uri()}'>",
                    "<xsl:apply-templates/>",
                    "</xsl:element>",
                    "</xsl:template>",
                    "</xsl:stylesheet>"});

                    Log.DebugFormat("SubstituteStream(Stream inputStream {0})\nsXslt= {1}", inputStream, sXslt );

                    XslCompiledTransform objXslTrans = new XslCompiledTransform();
                    objXslTrans.Load(new XmlTextReader(new StringReader(sXslt)));
                    XmlWriter xmlWriter = XmlWriter.Create(ms);
                    objXslTrans.Transform(new XmlTextReader(new StringReader(messageBody)), xmlWriter);

                    xmlWriter.Flush();
                }
                catch (Exception ex)
                {
                    Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
                }
            }
            else
            {
                ms = new MemoryStream(Encoding.UTF8.GetBytes(messageBody ?? ""));
            }

            return ms;
        }

        #endregion

        #region Utility functionality

        /// <summary>
        /// Reads property value from property bag
        /// </summary>
        /// <param name="pb">Property bag</param>
        /// <param name="propName">Name of property</param>
        /// <returns>Value of the property</returns>
        private object ReadPropertyBag(IPropertyBag pb, string propName)
        {
            object val = null;
            try
            {
                pb.Read(propName, out val, 0);
            }
            catch (ArgumentException argEx)
            {
                diagnostics.Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, argEx));
                return val;
            }
            catch (Exception ex)
            {
                diagnostics.Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), null, ex));

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
        private void WritePropertyBag(IPropertyBag pb, string propName, object val)
        {
            try
            {
                pb.Write(propName, ref val);
            }
            catch (Exception ex)
            {
                diagnostics.Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));

                throw new ApplicationException(ex.Message);
            }
        }

        #endregion
    }
}
