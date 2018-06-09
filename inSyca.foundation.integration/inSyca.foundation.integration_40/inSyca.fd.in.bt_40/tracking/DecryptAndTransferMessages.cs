using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Data;
using System.Xml.Linq;
using Microsoft.BizTalk.Agent.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;


namespace inSyca.foundation.integration.biztalk.tracking
{
    class DecryptAndTransferMessages
    {
        public class btMessaging
        {

            public void Main()
            {
                try
                {
                    string decryptAndTransferMessagesConnection = @"Data Source=SRZMS450011\E2PBIZ;Initial Catalog=TestDB;Integrated Security=True";
                    string decryptAndTransferMessagesConnectionSelectCommand = "SELECT * FROM tbl_test1";
                    string decryptAndTransferMessagesConnectionUpdateCommand = "UPDATE tbl_test1 SET part=@part, context=@context, propBag=@propBag, processed=1 WHERE messageid = @messageid";
                    string pipelineAssemblyName = string.Concat(@"C:\Program Files (x86)\Microsoft BizTalk Server 2013 R2", @"\Microsoft.BizTalk.Pipeline.dll");
                    string compressionStreamsTypeName = "Microsoft.BizTalk.Message.Interop.CompressionStreams";

                    //Connection to DTA database on localhost                
                    SqlConnection conSelect = new SqlConnection(decryptAndTransferMessagesConnection);
                    SqlConnection conUpdate = new SqlConnection(decryptAndTransferMessagesConnection);

                    try
                    {
                        SqlCommand selectCmd = new SqlCommand(decryptAndTransferMessagesConnectionSelectCommand, conSelect);
                        SqlCommand updateCmd = new SqlCommand(decryptAndTransferMessagesConnectionUpdateCommand, conUpdate);

                        //Get the reader to retrieve the data 
                        SqlDataReader reader;
                        conSelect.Open();
                        conUpdate.Open();
                        reader = selectCmd.ExecuteReader();

                        while (reader.Read())
                        {
                            SqlBinary imgData = new SqlBinary((byte[])reader["imgPart"]);
                            SqlBinary imgPropBag = new SqlBinary((byte[])reader["imgPropBag"]);
                            SqlBinary contextData = new SqlBinary((byte[])reader["imgContext"]);

                            //Use memory stream and reflection to get the body
                            MemoryStream imgstream = new MemoryStream(imgData.Value);
                            Assembly pipelineAssembly = Assembly.LoadFrom(pipelineAssemblyName);
                            Type compressionStreamsType = pipelineAssembly.GetType(compressionStreamsTypeName, true);
                            StreamReader st = new StreamReader((Stream)compressionStreamsType.InvokeMember("Decompress", BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static, null, null, new object[] { (object)imgstream }));

                            XElement propertyBagX = new XElement("PropertyBag", new XAttribute("PropertiesCount", 0));
                            XElement contextInfoX = new XElement("ContextInfo", new XAttribute("PropertiesCount", 0));

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

                            //Use memory stream and IBTMessageAgentFactory to get context
                            MemoryStream contextstream = new MemoryStream(contextData.Value);
                            IBaseMessageContext context = ((IBTMessageAgentFactory)((IBTMessageAgent)new BTMessageAgent())).CreateMessageContext();
                            ((IPersistStream)context).Load(contextstream);

                            contextInfoX.Attribute("PropertiesCount").Value = context.CountProperties.ToString();

                            for (int i = 0; i < context.CountProperties; ++i)
                            {
                                string propName;
                                string propNamespace;
                                object propValue = context.ReadAt(i, out propName, out propNamespace);

                                contextInfoX.Add(new XElement("Property", new XAttribute("Name", propName), new XAttribute("Namespace", propNamespace), propValue));
                            }

                            updateCmd.Parameters.Clear();
                            updateCmd.Parameters.AddWithValue("messageid", reader["messageid"]);
                            updateCmd.Parameters.AddWithValue("part", st.ReadToEnd());
                            updateCmd.Parameters.AddWithValue("context", contextInfoX.ToString());
                            updateCmd.Parameters.AddWithValue("propBag", propertyBagX.ToString());
                            updateCmd.ExecuteNonQuery();

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
}
