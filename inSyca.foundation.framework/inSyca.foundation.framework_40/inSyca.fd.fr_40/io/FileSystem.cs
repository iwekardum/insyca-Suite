using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

using inSyca.foundation.framework.configuration;
using inSyca.foundation.framework.diagnostics;
using System.Data;
using System.IO.Compression;

namespace inSyca.foundation.framework.io
{
    public class FileSystem
    {
        private List<Attachment> attachments;
        public List<Attachment> Attachments
        {
            get
            {
                return attachments;
            }
        }

        private data.dsFileSystemConfig dsFileSystemConfig;
        public data.dsFileSystemConfig FileSystemConfig
        {
            set
            {
                dsFileSystemConfig = value;
            }
        }

        private data.dsFileSystemEntry dsFileSystemEntry;
        public data.dsFileSystemEntry FileSystemEntry
        {
            get
            {
                return dsFileSystemEntry;
            }
        }

        public FileSystem()
        {
            attachments = new List<Attachment>();
            dsFileSystemEntry = new data.dsFileSystemEntry();
            dsFileSystemConfig = new data.dsFileSystemConfig();
        }

        public void Process()
        {
            Log.Debug("Process()");

            dsFileSystemConfig = new data.dsFileSystemConfig();
            dsFileSystemConfig.ReadXml(Path.Combine(new FileInfo(Configuration.ConfigFileInfo).DirectoryName, "foundation.framework.filesystem.xml"));

            if (dsFileSystemConfig.Entry != null)
            {
                foreach (data.dsFileSystemConfig.EntryRow oScanFilesRow in dsFileSystemConfig.Entry.Rows)
                {
                    data.dsFileSystemEntry.dtResultRow resultRow;

                    ProcessEntryRow(oScanFilesRow, out resultRow);

                    LogEntry logEntry;

                    SetHtmlMessage(oScanFilesRow, resultRow, out logEntry);

                    dsFileSystemEntry.Clear();

                    Log.Info(logEntry);
                }
            }
        }

        public void ProcessEntryRow(data.dsFileSystemConfig.EntryRow oScanFilesRow, out data.dsFileSystemEntry.dtResultRow resultRow)
        {
            attachments.Clear();

            resultRow = dsFileSystemEntry.dtResult.NewdtResultRow();

            resultRow.folder_found = 0;
            resultRow.folder_error = 0;
            resultRow.files_found = 0;
            resultRow.files_found_size = 0;
            resultRow.file_error = 0;

            List<string> foundFolders = new List<string>();
            List<string> errorFolders = new List<string>();
            List<string> errorFiles = new List<string>();

            GetFolders(oScanFilesRow.Folder, oScanFilesRow.FolderMask, oScanFilesRow.ScanSubFolder, ref foundFolders, ref errorFolders);

            foreach (string strFolder in foundFolders)
                ProcessFolder(oScanFilesRow, resultRow, strFolder, ref errorFiles);

            foreach (string strFolder in errorFolders)
            {
                dsFileSystemEntry.dtError.AdddtErrorRow(strFolder);
                resultRow.folder_error++;
            }

            foreach (string strFile in errorFiles)
            {
                dsFileSystemEntry.dtError.AdddtErrorRow(strFile);
                resultRow.file_error++;
            }

            resultRow.folder_found += foundFolders.Count;

            dsFileSystemEntry.dtResult.AdddtResultRow(resultRow);
            dsFileSystemEntry.AcceptChanges();

            Log.InfoFormat("Action: {0}\nfolder_found = {1}\nfolder_error = {2}\nfiles_found = {3}\nfiles_found_size = {4}\nfile_error = {5}", oScanFilesRow.Action, resultRow.folder_found, resultRow.folder_error, resultRow.files_found, resultRow.files_found_size, resultRow.file_error );

            string contentName = string.Format("{0}_Success.xml.zip", oScanFilesRow.Action);

            Stream folderEntryStream = BuildAttachment(dsFileSystemEntry.dtFolderEntry, ref contentName);
            attachments.Add(new Attachment(folderEntryStream, contentName));

            contentName = string.Format("{0}_Failed.xml.zip", oScanFilesRow.Action);

            Stream errorStream = BuildAttachment(dsFileSystemEntry.dtError, ref contentName);
            attachments.Add(new Attachment(errorStream, contentName));
        }

        private MemoryStream BuildAttachment(DataTable dataTable, ref string contentName)
        {
            var inStream = new MemoryStream();
            dataTable.WriteXml(inStream);
            inStream.Position = 0;

            MemoryStream oTargetStream = new MemoryStream();

            ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(oTargetStream);

            zipStream.SetLevel(9); // 0-9, 9 being the highest compression

            ICSharpCode.SharpZipLib.Zip.ZipEntry newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(contentName.Replace(".xml.zip", ".xml"));
            newEntry.DateTime = DateTime.Now;

            zipStream.PutNextEntry(newEntry);

            ICSharpCode.SharpZipLib.Core.StreamUtils.Copy(inStream, zipStream, new byte[4096]);
            zipStream.CloseEntry();

            zipStream.IsStreamOwner = false;
            zipStream.Close();

            oTargetStream.Position = 0;

            if (oTargetStream.Length > 524288)
            {
                oTargetStream = new MemoryStream();
                data.dsFileSystemEntry dsAttachmentErrorEntry = new data.dsFileSystemEntry();
                dsAttachmentErrorEntry.dtError.AdddtErrorRow("Too many results. Cannot send attachment by mail");
                dsAttachmentErrorEntry.dtError.AcceptChanges();
                dsAttachmentErrorEntry.dtError.WriteXml(oTargetStream);
                oTargetStream.Position = 0;
                contentName = contentName.Replace(".xml.zip", ".xml");
            }

            return oTargetStream;
        }

        private void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        private void ProcessFolder(data.dsFileSystemConfig.EntryRow scanFilesRow, data.dsFileSystemEntry.dtResultRow resultRow, string strFolder, ref List<string> errorFiles)
        {
            data.dsFileSystemEntry.dtFolderEntryRow folderEntryRow = dsFileSystemEntry.dtFolderEntry.NewdtFolderEntryRow();
            folderEntryRow.FilesInFolder = 0;
            folderEntryRow.FilesInFolderSize = 0;

            folderEntryRow.Id = Guid.NewGuid().ToString();

            try
            {
                foreach (string strFile in Directory.GetFiles(strFolder, scanFilesRow.FileMask, SearchOption.TopDirectoryOnly))
                    ProcessFile(scanFilesRow, resultRow, folderEntryRow, strFile, ref errorFiles);
            }
            catch
            {
                ;
            }

            folderEntryRow.Name = strFolder;

            dsFileSystemEntry.dtFolderEntry.AdddtFolderEntryRow(folderEntryRow);
        }


        private void ProcessFile(data.dsFileSystemConfig.EntryRow entryRow, data.dsFileSystemEntry.dtResultRow resultRow, data.dsFileSystemEntry.dtFolderEntryRow folderEntryRow, string strFile, ref List<string> errorFiles)
        {
            FileInfo fileInfo = new FileInfo(strFile);

            if (DateTime.Now.AddDays(-entryRow.FileAge) > fileInfo.CreationTime)
                switch (entryRow.Action.ToLower())
                {
                    case "scan":
                        Scan(resultRow, folderEntryRow, fileInfo, ref errorFiles);
                        break;
                    case "copy":
                        Copy(resultRow, folderEntryRow, fileInfo, entryRow.DestinationFolder, ref errorFiles);
                        break;
                    case "move":
                        Move(resultRow, folderEntryRow, fileInfo, entryRow.DestinationFolder, ref errorFiles);
                        break;
                    case "delete":
                        Delete(resultRow, folderEntryRow, fileInfo, ref errorFiles);
                        break;
                    default:
                        break;
                }
        }

        private void Scan(data.dsFileSystemEntry.dtResultRow resultRow, data.dsFileSystemEntry.dtFolderEntryRow folderEntryRow, FileInfo fileInfo, ref List<string> errorFiles)
        {
            data.dsFileSystemEntry.dtFileEntryRow fileEntryRow = dsFileSystemEntry.dtFileEntry.NewdtFileEntryRow();
            fileEntryRow.Id = folderEntryRow.Id;
            fileEntryRow.Name = Path.Combine(fileInfo.DirectoryName, fileInfo.Name);
            fileEntryRow.CreationTime = fileInfo.CreationTimeUtc;
            fileEntryRow.LastAccessTime = fileInfo.LastAccessTimeUtc;
            fileEntryRow.LastWriteTime = fileInfo.LastWriteTimeUtc;
            fileEntryRow.Size = fileInfo.Length;
            dsFileSystemEntry.dtFileEntry.AdddtFileEntryRow(fileEntryRow);

            resultRow.files_found++;
            resultRow.files_found_size += fileInfo.Length;

            folderEntryRow.FilesInFolder++;
            folderEntryRow.FilesInFolderSize += fileInfo.Length;
        }

        private void Copy(data.dsFileSystemEntry.dtResultRow resultRow, data.dsFileSystemEntry.dtFolderEntryRow folderEntryRow, FileInfo fileInfo, string destinationFolder, ref List<string> errorFiles)
        {
            data.dsFileSystemEntry.dtFileEntryRow fileEntryRow = dsFileSystemEntry.dtFileEntry.NewdtFileEntryRow();
            fileEntryRow.Id = folderEntryRow.Id;
            fileEntryRow.Name = Path.Combine(fileInfo.DirectoryName, fileInfo.Name);
            fileEntryRow.CreationTime = fileInfo.CreationTimeUtc;
            fileEntryRow.LastAccessTime = fileInfo.LastAccessTimeUtc;
            fileEntryRow.LastWriteTime = fileInfo.LastWriteTimeUtc;
            fileEntryRow.Size = fileInfo.Length;
            dsFileSystemEntry.dtFileEntry.AdddtFileEntryRow(fileEntryRow);

            resultRow.files_found++;
            resultRow.files_found_size += fileInfo.Length;

            folderEntryRow.FilesInFolder++;
            folderEntryRow.FilesInFolderSize += fileInfo.Length;

            try
            {
                System.IO.File.Copy(Path.Combine(fileInfo.DirectoryName, fileInfo.Name), Path.Combine(destinationFolder, fileInfo.Name));
            }
            catch (Exception e)
            {
                errorFiles.Add(e.Message);
            }
        }

        private void Move(data.dsFileSystemEntry.dtResultRow resultRow, data.dsFileSystemEntry.dtFolderEntryRow folderEntryRow, FileInfo fileInfo, string destinationFolder, ref List<string> errorFiles)
        {
            data.dsFileSystemEntry.dtFileEntryRow fileEntryRow = dsFileSystemEntry.dtFileEntry.NewdtFileEntryRow();
            fileEntryRow.Id = folderEntryRow.Id;
            fileEntryRow.Name = Path.Combine(fileInfo.DirectoryName, fileInfo.Name);
            fileEntryRow.CreationTime = fileInfo.CreationTimeUtc;
            fileEntryRow.LastAccessTime = fileInfo.LastAccessTimeUtc;
            fileEntryRow.LastWriteTime = fileInfo.LastWriteTimeUtc;
            fileEntryRow.Size = fileInfo.Length;
            dsFileSystemEntry.dtFileEntry.AdddtFileEntryRow(fileEntryRow);

            resultRow.files_found++;
            resultRow.files_found_size += fileInfo.Length;

            folderEntryRow.FilesInFolder++;
            folderEntryRow.FilesInFolderSize += fileInfo.Length;

            try
            {
                System.IO.File.Move(Path.Combine(fileInfo.DirectoryName, fileInfo.Name), Path.Combine(destinationFolder, fileInfo.Name));
            }
            catch (Exception e)
            {
                errorFiles.Add(e.Message);
            }
        }

        private void Delete(data.dsFileSystemEntry.dtResultRow resultRow, data.dsFileSystemEntry.dtFolderEntryRow folderEntryRow, FileInfo fileInfo, ref List<string> errorFiles)
        {
            data.dsFileSystemEntry.dtFileEntryRow fileEntryRow = dsFileSystemEntry.dtFileEntry.NewdtFileEntryRow();
            fileEntryRow.Id = folderEntryRow.Id;
            fileEntryRow.Name = Path.Combine(fileInfo.DirectoryName, fileInfo.Name);
            fileEntryRow.CreationTime = fileInfo.CreationTimeUtc;
            fileEntryRow.LastAccessTime = fileInfo.LastAccessTimeUtc;
            fileEntryRow.LastWriteTime = fileInfo.LastWriteTimeUtc;
            fileEntryRow.Size = fileInfo.Length;
            dsFileSystemEntry.dtFileEntry.AdddtFileEntryRow(fileEntryRow);

            resultRow.files_found++;
            resultRow.files_found_size += fileInfo.Length;

            folderEntryRow.FilesInFolder++;
            folderEntryRow.FilesInFolderSize += fileInfo.Length;

            try
            {
                System.IO.File.Delete(Path.Combine(fileInfo.DirectoryName, fileInfo.Name));
            }
            catch (Exception e)
            {
                errorFiles.Add(e.Message);
            }
        }

        protected void SetHtmlMessage(data.dsFileSystemConfig.EntryRow oScanFilesRow, data.dsFileSystemEntry.dtResultRow resultDataRow, out LogEntry logEntry)
        {
            logEntry = new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null);

            //logEntry.Settings.MailLogLevel = 2;

            string entryTypeColor = EntryType.InformationColor;

            string htmlHeader = string.Empty;
            string htmlBody = string.Empty;
            string fileSize = string.Empty;
            string destinationFolder = string.Empty;

            if (resultDataRow.files_found_size < 1024)
                fileSize = string.Format("{0:0.000} Bytes", resultDataRow.files_found_size);
            else if (resultDataRow.files_found_size < 1024 * 1024)
                fileSize = string.Format("{0:0} KB", resultDataRow.files_found_size / 1024.0);
            else if (resultDataRow.files_found_size < 1024 * 1024 * 1024)
                fileSize = string.Format("{0:0.00} MB", resultDataRow.files_found_size / 1024.0 / 1024.0);
            else
                fileSize = string.Format("{0:0.00} GB", resultDataRow.files_found_size / 1024.0 / 1024.0 / 1024.0);

            if (string.IsNullOrEmpty(oScanFilesRow.DestinationFolder))
                destinationFolder = "n.A.";
            else
                destinationFolder = oScanFilesRow.DestinationFolder;

            htmlHeader = Properties.Resources.FileSystem.Substring(0, Properties.Resources.FileSystem.IndexOf("<body>"));
            htmlBody = string.Format(Properties.Resources.FileSystem.Substring(Properties.Resources.FileSystem.IndexOf("<body>")),
                                            string.Format("<span style='color: {0}'>{1}</span>", entryTypeColor, oScanFilesRow.Action),
                                            oScanFilesRow.FileMask,
                                            oScanFilesRow.FolderMask,
                                            oScanFilesRow.Folder,
                                            oScanFilesRow.ScanSubFolder,
                                            oScanFilesRow.FileAge,
                                            destinationFolder,
                                            resultDataRow.folder_found.ToString(),
                                            resultDataRow.folder_error.ToString(),
                                            resultDataRow.files_found.ToString(),
                                            fileSize,
                                            resultDataRow.file_error.ToString());

            logEntry.HtmlString = htmlHeader + htmlBody;
            logEntry.AttachmentCollection.AddRange(attachments);
        }

        private static void GetFolders(string strTopFolder, string strFolderMask, bool scanSubFolder, ref List<string> Folders, ref List<string> errorFolders)
        {
            string[] foundMaskedDirectories = null;

            try
            {
                Folders.Add(strTopFolder);

                if (scanSubFolder)
                {
                    foundMaskedDirectories = Directory.GetDirectories(strTopFolder, strFolderMask);

                    Folders.AddRange(foundMaskedDirectories);

                    foreach (string strDirectory in foundMaskedDirectories)
                        GetFolders(strDirectory, strFolderMask, true, ref Folders, ref errorFolders);
                }
            }
            catch (Exception ex)
            {
                errorFolders.Add(ex.Message);
            }
        }
    }
}
