using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Xml.Linq;

namespace inSyca.foundation.integration.itf
{

	[ServiceContract(Namespace = "http://www.inSyca.com/ITrackingMonitor")]
	public interface ITrackingMonitor
	{
		[OperationContract(Action = "http://www.inSyca.com/ITrackingMonitor/GetVersion")]
		object getVersion();

		[OperationContract(Action = "http://www.inSyca.com/ITrackingMonitor/GetTrackingInformation")]
		IEnumerable<TrackingInformation> GetTrackingInformation(string command, Dictionary<string, object> parameters);
		[OperationContract(Action = "http://www.inSyca.com/ITrackingMonitor/GetKPI")]
		IEnumerable<KPI> GetKPI(string command, Dictionary<string, object> parameters);
		[OperationContract(Action = "http://www.inSyca.com/ITrackingMonitor/GetMetadata")]
		IEnumerable<MetaData> GetMetaData();
	}

	[DataContract]
	public class TrackingInformation
	{
		[DataMember]
		public int ID { get; set; }
		[DataMember]
		public Guid InterchangeID { get; set; }
		[DataMember]
		public DateTime StartTime { get; set; }
		[DataMember]
		public DateTime EndTime { get; set; }
		[DataMember]
		public string Host { get; set; }
		[DataMember]
		public string ServiceName { get; set; }
		[DataMember]
		public string Port { get; set; }
		[DataMember]
		public string Direction { get; set; }
		[DataMember]
		public string State { get; set; }
		[DataMember]
		public string Url { get; set; }

		[DataMember]
		public string Message { get; set; }
		[DataMember]
		public string MessageProperties { get; set; }
		[DataMember]
		public string Context { get; set; }

		[DataMember]
		public Dictionary<string, string> CustomFields { get; set; }
	}

	[DataContract]
	public class MetaData
	{
		[DataMember]
		public String Name { get; set; }
		[DataMember]
		public String Label { get; set; }
	}
	[DataContract]
	public class KPI
	{
		[DataMember]
		public String Item { get; set; }
		[DataMember]
		public String Direction { get; set; }
		[DataMember]
		public DateTime From { get; set; }
		[DataMember]
		public DateTime To { get; set; }
		[DataMember]
		public int Amount { get; set; }
		[DataMember]
		public XElement Params { get; set; }
	}
}
