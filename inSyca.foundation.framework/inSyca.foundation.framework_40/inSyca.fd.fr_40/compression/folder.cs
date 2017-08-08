using System.IO;

namespace inSyca.foundation.framework.compression
{
    public class folder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSourceDirectory"></param>
        /// <param name="strSourceSearchPattern"></param>
        /// <param name="oSearchOption"></param>
        /// <param name="strTargetFile"></param>
        /// <param name="password"></param>
        public static void ZipFiles(string strSourceDirectory, string strSourceSearchPattern, SearchOption oSearchOption, string strTargetFile, string password)
        {
            // Zip up the files
            using (ICSharpCode.SharpZipLib.Zip.ZipOutputStream oZipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(File.Create(string.Format(@"{0}\{1}.zip", strSourceDirectory, strTargetFile))))
            {
                if (!string.IsNullOrEmpty(password))
                    oZipStream.Password = password;

                oZipStream.SetLevel(9); // 0-9, 9 being the highest compression

                ZipFiles(strSourceDirectory, strSourceSearchPattern, oSearchOption, oZipStream);

                oZipStream.Finish();
                oZipStream.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSourceDirectory"></param>
        /// <param name="strSourceSearchPattern"></param>
        /// <param name="oSearchOption"></param>
        /// <param name="oTargetStream"></param>
        public static void ZipFiles(string strSourceDirectory, string strSourceSearchPattern, SearchOption oSearchOption, out MemoryStream oTargetStream)
        {
            oTargetStream = new MemoryStream();

            // Zip up the files
            using (ICSharpCode.SharpZipLib.Zip.ZipOutputStream oZipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(oTargetStream))
            {
                oZipStream.SetLevel(9); // 0-9, 9 being the highest compression

                ZipFiles(strSourceDirectory, strSourceSearchPattern, oSearchOption, oZipStream);

                oZipStream.IsStreamOwner = false;
                oZipStream.Close();
            }

            oTargetStream.Position = 0;
        }

        private static void ZipFiles(string strSourceDirectory, string strSourceSearchPattern, SearchOption oSearchOption, ICSharpCode.SharpZipLib.Zip.ZipOutputStream oZipStream)
        {
            string[] filenames = Directory.GetFiles(strSourceDirectory, strSourceSearchPattern, oSearchOption);

            foreach (string file in filenames)
            {
                ICSharpCode.SharpZipLib.Zip.ZipEntry oZipEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(Path.GetFileName(file));
                oZipStream.PutNextEntry(oZipEntry);

                if (!file.EndsWith(@"\")) // if a file ends with '/' its a directory
                {
                    FileStream oStream = File.OpenRead(file);
                    byte[] oBuffer = new byte[oStream.Length];
                    oStream.Read(oBuffer, 0, oBuffer.Length);
                    oZipStream.Write(oBuffer, 0, oBuffer.Length);
                    oStream.Close();
                }
            }
        }

    }
}
