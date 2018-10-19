using inSyca.foundation.integration.itf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Xml.Linq;


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

		public IEnumerable<TrackingInformation> GetTrackingInformation(string command, Dictionary<string, object> parameters)
		{
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

					con.Open();
					SqlDataReader reader = cmd.ExecuteReader();

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

		public IEnumerable<KPI> GetKPI(string command, Dictionary<string, object> parameters)
		{
			List<KPI> lstKPI = new List<KPI>();

			return lstKPI;

			//var configuration = XElement.Load(Server.MapPath("~/App_Data/dashboard.xml")).Descendants("kpi").Descendants("item");

			//using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
			//{
			//	con.Open();

			//	using (SqlCommand cmd = new SqlCommand("proc_get_messagecount", con))
			//	{
			//		cmd.CommandType = CommandType.StoredProcedure;

			//		foreach (XElement item in configuration)
			//		{
			//			KPI kpi = new KPI();

			//			DateTime minDate = DateTime.MaxValue;
			//			DateTime maxDate = DateTime.MinValue;

			//			cmd.Parameters.Clear();
			//			cmd.Parameters.Add(new SqlParameter("date_min", searchParameters.From));
			//			cmd.Parameters.Add(new SqlParameter("date_max", searchParameters.To));
			//			cmd.Parameters.Add(new SqlParameter("item", item.ToString()));

			//			SqlDataReader reader = cmd.ExecuteReader();


			//			while (reader.Read())
			//			{
			//				DateTime dtmin = DateTime.Parse(reader["date_min"].ToString());
			//				DateTime dtmax = DateTime.Parse(reader["date_max"].ToString());
			//				if (dtmin < minDate)
			//					minDate = dtmin;

			//				if (dtmax > maxDate)
			//					maxDate = dtmax;

			//				kpi.Amount += int.Parse(reader["messages"].ToString());
			//			}

			//			kpi.From = minDate;
			//			kpi.To = maxDate;

			//			kpi.Item = item.Attribute("name").Value;
			//			kpi.Direction = item.Attribute("direction").Value;
			//			kpi.Params = item;

			//			if (kpi.Amount > 0)
			//				lstKPI.Add(kpi);

			//			reader.Close();
			//		}
			//	}
			//}

			//return lstKPI.AsEnumerable();
		}

		public IEnumerable<MetaData> GetMetaData()
		{
			List<MetaData> metadata = new List<MetaData>();

			//var configuration = XElement.Load(Server.MapPath("~/App_Data/dashboard.xml")).Descendants("searchitems").Elements();

			//foreach (var searchElement in configuration)
			//	metadata.Add(new MetaData() { Name = searchElement.Name.LocalName, Label = searchElement.Name.LocalName });

			return metadata;
		}

	}
}
