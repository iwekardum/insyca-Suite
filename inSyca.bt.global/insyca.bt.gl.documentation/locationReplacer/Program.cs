using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace locationReplacer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
                return;

            string source           = args[0];
            string schemaLocation   = args[1];

            DirectoryInfo directoryInfo = new DirectoryInfo(source);

            FileInfo[] fileInfo = directoryInfo.GetFiles("*.xsd");

            foreach (var file in fileInfo)
            {
                ProcessFile(file.FullName, schemaLocation);
            }
        }

        private static void ProcessFile(string source, string schemaLocation)
        {
            string[] lines = File.ReadAllLines(source);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(Properties.Settings.Default.lineIdentifier))
                {
                    string[] stringParts = lines[i].Split(new char[] { Properties.Settings.Default.splitParameter1, Properties.Settings.Default.splitParameter2 });

                    for (int j = 0; j < stringParts.Length; j++)
                    {
                        if (stringParts[j].Contains(Properties.Settings.Default.stringIdentifier))
                        {
                            stringParts[j] = stringParts[j].Replace(Properties.Settings.Default.replaceParameter1_A, Properties.Settings.Default.replaceParameter1_B);
                            stringParts[j] = stringParts[j].Replace(Properties.Settings.Default.replaceParameter2_A, schemaLocation);
                            stringParts[j] = stringParts[j] + Properties.Settings.Default.extensionParameter;
                            break;

                        }
                    }
                    lines[i] = string.Join("\"", stringParts);
                }
            }

            // Save file
            Encoding utf8WithoutBom = new UnicodeEncoding(true, true);
            TextWriter tw = new StreamWriter(source, false, utf8WithoutBom);
            foreach (string s in lines)
                tw.WriteLine(s);
            tw.Close();
        }
    }
}
