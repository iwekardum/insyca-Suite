using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using Microsoft.BizTalk.ExplorerOM;
using shfb.helper;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk applications that were documented.
    /// </summary>
    internal class SchemasTopic : TopicFile
    {

        List<String> schemaNames = new List<String>();

        string appName;

        public SchemasTopic(List<String> schemaNames, string appName)
        {
            this.schemaNames = schemaNames;
            this.appName = appName;
            Title = "Schemas";
            LinkTitle = "Schemas";
            LinkUri = "##Schemas";
            TopicType = TopicType.Schemas;
            tokenId = CleanAndPrep(appName + ".Schemas");
            TokenFile.GetTokenFile().AddTopicToken(tokenId, Id);
        }

        protected override void SaveTopic(MamlWriter writer)
        {
            var root = new XElement(xmlns + "developerOrientationDocument", new XAttribute("xmlns", xmlns),
                                       new XAttribute(XNamespace.Xmlns + "xlink", xlink));
            List<XElement> links = new List<XElement>();
            foreach(string sn in this.schemaNames)
            {
                links.Add(new XElement(xmlns + "para", new XElement(xmlns + "token", new XText(CleanAndPrep(appName + ".Schemas." + sn)))));
            }

            var inThis = new XElement(xmlns + "inThisSection", new XText("This application contains the following schemas:"));
            inThis.Add(links.ToArray());
            root.Add(inThis);

            var test = new XDocument(
            new XElement("topic",
                 new XAttribute("id", Id),
                 new XAttribute("revision", "1")));
            writer.WriteRaw(test.ToString());
            //writer.StartIntroduction();
            //writer.StartParagraph();
            writer.WriteRaw(root.ToString());
            //writer.EndParagraph();
            //writer.EndIntroduction();
        }
    }
}
