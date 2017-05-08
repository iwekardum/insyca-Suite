using inSyca.foundation.communication.components.diagnostics;
using inSyca.foundation.framework;
using System;
using System.IO;
using System.Net;
using System.Text;

/// <summary>
/// Namespace for communication components
/// </summary>
namespace inSyca.foundation.communication.components
{
    public class Ftp
    {
        private FtpWebRequest oFtpWebRequest;

        public string ServerIP { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string RemoteDirectory { get; set; }
        public string LocalDirectory { get; set; }
        public bool UseBinary { get; set; }
        public bool UsePassive { get; set; }

        private void CreateFtpWebRequest(Uri serverUri, string strWebRequestMethod)
        {
            Log.DebugFormat("CreateFtpWebRequest(Uri serverUri {0}, string strWebRequestMethod {1})", serverUri, strWebRequestMethod);

            if (serverUri.Scheme != Uri.UriSchemeFtp)
            {
                return;
            }

            try
            {
                oFtpWebRequest = (FtpWebRequest)FtpWebRequest.Create(serverUri);
                oFtpWebRequest.UseBinary = UseBinary;
                oFtpWebRequest.UsePassive = UsePassive;
                oFtpWebRequest.Credentials = new NetworkCredential(UserID, Password);
                oFtpWebRequest.Method = strWebRequestMethod;
                oFtpWebRequest.Proxy = null;
                oFtpWebRequest.KeepAlive = false;

                Log.DebugFormat("CreateFtpWebRequest(Uri serverUri {0}, string strWebRequestMethod {1})\nFtpWebRequest created\nUseBinary:{2}\nUsePassive:{3}\nUserID:{4}", serverUri, strWebRequestMethod, UseBinary, UsePassive, UserID);
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { serverUri, strWebRequestMethod }, ex));
            }
        }

        public string[] GetFileList()
        {
            Log.Debug("GetFileList()");

            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;
            Uri serverUri;

            if (string.IsNullOrEmpty(ServerIP))
                return null;

            if (!string.IsNullOrEmpty(RemoteDirectory))
                serverUri = new Uri(string.Format("ftp://{0}/{1}/", ServerIP, RemoteDirectory));
            else
                serverUri = new Uri(string.Format("ftp://{0}/", ServerIP));

            CreateFtpWebRequest(serverUri, WebRequestMethods.Ftp.ListDirectory);

            try
            {
                response = oFtpWebRequest.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();

                if (line == null)
                    return new string[0];

                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }

                // to remove the trailing '\n'
                if (result.Length > 0)
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);

                return result.ToString().Split('\n');
            }
            catch (System.Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));

                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                downloadFiles = null;
                return downloadFiles;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSourceFile"></param>
        /// <param name="strDestinationFile"></param>
        /// <returns></returns>
        public bool Download(string strSourceFile, string strDestinationFile)
        {
            Log.DebugFormat("Download(string strSourceFile {0}, string strDestinationFile {1})", strSourceFile, strDestinationFile);

            Uri serverUri;

            if (string.IsNullOrEmpty(ServerIP))
                return false;

            if (!string.IsNullOrEmpty(RemoteDirectory))
                serverUri = new Uri(string.Format("ftp://{0}/{1}/{2}", ServerIP, RemoteDirectory, strSourceFile));
            else
                serverUri = new Uri(string.Format("ftp://{0}/{1}", ServerIP, strSourceFile));

            CreateFtpWebRequest(serverUri, WebRequestMethods.Ftp.DownloadFile);

            Stream input = null;

            try
            {
                using (FtpWebResponse response = (FtpWebResponse)oFtpWebRequest.GetResponse())
                {
                    input = response.GetResponseStream();

                    if (input != null)
                    {
                        using (Stream output = File.OpenWrite(strDestinationFile))
                        {
                            IO.CopyStream(input, output);
                        }
                        Log.DebugFormat("Download(string strSourceFile {0}, string strDestinationFile {1})\nIO.CopyStream success", strSourceFile, strDestinationFile);
                        return true;
                    }
                    else
                    {
                        Log.Warn(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { strSourceFile, strDestinationFile }, "Warning: {0}", new object[] {"input stream is null"}));
                        return false;
                    }
                }
            }
            catch (WebException wEx)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { strSourceFile, strDestinationFile }, "Error: {0}", new object[] {wEx.Message}));
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { strSourceFile, strDestinationFile }, "Error: {0}", new object[] {ex.Message}));
            }

            return false ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oBytes"></param>
        /// <param name="strFile"></param>
        public void Upload(byte[] oBytes, string strFile)
        {
            Log.DebugFormat("Upload(byte[] oBytes {0}, string strFile {1})", oBytes, strFile);

            Uri serverUri;

            if (string.IsNullOrEmpty(ServerIP))
                return;

            if (!string.IsNullOrEmpty(RemoteDirectory))
                serverUri = new Uri(string.Format("ftp://{0}/{1}/{2}", ServerIP, RemoteDirectory, strFile));
            else
                serverUri = new Uri(string.Format("ftp://{0}/{1}", ServerIP, strFile));

            CreateFtpWebRequest(serverUri, WebRequestMethods.Ftp.UploadFile);

            try
            {
                Stream oFtpStream = oFtpWebRequest.GetRequestStream();

                oFtpStream.Write(oBytes, 0, oBytes.Length);

                oFtpStream.Close();

                FtpWebResponse response = (FtpWebResponse)oFtpWebRequest.GetResponse();
            }
            catch (WebException wEx)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { oBytes, strFile }, wEx));
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { oBytes, strFile }, ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFile"></param>
        public void Delete(string strFile)
        {
            Log.DebugFormat("Delete(string strFile {0})", strFile);

            Uri serverUri;

            if (!string.IsNullOrEmpty(ServerIP))
                return;

            if (!string.IsNullOrEmpty(RemoteDirectory))
                serverUri = new Uri(string.Format("ftp://{0}/{1}/{2}", ServerIP, RemoteDirectory, strFile));
            else
                serverUri = new Uri(string.Format("ftp://{0}/{1}", ServerIP, strFile));

            try
            {
                CreateFtpWebRequest(serverUri, WebRequestMethods.Ftp.DeleteFile);

                FtpWebResponse response = (FtpWebResponse)oFtpWebRequest.GetResponse();
            }
            catch (WebException wEx)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { strFile }, wEx));
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { strFile }, ex));
            }
        }
    }
}