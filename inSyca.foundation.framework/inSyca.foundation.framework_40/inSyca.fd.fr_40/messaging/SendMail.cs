using System;
using System.Net;

using inSyca.foundation.framework.configuration;
using inSyca.foundation.framework.diagnostics;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.ComponentModel;
using System.Threading;

namespace inSyca.foundation.framework.messaging
{
    public class SendMail
    {
        public static bool SendAsync(Settings settings, MailMessage message, SmtpClient smtp)
        {
            Log.DebugFormat("SendAsync(Settings settings {0}, MailMessage message {1}, SmtpClient smtp {2})", settings, message, smtp);

            try
            {
                if (smtp == null)
                    if (!SetupSmtpClient(settings, message, ref smtp))
                        return false;

                Log.InfoFormat("SendAsync(Settings settings {0}, MailMessage message {1}, SmtpClient smtp{2})\nHost: {3}\nMessageFrom: {4}\nMessageTo: {5}\nMessageCC: {6}\nMessageBCC: {7}", settings, message, smtp, smtp.Host, message.From, message.To, message.CC, message.Bcc);

                smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

                smtp.SendAsync(message, settings);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { settings, message, smtp }, ex));
                return false;
            }

            return true;
        }

        public static bool Send(Settings settings, MailMessage message, SmtpClient smtp)
        {
            Log.DebugFormat("Send(Settings settings {0}, MailMessage message {1}, SmtpClient smtp {2})", settings, message, smtp);

            try
            {
                if (smtp == null)
                    if (!SetupSmtpClient(settings, message, ref smtp))
                        return false;

                message.BodyEncoding = Encoding.UTF8;
                message.SubjectEncoding = Encoding.UTF8;

                Log.InfoFormat("Send(Settings settings {0}, MailMessage message {1}, SmtpClient smtp{2})\nHost: {3}\nMessageFrom: {4}\nMessageTo: {5}\nMessageCC: {6}\nMessageBCC: {7}", settings, message, smtp, smtp.Host, message.From, message.To, message.CC, message.Bcc);

                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { settings, message, smtp }, ex));
                return false;
            }

            return true;
        }

        private static bool SetupSmtpClient(Settings settings, MailMessage message, ref SmtpClient smtp)
        {
            Log.DebugFormat("SetupSmtpClient(Settings settings {0}, MailMessage message {1}, ref SmtpClient smtp {2})", settings, message, smtp);

            Settings logEventEntrySettings = new Settings(settings);

            if (smtp == null)
            {
                smtp = new SmtpClient();

                //smtp.Host = settings.SmtpSection.Network.Host;
                //smtp.Port = settings.SmtpSection.Network.Port;
                //smtp.DeliveryMethod = settings.SmtpSection.DeliveryMethod;
                //smtp.UseDefaultCredentials = settings.SmtpSection.Network.DefaultCredentials;
                //smtp.PickupDirectoryLocation = settings.SmtpSection.SpecifiedPickupDirectory.PickupDirectoryLocation;

                //if (!string.IsNullOrEmpty(settings.SmtpSection.Network.UserName) || !string.IsNullOrEmpty(settings.SmtpSection.Network.Password))
                //    smtp.Credentials = new System.Net.NetworkCredential(settings.SmtpSection.Network.UserName, settings.SmtpSection.Network.Password);

                //if (string.IsNullOrEmpty(smtp.Host))
                //{
                //    settings.MailLogLevel = -1;

                //    Log.Warn(new LogEntry(logEventEntrySettings, System.Reflection.MethodBase.GetCurrentMethod(), new object[] { settings, message, smtp }, "Warning: SMTP settings invalid (check application configuration file -> {0}{1})", new object[] { AppDomain.CurrentDomain.SetupInformation.ApplicationBase, AppDomain.CurrentDomain.FriendlyName }));
                //    return false;
                //}

                //if (string.IsNullOrEmpty(Configuration.MailMessageTo))
                //{
                //    settings.MailLogLevel = -1;

                //    Log.Warn(new LogEntry(logEventEntrySettings, System.Reflection.MethodBase.GetCurrentMethod(), new object[] { settings, message, smtp }, "Warning: 'MailMessageTo' not set (check application configuration file -> {0}{1})", new object[] { AppDomain.CurrentDomain.SetupInformation.ApplicationBase, AppDomain.CurrentDomain.FriendlyName }));
                //    return false;
                //}
            }

            return true;
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            Settings logEventEntrySettings = new Settings((Settings)e.UserState);

            if (e.Cancelled)
                Log.WarnFormat("SendCompletedCallback(object sender {0}, AsyncCompletedEventArgs e {1})\nSend canceled!!!\n", sender, e);
            if (e.Error != null)
                Log.Error(new LogEntry(MethodBase.GetCurrentMethod(), new object[] { sender, e }, "\n{0}!!!\n", new object[] { e.Error.ToString() }));
            else
                Log.InfoFormat("SendCompletedCallback(object sender {0}, AsyncCompletedEventArgs e {1})\nMessage sent!!!\n", sender, e);

            SmtpClient smtpClient = sender as SmtpClient;
            smtpClient.Dispose();
        }
    }
}
