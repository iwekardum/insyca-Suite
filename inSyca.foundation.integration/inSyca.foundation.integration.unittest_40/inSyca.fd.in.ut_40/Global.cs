using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using inSyca.foundation.integration.biztalk.components;
using inSyca.foundation.integration.biztalk.functions;

namespace inSyca.foundation.integration.unittest
{
    [TestClass]
    public class Global
    {
        [TestMethod]
        public void ExcelToXml()
        {
            FileStream fs = new FileStream(@"C:\temp\mappe1.xlsx", FileMode.Open);
            sandbox.Execute(fs);
            fs.Close();
            //Excel2007Decoder sxDecoder = new Excel2007Decoder();

            //sxDecoder.IsFirstRowHeader = true;
            //sxDecoder.NamespaceBase = "http:\\test";
            //sxDecoder.NamespacePrefix = "ns0";
            //sandbox.ImportExcelXML(fs, true, true);
            //sandbox.ImportExcelXLS(@"C:\temp\mappe1.xlsx");
        }

        [TestMethod]
        public void SplitAddressPart()
        {
            string part1 = "Jakobi 45 str. 1A";
            string part2 = "39018 Terlan BZ";

            string street = scripting.ExtractAddressPart("{STR} {NBR}", "{STR}", part1);
            string number = scripting.ExtractAddressPart("{STR} {NBR}", "{NBR}", part1);
            string country = scripting.ExtractAddressPart("{ZIP} {CTY} {CON}", "{CON}", part2);
            string zipcode = scripting.ExtractAddressPart("{ZIP} {CTY} {CON}", "{ZIP}", part2);
            string city = scripting.ExtractAddressPart("{ZIP} {CTY} {CON}", "{CTY}", part2);
        }
    }
}
