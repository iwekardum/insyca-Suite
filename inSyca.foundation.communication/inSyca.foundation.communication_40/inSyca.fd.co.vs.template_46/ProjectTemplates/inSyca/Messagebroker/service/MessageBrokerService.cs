using inSyca.foundation.communication.itf;
using inSyca.foundation.communication.wcf;
using inSyca.foundation.framework;
using inSyca.foundation.framework.configuration;
using inSyca.foundation.framework.schedules;
using System;
using System.Reflection;
using System.ServiceModel;

namespace inSyca.messagebroker.ns
{
    [ServiceBehavior(Namespace = "inSyca.messagebroker", InstanceContextMode = InstanceContextMode.Single)]
    public partial class MessageBrokerService : inSyca.foundation.communication.service.MessageBrokerService
    {
        protected override bool Init_Configuration()
        {
            /*
            if (!Init_SQLDatabase())
                return false;
            */
            return base.Init_Configuration();
        }

        protected override bool Init_Schedules()
        {
            return base.Init_Schedules();
        }

        override protected AppSchedules appSchedules
        {
            get
            {
                return Configuration.GetAppSchedules();
            }
        }

        // Specify what you want to happen when the Elapsed event is 
        // raised.
        override protected void OnScheduledEvent(Task task)
        {
            switch (task.Name)
            {
                case "TestScheduler":
                    Log.InfoFormat("Name: {0}\nCommand: {1}\nOccurence: {2}\nStartup Delay: {3}\nStart Date: {4}\nStart Time: {5}\nEnd Date: {6}\nEnd Time: {7}", task.Name, task.AppSchedule.Command, task.AppSchedule.Occurrence, task.AppSchedule.StartupDelay, task.AppSchedule.StartDate, task.AppSchedule.StartTime, task.AppSchedule.EndDate, task.AppSchedule.EndTime);
                    break;

                default:
                    break;
            }
        }

        override public service_response logMessage(BizTalkMessageWrapper inDocument)
        {
            InitServiceResponse();

            Console.ForegroundColor = ConsoleColor.Red;

            Log.Info("--- SERVICE CALL: logMessage(BizTalkMessageWrapper inDocument) ---");

            Log.Info(inDocument.BizTalkMessage.ToString());

            Console.ForegroundColor = ConsoleColor.White;

            serviceResponseRow.status = 404;
            serviceResponseRow.message = string.Format("No logging implemented in serviceclass: {0}!", Assembly.GetExecutingAssembly().FullName);

            AssembleServiceResponse();

            return serviceResponse;
        }

        public void TestScheduledEvent(string scheduleName)
        {
            Task task = new Task(scheduleName);

            OnScheduledEvent(task);
        }
    }
}
