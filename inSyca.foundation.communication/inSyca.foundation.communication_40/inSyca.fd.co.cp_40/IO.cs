using System;
using System.IO;
using inSyca.foundation.communication.components.diagnostics;
using inSyca.foundation.framework;

namespace inSyca.foundation.communication.components
{
    internal class IO
    {
        /// <summary>
        /// Copies the contents of input to output. Doesn't close either stream.
        /// </summary>
        internal static void CopyStream(Stream input, Stream output)
        {
            Log.DebugFormat("CopyStream(Stream input {0}, Stream output {1})", input, output);

            byte[] buffer = new byte[8 * 1024];
            int len;

            try
            {
                while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, len);
                }
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { input, output }, ex));
            }
        }
    }
}
