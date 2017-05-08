using log4net.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace inSyca.foundation.framework
{
    enum EntryLevel { Success, NoParams, InformationError, ParamsError };

    public class Attachment
    {
        public System.Net.Mail.Attachment MailAttachment
        {
            get
            {
                return new System.Net.Mail.Attachment(ContentStream, ContentName);
            }
        }

        public Stream ContentStream { get; set; }
        public string ContentName { get; set; }

        public Attachment(Stream contentStream, string contentName)
        {
            ContentStream = contentStream;
            ContentName = contentName;
        }
    }

    public class LogEntry
    {
        public MethodBase Method { get; set; }
        public object[] MethodParameters { get; set; }
        public string AdditionalString { get; set; }
        public object[] AdditionalParameters { get; set; }
        public string MailSubjectString { get; set; }
        public object[] MailSubjectParameters { get; set; }
        public string HtmlString { get; set; }
        public LoggingEvent LoggingEvent { get; set; }

        public bool HasAdditionalInformation
        {
            get
            {
                if (string.IsNullOrEmpty(AdditionalString) && AdditionalParameters == null)
                    return false;

                return true;
            }
        }

        public System.Net.Mail.Attachment LogoAttachment
        {
            get
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("inSyca.foundation.framework.Properties.logo.png");

                System.Net.Mail.Attachment logoAttachment = new System.Net.Mail.Attachment(stream, "logo.png");
                logoAttachment.ContentType = new System.Net.Mime.ContentType("image/png");

                logoAttachment.ContentId = "logo_001";

                return logoAttachment;
            }
        }

        public List<Attachment> attachmentCollection;
        public List<Attachment> AttachmentCollection
        {
            get
            {
                if (attachmentCollection == null)
                    attachmentCollection = new List<Attachment>();

                return attachmentCollection;
            }
        }

        public string MessageEntry
        {
            get
            {
                StringBuilder sbEntry = new StringBuilder();

                GetDefaultEntry(sbEntry);

                if (HasAdditionalInformation)
                    GetAdditionalEntry(sbEntry);

                return sbEntry.ToString();
            }
        }

        public string MessageEntryShort
        {
            get
            {
                StringBuilder sbEntry = new StringBuilder();

                GetDefaultEntry(sbEntry);

                if (HasAdditionalInformation)
                    GetAdditionalEntry(sbEntry);

                if (sbEntry.Length > 5000)
                    return sbEntry.ToString().Substring(0, 5000);

                return sbEntry.ToString();
            }
        }

        public string MailSubject
        {
            get
            {
                try
                {
                    return string.Format(MailSubjectString, MailSubjectParameters);
                }
                catch (Exception)
                {
                    return AdditionalString;
                }
            }
        }

        private bool MonitorEvent { get; set; }

        public LogEntry(bool isMonitorEvent)
        {
            MonitorEvent = isMonitorEvent;
        }

        public LogEntry(MethodBase method, object[] methodParameters)
        {
            Method = method;
            MethodParameters = methodParameters;
        }

        public LogEntry(MethodBase method, object[] methodParameters, Exception exception)
        {
            Method = method;
            MethodParameters = methodParameters;

            StringBuilder additionalString = new StringBuilder();

            additionalString.AppendFormat("Exception: {0}", exception.Message);

            Exception ex = exception.InnerException;

            while (ex != null)
            {
                additionalString.AppendFormat("\nInnerException: {0}", ex.Message);
                ex = ex.InnerException;
            }

            AdditionalString = additionalString.ToString();
        }

        public LogEntry(MethodBase method, object[] methodParameters, string additionalString, object[] additionalParameters)
        {
            Method = method;
            MethodParameters = methodParameters;
            AdditionalString = additionalString;
            AdditionalParameters = additionalParameters;
        }

        public override string ToString()
        {
            if(MonitorEvent && HasAdditionalInformation)
                if (AdditionalParameters != null)
                    return string.Format(AdditionalString, AdditionalParameters);
                else
                    return AdditionalString;
            else
                return MessageEntry;
        }

        private string GetEntry()
        {
            StringBuilder eventEntry = new StringBuilder();

            GetDefaultEntry(eventEntry);

            if (HasAdditionalInformation)
                GetAdditionalEntry(eventEntry);

            return eventEntry.ToString();
        }

        private EntryLevel GetDefaultEntry(StringBuilder eventEntry)
        {
            ParameterInfo[] pars = null;

            try
            {
                //eventEntry.AppendFormat("Timestamp: {0}\n", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                eventEntry.AppendFormat("User: {0}\n", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                eventEntry.AppendFormat("Machine: {0}\n", Dns.GetHostName());
                eventEntry.AppendFormat("Domain: {0}\n\r", System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName);

                eventEntry.AppendFormat("Function: \n{0}\n\r", Method);

                if (MethodParameters == null || MethodParameters.Length < 1)
                    return EntryLevel.NoParams;

                eventEntry.Append("Parameters:\n");
            }
            catch (Exception ex)
            {
                eventEntry.AppendFormat("Assign Base Information -> Error: {0}", ex.Message);
                return EntryLevel.InformationError;
            }

            try
            {
                pars = Method.GetParameters();
            }
            catch (Exception ex)
            {
                eventEntry.AppendFormat("method.GetParameters() -> Error: {0}", ex.Message);
                return EntryLevel.ParamsError;
            }

            string strParameterValue;

            int i = 0;
            foreach (ParameterInfo p in pars)
            {
                try
                {
                    if (MethodParameters[i] == null)
                    {
                        strParameterValue = "No Value";
                    }
                    else
                    {
                        strParameterValue = convertMethodParametersToString(p, MethodParameters[i]);
                    }

                    eventEntry.AppendFormat("{0} -> {1}\n", p.Name, strParameterValue);
                    i++;
                }
                catch (Exception ex)
                {
                    eventEntry.AppendFormat("{0} -> undefined\nError: {1}\n", p.Name, ex.Message);
                    return EntryLevel.ParamsError;
                }
            }

            return EntryLevel.Success;
        }

        internal EntryLevel GetAdditionalEntry(StringBuilder eventEntry)
        {
            try
            {
                eventEntry.Append("\rAdditional Information:\n");

                if (AdditionalParameters != null)
                    eventEntry.AppendFormat(AdditionalString, AdditionalParameters);
                else
                    eventEntry.Append(AdditionalString);
            }
            catch (Exception ex)
            {
                eventEntry.AppendFormat("eventEntry.AppendFormat(additionalString, additionalParameters) -> Error: {0}, EntryString:{1}", ex.Message, eventEntry.ToString());
                return EntryLevel.ParamsError;
            }

            return EntryLevel.Success;
        }

        private static string convertMethodParametersToString(ParameterInfo pInfo, object pValue)
        {
            if (pInfo.ParameterType == null)
                return "No Value";

            StringBuilder stringBuilder = new StringBuilder();

            switch (pInfo.ParameterType.FullName)
            {
                case "inSyca.foundation.messagebroker.wcf.integrationMessageWrapper":
                case "inSyca.foundation.messagebroker.wcf.integrationMessageWrapper&":
                    stringBuilder.Append("\n");

                    foreach (XmlElement xmlElement in pValue as System.Xml.XmlElement[])
                        stringBuilder.Append(XElement.Parse(xmlElement.OuterXml).ToString());

                    stringBuilder.Append("\n");
                    break;

                case "System.ServiceModel.Channels.Message&":
                    stringBuilder.Append("\n");
                    stringBuilder.Append(pValue);
                    break;

                default:
                    if (pValue == null)
                        stringBuilder.Append(string.Format("{0} (value object = 'null')", pValue));
                    else
                        stringBuilder.Append(string.Format("{0} (value object type = {1})", pValue, pValue.GetType()));
                    break;
            }

            return stringBuilder.ToString();
        }
    }
}
