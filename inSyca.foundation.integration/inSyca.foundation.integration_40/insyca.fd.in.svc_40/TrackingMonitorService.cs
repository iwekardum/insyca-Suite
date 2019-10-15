using inSyca.foundation.framework;
using inSyca.foundation.integration.itf;
using inSyca.foundation.integration.service.diagnostics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.ServiceModel;


namespace inSyca.foundation.integration.service
{
	[ServiceBehavior(Namespace = "http://www.inSyca.com/trackingmonitor", InstanceContextMode = InstanceContextMode.Single)]
	public class TrackingMonitorService : ITrackingMonitor, IDisposable
	{
		public void Dispose()
		{
		}

		public object getVersion()
		{
			return null;
		}

		public IEnumerable<TrackingInformation> GetTrackingInformationCollection(string command, Dictionary<string, object> parameters)
		{
            Log.Info(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { command, parameters }));

            SqlDataReader reader = null;
            List<TrackingInformation> lstMessages = new List<TrackingInformation>();

			int timeOffset = 2;
            bool hasAdditionalData = false;

			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand(command, con))
				{
					cmd.CommandTimeout = 0;

					foreach (var item in parameters)
						cmd.Parameters.AddWithValue(item.Key, item.Value);

					cmd.CommandType = CommandType.StoredProcedure;

                    try
                    { 
					    con.Open();
					    reader = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { command, parameters }, ex));

                        return lstMessages.AsEnumerable();
                    }

                    if (Enumerable.Range(0, reader.FieldCount).Any(i => string.Equals(reader.GetName(i), "propbag", StringComparison.OrdinalIgnoreCase))
                        && Enumerable.Range(0, reader.FieldCount).Any(i => string.Equals(reader.GetName(i), "context", StringComparison.OrdinalIgnoreCase))
                        && Enumerable.Range(0, reader.FieldCount).Any(i => string.Equals(reader.GetName(i), "part", StringComparison.OrdinalIgnoreCase)))
                        hasAdditionalData = true;

                    while (reader.Read())
                    {
                        DateTime dtValue;
                        TrackingInformation btMessage = new TrackingInformation();

                        btMessage.ID = (int)reader["id"];

                        if (!reader["interchangeid"].Equals(DBNull.Value))
                            btMessage.InterchangeID = Guid.Parse(reader["interchangeid"].ToString());

                        if (DateTime.TryParse(reader["starttime"].ToString(), out dtValue))
                            btMessage.StartTime = dtValue.AddHours(timeOffset);

                        if (DateTime.TryParse(reader["endtime"].ToString(), out dtValue))
                            btMessage.EndTime = dtValue.AddHours(timeOffset);

                        btMessage.Direction = reader["direction"].ToString();
                        btMessage.Port = reader["port"].ToString();
                        btMessage.Url = reader["url"].ToString();
                        btMessage.Host = reader["hostname"].ToString();

                        if(hasAdditionalData)
                            ExtractAdditionalData(command, parameters, reader, btMessage);

                        lstMessages.Add(btMessage);
                    }
                }
			}

			return lstMessages.AsEnumerable();
		}

        private static void ExtractAdditionalData(string command, Dictionary<string, object> parameters, SqlDataReader reader, TrackingInformation btMessage)
        {
            try
            {
                    btMessage.MessageProperties = reader["propbag"].ToString();
            }
            catch (IndexOutOfRangeException ex)
            {
                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { command, parameters }, ex));
            }

            try
            {
                    btMessage.Context = reader["context"].ToString();
            }
            catch (IndexOutOfRangeException ex)
            {
                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { command, parameters }, ex));
            }

            try
            {
                    btMessage.Message = reader["part"].ToString();
            }
            catch (IndexOutOfRangeException ex)
            {
                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { command, parameters }, ex));
            }
        }

        public IEnumerable<KPI> GetKPICollection(string command, Dictionary<string, object> parameters)
		{
            Log.Info(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { command, parameters }));

            List<KPI> lstKPI = new List<KPI>();
            SqlDataReader reader = null;


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand(command, con))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					foreach (var item in parameters)
						cmd.Parameters.AddWithValue(item.Key, item.Value);

                    try
                    {
                        con.Open();
                        reader = cmd.ExecuteReader();
                    }
                    catch(Exception ex)
                    {
                        Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { command, parameters }, ex));
                        return lstKPI.AsEnumerable();
                    }

					while (reader.Read())
					{
						KPI kpi = new KPI();

						kpi.Item = reader["port"].ToString();
						kpi.From = DateTime.Parse(reader["date_min"].ToString());
						kpi.To = DateTime.Parse(reader["date_max"].ToString());
						kpi.Amount = int.Parse(reader["messages"].ToString());

						lstKPI.Add(kpi);
					}

					reader.Close();
				}
			}

			return lstKPI.AsEnumerable();
		}

		public IEnumerable<Port> GetPortCollection(string command, Dictionary<string, object> parameters)
		{
            Log.Info(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { command, parameters }));

            List<Port> lstPort = new List<Port>();
            SqlDataReader reader = null;

            lstPort.Add(new Port { Name = "%%", FriendlyName = "All" });

			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand(command, con))
				{
					cmd.CommandTimeout = 0;

					foreach (var item in parameters)
						cmd.Parameters.AddWithValue(item.Key, item.Value);

					cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        con.Open();
                        reader = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { command, parameters }, ex));
                        return lstPort.AsEnumerable();
                    }

                    while (reader.Read())
					{
						Port btPort = new Port();

						btPort.Name = reader["name"].ToString();
						btPort.FriendlyName = reader["friendlyname"].ToString();

						lstPort.Add(btPort);
					}
				}
			}

			return lstPort.AsEnumerable();
		}

		public IEnumerable<MetaData> GetMetaDataCollection()
		{
            Log.Info(new LogEntry(MethodBase.GetCurrentMethod(), null));

            List<MetaData> metadata = new List<MetaData>();

			//var configuration = XElement.Load(Server.MapPath("~/App_Data/dashboard.xml")).Descendants("searchitems").Elements();

			//foreach (var searchElement in configuration)
			//	metadata.Add(new MetaData() { Name = searchElement.Name.LocalName, Label = searchElement.Name.LocalName });

			return metadata;
		}

	}
}
