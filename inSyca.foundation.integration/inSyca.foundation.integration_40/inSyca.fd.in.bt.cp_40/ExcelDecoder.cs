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

using inSyca.foundation.framework;
using inSyca.foundation.integration.biztalk.components.diagnostics;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Xml;

namespace inSyca.foundation.integration.biztalk.components
{

    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [ComponentCategory(CategoryTypes.CATID_Decoder)]
    [System.Runtime.InteropServices.Guid("dd0af82f-01dc-4aa1-9d0f-c613aa323c69")]
    public class Excel2007Decoder : IBaseComponent, Microsoft.BizTalk.Component.Interop.IComponent, IPersistPropertyBag, IComponentUI
    {
        static private readonly ResourceManager _resourceManager = new ResourceManager("inSyca.foundation.integration.biztalk.components.Properties.Resources", Assembly.GetExecutingAssembly());

        static string namespaceBaseName = "NamespaceBase";
        static string rootNodeNameName = "RootNodeName";
        static string namespacePrefixName = "NamespacePrefix";
        static string isFirstRowHeaderName = "IsFirstRowHeader";
        static string tempFolderName = "TempFolder";

        #region Properties

        public string NamespaceBase { get; set; }
        public string NamespacePrefix { get; set; }
        public string RootNodeName { get; set; }
        public bool IsFirstRowHeader { get; set; }
        public string TempFolder { get; set; }

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
                return _resourceManager.GetString("excelDecoder_name");
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
                return _resourceManager.GetString("excelDecoder_version");
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
                return _resourceManager.GetString("excelDecoder_description");
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
        public void GetClassID(out Guid classid)
        {
            classid = new Guid("dd0af82f-08dc-4aa1-9d0f-c613aa323c69");
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
        public virtual void Load(IPropertyBag pb, int errlog)
        {
            object val = null;
            val = ReadPropertyBag(pb, namespaceBaseName);
            if ((val != null))
            {
                NamespaceBase = ((string)(val));
            }
            val = ReadPropertyBag(pb, rootNodeNameName);
            if ((val != null))
            {
                RootNodeName = ((string)(val));
            }
            val = ReadPropertyBag(pb, namespacePrefixName);
            if ((val != null))
            {
                NamespacePrefix = ((string)(val));
            }

            val = ReadPropertyBag(pb, isFirstRowHeaderName);
            if ((val != null))
            {
                IsFirstRowHeader = ((bool)(val));
            }

            val = ReadPropertyBag(pb, tempFolderName);
            if ((val != null))
            {
                TempFolder = ((string)(val));
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
            WritePropertyBag(pb, namespaceBaseName, NamespaceBase);
            WritePropertyBag(pb, namespacePrefixName, NamespacePrefix);
            WritePropertyBag(pb, rootNodeNameName, RootNodeName);
            WritePropertyBag(pb, isFirstRowHeaderName, IsFirstRowHeader);
            WritePropertyBag(pb, tempFolderName, TempFolder);
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
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, argEx));
                return val;
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
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
                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), null, ex));
                throw new ApplicationException(ex.Message);
            }
        }

        #endregion

        #region IComponentUI Members

        /// <summary>
        /// Component icon to use in BizTalk Editor
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

            if(string.IsNullOrEmpty(TempFolder))
                TempFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            if (string.IsNullOrEmpty(NamespaceBase))
                NamespaceBase = "http://insyca.bt.gl.esb.schemas.integration.components.exceldecoder_10";

            if (string.IsNullOrEmpty(RootNodeName))
                RootNodeName = "exceldecoder";

            if (string.IsNullOrEmpty(NamespacePrefix))
                RootNodeName = "ex0";

            var outMsg = pipelineContext.GetMessageFactory().CreateMessage();

            try
            {
                if (inMsg == null || inMsg.BodyPart == null || inMsg.BodyPart.Data == null)
                {
                    diagnostics.Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, "pInMsg Error", null));
                    throw new ArgumentNullException("pInMsg");
                }

                var fileAdapterTargetNamespace = "http://schemas.microsoft.com/BizTalk/2003/file-properties";
                var bodyPart = inMsg.BodyPart;

                var inputFileName = Convert.ToString(inMsg.Context.Read("ReceivedFileName", fileAdapterTargetNamespace));

                var inboundStream = bodyPart.GetOriginalDataStream();

                MemoryStream stream = ConvertExceltoXML(inboundStream, inputFileName);

                stream.Seek(0, SeekOrigin.Begin);
                outMsg.AddPart("Body", pipelineContext.GetMessageFactory().CreateMessagePart(), true);
                outMsg.BodyPart.Data = stream;

                //Promote properties if required.
                for (int iProp = 0; iProp < inMsg.Context.CountProperties; iProp++)
                {
                    string strName;
                    string strNSpace;
                    object val = inMsg.Context.ReadAt(iProp, out strName, out strNSpace);

                    // If the property has been promoted, respect the settings
                    if (inMsg.Context.IsPromoted(strName, strNSpace))
                        outMsg.Context.Promote(strName, strNSpace, val);
                    else
                        outMsg.Context.Write(strName, strNSpace, val);

                    //update the ReceivedFileName with the actual file entry name
                    if (strName == "ReceivedFileName")
                    {
                        outMsg.Context.Write(strName, strNSpace, inputFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                diagnostics.Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
            }
          
            return outMsg;
        }

        #endregion

        #region Helper

        private MemoryStream ConvertExceltoXML(Stream excelStream, string inputFileName)
        {
            Log.DebugFormat("ConvertExceltoXML(Stream excelStream {0}, string inputFileName {1})", excelStream, inputFileName);

            var tempDir = string.Format("{0}\\{1}", TempFolder, Guid.NewGuid());
            string fileName = string.Format("{0}\\{1}", tempDir, Path.GetFileName(inputFileName));
            string HDR = IsFirstRowHeader ? "Yes" : "No";
            string strConn;

            try
            {
                Log.DebugFormat("ConvertExceltoXML(Stream excelStream {0}, string inputFileName {1})\ntempDir: {2}", excelStream, inputFileName, tempDir);

                if (!Directory.Exists(tempDir))
                {
                    Directory.CreateDirectory(tempDir);
                }

                Log.DebugFormat("ConvertExceltoXML(Stream excelStream {0}, string inputFileName {1})\nfileName: {2}", excelStream, inputFileName, fileName);

                using (FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    excelStream.CopyTo(file);
                    using (StreamWriter s = new StreamWriter(file))
                        s.Flush();
                }

                if (fileName.Substring(fileName.LastIndexOf('.')).ToLower() == ".xlsx")
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";

                DataSet dataSet = new DataSet(RootNodeName);
                dataSet.Namespace = NamespaceBase;
                dataSet.Prefix = NamespacePrefix;

                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    DataTable schemaTable = conn.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                    foreach (DataRow schemaRow in schemaTable.Rows)
                    {
                        string sheet = schemaRow["TABLE_NAME"].ToString();

                        if (!sheet.EndsWith("_"))
                        {
                            try
                            {
                                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                                cmd.CommandType = CommandType.Text;

                                DataTable outputTable = new DataTable(sheet);
                                outputTable.Namespace = NamespaceBase;
                                outputTable.Prefix = NamespacePrefix;
                                dataSet.Tables.Add(outputTable);
                                new OleDbDataAdapter(cmd).Fill(outputTable);
                            }
                            catch (Exception ex)
                            {
                                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), null, ex));

                                throw new Exception(ex.Message + string.Format("Sheet:{0}.File:F{1}", sheet, fileName), ex);
                            }
                        }
                    }
                }

                var stream = new MemoryStream();
                dataSet.WriteXml(stream);
                return stream;
            }
            catch (Exception ex)
            {
                diagnostics.Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
                throw ex;
            }
            finally
            {
                //// Clean up
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }

        public static Stream ConvertExceltoXML(Stream inputFileStream, bool hasHeaders, bool autoDetectColumnType)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new XmlTextReader(inputFileStream));
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

            nsmgr.AddNamespace("o", "urn:schemas-microsoft-com:office:office");
            nsmgr.AddNamespace("x", "urn:schemas-microsoft-com:office:excel");
            nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");

            DataSet dataSet = new DataSet();

            foreach (XmlNode node in
              doc.DocumentElement.SelectNodes("//ss:Worksheet", nsmgr))
            {
                DataTable dt = new DataTable(node.Attributes["ss:Name"].Value);
                dataSet.Tables.Add(dt);
                XmlNodeList rows = node.SelectNodes("ss:Table/ss:Row", nsmgr);
                if (rows.Count > 0)
                {

                    //*************************
                    //Add Columns To Table from header row
                    //*************************
                    List<ColumnType> columns = new List<ColumnType>();
                    int startIndex = 0;
                    if (hasHeaders)
                    {
                        foreach (XmlNode data in rows[0].SelectNodes("ss:Cell/ss:Data", nsmgr))
                        {
                            columns.Add(new ColumnType(typeof(string)));//default to text
                            dt.Columns.Add(data.InnerText, typeof(string));
                        }
                        startIndex++;
                    }
                    //*************************
                    //Update Data-Types of columns if Auto-Detecting
                    //*************************
                    if (autoDetectColumnType && rows.Count > 0)
                    {
                        XmlNodeList cells = rows[startIndex].SelectNodes("ss:Cell", nsmgr);
                        int actualCellIndex = 0;
                        for (int cellIndex = 0; cellIndex < cells.Count; cellIndex++)
                        {
                            XmlNode cell = cells[cellIndex];
                            if (cell.Attributes["ss:Index"] != null)
                                actualCellIndex =
                                  int.Parse(cell.Attributes["ss:Index"].Value) - 1;

                            ColumnType autoDetectType =
                              getType(cell.SelectSingleNode("ss:Data", nsmgr));

                            if (actualCellIndex >= dt.Columns.Count)
                            {
                                dt.Columns.Add("Column" +
                                  actualCellIndex.ToString(), autoDetectType.type);
                                columns.Add(autoDetectType);
                            }
                            else
                            {
                                dt.Columns[actualCellIndex].DataType = autoDetectType.type;
                                columns[actualCellIndex] = autoDetectType;
                            }

                            actualCellIndex++;
                        }
                    }
                    //*************************
                    //Load Data
                    //*************************
                    for (int i = startIndex; i < rows.Count; i++)
                    {
                        DataRow row = dt.NewRow();
                        XmlNodeList cells = rows[i].SelectNodes("ss:Cell", nsmgr);
                        int actualCellIndex = 0;
                        for (int cellIndex = 0; cellIndex < cells.Count; cellIndex++)
                        {
                            XmlNode cell = cells[cellIndex];
                            if (cell.Attributes["ss:Index"] != null)
                                actualCellIndex = int.Parse(cell.Attributes["ss:Index"].Value) - 1;

                            XmlNode data = cell.SelectSingleNode("ss:Data", nsmgr);

                            if (actualCellIndex >= dt.Columns.Count)
                            {
                                for (int j = dt.Columns.Count; j < actualCellIndex; j++)
                                {
                                    dt.Columns.Add("Column" +
                                               actualCellIndex.ToString(), typeof(string));
                                    columns.Add(getDefaultType());
                                }
                                ColumnType autoDetectType =
                                   getType(cell.SelectSingleNode("ss:Data", nsmgr));
                                dt.Columns.Add("Column" + actualCellIndex.ToString(),
                                               typeof(string));
                                columns.Add(autoDetectType);
                            }
                            if (data != null)
                                row[actualCellIndex] = data.InnerText;

                            actualCellIndex++;
                        }

                        dt.Rows.Add(row);
                    }
                }
            }

            var stream = new MemoryStream();
            dataSet.WriteXml(stream);
            dataSet.WriteXml(@"c:\temp\excel.xml");
            return stream;
        }

        struct ColumnType
        {
            public Type type;
            private string name;
            public ColumnType(Type type) { this.type = type; this.name = type.ToString().ToLower(); }
            public object ParseString(string input)
            {
                if (String.IsNullOrEmpty(input))
                    return DBNull.Value;
                switch (type.ToString())
                {
                    case "system.datetime":
                        return DateTime.Parse(input);
                    case "system.decimal":
                        return decimal.Parse(input);
                    case "system.boolean":
                        return bool.Parse(input);
                    default:
                        return input;
                }
            }
        }

        private static ColumnType getDefaultType()
        {
            return new ColumnType(typeof(String));
        }

        private static ColumnType getType(XmlNode data)
        {
            string type = null;
            if (data.Attributes["ss:Type"] == null || data.Attributes["ss:Type"].Value == null)
                type = "";
            else
                type = data.Attributes["ss:Type"].Value;

            switch (type)
            {
                case "DateTime":
                    return new ColumnType(typeof(DateTime));
                case "Boolean":
                    return new ColumnType(typeof(Boolean));
                case "Number":
                    return new ColumnType(typeof(Decimal));
                case "":
                    decimal test2;
                    if (data == null || String.IsNullOrEmpty(data.InnerText) || decimal.TryParse(data.InnerText, out test2))
                    {
                        return new ColumnType(typeof(Decimal));
                    }
                    else
                    {
                        return new ColumnType(typeof(String));
                    }
                default://"String"
                    return new ColumnType(typeof(String));
            }
        }
        #endregion
    }
}
