using inSyca.foundation.integration.biztalk.tracking.data;
using inSyca.foundation.integration.biztalk.tracking.diagnostics;
using inSyca.foundation.integration.biztalk.tracking.helper;
using Microsoft.BizTalk.Agent.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace inSyca.foundation.integration.biztalk.tracking
{
    class Program
    {
        static Type compressionStreamsType;
        static string bizsqlMessagesConnection;
        static int maxDaysForInitialization;
        static CultureInfo cultureInfo;
        static string dateTimePattern;

        static void Main(string[] args)
        {
            string logString = "Main(string[] args)\nArguments:\n";

            if (args.Length < 1)
                logString += "none";
            else
                foreach (var argument in args)
                    logString += string.Format("{0}\n", argument);

            Log.InfoFormat("integration.tracking.processing started\n{0}", logString);

            string pipelineAssemblyName = string.Concat(ConfigurationManager.AppSettings["PipelineAssemblyLocation"], ConfigurationManager.AppSettings["PipelineAssemblyName"]);
            string compressionStreamsTypeName = ConfigurationManager.AppSettings["CompressionStreamsTypeName"];
            bizsqlMessagesConnection = ConfigurationManager.AppSettings["ConnectionBizSQL"];

            string dateTimeCulture = ConfigurationManager.AppSettings["DateTimeCulture"];

            Log.InfoFormat("DateTimeCulture {0}", dateTimeCulture);

            if (!string.IsNullOrEmpty(dateTimeCulture))
            {
                cultureInfo = new CultureInfo(dateTimeCulture);
                dateTimePattern = cultureInfo.DateTimeFormat.ShortDatePattern + " HH:mm:ss.fff";
            }
            else
                cultureInfo = null;

            if (!int.TryParse(ConfigurationManager.AppSettings["MaxDaysForInitialization"], out maxDaysForInitialization))
                maxDaysForInitialization = 1;

            Log.InfoFormat("MaxDaysForInitialization {0}", maxDaysForInitialization);

            try
            {
                Assembly pipelineAssembly = Assembly.LoadFrom(ConfigurationManager.AppSettings["PipelineAssemblyName"]);
                compressionStreamsType = pipelineAssembly.GetType(compressionStreamsTypeName, true);
            }
            catch (Exception ex)
            {
                Log.Error("Assembly.LoadFrom(ConfigurationManager.AppSettings['PipelineAssemblyName'])", ex);
            }

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ConnectionES"]))
                ProcessTrackingDataSQL();
            else
                ProcessTrackingDataES();
        }

        private static void ProcessTrackingDataES()
        {
            IAuthenticator authenticator = new HttpBasicAuthenticator(ConfigurationManager.AppSettings["userES"], ConfigurationManager.AppSettings["passwordES"]);

            Dictionary<string, string> _headers = new Dictionary<string, string>
                                                        {
                                                            { "Content-Type", "application/json" },
                                                        };

            Uri uri = new Uri($"{ConfigurationManager.AppSettings["ConnectionES"]}/biztalk/_search");
            JToken jsonResponse = RestWrapper.GetJsonResponse("POST", uri, _headers, PayLoad.SearchTimestamp, authenticator);

            DateTime[] timestampArray = jsonResponse
                .SelectTokens("hits.hits[*]._source")
                .Select(t => t["timestamp"].ToObject<DateTime>())
                .ToArray();

            DateTime lastTimestamp = DateTime.MinValue;
            if (timestampArray.Any())
                lastTimestamp = timestampArray.First();

            List<JObject> trackingMessages = GetMessagesFromBizTalk(lastTimestamp);

            if (trackingMessages.Any())
            {
                Log.InfoFormat("Messages found {0}", trackingMessages.Count);

                uri = new Uri($"{ConfigurationManager.AppSettings["ConnectionES"]}/biztalk/_bulk");
                JToken jsonResponseList = RestWrapper.GetJsonResponse("POST", uri, _headers, trackingMessages, authenticator);
            }
            else
                Log.InfoFormat("No Messages found");
        }
        static void ProcessTrackingDataSQL()
        {
            using (SqlConnection conSQLServer = new SqlConnection(ConfigurationManager.AppSettings["ConnectionConnectionSQL"]))
            {
                Log.Info("Update ports");

                conSQLServer.Open();

                using (SqlCommand sqlUpdatePorts = conSQLServer.CreateCommand())
                {
                    sqlUpdatePorts.CommandTimeout = 0;

                    sqlUpdatePorts.CommandText = "isc_update_ports";
                    sqlUpdatePorts.CommandType = CommandType.StoredProcedure;

                    var sqlUpdatePortsResult = sqlUpdatePorts.ExecuteScalar();

                    Log.InfoFormat("isc_update_ports: {0}", sqlUpdatePortsResult);
                }

                var table = new DataTable();
                using (var adapter = new SqlDataAdapter("SELECT TOP 0 * FROM isc_pipeline_messages", conSQLServer))
                    adapter.Fill(table);

                DateTime lastTimestamp;

                using (SqlCommand sqlGetLastTimestamp = conSQLServer.CreateCommand())
                {
                    sqlGetLastTimestamp.CommandTimeout = 0;

                    sqlGetLastTimestamp.CommandText = "isc_get_timestamp";
                    sqlGetLastTimestamp.CommandType = CommandType.StoredProcedure;

                    lastTimestamp = Convert.ToDateTime(sqlGetLastTimestamp.ExecuteScalar());

                    Log.InfoFormat("isc_get_timestamp: {0}", lastTimestamp);
                }

                List<JObject> test = GetMessagesFromBizTalk(lastTimestamp);

                foreach (var item in test)
                {
                    var row = table.NewRow();
                    row["messageinstanceid"] = item["messageinstanceid"];
                    row["serviceinstanceid"] = item["serviceinstanceid"];
                    row["activityid"] = item["activityid"];
                    row["timestamp"] = item["timestamp"];
                    row["servicetype"] = item["servicetype"];
                    row["direction"] = item["direction"];
                    row["adapter"] = item["adapter"];
                    row["port"] = item["port"];
                    row["url"] = item["url"];
                    row["servicename"] = item["servicename"];
                    row["hostname"] = item["hostname"];
                    row["starttime"] = item["starttime"];
                    row["endtime"] = item["endtime"];
                    row["state"] = item["state"];
                    //row["imgcontext"] = reader["imgcontext"];
                    //row["imgpropbag"] = reader["imgpropbag"];
                    //row["imgpart"] = reader["imgpart"];
                    row["context"] = item["context"];
                    row["interchangeid"] = item["interchangeID"];
                    row["propbag"] = item["imgpropbag"];
                    row["part"] = item["part"];
                    row["nNumFragments"] = item["nNumFragments"];
                    row["uidPartID"] = item["uidPartID"];
                    row["processed"] = DBNull.Value;
                    table.Rows.Add(row);
                }

                Log.InfoFormat("BulkCopy: {0}", table.Rows.Count);

                using (var bulk = new SqlBulkCopy(conSQLServer))
                {
                    bulk.BulkCopyTimeout = 0;
                    bulk.DestinationTableName = "isc_pipeline_messages";
                    bulk.WriteToServer(table);
                }
            }
        }

        private static List<JObject> GetMessagesFromBizTalk(DateTime lastTimestamp)
        {
            SqlConnection conSelect;
            object interchangeID;
            string strLastTimestamp;
            string strActualTimestamp;

            List<JObject> test = new List<JObject>();

            Log.Info($"lastTimestamp {lastTimestamp}");

            if (lastTimestamp.Equals(DateTime.MinValue) || lastTimestamp.Equals(DBNull.Value))
            {
                if (cultureInfo != null)
                    strLastTimestamp = DateTime.Now.AddDays(0 - maxDaysForInitialization).ToUniversalTime().ToString(dateTimePattern);
                else
                    strLastTimestamp = DateTime.Now.AddDays(0 - maxDaysForInitialization).ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            else
            {
                if (cultureInfo != null)
                    strLastTimestamp = lastTimestamp.ToString(dateTimePattern);
                else
                    strLastTimestamp = lastTimestamp.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }

            if (cultureInfo != null)
                strActualTimestamp = DateTime.Now.AddSeconds(-30).ToUniversalTime().ToString(dateTimePattern);
            else
                strActualTimestamp = DateTime.Now.AddSeconds(-30).ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fff");

            Log.Debug($"strActualTimestamp {strActualTimestamp} | strLastTimestamp {strLastTimestamp}");

            string decryptAndTransferMessagesConnectionSelectCommand = string.Format(Properties.Resources.vw_pipeline_messages.ToString(), strLastTimestamp, strActualTimestamp);

            Log.Debug($"select command: {decryptAndTransferMessagesConnectionSelectCommand.Substring(0,200)}...");

            using (conSelect = new SqlConnection(bizsqlMessagesConnection))
            {
                Log.Debug("OpenConnection");

                conSelect.Open();

                using (SqlCommand selectCmd = new SqlCommand(decryptAndTransferMessagesConnectionSelectCommand, conSelect))
                {
                    selectCmd.CommandTimeout = 0;

                    SqlDataReader reader = selectCmd.ExecuteReader();

                    Log.Info("ExecuteReader");

                    while (reader.Read())
                    {
                        test.Add(
                            new JObject
                            {
                                ["id"] = Guid.NewGuid(),
                                ["messageinstanceid"] = reader["messageinstanceid"].ToString(),
                                ["serviceinstanceid"] = reader["serviceinstanceid"].ToString(),
                                ["activityid"] = reader["activityid"].ToString(),
                                ["timestamp"] = Convert.ToDateTime(reader["timestamp"]),
                                ["servicetype"] = reader["servicetype"].ToString(),
                                ["direction"] = reader["direction"].ToString(),
                                ["adapter"] = reader["adapter"].ToString(),
                                ["port"] = reader["port"].ToString(),
                                ["url"] = reader["url"].ToString(),
                                ["servicename"] = reader["servicename"].ToString(),
                                ["hostname"] = reader["hostname"].ToString(),
                                ["starttime"] = reader["starttime"] != DBNull.Value ? Convert.ToDateTime(reader["starttime"]) : DateTime.MinValue,
                                ["endtime"] = reader["endtime"] != DBNull.Value ? Convert.ToDateTime(reader["endtime"]) : DateTime.MinValue,
                                ["state"] = reader["state"].ToString(),
                                //["imgcontext"] = reader["imgcontext"].ToString(),
                                //["imgpropbag"] = reader["imgpropbag"].ToString(),
                                //["imgpart"] = reader["imgpart"].ToString(),
                                ["context"] = getContext(reader["imgcontext"], out interchangeID).ToString(),
                                ["interchangeid"] = interchangeID.ToString(),
                                ["propbag"] = getPropertyBag(reader["imgpropbag"]).ToString(),
                                ["part"] = getMessage(reader["imgPart"], reader["nNumFragments"], reader["uidPartID"]).ToString(),
                                ["nNumFragments"] = reader["nNumFragments"].ToString(),
                                ["uidPartID"] = reader["uidPartID"].ToString(),
                                ["processed"] = false,
                            }
                        );
                    }
                }
            }

            return test;
        }

        static object getMessage(object readerImgPart, object readerNumFragments, object readerPartID)
        {
            SqlInt64 nNumFragments = 0;
            SqlGuid uidPartID = SqlGuid.Null;
            StringBuilder sb = new StringBuilder();

            if (readerNumFragments != DBNull.Value)
                nNumFragments = int.Parse(readerNumFragments.ToString());

            if (readerPartID != DBNull.Value)
                uidPartID = Guid.Parse(readerPartID.ToString());

            //Message Information available?
            if (!readerImgPart.Equals(DBNull.Value))
            {
                sb.Clear();

                SqlBinary imgData = new SqlBinary((byte[])readerImgPart);

                //Use memory stream and reflection to get the body
                MemoryStream imgstream = new MemoryStream(imgData.Value);
                StreamReader st = new StreamReader((Stream)compressionStreamsType.InvokeMember("Decompress", BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static, null, null, new object[] { (object)imgstream }));

                sb.Append(st.ReadToEnd());

                if (nNumFragments > 1)
                {
                    SqlConnection conSelectFragments = new SqlConnection(bizsqlMessagesConnection);
                    SqlCommand selectFragmentsCmd = new SqlCommand("SELECT uidPartID, nFragmentNumber, nOffsetStart, nOffsetEnd, imgFrag FROM BizTalkDTADb.dbo.btsv_Tracking_Fragments WHERE [uidPartID]=@partid order by [nFragmentNumber]", conSelectFragments);
                    selectFragmentsCmd.CommandTimeout = 600;
                    selectFragmentsCmd.Parameters.AddWithValue("partid", uidPartID);// uidPartID);

                    SqlDataReader fragmentsReader;
                    conSelectFragments.Open();
                    fragmentsReader = selectFragmentsCmd.ExecuteReader();

                    while (fragmentsReader.Read())
                    {
                        imgData = new SqlBinary((byte[])fragmentsReader["imgFrag"]);
                        imgstream = new MemoryStream(imgData.Value);
                        st = new StreamReader((Stream)compressionStreamsType.InvokeMember("Decompress", BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static, null, null, new object[] { (object)imgstream }));
                        sb.Append(st.ReadToEnd());
                    }
                }

                if (sb.Length > 0)
                    return sb.ToString();
            }
            return DBNull.Value;
        }

        static object getPropertyBag(object readerPropBag)
        {
            SqlBinary imgPropBag = null;

            //PropertyBag Information available?
            if (!readerPropBag.Equals(DBNull.Value))
            {
                imgPropBag = new SqlBinary((byte[])readerPropBag);

                XElement propertyBagX = new XElement("PropertyBag", new XAttribute("PropertiesCount", 0));

                //Use memory stream and IBTMessageAgentFactory to get context
                MemoryStream propertystream = new MemoryStream(imgPropBag.Value);
                IBasePropertyBag propertyBag = ((IBTMessageAgentFactory)((IBTMessageAgent)new BTMessageAgent())).CreatePropertyBag();
                ((IPersistStream)propertyBag).Load(propertystream);

                propertyBagX.Attribute("PropertiesCount").Value = propertyBag.CountProperties.ToString();

                for (int i = 0; i < propertyBag.CountProperties; ++i)
                {
                    string propName;
                    string propNamespace;
                    object propValue = propertyBag.ReadAt(i, out propName, out propNamespace);

                    propertyBagX.Add(new XElement("Property", new XAttribute("Name", propName), new XAttribute("Namespace", propNamespace), propValue));
                }

                return propertyBagX.ToString();
            }

            return DBNull.Value;
        }

        static object getContext(object readerContext, out object interchangeID)
        {
            SqlBinary contextData = null;

            //Context Information available?
            if (!readerContext.Equals(DBNull.Value))
            {
                contextData = new SqlBinary((byte[])readerContext);

                XElement contextInfoX = new XElement("ContextInfo", new XAttribute("PropertiesCount", 0));

                //Use memory stream and IBTMessageAgentFactory to get context
                MemoryStream contextstream = new MemoryStream(contextData.Value);
                IBaseMessageContext context = ((IBTMessageAgentFactory)((IBTMessageAgent)new BTMessageAgent())).CreateMessageContext();
                ((IPersistStream)context).Load(contextstream);

                contextInfoX.Attribute("PropertiesCount").Value = context.CountProperties.ToString();

                Guid interchangeid = Guid.Empty;

                for (int i = 0; i < context.CountProperties; ++i)
                {
                    string propName;
                    string propNamespace;
                    object propValue = context.ReadAt(i, out propName, out propNamespace);

                    contextInfoX.Add(new XElement("Property", new XAttribute("Name", propName), new XAttribute("Namespace", propNamespace), propValue));

                    if (propName == "InterchangeID")
                        interchangeid = Guid.Parse(propValue.ToString());
                }
                interchangeID = interchangeid;
                return contextInfoX.ToString();
            }
            interchangeID = DBNull.Value;
            return DBNull.Value;
        }

        static void DecryptTrackingData()
        {
            try
            {
                string decryptAndTransferMessagesConnection = ConfigurationManager.AppSettings["Connection"];
                string decryptAndTransferMessagesConnectionSelectCommand = ConfigurationManager.AppSettings["SelectCommand"];
                string decryptAndTransferMessagesConnectionUpdateCommand = ConfigurationManager.AppSettings["UpdateCommand"];
                string pipelineAssemblyName = string.Concat(ConfigurationManager.AppSettings["PipelineAssemblyLocation"], ConfigurationManager.AppSettings["PipelineAssemblyName"]);
                string compressionStreamsTypeName = ConfigurationManager.AppSettings["CompressionStreamsTypeName"];

                //Connection to DTA database on localhost                
                SqlConnection conSelect = new SqlConnection(decryptAndTransferMessagesConnection);
                SqlConnection conUpdate = new SqlConnection(decryptAndTransferMessagesConnection);

                try
                {
                    SqlCommand selectCmd = new SqlCommand(decryptAndTransferMessagesConnectionSelectCommand, conSelect);
                    selectCmd.CommandTimeout = 600;
                    SqlCommand updateCmd = new SqlCommand(decryptAndTransferMessagesConnectionUpdateCommand, conUpdate);
                    updateCmd.CommandTimeout = 600;
                    updateCmd.CommandType = System.Data.CommandType.StoredProcedure;

                    StringBuilder sb = new StringBuilder();

                    //Get the reader to retrieve the data 
                    SqlDataReader reader;
                    conSelect.Open();
                    conUpdate.Open();
                    reader = selectCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SqlBinary imgData = null;
                        SqlBinary imgPropBag = null;
                        SqlBinary contextData = null;
                        SqlInt64 nNumFragments = 0;
                        SqlGuid uidPartID = SqlGuid.Null;

                        if (reader["nNumFragments"] != DBNull.Value)
                            nNumFragments = int.Parse(reader["nNumFragments"].ToString());

                        if (reader["uidPartID"] != DBNull.Value)
                            uidPartID = Guid.Parse(reader["uidPartID"].ToString());

                        updateCmd.Parameters.Clear();

                        //Message Information available?
                        if (!reader["imgPart"].Equals(DBNull.Value))
                        {
                            sb.Clear();

                            imgData = new SqlBinary((byte[])reader["imgPart"]);

                            //Use memory stream and reflection to get the body
                            MemoryStream imgstream = new MemoryStream(imgData.Value);
                            Assembly pipelineAssembly = Assembly.LoadFrom(pipelineAssemblyName);
                            Type compressionStreamsType = pipelineAssembly.GetType(compressionStreamsTypeName, true);
                            StreamReader st = new StreamReader((Stream)compressionStreamsType.InvokeMember("Decompress", BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static, null, null, new object[] { (object)imgstream }));

                            sb.Append(st.ReadToEnd());

                            if (nNumFragments > 1)
                            {
                                SqlConnection conSelectFragments = new SqlConnection(decryptAndTransferMessagesConnection);
                                SqlCommand selectFragmentsCmd = new SqlCommand("SELECT * FROM vw_message_fragments WHERE [uidPartID]=@partid order by [nFragmentNumber]", conSelectFragments);
                                selectFragmentsCmd.CommandTimeout = 600;
                                selectFragmentsCmd.Parameters.AddWithValue("partid", uidPartID);// uidPartID);

                                SqlDataReader fragmentsReader;
                                conSelectFragments.Open();
                                fragmentsReader = selectFragmentsCmd.ExecuteReader();

                                while (fragmentsReader.Read())
                                {
                                    imgData = new SqlBinary((byte[])fragmentsReader["imgFrag"]);
                                    imgstream = new MemoryStream(imgData.Value);
                                    st = new StreamReader((Stream)compressionStreamsType.InvokeMember("Decompress", BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static, null, null, new object[] { (object)imgstream }));
                                    sb.Append(st.ReadToEnd());
                                }
                            }

                            updateCmd.Parameters.AddWithValue("part", sb.ToString());
                        }
                        else
                            updateCmd.Parameters.AddWithValue("part", DBNull.Value);

                        //PropertyBag Information available?
                        if (!reader["imgPropBag"].Equals(DBNull.Value))
                        {
                            imgPropBag = new SqlBinary((byte[])reader["imgPropBag"]);

                            XElement propertyBagX = new XElement("PropertyBag", new XAttribute("PropertiesCount", 0));

                            //Use memory stream and IBTMessageAgentFactory to get context
                            MemoryStream propertystream = new MemoryStream(imgPropBag.Value);
                            IBasePropertyBag propertyBag = ((IBTMessageAgentFactory)((IBTMessageAgent)new BTMessageAgent())).CreatePropertyBag();
                            ((IPersistStream)propertyBag).Load(propertystream);

                            propertyBagX.Attribute("PropertiesCount").Value = propertyBag.CountProperties.ToString();

                            for (int i = 0; i < propertyBag.CountProperties; ++i)
                            {
                                string propName;
                                string propNamespace;
                                object propValue = propertyBag.ReadAt(i, out propName, out propNamespace);

                                propertyBagX.Add(new XElement("Property", new XAttribute("Name", propName), new XAttribute("Namespace", propNamespace), propValue));
                            }

                            updateCmd.Parameters.AddWithValue("propBag", propertyBagX.ToString());
                        }
                        else
                            updateCmd.Parameters.AddWithValue("propBag", DBNull.Value);


                        //Context Information available?
                        if (!reader["imgContext"].Equals(DBNull.Value))
                        {
                            contextData = new SqlBinary((byte[])reader["imgContext"]);

                            XElement contextInfoX = new XElement("ContextInfo", new XAttribute("PropertiesCount", 0));

                            //Use memory stream and IBTMessageAgentFactory to get context
                            MemoryStream contextstream = new MemoryStream(contextData.Value);
                            IBaseMessageContext context = ((IBTMessageAgentFactory)((IBTMessageAgent)new BTMessageAgent())).CreateMessageContext();
                            ((IPersistStream)context).Load(contextstream);

                            contextInfoX.Attribute("PropertiesCount").Value = context.CountProperties.ToString();

                            Guid interchangeid = Guid.Empty;

                            for (int i = 0; i < context.CountProperties; ++i)
                            {
                                string propName;
                                string propNamespace;
                                object propValue = context.ReadAt(i, out propName, out propNamespace);

                                contextInfoX.Add(new XElement("Property", new XAttribute("Name", propName), new XAttribute("Namespace", propNamespace), propValue));

                                if (propName == "InterchangeID")
                                    interchangeid = Guid.Parse(propValue.ToString());
                            }
                            updateCmd.Parameters.AddWithValue("interchangeid", interchangeid);
                            updateCmd.Parameters.AddWithValue("context", contextInfoX.ToString());
                        }
                        else
                        {
                            updateCmd.Parameters.AddWithValue("interchangeid", DBNull.Value);
                            updateCmd.Parameters.AddWithValue("context", DBNull.Value);
                        }

                        updateCmd.Parameters.AddWithValue("id", reader["id"]);

                        Console.WriteLine("Update starting...: {0}", reader["id"]);

                        updateCmd.ExecuteNonQuery();

                        Console.WriteLine("Processed: {0}", reader["id"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    conSelect.Close();
                    conUpdate.Close();
                }

            }
            catch (Exception exSQL)
            {
                Console.WriteLine("Failed to get message from msgbox: " + exSQL.Message);
            }
        }

        static void UpdateInterchangeID()
        {
            try
            {
                string decryptAndTransferMessagesConnection = ConfigurationManager.AppSettings["Connection"];
                string decryptAndTransferMessagesConnectionSelectCommand = ConfigurationManager.AppSettings["SelectCommand"];
                string decryptAndTransferMessagesConnectionUpdateCommand = ConfigurationManager.AppSettings["UpdateCommand"];

                //Connection to DTA database on localhost                
                SqlConnection conSelect = new SqlConnection(decryptAndTransferMessagesConnection);
                SqlConnection conUpdate = new SqlConnection(decryptAndTransferMessagesConnection);

                try
                {
                    SqlCommand selectCmd = new SqlCommand("SELECT * FROM tbl_pipeline_messages", conSelect);
                    SqlCommand updateCmd = new SqlCommand("UPDATE tbl_pipeline_messages SET interchangeid=@interchangeid WHERE id = @id", conUpdate);
                    SqlDataReader reader;
                    conSelect.Open();
                    conUpdate.Open();
                    reader = selectCmd.ExecuteReader();
                    //Get the reader to retrieve the data 
                    while (reader.Read())
                    {
                        if (!reader["context"].Equals(DBNull.Value))
                        {
                            XElement xContext = XElement.Parse(reader["context"].ToString());
                            XElement result = xContext.Descendants("Property").FirstOrDefault(el => (string)el.Attribute("Name") == "InterchangeID");
                            Guid interchangeid = Guid.Parse(result.Value.ToString());
                            updateCmd.Parameters.Clear();
                            updateCmd.Parameters.AddWithValue("id", reader["id"]);
                            updateCmd.Parameters.AddWithValue("interchangeid", interchangeid);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    conSelect.Close();
                    conUpdate.Close();
                }
            }
            catch (Exception exSQL)
            {
                Console.WriteLine("Failed to get message from msgbox: " + exSQL.Message);
            }
        }
    }
}
