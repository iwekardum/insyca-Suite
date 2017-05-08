using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace inSyca.foundation.framework.compression
{
    public class file
    {
        #region helpers

        public static void UnzipFile(string zipFileName, string targetDirectory)
        {
            (new FastZip()).ExtractZip(zipFileName, targetDirectory, null);
        }

        public static void UnzipFile(Stream file, string targetFolder)
        {
            try
            {
                using (var ZipStream = new ZipInputStream(file))
                {
                    ZipEntry theEntry;
                    while ((theEntry = ZipStream.GetNextEntry()) != null)
                    {
                        if (theEntry.IsFile)
                        {
                            if (theEntry.Name != "")
                            {
                                string strNewFile = @"" + targetFolder + @"\" + theEntry.Name;
                                string fullDirPath = Path.GetDirectoryName(strNewFile);
                                if (!Directory.Exists(fullDirPath)) 
                                    Directory.CreateDirectory(fullDirPath);


                                if (File.Exists(strNewFile))
                                    continue;

                                using (FileStream streamWriter = File.Create(strNewFile))
                                {
                                    int size = 2048;
                                    byte[] data = new byte[2048];
                                    while (true)
                                    {
                                        size = ZipStream.Read(data, 0, data.Length);
                                        if (size > 0)
                                            streamWriter.Write(data, 0, size);
                                        else
                                            break;
                                    }
                                    streamWriter.Close();
                                }
                            }
                        }
                        else if (theEntry.IsDirectory)
                        {
                            string strNewDirectory = @"" + targetFolder + @"\" + theEntry.Name;
                           
                            if (!Directory.Exists(strNewDirectory))
                                Directory.CreateDirectory(strNewDirectory);
                        }
                    }
                    ZipStream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
