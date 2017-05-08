using inSyca.foundation.framework;
using inSyca.foundation.framework.diagnostics;
using inSyca.foundation.integration.biztalk.data;
using Microsoft.BizTalk.Operations;
using System;
using System.Data;
using System.Management;

namespace inSyca.foundation.integration.biztalk.diagnostics
{
    /// <summary>
    /// 
    /// </summary>
    internal class BizTalkEventType : EventType
    {
        internal const string hostInstanceEvent = "HostInstance Event";
        internal const string suspendedMessageEvent = "SuspendedMessage Event";
        internal const string receiveLocationEvent = "ReceiveLocation Event";
        internal const string sendPortEvent = "SendPort Event";
    }

    /// <summary>
    /// 
    /// </summary>
    public class MonitorBizTalk : Monitor<MonitorBizTalk>
    {
        private enum SendPortStatus { Bound = 1, Stopped = 2, Started = 3 };
        private enum HostInstanceServiceState { Stopped = 1, StartPending = 2, StopPending = 3, Running = 4, Continuepending = 5, PausePending = 6, Paused = 7, Unknown = 8 };
        private enum HostInstanceType { InProcess = 1, Isolated = 2 };
        private enum ConfigurationState { Installed = 1, InstallationFailed = 2, UninstallationFailed = 3, UpdateFailed = 4, NotInstalled = 5 };
        private enum ClusterInstanceType { UnClusteredInstance = 0, ClusteredInstance = 1, ClusteredVirtualInstance = 2 };
        private enum ServiceStatus { ReadyToRun = 1, Active = 2, SuspendedResumable = 4, Dehydrated = 8, CompletedWithDiscardedMessages = 16, SuspendedNotResumable = 32, InBreakpoint = 64 };
        private enum ServiceClass { Orchestration = 1, Tracking = 2, Messaging = 4, MSMQT = 8, Other = 16, IsolatedAdapter = 32, RoutingFailureReport = 64 };

        /// <summary>
        /// 
        /// </summary>
        public MonitorBizTalk()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArrivedEventArgs"></param>
        /// <param name="eventType"></param>
        /// <param name="logEntryDataTable"></param>
        protected override void SetupLogEntry(EventArrivedEventArgs eventArrivedEventArgs, string eventType, out DataTable logEntryDataTable)
        {
            Log.DebugFormat("SetupLogEntry(EventArrivedEventArgs eventArrivedEventArgs {0}, string eventType {1}, out DataTable logEntryDataTable)", eventArrivedEventArgs, eventType);

            dsLogEntry logEntry = new dsLogEntry();
            logEntry.EnforceConstraints = false;
            dsLogEntry.dtLogEntryRow logEntryRow = logEntry.dtLogEntry.NewdtLogEntryRow();

            logEntryRow.dtLogEntry_Id = Guid.NewGuid().ToString();
            logEntryRow.eventtype = eventType;

            SetLogEntry(eventArrivedEventArgs, eventType, logEntry, logEntryRow);

            logEntry.dtLogEntry.AdddtLogEntryRow(logEntryRow);
            logEntry.AcceptChanges();
            logEntry.EnforceConstraints = true;

            logEntryDataTable = logEntry.dtLogEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logEntryTable"></param>
        /// <param name="eventEntryDataRow"></param>
        /// <returns></returns>
        protected override int SetupEventEntry(DataTable logEntryTable, out DataRow eventEntryDataRow)
        {
            Log.DebugFormat("SetupEventEntry(DataTable logEntryTable {0}, out DataRow eventEntryDataRow))", logEntryTable);

            dsEventEntry eventEntry = new dsEventEntry();
            eventEntryDataRow = eventEntry.dtEventEntry.NewdtEventEntryRow();

            TransformLogEntry(logEntryTable, eventEntryDataRow);

            eventEntry.dtEventEntry.AdddtEventEntryRow((dsEventEntry.dtEventEntryRow)eventEntryDataRow);
            eventEntry.AcceptChanges();

            return Convert.ToInt32(((dsEventEntry.dtEventEntryRow)eventEntryDataRow).entrytypecode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="logEntryDataSet"></param>
        /// <param name="logEntryDataRow"></param>
        /// <param name="managementBaseObject"></param>
        protected override void InsertAdditionalEntryRow(string eventType, DataSet logEntryDataSet, DataRow logEntryDataRow, ManagementBaseObject managementBaseObject)
        {
            Log.DebugFormat("InsertAdditionalEntryRow(string eventType {0}, DataSet logEntryDataSet {1}, DataRow logEntryDataRow {2}, ManagementBaseObject managementBaseObject {3})", eventType, logEntryDataSet, logEntryDataRow, managementBaseObject);

            if (eventType != BizTalkEventType.suspendedMessageEvent)
            {
                Log.DebugFormat("InsertAdditionalEntryRow(string eventType {0}, DataSet logEntryDataSet {1}, DataRow logEntryDataRow {2}, ManagementBaseObject managementBaseObject {3})\neventType != BizTalkEventType.suspendedMessageEvent - No AdditionalEntryRow needed", eventType, logEntryDataSet, logEntryDataRow, managementBaseObject);
                return;
            }

            dsLogEntry logEntry = (dsLogEntry)logEntryDataSet;
            dsLogEntry.dtLogEntryRow logEntryRow = (dsLogEntry.dtLogEntryRow)logEntryDataRow;

            dsLogEntry.dtManagementBaseObjectRow managementBaseObjectRow = logEntry.dtManagementBaseObject.NewdtManagementBaseObjectRow();
            managementBaseObjectRow.dtLogEntryRow = logEntryRow;
            managementBaseObjectRow.classname = managementBaseObject.ClassPath.ClassName;

            PropertyDataCollection propertyDataCollection = managementBaseObject.Properties;
            PropertyDataCollection.PropertyDataEnumerator propertyDataEnumerator = propertyDataCollection.GetEnumerator();

            while (propertyDataEnumerator.MoveNext())
            {
                PropertyData data = propertyDataEnumerator.Current;

                if (data.Value != null)
                {
                    dsLogEntry.dtPropertiesRow propertiesRow = logEntry.dtProperties.NewdtPropertiesRow();
                    propertiesRow.dtManagementBaseObjectRow = managementBaseObjectRow;
                    propertiesRow.name = data.Name;
                    propertiesRow.value = data.Value.ToString();
                    logEntry.dtProperties.AdddtPropertiesRow(propertiesRow);
                }

                if (data.Value != null && data.Name == "InstanceID")
                {
                    dsLogEntry.dtSuspendedMessageRow suspendedMessageRow = logEntry.dtSuspendedMessage.NewdtSuspendedMessageRow();
                    suspendedMessageRow.dtLogEntryRow = logEntryRow;
                    suspendedMessageRow.name = data.Name;
                    suspendedMessageRow.value = data.Value.ToString();

                    FillMessageTable(logEntry, logEntryRow, suspendedMessageRow);

                    logEntry.dtSuspendedMessage.AdddtSuspendedMessageRow(suspendedMessageRow);
                }
            }

            QualifierDataCollection qualifierDataCollection = managementBaseObject.Qualifiers;
            QualifierDataCollection.QualifierDataEnumerator qualifierDataEnumerator = qualifierDataCollection.GetEnumerator();

            while (qualifierDataEnumerator.MoveNext())
            {
                QualifierData data = qualifierDataEnumerator.Current;

                if (data.Value != null)
                {
                    dsLogEntry.dtQualifiersRow qualifiersRow = logEntry.dtQualifiers.NewdtQualifiersRow();
                    qualifiersRow.dtManagementBaseObjectRow = managementBaseObjectRow;
                    qualifiersRow.name = data.Name;
                    qualifiersRow.value = data.Value.ToString();
                    logEntry.dtQualifiers.AdddtQualifiersRow(qualifiersRow);
                }
            }

            PropertyDataCollection systemPropertiesDataCollection = managementBaseObject.SystemProperties;
            PropertyDataCollection.PropertyDataEnumerator systemPropertiesDataEnumerator = systemPropertiesDataCollection.GetEnumerator();

            while (systemPropertiesDataEnumerator.MoveNext())
            {
                PropertyData data = systemPropertiesDataEnumerator.Current;

                if (data.Value != null)
                {
                    dsLogEntry.dtSystemPropertiesRow systemPropertiesRow = logEntry.dtSystemProperties.NewdtSystemPropertiesRow();
                    systemPropertiesRow.dtManagementBaseObjectRow = managementBaseObjectRow;
                    systemPropertiesRow.name = data.Name;
                    systemPropertiesRow.value = data.Value.ToString();
                    logEntry.dtSystemProperties.AdddtSystemPropertiesRow(systemPropertiesRow);
                }
            }

            logEntry.dtManagementBaseObject.AdddtManagementBaseObjectRow(managementBaseObjectRow);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="logEntryDataSet"></param>
        /// <param name="logEntryDataRow"></param>
        /// <param name="data"></param>
        protected override void InsertLogEntryRow(string eventType, DataSet logEntryDataSet, DataRow logEntryDataRow, PropertyData data)
        {
            Log.DebugFormat("InsertLogEntryRow(string eventType {0}, DataSet logEntryDataSet {1}, DataRow logEntryDataRow {2}, PropertyData data {3})", eventType, logEntryDataSet, logEntryDataRow, data);

            dsLogEntry logEntry = (dsLogEntry)logEntryDataSet;
            dsLogEntry.dtLogEntryRow logEntryRow = (dsLogEntry.dtLogEntryRow)logEntryDataRow;

            if (data.Value != null && (data.Name == "TargetInstance"))
            {
                ManagementBaseObject mbo = (ManagementBaseObject)data.Value;
                dsLogEntry.dtManagementBaseObjectRow managementBaseObjectRow = logEntry.dtManagementBaseObject.NewdtManagementBaseObjectRow();
                managementBaseObjectRow.dtLogEntryRow = logEntryRow;
                managementBaseObjectRow.classname = mbo.ClassPath.ClassName;

                foreach (var item in mbo.Properties)
                    if (item.Value != null)
                    {
                        dsLogEntry.dtPropertiesRow propertiesRow = logEntry.dtProperties.NewdtPropertiesRow();
                        propertiesRow.dtManagementBaseObjectRow = managementBaseObjectRow;
                        propertiesRow.name = item.Name;
                        propertiesRow.value = item.Value.ToString();
                        logEntry.dtProperties.AdddtPropertiesRow(propertiesRow);
                    }

                foreach (var item in mbo.Qualifiers)
                    if (item.Value != null)
                    {
                        dsLogEntry.dtQualifiersRow qualifiersRow = logEntry.dtQualifiers.NewdtQualifiersRow();
                        qualifiersRow.dtManagementBaseObjectRow = managementBaseObjectRow;
                        qualifiersRow.name = item.Name;
                        qualifiersRow.value = item.Value.ToString();
                        logEntry.dtQualifiers.AdddtQualifiersRow(qualifiersRow);
                    }

                foreach (var item in mbo.SystemProperties)
                    if (item.Value != null)
                    {
                        dsLogEntry.dtSystemPropertiesRow systemPropertiesRow = logEntry.dtSystemProperties.NewdtSystemPropertiesRow();
                        systemPropertiesRow.dtManagementBaseObjectRow = managementBaseObjectRow;
                        systemPropertiesRow.name = item.Name;
                        systemPropertiesRow.value = item.Value.ToString();
                        logEntry.dtSystemProperties.AdddtSystemPropertiesRow(systemPropertiesRow);
                    }

                logEntry.dtManagementBaseObject.AdddtManagementBaseObjectRow(managementBaseObjectRow);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logEntryDataRow"></param>
        /// <param name="eventEntryDataRow"></param>
        protected override void SetEventEntry(DataRow logEntryDataRow, DataRow eventEntryDataRow)
        {
            Log.DebugFormat("SetEventEntry(DataRow logEntryDataRow {0}, DataRow eventEntryDataRow {1})", logEntryDataRow, eventEntryDataRow);

            dsLogEntry.dtLogEntryRow logEntryRow = (dsLogEntry.dtLogEntryRow)logEntryDataRow;
            dsEventEntry.dtEventEntryRow eventEntryRow = (dsEventEntry.dtEventEntryRow)eventEntryDataRow;

            eventEntryRow.eventtype = logEntryRow.eventtype;

            switch (logEntryRow.eventtype)
            {
                case BizTalkEventType.hostInstanceEvent:
                    {
                        foreach (dsLogEntry.dtManagementBaseObjectRow managementBaseObjectRow in logEntryRow.GetChildRows("dtLogEntry_dtManagementBaseObject"))
                        {
                            eventEntryRow.classname = managementBaseObjectRow.classname;

                            foreach (dsLogEntry.dtPropertiesRow propertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtProperties"))
                                switch (propertiesRow.name)
                                {
                                    case "Logfile":
                                        eventEntryRow.logname = propertiesRow.value;
                                        break;
                                    case "RunningServer":
                                    case "ComputerName":
                                        eventEntryRow.computername = propertiesRow.value;
                                        break;
                                    case "Status":
                                    case "ServiceState":
                                    case "ErrorDescription":
                                        GenerateHostInstanceMessage(Convert.ToInt16(propertiesRow.value), eventEntryRow);
                                        break;
                                    case "Type":
                                        eventEntryRow.entrytype = propertiesRow.value;
                                        break;
                                    case "EventType":
                                        eventEntryRow.entrytypecode = propertiesRow.value;
                                        break;
                                    case "TimeGenerated":
                                        eventEntryRow.timegenerated = propertiesRow.value;
                                        break;
                                    case "HostName":
                                        eventEntryRow.hostname = propertiesRow.value;
                                        break;
                                    case "IsDisabled":
                                        eventEntryRow.isdisabled = propertiesRow.value;
                                        break;
                                    case "Logon":
                                        eventEntryRow.logon = propertiesRow.value;
                                        break;
                                    case "NTGroupName":
                                        eventEntryRow.ntgroupname = propertiesRow.value;
                                        break;
                                    case "Name":
                                        eventEntryRow.name = propertiesRow.value;
                                        break;
                                    case "HostType":
                                        GenerateHostType(Convert.ToInt16(propertiesRow.value), eventEntryRow);
                                        break;
                                    case "ClusterInstanceType":
                                        GenerateClusterInstanceType(Convert.ToInt16(propertiesRow.value), eventEntryRow);
                                        break;
                                    case "ConfigurationState":
                                        GenerateConfigurationState(Convert.ToInt16(propertiesRow.value), eventEntryRow);
                                        break;
                                }
                            foreach (dsLogEntry.dtQualifiersRow qualifiersRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtQualifiers"))
                                ;
                            foreach (dsLogEntry.dtSystemPropertiesRow systemPropertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtSystemProperties"))
                                switch (systemPropertiesRow.name)
                                {
                                    case "__SERVER":
                                        eventEntryRow.server = systemPropertiesRow.value;
                                        break;
                                    case "__CLASS":
                                        eventEntryRow.wmiclass = systemPropertiesRow.value;
                                        break;

                                }
                        }
                    }
                    break;
                case BizTalkEventType.suspendedMessageEvent:
                    {
                        eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Error).ToString();
                        eventEntryRow.hostname = "";
                        foreach (dsLogEntry.dtSuspendedMessageRow suspendedMessageRow in logEntryRow.GetChildRows("dtLogEntry_dtSuspendedMessage"))
                            ;
                        foreach (dsLogEntry.dtMessageRow messageRow in logEntryRow.GetChildRows("dtLogEntry_dtMessage"))
                            switch (messageRow.name)
                            {
                                case "HostName":
                                    eventEntryRow.hostname = messageRow.value;
                                    break;
                                case "CreationTime":
                                    eventEntryRow.timegenerated = messageRow.value;
                                    break;
                                case "MessageStatus":
                                    eventEntryRow.messagestatus = messageRow.value;
                                    break;
                                case "OriginatorSecurityName":
                                    eventEntryRow.originatorsecurityname = messageRow.value;
                                    break;
                                case "Submitter":
                                    eventEntryRow.submitter = messageRow.value;
                                    break;
                                case "Url":
                                    eventEntryRow.url = messageRow.value;
                                    break;
                                case "AdapterName":
                                    eventEntryRow.adaptername = messageRow.value;
                                    break;
                                case "ServiceType":
                                    eventEntryRow.servicetype = messageRow.value;
                                    break;
                                case "Class":
                                    eventEntryRow.biztalkclass = messageRow.value;
                                    break;
                                case "MessageBox.DBServer":
                                    eventEntryRow.dbserver = messageRow.value;
                                    break;
                                case "MessageBox.DBName":
                                    eventEntryRow.dbname = messageRow.value;
                                    break;
                            }
                        foreach (dsLogEntry.dtManagementBaseObjectRow managementBaseObjectRow in logEntryRow.GetChildRows("dtLogEntry_dtManagementBaseObject"))
                        {
                            eventEntryRow.classname = managementBaseObjectRow.classname;

                            foreach (dsLogEntry.dtPropertiesRow propertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtProperties"))
                                switch (propertiesRow.name)
                                {
                                    case "ErrorDescription":
                                        eventEntryRow.message = propertiesRow.value;
                                        break;
                                    case "ServiceStatus":
                                        GenerateServiceStatusMessage(Convert.ToInt16(propertiesRow.value), eventEntryRow);
                                        break;
                                    case "ServiceClass":
                                        GenerateServiceClassMessage(Convert.ToInt16(propertiesRow.value), eventEntryRow);
                                        break;
                                    case "ErrorId":
                                        eventEntryRow.errorid = propertiesRow.value;
                                        break;
                                    case "ErrorCategory":
                                        eventEntryRow.errorcategory = propertiesRow.value;
                                        break;
                                }
                            foreach (dsLogEntry.dtSystemPropertiesRow systemPropertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtSystemProperties"))
                                switch (systemPropertiesRow.name)
                                {
                                    case "__SERVER":
                                        eventEntryRow.server = systemPropertiesRow.value;
                                        break;
                                    case "__CLASS":
                                        eventEntryRow.wmiclass = systemPropertiesRow.value;
                                        break;

                                }
                        }
                    }
                    break;
                case BizTalkEventType.receiveLocationEvent:
                    {
                        eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Information).ToString();
                        foreach (dsLogEntry.dtManagementBaseObjectRow managementBaseObjectRow in logEntryRow.GetChildRows("dtLogEntry_dtManagementBaseObject"))
                        {
                            eventEntryRow.classname = managementBaseObjectRow.classname;

                            foreach (dsLogEntry.dtPropertiesRow propertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtProperties"))
                                switch (propertiesRow.name)
                                {
                                    case "ActiveStartDT":
                                        eventEntryRow.activestartdt = propertiesRow.value;
                                        break;
                                    case "ActiveStopDT":
                                        eventEntryRow.activestopdt = propertiesRow.value;
                                        break;
                                    case "AdapterName":
                                        eventEntryRow.adaptername = propertiesRow.value;
                                        break;
                                    case "CustomCfg":
                                        eventEntryRow.customcfg = propertiesRow.value;
                                        break;
                                    case "Description":
                                        eventEntryRow.description = propertiesRow.value;
                                        break;
                                    case "HostName":
                                        eventEntryRow.hostname = propertiesRow.value;
                                        break;
                                    case "InboundAddressableURL":
                                        eventEntryRow.inboundaddressableurl = propertiesRow.value;
                                        break;
                                    case "InboundTransportURL":
                                        eventEntryRow.inboundtransporturl = propertiesRow.value;
                                        break;
                                    case "IsDisabled":
                                        GenerateReceiveLocationMessage(Convert.ToBoolean(propertiesRow.value), eventEntryRow);
                                        eventEntryRow.isdisabled = propertiesRow.value;
                                        break;
                                    case "OutboundTransforms":
                                        eventEntryRow.outboundtransforms = propertiesRow.value;
                                        break;
                                    case "IsPrimary":
                                        eventEntryRow.isprimary = propertiesRow.value;
                                        break;
                                    case "MgmtDbNameOverride":
                                        eventEntryRow.mgmtdbnameoverride = propertiesRow.value;
                                        break;
                                    case "MgmtDbServerOverride":
                                        eventEntryRow.mgmtdbserveroverride = propertiesRow.value;
                                        break;
                                    case "Name":
                                        eventEntryRow.name = propertiesRow.value;
                                        break;
                                    case "OperatingWindowEnabled":
                                        eventEntryRow.operatingwindowenabled = propertiesRow.value;
                                        break;
                                    case "PipelineName":
                                        eventEntryRow.pipelinename = propertiesRow.value;
                                        break;
                                    case "ReceivePortName":
                                        eventEntryRow.receiveportname = propertiesRow.value;
                                        break;
                                    case "SendPipeline":
                                        eventEntryRow.sendpipeline = propertiesRow.value;
                                        break;
                                    case "SendPipelineData":
                                        eventEntryRow.sendpipelinedata = propertiesRow.value;
                                        break;
                                    case "SrvWinStartDT":
                                        eventEntryRow.srvwinstartdt = propertiesRow.value;
                                        break;
                                    case "SrvWinStopDT":
                                        eventEntryRow.srvwinstopdt = propertiesRow.value;
                                        break;
                                    case "StartDateEnabled":
                                        eventEntryRow.startdateenabled = propertiesRow.value;
                                        break;
                                    case "StopDateEnabled":
                                        eventEntryRow.stopdateenabled = propertiesRow.value;
                                        break;
                                }
                            foreach (dsLogEntry.dtSystemPropertiesRow systemPropertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtSystemProperties"))
                                switch (systemPropertiesRow.name)
                                {
                                    case "__SERVER":
                                        eventEntryRow.server = systemPropertiesRow.value;
                                        break;
                                    case "__CLASS":
                                        eventEntryRow.wmiclass = systemPropertiesRow.value;
                                        break;

                                }
                        }
                    }
                    break;
                case BizTalkEventType.sendPortEvent:
                    {
                        eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Information).ToString();
                        foreach (dsLogEntry.dtManagementBaseObjectRow managementBaseObjectRow in logEntryRow.GetChildRows("dtLogEntry_dtManagementBaseObject"))
                        {
                            eventEntryRow.classname = managementBaseObjectRow.classname;

                            foreach (dsLogEntry.dtPropertiesRow propertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtProperties"))
                                switch (propertiesRow.name)
                                {
                                    case "EncryptionCert":
                                        eventEntryRow.encryptioncert = propertiesRow.value;
                                        break;
                                    case "Filter":
                                        eventEntryRow.filter = propertiesRow.value;
                                        break;
                                    case "InboundTransforms":
                                        eventEntryRow.inboundtransforms = propertiesRow.value;
                                        break;
                                    case "IsDynamic":
                                        eventEntryRow.isdynamic = propertiesRow.value;
                                        break;
                                    case "IsTwoWay":
                                        eventEntryRow.istwoway = propertiesRow.value;
                                        break;
                                    case "MgmtDbNameOverride":
                                        eventEntryRow.mgmtdbnameoverride = propertiesRow.value;
                                        break;
                                    case "MgmtDbServerOverride":
                                        eventEntryRow.mgmtdbserveroverride = propertiesRow.value;
                                        break;
                                    case "Name":
                                        eventEntryRow.name = propertiesRow.value;
                                        break;
                                    case "OrderedDelivery":
                                        eventEntryRow.ordereddelivery = propertiesRow.value;
                                        break;
                                    case "OutboundTransforms":
                                        eventEntryRow.outboundtransforms = propertiesRow.value;
                                        break;
                                    case "Priority":
                                        eventEntryRow.priority = propertiesRow.value;
                                        break;
                                    case "PTAddress":
                                        eventEntryRow.ptaddress = propertiesRow.value;
                                        break;
                                    case "PTCustomCfg":
                                        eventEntryRow.ptcustomcfg = propertiesRow.value;
                                        break;
                                    case "PTFromTime":
                                        eventEntryRow.ptfromtime = propertiesRow.value;
                                        break;
                                    case "PTOrderedDelivery":
                                        eventEntryRow.ptorderedDelivery = propertiesRow.value;
                                        break;
                                    case "PTRetryCount":
                                        eventEntryRow.ptretrycount = propertiesRow.value;
                                        break;
                                    case "PTRetryInterval":
                                        eventEntryRow.ptretryinterval = propertiesRow.value;
                                        break;
                                    case "PTServiceWindowEnabled":
                                        eventEntryRow.ptservicewindowenabled = propertiesRow.value;
                                        break;
                                    case "PTToTime":
                                        eventEntryRow.pttotime = propertiesRow.value;
                                        break;
                                    case "PTTransportType":
                                        eventEntryRow.pttransporttype = propertiesRow.value;
                                        break;
                                    case "ReceivePipeline":
                                        eventEntryRow.receivepipeline = propertiesRow.value;
                                        break;
                                    case "RouteFailedMessage":
                                        eventEntryRow.routefailedmessage = propertiesRow.value;
                                        break;
                                    case "SendPipeline":
                                        eventEntryRow.sendpipeline = propertiesRow.value;
                                        break;
                                    case "STAddress":
                                        eventEntryRow.staddress = propertiesRow.value;
                                        break;
                                    case "Status":
                                        GenerateSendPortMessage(Convert.ToInt16(propertiesRow.value), eventEntryRow);
                                        break;
                                    case "STCustomCfg":
                                        eventEntryRow.stcustomcfg = propertiesRow.value;
                                        break;
                                    case "STFromTime":
                                        eventEntryRow.stfromtime = propertiesRow.value;
                                        break;
                                    case "StopSendingOnFailure":
                                        eventEntryRow.stopsendingonfailure = propertiesRow.value;
                                        break;
                                    case "STOrderedDelivery":
                                        eventEntryRow.stordereddelivery = propertiesRow.value;
                                        break;
                                    case "STRetryCount":
                                        eventEntryRow.stretrycount = propertiesRow.value;
                                        break;
                                    case "STRetryInterval":
                                        eventEntryRow.stretryinterval = propertiesRow.value;
                                        break;
                                    case "STServiceWindowEnabled":
                                        eventEntryRow.stservicewindowenabled = propertiesRow.value;
                                        break;
                                    case "STToTime":
                                        eventEntryRow.sttotime = propertiesRow.value;
                                        break;
                                    case "STTransportType":
                                        eventEntryRow.sttransporttype = propertiesRow.value;
                                        break;
                                    case "Tracking":
                                        eventEntryRow.tracking = propertiesRow.value;
                                        break;
                                }
                            foreach (dsLogEntry.dtSystemPropertiesRow systemPropertiesRow in managementBaseObjectRow.GetChildRows("dtManagementBaseObject_dtSystemProperties"))
                                switch (systemPropertiesRow.name)
                                {
                                    case "__SERVER":
                                        eventEntryRow.server = systemPropertiesRow.value;
                                        break;
                                    case "__CLASS":
                                        eventEntryRow.wmiclass = systemPropertiesRow.value;
                                        break;
                                }
                        }
                    }
                    break;

                default:
                    break;
            }

            if (string.IsNullOrEmpty(eventEntryRow.timegenerated))
                eventEntryRow.timegenerated = DateTime.Now.ToString();

            if (string.IsNullOrEmpty(eventEntryRow.user))
                eventEntryRow.user = "N/A";
        }

        private void GenerateReceiveLocationMessage(bool value, dsEventEntry.dtEventEntryRow eventEntryRow)
        {
            Log.DebugFormat("GenerateReceiveLocationMessage(bool value {0}, dsEventEntry.dtEventEntryRow eventEntryRow {1})", value, eventEntryRow );

            if (value)
            {
                eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Warning).ToString();
                eventEntryRow.message = string.Format("<span style='color: {0}'>Disabled</span>", EntryType.InactiveColor);
                eventEntryRow.status = "Disabled";
            }
            else
            {
                eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Information).ToString();
                eventEntryRow.message = string.Format("<span style='color: {0}'>Enabled</span>", EntryType.ActiveColor);
                eventEntryRow.status = "Enabled";
            }
        }

        private void GenerateSendPortMessage(short value, dsEventEntry.dtEventEntryRow eventEntryRow)
        {
            Log.DebugFormat("GenerateSendPortMessage(short value {0}, dsEventEntry.dtEventEntryRow eventEntryRow {1})", value, eventEntryRow);

            switch (value)
            {
                case (int)SendPortStatus.Bound:
                    eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Information).ToString();
                    eventEntryRow.message = string.Format("<span style='color: {0}'>Bound</span>", EntryType.InformationColor);
                    eventEntryRow.status = "Bound";
                    break;
                case (int)SendPortStatus.Stopped:
                    eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Warning).ToString();
                    eventEntryRow.message = string.Format("<span style='color: {0}'>Stopped</span>", EntryType.InactiveColor);
                    eventEntryRow.status = "Stopped";
                    break;
                case (int)SendPortStatus.Started:
                    eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Information).ToString();
                    eventEntryRow.message = string.Format("<span style='color: {0}'>Started</span>", EntryType.ActiveColor);
                    eventEntryRow.status = "Started";
                    break;
            }
        }

        private void GenerateServiceClassMessage(short value, dsEventEntry.dtEventEntryRow eventEntryRow)
        {
            Log.DebugFormat("GenerateServiceClassMessage(short value {0}, dsEventEntry.dtEventEntryRow eventEntryRow {1})", value, eventEntryRow);

            switch (value)
            {
                case (int)ServiceClass.IsolatedAdapter:
                    eventEntryRow.serviceclass = "Isolated Adapter";
                    break;
                case (int)ServiceClass.Messaging:
                    eventEntryRow.serviceclass = "Messaging";
                    break;
                case (int)ServiceClass.MSMQT:
                    eventEntryRow.serviceclass = "MSMQT";
                    break;
                case (int)ServiceClass.Orchestration:
                    eventEntryRow.serviceclass = "Orchestration";
                    break;
                case (int)ServiceClass.Other:
                    eventEntryRow.serviceclass = "Other";
                    break;
                case (int)ServiceClass.RoutingFailureReport:
                    eventEntryRow.configurationstate = "RoutingFailureReport";
                    break;
                case (int)ServiceClass.Tracking:
                    eventEntryRow.serviceclass = "Tracking";
                    break;
                default:
                    eventEntryRow.serviceclass = "N/A";
                    break;
            }
        }

        private void GenerateServiceStatusMessage(short value, dsEventEntry.dtEventEntryRow eventEntryRow)
        {
            Log.DebugFormat("GenerateServiceStatusMessage(short value {0}, dsEventEntry.dtEventEntryRow eventEntryRow {1})", value, eventEntryRow);

            switch (value)
            {
                case (int)ServiceStatus.Active:
                    eventEntryRow.servicestatus = "Active";
                    break;
                case (int)ServiceStatus.CompletedWithDiscardedMessages:
                    eventEntryRow.servicestatus = "Completed With Discarded Messages";
                    break;
                case (int)ServiceStatus.Dehydrated:
                    eventEntryRow.servicestatus = "Dehydrated";
                    break;
                case (int)ServiceStatus.InBreakpoint:
                    eventEntryRow.servicestatus = "In Breakpoint";
                    break;
                case (int)ServiceStatus.ReadyToRun:
                    eventEntryRow.servicestatus = "Ready To Run";
                    break;
                case (int)ServiceStatus.SuspendedNotResumable:
                    eventEntryRow.servicestatus = "Suspended Not Resumable";
                    break;
                case (int)ServiceStatus.SuspendedResumable:
                    eventEntryRow.servicestatus = "Suspended Resumable";
                    break;
            }
        }

        private void GenerateConfigurationState(int value, dsEventEntry.dtEventEntryRow eventEntryRow)
        {
            Log.DebugFormat("GenerateConfigurationState(short value {0}, dsEventEntry.dtEventEntryRow eventEntryRow {1})", value, eventEntryRow);

            switch (value)
            {
                case (int)ConfigurationState.InstallationFailed:
                    eventEntryRow.configurationstate = "Installation Failed";
                    break;
                case (int)ConfigurationState.Installed:
                    eventEntryRow.configurationstate = "Installed";
                    break;
                case (int)ConfigurationState.NotInstalled:
                    eventEntryRow.configurationstate = "Not Installed";
                    break;
                case (int)ConfigurationState.UninstallationFailed:
                    eventEntryRow.configurationstate = "Uninstallation Failed";
                    break;
                case (int)ConfigurationState.UpdateFailed:
                    eventEntryRow.configurationstate = "Update Failed";
                    break;
            }
        }

        private void GenerateClusterInstanceType(int value, dsEventEntry.dtEventEntryRow eventEntryRow)
        {
            Log.DebugFormat("GenerateClusterInstanceType(int value {0}, dsEventEntry.dtEventEntryRow eventEntryRow {1})", value, eventEntryRow);

            switch (value)
            {
                case (int)ClusterInstanceType.ClusteredInstance:
                    eventEntryRow.clusterinstancetype = "Clustered Instance";
                    break;
                case (int)ClusterInstanceType.ClusteredVirtualInstance:
                    eventEntryRow.clusterinstancetype = "Clustered Virtual Instance";
                    break;
                case (int)ClusterInstanceType.UnClusteredInstance:
                    eventEntryRow.clusterinstancetype = "Unclustered Instance";
                    break;
            }
        }

        private void GenerateHostType(int value, dsEventEntry.dtEventEntryRow eventEntryRow)
        {
            Log.DebugFormat("GenerateHostType(int value {0}, dsEventEntry.dtEventEntryRow eventEntryRow {1})", value, eventEntryRow);

            switch (value)
            {
                case (int)HostInstanceType.InProcess:
                    eventEntryRow.hosttype = "In Process";
                    break;
                case (int)HostInstanceType.Isolated:
                    eventEntryRow.hosttype = "Isolated";
                    break;
            }
        }

        private void GenerateHostInstanceMessage(int value, dsEventEntry.dtEventEntryRow eventEntryRow)
        {
            Log.DebugFormat("GenerateHostInstanceMessage(int value {0}, dsEventEntry.dtEventEntryRow eventEntryRow {1})", value, eventEntryRow);

            switch (value)
            {
                case (int)HostInstanceServiceState.Continuepending:
                    eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Warning).ToString();
                    eventEntryRow.message = string.Format("<span style='color: {0}'>Continue Pending</span>", EntryType.WarningColor);
                    eventEntryRow.status = "Continue Pending";
                    break;
                case (int)HostInstanceServiceState.Paused:
                    eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Warning).ToString();
                    eventEntryRow.message = string.Format("<span style='color: {0}'>Paused</span>", EntryType.WarningColor);
                    eventEntryRow.status = "Paused";
                    break;
                case (int)HostInstanceServiceState.PausePending:
                    eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Warning).ToString();
                    eventEntryRow.message = string.Format("<span style='color: {0}'>Pause Pending</span>", EntryType.WarningColor);
                    eventEntryRow.status = "Pause Pending";
                    break;
                case (int)HostInstanceServiceState.Running:
                    eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Information).ToString();
                    eventEntryRow.message = string.Format("<span style='color: {0}'>Running</span>", EntryType.ActiveColor);
                    eventEntryRow.status = "Running";
                    break;
                case (int)HostInstanceServiceState.StartPending:
                    eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Warning).ToString();
                    eventEntryRow.message = string.Format("<span style='color: {0}'>Start Pending</span>", EntryType.WarningColor);
                    eventEntryRow.status = "Start Pending";
                    break;
                case (int)HostInstanceServiceState.Stopped:
                    eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Warning).ToString();
                    eventEntryRow.message = string.Format("<span style='color: {0}'>Stopped</span>", EntryType.InactiveColor);
                    eventEntryRow.status = "Stopped";
                    break;
                case (int)HostInstanceServiceState.StopPending:
                    eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Warning).ToString();
                    eventEntryRow.message = string.Format("<span style='color: {0}'>Stop Pending</span>", EntryType.WarningColor);
                    eventEntryRow.status = "Stop Pending";
                    break;
                case (int)HostInstanceServiceState.Unknown:
                    eventEntryRow.entrytypecode = ((int)System.Diagnostics.EventLogEntryType.Warning).ToString();
                    eventEntryRow.message = string.Format("<span style='color: {0}'>Unknown</span>", EntryType.InactiveColor);
                    eventEntryRow.status = "Unknown";
                    break;
            }
        }

        protected override void SetHtmlMessage(DataRow eventEntryDataRow, out LogEntry logEntry)
        {
            Log.DebugFormat("SetHtmlMessage(DataRow eventEntryDataRow {0}, out LogEntry logEntry)", eventEntryDataRow);

            logEntry = new LogEntry(true);

            dsEventEntry.dtEventEntryRow eventEntryRow = (dsEventEntry.dtEventEntryRow)eventEntryDataRow;

            string entryTypeColor;

            switch (Convert.ToInt32(eventEntryRow.entrytypecode))
            {
                case (int)System.Diagnostics.EventLogEntryType.Error:
                    entryTypeColor = EntryType.ErrorColor;
                    break;
                case (int)System.Diagnostics.EventLogEntryType.Warning:
                    entryTypeColor = EntryType.WarningColor;
                    break;
                default:
                    entryTypeColor = EntryType.InformationColor;
                    break;
            }

            string htmlHeader = string.Empty;
            string htmlBody = string.Empty;

            switch (eventEntryRow.eventtype)
            {
                case BizTalkEventType.suspendedMessageEvent:
                    {
                        logEntry.MailSubjectString = "Host Name: {0}, Service Name: {1}, Message Status {2}, Adapter Type: {3}, Url: {4}";
                        logEntry.MailSubjectParameters = new object[] { eventEntryRow.hostname, eventEntryRow.servicetype, eventEntryRow.messagestatus, eventEntryRow.adaptername, eventEntryRow.url };

                        logEntry.AdditionalString = "Host Name: {0}, Service Name: {1}, Message Status {2}, Adapter Type: {3}, Url: {4}";
                        logEntry.AdditionalParameters = new object[] { eventEntryRow.hostname, eventEntryRow.servicetype, eventEntryRow.messagestatus, eventEntryRow.adaptername, eventEntryRow.url };

                        htmlHeader = Properties.Resource.SuspendedMessage.Substring(0, Properties.Resource.SuspendedMessage.IndexOf("<body>"));
                        htmlBody = string.Format(Properties.Resource.SuspendedMessage.Substring(Properties.Resource.SuspendedMessage.IndexOf("<body>")),
                                                        string.Format("<span style='color: {0}'>{1}</span>", entryTypeColor, eventEntryRow.messagestatus),
                                                        eventEntryRow.dbserver,
                                                        eventEntryRow.dbname,
                                                        eventEntryRow.hostname,
                                                        eventEntryRow.originatorsecurityname,
                                                        eventEntryRow.submitter,
                                                        eventEntryRow.url,
                                                        eventEntryRow.adaptername,
                                                        eventEntryRow.servicetype,
                                                        eventEntryRow.biztalkclass,
                                                        eventEntryRow.servicestatus,
                                                        eventEntryRow.serviceclass,
                                                        eventEntryRow.errorid,
                                                        eventEntryRow.errorcategory,
                                                        eventEntryRow.timegenerated,
                                                        eventEntryRow.message);
                    }
                    break;
                case BizTalkEventType.hostInstanceEvent:
                    {
                        logEntry.MailSubjectString = "Server: {0}, Hostinstance Name: {1}, Hostinstance Status: {2}, Hostinstance Type: {3}";
                        logEntry.MailSubjectParameters = new object[] { eventEntryRow.server, eventEntryRow.hostname, eventEntryRow.status, eventEntryRow.hosttype };

                        logEntry.AdditionalString = "Server: {0}, Hostinstance Name: {1}, Hostinstance Status: {2}, Hostinstance Type: {3}";
                        logEntry.AdditionalParameters = new object[] { eventEntryRow.server, eventEntryRow.hostname, eventEntryRow.status, eventEntryRow.hosttype };

                        htmlHeader = Properties.Resource.HostInstance.Substring(0, Properties.Resource.HostInstance.IndexOf("<body>"));
                        htmlBody = string.Format(Properties.Resource.HostInstance.Substring(Properties.Resource.HostInstance.IndexOf("<body>")),
                                                        eventEntryRow.message,
                                                        eventEntryRow.server,
                                                        eventEntryRow.hostname,
                                                        eventEntryRow.hosttype,
                                                        eventEntryRow.name,
                                                        eventEntryRow.logon,
                                                        eventEntryRow.ntgroupname,
                                                        eventEntryRow.isdisabled,
                                                        eventEntryRow.configurationstate,
                                                        eventEntryRow.clusterinstancetype,
                                                        eventEntryRow.timegenerated);
                    }
                    break;
                case BizTalkEventType.receiveLocationEvent:
                    {
                        logEntry.MailSubjectString = "Server: {0}, Receiveport Name: {1}, Receiveport Status: {2}, Receiveport Type: {3}, Receiveport Address: {4}";
                        logEntry.MailSubjectParameters = new object[] { eventEntryRow.server, eventEntryRow.receiveportname, eventEntryRow.status, eventEntryRow.adaptername, eventEntryRow.inboundtransporturl };

                        logEntry.AdditionalString = "Server: {0}, Receiveport Name: {1}, Receiveport Status: {2}, Receiveport Type: {3}, Receiveport Address: {4}";
                        logEntry.AdditionalParameters = new object[] { eventEntryRow.server, eventEntryRow.receiveportname, eventEntryRow.status, eventEntryRow.adaptername, eventEntryRow.inboundtransporturl };

                        htmlHeader = Properties.Resource.ReceiveLocation.Substring(0, Properties.Resource.ReceiveLocation.IndexOf("<body>"));
                        htmlBody = string.Format(Properties.Resource.ReceiveLocation.Substring(Properties.Resource.ReceiveLocation.IndexOf("<body>")),
                                                        eventEntryRow.message,
                                                        eventEntryRow.server,
                                                        eventEntryRow.receiveportname,
                                                        eventEntryRow.inboundtransporturl,
                                                        eventEntryRow.adaptername,
                                                        eventEntryRow.pipelinename,
                                                        eventEntryRow.timegenerated);
                    }
                    break;
                case BizTalkEventType.sendPortEvent:
                    {
                        logEntry.MailSubjectString = "Server: {0}, Sendport Name: {1}, Sendport Status: {2}, Sendport Type: {3}, Sendport Address: {4}";
                        logEntry.MailSubjectParameters = new object[] { eventEntryRow.server, eventEntryRow.name, eventEntryRow.status, eventEntryRow.pttransporttype, eventEntryRow.ptaddress };

                        logEntry.AdditionalString = "Server: {0}, Sendport Name: {1}, Sendport Status: {2}, Sendport Type: {3}, Sendport Address: {4}";
                        logEntry.AdditionalParameters = new object[] { eventEntryRow.server, eventEntryRow.name, eventEntryRow.status, eventEntryRow.pttransporttype, eventEntryRow.ptaddress };

                        htmlHeader = Properties.Resource.SendPort.Substring(0, Properties.Resource.SendPort.IndexOf("<body>"));
                        htmlBody = string.Format(Properties.Resource.SendPort.Substring(Properties.Resource.SendPort.IndexOf("<body>")),
                                                        eventEntryRow.message,
                                                        eventEntryRow.server,
                                                        eventEntryRow.name,
                                                        eventEntryRow.ptaddress,
                                                        eventEntryRow.pttransporttype,
                                                        eventEntryRow.sendpipeline,
                                                        eventEntryRow.receivepipeline,
                                                        eventEntryRow.timegenerated);
                    }
                    break;
            }

            logEntry.HtmlString = htmlHeader + htmlBody;
            logEntry.AttachmentCollection.AddRange(Attachments);
        }

        private void FillMessageTable(dsLogEntry logEntry, dsLogEntry.dtLogEntryRow logEntryRow, dsLogEntry.dtSuspendedMessageRow suspendedMessageRow)
        {
            Log.DebugFormat("FillMessageTable(dsLogEntry logEntry {0}, dsLogEntry.dtLogEntryRow logEntryRow {1}, dsLogEntry.dtSuspendedMessageRow suspendedMessageRow {2})", logEntry, logEntryRow, suspendedMessageRow );

            EnumerationOptions enumOptions = new EnumerationOptions();

            enumOptions.ReturnImmediately = false;

            ManagementObjectSearcher MessageInstancesInServiceInstance = new ManagementObjectSearcher("root\\MicrosoftBizTalkServer", "Select * from MSBTS_MessageInstance where ServiceInstanceID='" + suspendedMessageRow.value + "'", enumOptions);

            BizTalkOperations bizTalkOperations = new Microsoft.BizTalk.Operations.BizTalkOperations();

            ManagementObjectCollection managementObjectCollection = MessageInstancesInServiceInstance.Get();

            foreach (ManagementObject MessageInstance in managementObjectCollection)
            {
                BizTalkMessage bizTalkMessage = bizTalkOperations.GetMessage(new Guid(MessageInstance["MessageInstanceID"].ToString()), new Guid(suspendedMessageRow.value));

                var props = bizTalkMessage.GetType().GetProperties();
                foreach (var prop in props)
                {
                    object value = prop.GetValue(bizTalkMessage, null);

                    if (value != null)
                    {
                        dsLogEntry.dtMessageRow messageRow = logEntry.dtMessage.NewdtMessageRow();
                        messageRow.dtLogEntryRow = logEntryRow;

                        switch (prop.Name)
                        {
                            case "MessageBox":
                                if (bizTalkMessage.MessageBox != null)
                                {
                                    messageRow.name = "MessageBox.DBName";
                                    messageRow.value = bizTalkMessage.MessageBox.DBName;
                                    logEntry.dtMessage.AdddtMessageRow(messageRow);

                                    messageRow = logEntry.dtMessage.NewdtMessageRow();
                                    messageRow.dtLogEntryRow = logEntryRow;
                                    messageRow.name = "MessageBox.DBServer";
                                    messageRow.value = bizTalkMessage.MessageBox.DBServer;
                                }
                                break;

                            case "BodyPart":
                                if (bizTalkMessage.BodyPart != null)
                                {
                                    bizTalkMessage.BodyPart.Data.Position = 0;
                                    Attachments.Add(new Attachment(bizTalkMessage.BodyPart.Data, "Message.xml"));
                                }
                                break;
                            default:
                                messageRow.name = prop.Name;
                                messageRow.value = value.ToString();
                                break;
                        }

                        logEntry.dtMessage.AdddtMessageRow(messageRow);
                    }
                }
            }
        }
    }
}
