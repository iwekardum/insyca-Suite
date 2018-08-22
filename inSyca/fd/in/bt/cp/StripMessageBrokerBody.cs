//---------------------------------------------------------------------
// File: StripMessageBrokerBody.cs
// 
// Summary: Pipeline component that applies Xsl transformation to convert XML messages to HTML documents.
//
// Sample: Xsl Transformation component SDK 
//
//---------------------------------------------------------------------
// This file is part of the Microsoft integration Server 2009 SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft integration
// Server 2009 release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------

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
    /// <summary>
    /// Implements a pipeline component that applies Xsl Transformations to XML messages
    /// </summary>
    /// <remarks>
    /// StripMessageBrokerBody class implements pipeline components that can be used in send pipelines
    /// to convert XML messages to HTML format for sending using SMTP transport. Component can
    /// be placed only in Encoding stage of send pipeline
    /// </remarks>
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Decoder)]
    [System.Runtime.InteropServices.Guid("0B451084-3BCF-41A0-92C3-51E1A9A8621E")]
    public class StripMessageBrokerBody : IBaseComponent, IComponent, IPersistPropertyBag, IComponentUI
    {
        static private readonly ResourceManager _resourceManager = new ResourceManager("inSyca.foundation.integration.biztalk.components.Properties.Resources", Assembly.GetExecutingAssembly());

        private string outerXmlNodeName = "part";

        #region Properties
        /// <summary>
        /// Location of Xsl transform file.
        /// </summary>
        public string OuterXmlNodeName
        {
            get { return outerXmlNodeName; }
            set { outerXmlNodeName = value; }
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
                return _resourceManager.GetString("stripMessageBrokerBody_name", CultureInfo.InvariantCulture);
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
                return _resourceManager.GetString("stripMessageBrokerBody_version", CultureInfo.InvariantCulture);
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
                return _resourceManager.GetString("stripMessageBrokerBody_description", CultureInfo.InvariantCulture);
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
            classid = new System.Guid("0B451084-3BCF-41A0-92C3-51E1A9A8621E");
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public void InitNew()
        {
        }

        public void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Int32 errlog)
        {
            string val = (string)ReadPropertyBag(pb, "OuterXmlNodeName");
            if (val != null) outerXmlNodeName = val;
        }

        /// <summary>
        /// Saves the current component configuration into the property bag.
        /// </summary>
        /// <param name="propertyBag">Configuration property bag.</param>
        /// <param name="clearDirty">Not used.</param>
        /// <param name="saveAllProperties">Not used.</param>
        public void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, Boolean fClearDirty, Boolean fSaveAllProperties)
        {
            object val = (object)outerXmlNodeName;
            WritePropertyBag(pb, "OuterXmlNodeName", val);
        }

        /// <summary>
        /// Reads property value from property bag.
        /// </summary>
        /// <param name="propertyBag">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <returns>Value of the property.</returns>
        private static object ReadPropertyBag(IPropertyBag pb, string propName)
        {
            object val = null;
            try
            {
                pb.Read(propName, out val, 0);
            }

            catch (System.ArgumentException)
            {
                return val;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return val;
        }

        private static void WritePropertyBag(IPropertyBag pb, string propName, object val)
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

        #region IComponent

        /// <summary>
        /// Implements IComponent.Execute method.
        /// </summary>
        /// <param name="pc">Pipeline context</param>
        /// <param name="inmsg">Input message.</param>
        /// <returns>Converted to HTML input message.</returns>
        /// <remarks>
        /// IComponent.Execute method is used to convert XML messages
        /// to HTML messages using provided Xslt file.
        /// It also sets the content type of the message part to be "text/html"
        /// which is necessary for client mail applications to correctly render
        /// the message
        /// </remarks>
        public IBaseMessage Execute(IPipelineContext pipelineContext, IBaseMessage inMsg)
        {
            Log.DebugFormat("Execute(IPipelineContext pipelineContext {0}, IBaseMessage inMsg {1})", pipelineContext, inMsg);

            inMsg.BodyPart.Data = TransformMessage(inMsg.BodyPart.Data);
            return inMsg;
        }
        #endregion

        #region Helper function
        /// <summary>
        /// Transforms XML message in input stream to HTML message
        /// </summary>
        /// <param name="stm">Stream with input XML message</param>
        /// <returns>Stream with output HTML message</returns>
        private Stream TransformMessage(Stream stream)
        {
            Log.DebugFormat("TransformMessage(Stream stream {0})", stream);

            MemoryStream ms = new MemoryStream();

            try
            {
                using (XmlWriter _Writer = XmlWriter.Create(ms))
                { 
                    _Writer.WriteStartDocument();

                    using (XmlReader outerReader = new XmlTextReader(stream))
                    {
                        outerReader.ReadToFollowing(outerXmlNodeName);
                        outerReader.Read();

                        using (XmlReader innerReader = outerReader.ReadSubtree())
                            while (!innerReader.EOF)
                                _Writer.WriteNode(innerReader, false);
                    }

                    _Writer.WriteEndDocument();

                    _Writer.Flush();
                }
                ms.Position = 0;
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { stream }, ex));
                throw ex;
            }

            return ms;
        }

        #endregion
    }
}
