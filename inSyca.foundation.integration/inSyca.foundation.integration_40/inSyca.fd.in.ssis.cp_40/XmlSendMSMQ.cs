using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using System.Messaging;
using System.Reflection;
using System.Globalization;
using System.Data.SqlClient;

using Microsoft.SqlServer.Dts.Pipeline;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;
using System.Collections.Generic;

namespace inSyca.foundation.integration.ssis.components
{
    [DtsPipelineComponent(DisplayName = "Xml Send MSMQ",
        ComponentType = ComponentType.SourceAdapter)]
    public class XmlSendMSMQ : PipelineComponent
    {
        // Variable that contains the XML document
        XmlWriter   xmlWriter;

        PipelineBuffer outputBuffer;

        string MSMQName = string.Empty;
        MemoryStream memoryStream;

        IDTSOutput100 output;
        IDTSOutputColumn100 outputColumn;

        #region Design Time

        protected string XmlRecordName
        {
            get
            {
                IDTSCustomProperty100 documentElementProperty =
                    this.ComponentMetaData.CustomPropertyCollection[Constants.XMLRECORDNAME];
                return documentElementProperty.Value.ToString();
            }
        }

        protected string XmlElementName
        {
            get
            {
                IDTSCustomProperty100 documentElementProperty =
                    this.ComponentMetaData.CustomPropertyCollection[Constants.XMLELEMENTNAME];
                return documentElementProperty.Value.ToString();
            }
        }


        protected string EventLogSource
        {
            get
            {
                IDTSCustomProperty100 documentElementProperty =
                    this.ComponentMetaData.CustomPropertyCollection[Constants.EVENTLOGSOURCE];
                return documentElementProperty.Value.ToString();
            }
        }

        protected string XmlNamespacePrefix
        {
            get
            {
                IDTSCustomProperty100 documentElementProperty =
                    this.ComponentMetaData.CustomPropertyCollection[Constants.XMLNAMESPACEPREFIX];
                return documentElementProperty.Value.ToString();
            }
        }

        protected string XmlRootName
        {
            get
            {
                IDTSCustomProperty100 documentElementProperty =
                    this.ComponentMetaData.CustomPropertyCollection[Constants.XMLROOTNAME];
                return documentElementProperty.Value.ToString();
            }
        }

        protected string XmlNamespace
        {
            get
            {
                IDTSCustomProperty100 documentElementProperty =
                    this.ComponentMetaData.CustomPropertyCollection[Constants.XMLNAMESPACE];
                return documentElementProperty.Value.ToString();
            }
        }

        protected string MSMQEntryLabel
        {
            get
            {
                IDTSCustomProperty100 documentElementProperty =
                    this.ComponentMetaData.CustomPropertyCollection[Constants.MSMQENTRYLABEL];
                return documentElementProperty.Value.ToString();
            }
        }

        public override void ProvideComponentProperties()
        {
            base.ProvideComponentProperties();

            // First, we'll clear out everything -- inputs, outputs, and connection managers.
            this.RemoveAllInputsOutputsAndCustomProperties();

            // Add an output object.
            output = ComponentMetaData.OutputCollection.New();
            output.Name = "Output From Xml MSMQ";
            output.SynchronousInputID = 0; //Aynchronous Transform 

            // Add an output column to store the start time of the component.
            outputColumn = output.OutputColumnCollection.New();
            outputColumn.Name = "DateStarted";
            outputColumn.SetDataTypeProperties(DataType.DT_WSTR, 20, 0, 0, 0);

            // Add the document element name and namespace to the component.
            AddProperty(Constants.XMLRECORDNAME, "", @"RECORD");
            AddProperty(Constants.XMLELEMENTNAME, "", @"ELEMENT");
            AddProperty(Constants.EVENTLOGSOURCE, "", @"inSyca.SSIS.Components");
            AddProperty(Constants.XMLNAMESPACEPREFIX, "", @"ns0");
            AddProperty(Constants.XMLROOTNAME, "", @"MM70_AR");
            AddProperty(Constants.XMLNAMESPACE, "", @"http://inSyca.Biz.SSIS.Components.XXX_XX_10");
            AddProperty(Constants.MSMQENTRYLABEL, "", @"MM70_AR");
            AddProperty(Constants.CONFIGSECTION, "", @"ConfigSection");
        }

        private void AddProperty(string name, string description, object value)
        {
            IDTSCustomProperty100 property = this.ComponentMetaData.CustomPropertyCollection.New();
            property.Name = name;
            property.Description = description;
            property.Value = value;
        }

        public override void ReinitializeMetaData()
        {
            ComponentMetaData.RemoveInvalidInputColumns();
            ReinitializeMetaData();
        }

        #endregion

        #region Run Time

        /// <summary>
        /// Validation could be improved.
        /// </summary>
        public override DTSValidationStatus Validate()
        {
            // TODO: Do more validation here
            return DTSValidationStatus.VS_ISVALID;
        }

        public override void PreExecute()
        {
            base.PreExecute();

            //IDTSCustomProperty100 documentElementProperty = this.ComponentMetaData.CustomPropertyCollection[Constants.CONFIGSECTION];

            //MSMQName =  Configuration.GetCustomSettingsValue(documentElementProperty.Value.ToString(), Constants.MSMQSETTING);

            OpenXmlDocument();

            // Create row element: 
            xmlWriter.WriteStartElement(XmlRecordName);

            // Create Column element: 
            xmlWriter.WriteStartElement(XmlElementName);

            xmlWriter.WriteString(XmlHelper.removeForbiddenXmlChars(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")));

            // Close column element: 
            xmlWriter.WriteEndElement();

            // Close row element: 
            xmlWriter.WriteEndElement();

            CloseXmlDocument();

            MSMQHelper.SendToMessageQueue(MSMQName, MSMQEntryLabel, 1, memoryStream);
        }

        public override void PrimeOutput(int outputs, int[] outputIDs, PipelineBuffer[] buffers)
        {
            if (buffers.Length != 0)
            {
                outputBuffer = buffers[0];

                outputBuffer.AddRow();
                outputBuffer[0] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            }

            if (outputBuffer != null)
                outputBuffer.SetEndOfRowset();

            base.PrimeOutput(outputs, outputIDs, buffers);
        }
        #endregion

        #region Helpers

        private void OpenXmlDocument()
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = new UnicodeEncoding();
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Indent = true;

            memoryStream = new MemoryStream();

            //Create a new XML document and use the filepath in the connection as XML-file
            xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);

            //Start writing the XML document: 
            xmlWriter.WriteStartDocument();

            //Create root element 
            xmlWriter.WriteStartElement(XmlNamespacePrefix, XmlRootName, XmlNamespace);
        }

        private void CloseXmlDocument()
        {
            // Close root element: 
            xmlWriter.WriteEndElement();

            // Stop writing the XML document
            xmlWriter.WriteEndDocument();

            xmlWriter.Flush();

            // Close document
            xmlWriter.Close();
            memoryStream.Close();
        }

        #endregion
    }
}

