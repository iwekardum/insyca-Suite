using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

namespace inSyca.foundation.unittest_40
{
	[TestClass]
	public class UT_IntegrationService
	{
		[TestMethod]
		public void TestTracking()
		{
			Thread th = new Thread(() => StartService());
			th.Start();

			svcTrackingMonitor.ITrackingMonitor svcTrackingMonitor = new svcTrackingMonitor.TrackingMonitorClient();

			object response = svcTrackingMonitor.getVersion();

			Dictionary < string, object> parameters = new Dictionary<string, object>();

			parameters.Add("@searchvalue", "%_mb_%");
			parameters.Add("@resultrows", 100);

			svcTrackingMonitor.message[] msg = svcTrackingMonitor.GetMessages("proc_select_fieldnames", parameters);
		}

		private void StartService()
		{
			ConsoleServiceHost csh = new ConsoleServiceHost();
			csh.StartServiceHost();
		}
	}
}
