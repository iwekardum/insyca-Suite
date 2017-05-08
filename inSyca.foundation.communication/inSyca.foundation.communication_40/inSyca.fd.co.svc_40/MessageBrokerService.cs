using inSyca.foundation.communication.itf;
using inSyca.foundation.communication.service.diagnostics;
using inSyca.foundation.communication.wcf;
using inSyca.foundation.framework;
using inSyca.foundation.framework.configuration;
using inSyca.foundation.framework.schedules;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Xml.Linq;

namespace inSyca.foundation.communication.service
{
    [ServiceBehavior(Namespace = "http://www.inSyca.com/messagebroker", InstanceContextMode = InstanceContextMode.Single)]
    public partial class MessageBrokerService : IMessageBroker, IDisposable
    {

        virtual public service_response serviceResponse { get; set; }
        virtual protected service_response.responseRow serviceResponseRow { get; set; }

        virtual protected AppSchedules appSchedules
        {
            get
            {
                return Configuration.GetAppSchedules();
            }
        }

        private Scheduler scheduler;

        public MessageBrokerService()
        {
            Log.Debug("MessageBrokerService()");

            bool bInitSuccess = true;

            if (!Init_ServiceResponse())
                bInitSuccess = false;

            if (!Init_Configuration())
                bInitSuccess = false;

            if (!Init_Schedules())
                bInitSuccess = false;

            if (!bInitSuccess)
                throw new Exception("Error initializing messagebroker, more Information can be found in the eventlog");
        }

        virtual public void Dispose()
        {
            Log.Debug("Dispose()");
        }

        virtual protected bool Init_ServiceResponse()
        {
            Log.Debug("Init_ServiceResponse()");

            serviceResponse = new service_response();
            serviceResponseRow = serviceResponse.response.NewresponseRow();

            return true;
        }

        virtual protected bool Init_Configuration()
        {
            Log.Debug("Init_Configuration()");

            return true;
        }

        virtual protected bool Init_Schedules()
        {
            Log.Debug("Init_Schedules()");

            if (appSchedules == null)
                return true;

            scheduler = new Scheduler();

            foreach (AppSchedulesInstanceElement appSchedule in appSchedules.Instances)
            {
                try
                {
                    Schedulation schedulation;
                    Task task = new Task(appSchedule, new DTaskStart(OnScheduledEvent));
                    scheduler.AddNewTask(task);
                    schedulation = new Schedulation(appSchedule.OccurrenceStartTime, task);
                    schedulation.OccurrenceStartTime = appSchedule.OccurrenceStartTime;
                    schedulation.OccurrenceStopTime = appSchedule.OccurrenceEndTime;
                    schedulation.OccurrenceType = appSchedule.Occurrence;
                    scheduler.AddNewScheduling(schedulation);

                    scheduler.StartScheduling();
                }
                catch (Exception ex)
                {
                    Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), null, ex ));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Specify what you want to happen when the Elapsed event is raised.
        /// </summary>
        /// <param name="task"></param>
        virtual protected void OnScheduledEvent(Task task)
        {
            Log.DebugFormat("Name: {0}\nCommand: {1}\nOccurence: {2}\nStartup Delay: {3}\nStart Date: {4}\nStart Time: {5}\nEnd Date: {6}\nEnd Time: {7}", 
                task.Name, 
                task.AppSchedule.Command, 
                task.AppSchedule.Occurrence, 
                task.AppSchedule.StartupDelay, 
                task.AppSchedule.StartDate, 
                task.AppSchedule.StartTime, 
                task.AppSchedule.EndDate, 
                task.AppSchedule.EndTime);
        }

        virtual protected bool InitServiceResponse(out BizTalkMessageWrapper outDocument)
        {
            Log.Debug("InitServiceResponse(out BizTalkMessageWrapper outDocument)");

            //document instance for WCF service response 
            outDocument = new BizTalkMessageWrapper();

            InitServiceResponse();

            return true;
        }

        virtual protected bool InitServiceResponse()
        {
            Log.Debug("InitServiceResponse()");

            //clear response table
            serviceResponse.response.Clear();
            //set start timestamp for function
            serviceResponseRow.timestamp_started = DateTime.Now;
            serviceResponseRow.message = "success";
            serviceResponseRow.status = 0;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inDocument"></param>
        /// <param name="outDocument"></param>
        /// <param name="xElementMessage"></param>
        /// <returns></returns>
        virtual protected bool AssembleServiceResponse(BizTalkMessageWrapper outDocument, XElement xElementMessage)
        {
            Log.DebugFormat("AssembleServiceResponse(BizTalkMessageWrapper outDocument {0}, XElement xElementMessage {1})", outDocument.BizTalkMessage, xElementMessage);

            AssembleServiceResponse(outDocument);

            try
            {
                XNamespace xNamespace = outDocument.BizTalkMessage.GetDefaultNamespace();
                XElement item = (from el in outDocument.BizTalkMessage.Descendants(xNamespace + "message") select el).FirstOrDefault();
                item.Value = string.Empty;
                item.Add(xElementMessage);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { outDocument.BizTalkMessage }, ex));
            }

            Log.DebugFormat("AssembleServiceResponse(BizTalkMessageWrapper outDocument {0}, XElement xElementMessage {2})", outDocument.BizTalkMessage, xElementMessage);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outDocument"></param>
        virtual protected bool AssembleServiceResponse(BizTalkMessageWrapper outDocument)
        {
            Log.DebugFormat("AssembleOutDocument(BizTalkMessageWrapper outDocument {0})", outDocument.BizTalkMessage);

            string xmlServiceResponse;

            AssembleServiceResponse(out xmlServiceResponse);

            XElement xmlOutDocument = XElement.Parse(xmlServiceResponse) as XElement;

            outDocument.BizTalkMessage = xmlOutDocument;

            Log.DebugFormat("AssembleOutDocument(BizTalkMessageWrapper outDocument {0})", outDocument.BizTalkMessage);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlServiceResponse"></param>
        /// <returns></returns>
        private bool AssembleServiceResponse(out string xmlServiceResponse)
        {
            serviceResponseRow.timestamp_finished = DateTime.Now;

            try
            {
                serviceResponse.response.AddresponseRow(serviceResponseRow);
                xmlServiceResponse = serviceResponse.GetXml();
            }
            catch (Exception ex)
            {
                xmlServiceResponse = string.Empty;

                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { xmlServiceResponse }, ex));

                return false;
            }

            Log.DebugFormat("AssembleServiceResponse(out string xmlServiceResponse {0})", xmlServiceResponse);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlServiceResponse"></param>
        /// <returns></returns>
        virtual protected bool AssembleServiceResponse()
        {
            Log.DebugFormat("AssembleServiceResponse()");

            serviceResponseRow.timestamp_finished = DateTime.Now;

            try
            {
                serviceResponse.response.AddresponseRow(serviceResponseRow);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), null, ex));

                return false;
            }

            return true;
        }

        public service_response getVersion()
        {
            InitServiceResponse();

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("--- SERVICE CALL: {0} ---", "getVersion()");

            Console.ForegroundColor = ConsoleColor.White;


            serviceResponseRow.status = 0;
            serviceResponseRow.message = Assembly.GetExecutingAssembly().FullName;

            AssembleServiceResponse();

            return serviceResponse;
        }

        virtual public service_response logMessage(BizTalkMessageWrapper inDocument)
        {
            InitServiceResponse();

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("--- SERVICE CALL: {0} ---", "logMessage(BizTalkMessageWrapper inDocument)");

            Console.ForegroundColor = ConsoleColor.White;

            serviceResponseRow.status = 404;
            serviceResponseRow.message = string.Format("No logging implemented in serviceclass: {0}!", Assembly.GetExecutingAssembly().FullName);

            AssembleServiceResponse();

            return serviceResponse;
        }
    }
}
