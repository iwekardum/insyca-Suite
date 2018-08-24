using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace inSyca.foundation.framework.conversion
{
    public static class StreamConverter
    {
        public static string StreamToString(Stream stream, Encoding encoding)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, encoding))
            {
                return reader.ReadToEnd();
            }
        }

        public static Stream StringToStream(string src, Encoding encoding)
        {
            byte[] byteArray = encoding.GetBytes(src);
            return new MemoryStream(byteArray);
        }
    }
}
