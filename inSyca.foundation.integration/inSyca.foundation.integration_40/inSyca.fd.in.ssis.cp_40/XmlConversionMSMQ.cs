using inSyca.foundation.framework;
using inSyca.foundation.integration.ssis.components.diagnostics;
using Microsoft.SqlServer.Dts.Pipeline;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace inSyca.foundation.integration.ssis.components
{
    [DtsPipelineComponent(DisplayName = "Xml Conversion MSMQ",
        ComponentType = ComponentType.Transform)]
    public class XmlConversionMSMQ : PipelineComponent
    {
        // Variable that contains the XML document
        XmlWriter xmlWriter;

        int rowsProcessed = 0;

        IDTSOutput100 output;
        IDTSOutputColumn100 outputColumn;

        string MSMQName = string.Empty;
        Dictionary<string, string> EXTENDELEMENT;
        List<string> GROUPBYFIELDLIST = new List<string>();
        Dictionary<string, object> ACTUALGROUPBYVALUES = new Dictionary<string, object>();
        Dictionary<string, object> INITIALGROUPBYVALUES = new Dictionary<string, object>();

        /// <summary>
        /// The cache of information from the input, virtual input,
        /// and column objects.  We build this cache during PreExecute()
        /// to avoid using the layout objects "in the loop" of ProcessInput()
        /// (which would be slow due to COM interop performance)
        /// 
        /// This component supports multiple inputs, so this cache is 
        /// a map of Input ID to lists of XmlColumnInfos.  Each 
        /// XmlColumnInfo corresponds to one mapped column from the Input.
        /// </summary>
        Dictionary<int, List<XmlColumnInfo>> xmlColumnInfos;
        PipelineBuffer outputBuffer;
        MemoryStream memoryStream;

        #region Design Time

        protected int RecordCountReset
        {
            get
            {
                IDTSCustomProperty100 documentElementProperty =
                    this.ComponentMetaData.CustomPropertyCollection[Constants.RECORDCOUNTRESET];
                return Convert.ToInt32(documentElementProperty.Value);
            }
        }

        protected string XmlRecordName
        {
            get
            {
                IDTSCustomProperty100 documentElementProperty =
                    this.ComponentMetaData.CustomPropertyCollection[Constants.XMLRECORDNAME];
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

        protected string[] GroupByFields
        {
            get
            {
                IDTSCustomProperty100 documentElementProperty =
                    this.ComponentMetaData.CustomPropertyCollection[Constants.GROUPBYFIELDS];
                return documentElementProperty.Value.ToString().Split(';');
            }
        }

        private bool IsNewGroup
        {
            get
            {
                if (RecordCountReset == 0 && GROUPBYFIELDLIST.Count <= 0)
                {
                    return false;
                }

                if (RecordCountReset != 0 && rowsProcessed >= RecordCountReset)
                {
                    Log.DebugFormat("IsNewGroup\nEvaluate rows processed\nIsNewGroup Result: TRUE\nRecordCountReset: {0}\nrowsProcessed: {1} ", RecordCountReset, rowsProcessed);

                    return true;
                }


                if (ACTUALGROUPBYVALUES.Count <= 0 || INITIALGROUPBYVALUES.Count <= 0)
                {
                    Log.Debug("IsNewGroup\nACTUALGROUPBYVALUES || INITIALGROUPBYVALUES does not have entries\nIsNewGroup Result: FALSE");

                    return false;
                }

                foreach (string GroupByField in GROUPBYFIELDLIST)
                {
                    Log.DebugFormat("IsNewGroup\nEvaluate GROUPBYFIELDLIST\nGroupByField: {0}", GroupByField);

                    if (ACTUALGROUPBYVALUES.ContainsKey(GroupByField) && INITIALGROUPBYVALUES.ContainsKey(GroupByField) && !ACTUALGROUPBYVALUES[GroupByField].Equals(INITIALGROUPBYVALUES[GroupByField]))
                    {
                        Log.DebugFormat("IsNewGroup\nACTUALGROUPBYVALUES[GroupByField] <> INITIALGROUPBYVALUES[GroupByField]\nIsNewGroup Result: TRUE\nACTUALGROUPBYVALUE: {0}\nINITIALGROUPBYVALUE: {1}", ACTUALGROUPBYVALUES[GroupByField], INITIALGROUPBYVALUES[GroupByField]);

                        return true;
                    }
                }

                return false;
            }
        }

        public override void ProvideComponentProperties()
        {
            Log.Debug("ProvideComponentProperties()");

            base.ProvideComponentProperties();

            // First, we'll clear out everything -- inputs, outputs, and connection managers.
            this.RemoveAllInputsOutputsAndCustomProperties();

            IDTSInput100 input = ComponentMetaData.InputCollection.New();
            input.Name = "Input To Xml MSMQ";

            // Add an output object.
            output = ComponentMetaData.OutputCollection.New();
            output.Name = "Output From Xml MSMQ";
            output.SynchronousInputID = 0; //Aynchronous Transform 

            // Add an output column to store the start time of the component.
            outputColumn = output.OutputColumnCollection.New();
            outputColumn.Name = "DateStarted";
            outputColumn.SetDataTypeProperties(DataType.DT_WSTR, 20, 0, 0, 0);

            // Add the document element name and namespace to the component.
            AddProperty(Constants.RECORDCOUNTRESET, "", @"0");
            AddProperty(Constants.XMLRECORDNAME, "", @"RECORD");
            AddProperty(Constants.EVENTLOGSOURCE, "", @"InSyca.SSIS.Components");
            AddProperty(Constants.XMLNAMESPACEPREFIX, "", @"ns0");
            AddProperty(Constants.XMLROOTNAME, "", @"ROOTNAME");
            AddProperty(Constants.XMLNAMESPACE, "", @"http://InSyca.Biz.SSIS.Components.XXX_XX_10");
            AddProperty(Constants.MSMQENTRYLABEL, "", @"ENTRYLABEL");
            AddProperty(Constants.CONFIGSECTION, "", @"ConfigSection");
            AddProperty(Constants.GROUPBYFIELDS, "", @"");
        }

        private void AddProperty(string name, string description, object value)
        {
            Log.DebugFormat("AddProperty(string name {0}, string description {1}, object value {2})", name, description, value);

            IDTSCustomProperty100 property = this.ComponentMetaData.CustomPropertyCollection.New();
            property.Name = name;
            property.Description = description;
            property.Value = value;
        }

        public override void ReinitializeMetaData()
        {
            Log.Debug("ReinitializeMetaData()");

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
            Log.Debug("Validate()");

            // TODO: Do more validation here
            return DTSValidationStatus.VS_ISVALID;
        }

        public override void PreExecute()
        {
            Log.Debug("PreExecute()");

            base.PreExecute();

            IDTSCustomProperty100 documentElementProperty = this.ComponentMetaData.CustomPropertyCollection[Constants.CONFIGSECTION];

            try
            {
                //Debuglevel = Convert.ToInt32(Configuration.GetCustomSettingsValue(documentElementProperty.Value.ToString(), Constants.DEBUGLEVELSETTING));
                //MSMQName = Configuration.GetCustomSettingsValue(documentElementProperty.Value.ToString(), Constants.MSMQSETTING);

                string[] EXTENDRECORDFIELDS = null;/* Configuration.GetCustomSettingsValue(documentElementProperty.Value.ToString(), Constants.EXTENDRECORDFIELDSSETTING).Split(new Char[] { ';' });*/

                EXTENDELEMENT = new Dictionary<string, string>();

                foreach (string extendedRecordFields in EXTENDRECORDFIELDS)
                {
                    string[] extendedElement = extendedRecordFields.Split(new Char[] { ':' });

                    if (extendedElement.Length > 1)
                        EXTENDELEMENT.Add(extendedElement[0].Trim(new Char[] { ';', ' ', ':' }), extendedElement[1].Trim(new Char[] { ';', ' ', ':' }));
                    else if (extendedElement.Length > 0)
                        EXTENDELEMENT.Add(extendedElement[0].Trim(new Char[] { ';', ' ', ':' }), string.Empty);
                }
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
            }

            // build a new metadata cache.
            xmlColumnInfos = new Dictionary<int, List<XmlColumnInfo>>();

            foreach (string GroupByField in GroupByFields)
            {
                if (!String.IsNullOrEmpty(GroupByField))
                {
                    GROUPBYFIELDLIST.Add(GroupByField);

                    Log.DebugFormat("PreExecute()\nGroupByField\nName: {0}", GroupByField );
                }
            }

            // Look at each input, and then each column, storing important metadata.
            foreach (IDTSInput100 input in this.ComponentMetaData.InputCollection)
            {
                List<XmlColumnInfo> cols = new List<XmlColumnInfo>();

                // We make one pass through each input, adding the elements, so that ProcessInput will write out  elements
                foreach (IDTSInputColumn100 col in input.InputColumnCollection)
                {
                    // Find the position in buffers that this column will take, and add it to the map.
                    cols.Add(new XmlColumnInfo(col.Name, col.DataType, BufferManager.FindColumnByLineageID(input.Buffer, col.LineageID)));

                    Log.DebugFormat("PreExecute()\nXmlColumnInfo\nName: {0}\nType: {1}\nLineageID: {2}", col.Name, col.DataType, col.LineageID);
                }
                xmlColumnInfos.Add(input.ID, cols);
            }

            OpenXmlDocument();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputs"></param>
        /// <param name="outputIDs"></param>
        /// <param name="buffers"></param>
        public override void PrimeOutput(int outputs, int[] outputIDs, PipelineBuffer[] buffers)
        {
            Log.DebugFormat("PrimeOutput(int outputs {0}, int[] outputIDs {1}, PipelineBuffer[] buffers {2})", outputs, outputIDs, buffers);

            if (buffers.Length != 0)
            {
                outputBuffer = buffers[0];

                outputBuffer.AddRow();
                outputBuffer[0] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputID"></param>
        /// <param name="buffer"></param>
        public override void ProcessInput(int inputID, PipelineBuffer buffer)
        {
            Log.DebugFormat("ProcessInput(int inputID {0}, PipelineBuffer buffer {1})", inputID, buffer);

            base.ProcessInput(inputID, buffer);

            // For performance, cache as much as possible now.
            // Count the number of columns.
            int columnCount = buffer.ColumnCount;

            // Find the cached column mapping list for this input.
            List<XmlColumnInfo> columns = xmlColumnInfos[inputID];

            // Find the cached column mapping list for this input.
            Dictionary<string, string> xmlValues = new Dictionary<string, string>();

            // Find the input object for this ID, so we can pull out the XML mapping properties.
            // This is pretty slow, but we only do it once per buffer.
            IDTSInput100 input = this.ComponentMetaData.InputCollection.GetObjectByID(inputID);

            while (buffer.NextRow())
            {
                object columnValue = String.Empty;

                // Loop through all columns and create a column element: valuevalue
                foreach (XmlColumnInfo columnInfo in columns)
                {
                    Log.DebugFormat("ProcessInput(int inputID {0}, PipelineBuffer buffer {1})\nColumn: {0}", inputID, buffer, columnInfo.Name);

                    // Get column value
                    try
                    {
                        columnValue = buffer[columnInfo.BufferIndex];

                        if (columnValue == null)
                            columnValue = String.Empty;
                    }
                    catch (System.Exception ex)
                    {
                        Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { inputID, buffer }, ex));
                    }
                    finally
                    {
                        xmlValues.Add(columnInfo.Name, XmlHelper.removeForbiddenXmlChars(columnValue));

                        if (GROUPBYFIELDLIST.Contains(columnInfo.Name))
                            UpdateGroupByValueLists(columnValue, columnInfo);
                    }
                }

                // Loop through all ExtendRecord strings and create a column element: 
                foreach (string key in EXTENDELEMENT.Keys)
                    xmlValues.Add(key, XmlHelper.removeForbiddenXmlChars(EXTENDELEMENT[key]));

                if (IsNewGroup)
                {
                    CloseXmlDocument();

                    MSMQHelper.SendToMessageQueue(MSMQName, MSMQEntryLabel, rowsProcessed, memoryStream);

                    OpenXmlDocument();

                    rowsProcessed = 0;

                    INITIALGROUPBYVALUES.Clear();
                    ACTUALGROUPBYVALUES.Clear();

                    Log.DebugFormat("ProcessInput(int inputID {0}, PipelineBuffer buffer {1})\nINITIALGROUPBYVALUES && ACTUALGROUPBYVALUES cleared!", inputID, buffer);
                }

                WriteXmlRow(xmlValues);
            }

            if (buffer.EndOfRowset)
            {
                CloseXmlDocument();

                MSMQHelper.SendToMessageQueue(MSMQName, MSMQEntryLabel, rowsProcessed, memoryStream);

                if (outputBuffer != null)
                    outputBuffer.SetEndOfRowset();
            }
        }

        private void WriteXmlRow(Dictionary<string, string> xmlValues)
        {
            // Create row element: 
            xmlWriter.WriteStartElement(XmlRecordName);

            foreach (var xmlValue in xmlValues)
            {
                // Use the SSIS column name as element name: 
                xmlWriter.WriteStartElement(xmlValue.Key);

                xmlWriter.WriteString(xmlValue.Value);

                // Close column element: 
                xmlWriter.WriteEndElement();
            }

            // Close row element: 
            xmlWriter.WriteEndElement();

            rowsProcessed++;

            xmlValues.Clear();
        }

        private void UpdateGroupByValueLists(object columnValue, XmlColumnInfo columnInfo)
        {
            Log.DebugFormat("UpdateGroupByValueLists(object columnValue {0}, XmlColumnInfo columnInfo {1})", columnValue, columnInfo);

            if (!INITIALGROUPBYVALUES.ContainsKey(columnInfo.Name))
            {
                INITIALGROUPBYVALUES.Add(columnInfo.Name, columnValue);

                Log.DebugFormat("UpdateGroupByValueLists(object columnValue {0}, XmlColumnInfo columnInfo {1})\nInitialGroupByValue added\nColumnName: {2}\nColumnValue: {3}", columnValue, columnInfo, columnInfo.Name, columnValue );
            }

            if (!ACTUALGROUPBYVALUES.ContainsKey(columnInfo.Name))
            {
                ACTUALGROUPBYVALUES.Add(columnInfo.Name, columnValue);

                Log.DebugFormat("UpdateGroupByValueLists(object columnValue {0}, XmlColumnInfo columnInfo {1})\nActualGroupByValue added\nColumnName: {2}\nColumnValue: {3}", columnValue, columnInfo, columnInfo.Name, columnValue);
            }
            else
            {
                ACTUALGROUPBYVALUES[columnInfo.Name] = columnValue;

                Log.DebugFormat("UpdateGroupByValueLists(object columnValue {0}, XmlColumnInfo columnInfo {1})\nActualGroupByValue updated\nColumnName: {2}\nColumnValue: {3}", columnValue, columnInfo, columnInfo.Name, columnValue);
            }
        }

        #endregion

        #region Helpers

        private void OpenXmlDocument()
        {
            Log.Debug("OpenXmlDocument()");

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
            Log.Debug("CloseXmlDocument()");

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

