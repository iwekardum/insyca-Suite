using log4net.Appender;
using log4net.Core;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;

namespace inSyca.foundation.framework.diagnostics
{
    public class SmtpMonitoringAppender : SmtpAppender, IAppender
    {
        public SmtpMonitoringAppender()
        {
        }

        private LogEntry LogEntry { get; set; }

        protected override bool FilterEvent(LoggingEvent loggingEvent)
        {
            LoggingEventData loggingEventData = loggingEvent.GetLoggingEventData();
            loggingEventData.Message = string.Format("{0} - {1}", LogEntry.MailSubject, LogEntry.HtmlString);

            return base.FilterEvent(new LoggingEvent(loggingEventData));
        }

        void IAppender.DoAppend(LoggingEvent loggingEvent)
        {
            if (loggingEvent.MessageObject is LogEntry)
            {
                LogEntry = loggingEvent.MessageObject as LogEntry;
                LogEntry.LoggingEvent = loggingEvent;
            }

            base.DoAppend(loggingEvent);
        }

        protected override void SendEmail(string messageBody)
        {
            // Create and configure the smtp client
            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Host = SmtpHost;
            smtpClient.Port = Port;
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network
            smtpClient.EnableSsl = EnableSsl;

            if (Authentication == SmtpAuthentication.Basic)
            {
                // Perform basic authentication
                smtpClient.Credentials = new NetworkCredential(Username, Password);
            }
            else if (Authentication == SmtpAuthentication.Ntlm)
            {
                // Perform integrated authentication (NTLM)
                smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            }

            Log.DebugFormat("smtpClient.ClientCertificates {0}, smtpClient.Credentials {1}, smtpClient.DeliveryMethod {2}, smtpClient.EnableSsl {3}, smtpClient.Host {4}, smtpClient.PickupDirectoryLocation {5}, smtpClient.Port {6}, smtpClient.ServicePoint {7}, smtpClient.TargetName {8}, smtpClient.Timeout {9}, smtpClient.UseDefaultCredentials {10}", smtpClient.ClientCertificates, smtpClient.Credentials, smtpClient.DeliveryMethod, smtpClient.EnableSsl, smtpClient.Host, smtpClient.PickupDirectoryLocation, smtpClient.Port, smtpClient.ServicePoint, smtpClient.TargetName, smtpClient.Timeout, smtpClient.UseDefaultCredentials);

            MailMessage mailMessage = new MailMessage();

            if (LogEntry != null)
            {
                mailMessage.Subject = string.Format("[{0}] - {1}", LogEntry.LoggingEvent.Level.DisplayName, LogEntry.MailSubject);
                mailMessage.Body = LogEntry.MessageEntry;
                if (!string.IsNullOrEmpty(LogEntry.HtmlString))
                {
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(LogEntry.HtmlString);
                    htmlView.ContentType = new System.Net.Mime.ContentType("text/html");
                    mailMessage.AlternateViews.Add(htmlView);
                }

                mailMessage.Attachments.Add(LogEntry.LogoAttachment);

                if (LogEntry.AttachmentCollection != null)
                    foreach (Attachment attachment in LogEntry.AttachmentCollection)
                        mailMessage.Attachments.Add(attachment.MailAttachment);
            }

            mailMessage.BodyEncoding = BodyEncoding;
            mailMessage.From = new MailAddress(From);
            mailMessage.Priority = Priority;

            foreach (string emailTo in To.Split(';'))
                mailMessage.To.Add(emailTo);

            if (!string.IsNullOrEmpty(Cc))
                foreach (string emailCc in Cc.Split(';'))
                    mailMessage.CC.Add(emailCc);

            if (!string.IsNullOrEmpty(Bcc))
                foreach (string emailBcc in Bcc.Split(';'))
                    mailMessage.Bcc.Add(emailBcc);

            if (!string.IsNullOrEmpty(this.ReplyTo))
                mailMessage.ReplyToList.Add(new MailAddress(this.ReplyTo));

            Log.DebugFormat("mailMessage.AlternateViews {0}, mailMessage.Attachments {1}, mailMessage.Bcc {2}, mailMessage.Body {3}, mailMessage.BodyEncoding {4}, mailMessage.CC {5}, mailMessage.DeliveryNotificationOptions {6}, mailMessage.From {7}, mailMessage.Headers {8}, mailMessage.HeadersEncoding {9}, mailMessage.IsBodyHtml {10}, mailMessage.Priority {11}, mailMessage.ReplyToList {12}, mailMessage.Sender {13}, mailMessage.Subject {14}, mailMessage.SubjectEncoding {15}, mailMessage.To {16}", mailMessage.AlternateViews, mailMessage.Attachments, mailMessage.Bcc, mailMessage.Body, mailMessage.BodyEncoding, mailMessage.CC, mailMessage.DeliveryNotificationOptions, mailMessage.From, mailMessage.Headers, mailMessage.HeadersEncoding, mailMessage.IsBodyHtml, mailMessage.Priority, mailMessage.ReplyToList, mailMessage.Sender, mailMessage.Subject, mailMessage.SubjectEncoding, mailMessage.To);

            smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            try
            {
                smtpClient.SendAsync(mailMessage, LogEntry);
            }
            catch(Exception ex)
            {
                Log.Error("SendAsync(smtpClient.SendAsync(mailMessage, LogEntry);", ex);
            }
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                Log.WarnFormat("SendCompletedCallback(object sender {0}, AsyncCompletedEventArgs e {1})\nSend canceled!!!", sender, e);
            if (e.Error != null)
                Log.ErrorFormat("SendCompletedCallback(object sender {0}, AsyncCompletedEventArgs e {1})\nSend error!!!\n{2}", sender, e, e.Error.ToString());
            else
                Log.InfoFormat("SendCompletedCallback(object sender {0}, AsyncCompletedEventArgs e {1})\nMessage sent!!!", sender, e);

            SmtpClient smtpClient = sender as SmtpClient;
            smtpClient.Dispose();
        }
    }
}