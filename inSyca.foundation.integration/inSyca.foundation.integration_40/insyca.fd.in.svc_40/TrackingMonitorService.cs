using inSyca.foundation.integration.itf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            SqlDataReader reader = null;
            List<TrackingInformation> lstMessages = new List<TrackingInformation>();

			int timeOffset = 2;

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
                        return lstMessages.AsEnumerable();
                    }

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

						try
						{
							btMessage.MessageProperties = reader["propbag"].ToString();
						}
						catch (IndexOutOfRangeException)
						{
						}

						try
						{
							btMessage.Context = reader["context"].ToString();
						}
						catch (IndexOutOfRangeException)
						{
						}

						try
						{
							btMessage.Message = reader["part"].ToString();
						}
						catch (IndexOutOfRangeException)
						{
						}

						lstMessages.Add(btMessage);
					}
				}
			}

			return lstMessages.AsEnumerable();
		}

		public IEnumerable<KPI> GetKPICollection(string command, Dictionary<string, object> parameters)
		{
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
			List<MetaData> metadata = new List<MetaData>();

			//var configuration = XElement.Load(Server.MapPath("~/App_Data/dashboard.xml")).Descendants("searchitems").Elements();

			//foreach (var searchElement in configuration)
			//	metadata.Add(new MetaData() { Name = searchElement.Name.LocalName, Label = searchElement.Name.LocalName });

			return metadata;
		}

	}
}
