using log4net.Appender;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using log4net.Core;
using log4net.Util;
using System.Net;
using System.ComponentModel;
using System.Drawing;

namespace inSyca.foundation.framework.diagnostics
{
    public class SmtpMonitoringAppender : SmtpAppender, IAppender
    {
        public SmtpMonitoringAppender()
        {
        }
        public LogEntry LogEntry { get; set; }

        public LoggingEvent LoggingEvent { get; set; }

        void IAppender.DoAppend(LoggingEvent loggingEvent)
        {
            LogEntry = loggingEvent.MessageObject as LogEntry;
            LoggingEvent = loggingEvent;

            base.DoAppend(loggingEvent);
        }

        protected override void SendEmail(string messageBody)
        {
            // Create and configure the smtp client
            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Host = SmtpHost;
            smtpClient.Port = Port;
            //            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = EnableSsl;

            if (Authentication == SmtpAuthentication.Basic)
            {
                // Perform basic authentication
                smtpClient.Credentials = new System.Net.NetworkCredential(Username, Password);
            }
            else if (Authentication == SmtpAuthentication.Ntlm)
            {
                // Perform integrated authentication (NTLM)
                smtpClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
            }

            MailMessage mailMessage = new MailMessage();

            if (LogEntry != null)
            {
                mailMessage.Subject = string.Format("[{0}] - {1}", LoggingEvent.Level.DisplayName, LogEntry.ToString());
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

            if (!String.IsNullOrEmpty(Cc))
                foreach (string emailCc in Cc.Split(';'))
                    mailMessage.CC.Add(emailCc);

            if (!String.IsNullOrEmpty(Bcc))
                foreach (string emailBcc in Bcc.Split(';'))
                    mailMessage.Bcc.Add(emailBcc);

            if (!String.IsNullOrEmpty(this.ReplyTo))
                mailMessage.ReplyToList.Add(new MailAddress(this.ReplyTo));

            smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            smtpClient.SendAsync(mailMessage, LogEntry);
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            LogEntry logEventEntrySettings = (LogEntry)e.UserState;

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