using inSyca.foundation.communication.service;
using inSyca.foundation.integration.service;
using System;
using System.ServiceModel;

namespace inSyca.foundation.unittest_40
{
	public class ConsoleServiceHost
	{
		public ServiceHost serviceHost = null;

		public void StartServiceHost()
		{
			Start();

			Console.WriteLine("The service is ready. Press 'Enter' to stop the service");
			Console.ReadLine();

			Stop();
		}


		#region Helper
		private void Start()
		{
			if (serviceHost != null)
			{
				serviceHost.Close();
			}

			// Create a ServiceHost for the CalculatorService type and 
			// provide the base address.

			//serviceHost = new MessageBrokerServiceHost(typeof(MessageBrokerService));
			serviceHost = new TrackingServiceHost(typeof(TrackingMonitorService));

			// Open the ServiceHostBase to create listeners and start 
			// listening for messages.
			serviceHost.Open();
		}

		private void Stop()
		{
			if (serviceHost != null)
			{
				serviceHost.Close();
				serviceHost = null;
			}
		}
		#endregion
	}

}
