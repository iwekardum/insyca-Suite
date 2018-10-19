using System;
using System.ServiceModel;

namespace inSyca.foundation.integration.service
{
	public class TrackingServiceHost : ServiceHost
	{
		public TrackingServiceHost(Type serviceType) : base(serviceType)
		{
		}
	}
}
