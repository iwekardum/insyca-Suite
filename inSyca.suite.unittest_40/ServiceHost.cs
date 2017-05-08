using inSyca.foundation.communication.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using System.Windows;

namespace inSyca.foundation.communication.unittest_40
{
    [TestClass]
    public class UT_ServiceHost
    {
        public ServiceHost serviceHostMessageBroker = null;
        public ServiceHost serviceHostBizTalk = null;
        public ServiceHost serviceHostOffice = null;
        public ServiceHost serviceHostIntranet = null;

        [TestMethod]
        public void StartServiceHost()
        {
            UT_ServiceHost serviceHost = new UT_ServiceHost();

            serviceHost.Start();

            MessageBox.Show("The service is ready.");

            serviceHost.Stop();
        }


        #region Helper
        public void Start()
        {
            if (serviceHostMessageBroker != null)
            {
                serviceHostMessageBroker.Close();
            }

            // Create a ServiceHost for the CalculatorService type and 
            // provide the base address.
            serviceHostMessageBroker = new MessageBrokerServiceHost(typeof(MessageBrokerService));

            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            serviceHostMessageBroker.Open();
        }

        public void Stop()
        {
            if (serviceHostMessageBroker != null)
            {
                serviceHostMessageBroker.Close();
                serviceHostMessageBroker = null;
            }
        }
        #endregion
    }

}
