using shfb.helper;
using Microsoft.BizTalk.ExplorerOM;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System;
using System.Reflection;

namespace BizTalkDocumentation
{
    /// <summary>
    /// The Sandcastle Orientation document for outlining the BizTalk schemas that were documented.
    /// </summary>
    internal class SchemaTopic : TopicFile
    {
        Schema schema;

        /// <summary>
        /// Creates a new schema topic instance.
        /// </summary>
        /// <param name="basePath">The path the schema topics will be persisted to.</param>
        /// <param name="imagePath">The path where all project images will be stored.</param>
        /// <param name="btsAppNames">The list of BizTalk schema names that will be documented.</param>
        /// <param name="rulesDb">The name of the BizTalk Rules Engine database.</param>
        public SchemaTopic(Schema _schema)
        {
            schema = _schema;

            Title = string.Format("{0}#{1}", schema.FullName, schema.RootName);
            LinkTitle = string.Format("{0}#{1}", schema.FullName, schema.RootName);
            LinkUri = "#" + string.Format("{0}#{1}", schema.FullName, schema.RootName);
            TopicType = TopicType.Schema;
            tokenId = CleanAndPrep(schema.Application.Name + ".Schemas." + schema.FullName + "___" + schema.RootName);
            TokenFile.GetTokenFile().AddTopicToken(tokenId, Id);

            appName = schema.Application.Name;
            assemblyName = schema.BtsAssembly.DisplayName;

        }
 
        protected override void SaveTopic(MamlWriter writer)
        {
            var intro = new XElement(xmlns + "introduction", new XElement(xmlns + "para", new XText(string.IsNullOrEmpty(schema.Description) ? "No description was found for the schema." : schema.Description)));
            var section = new XElement(xmlns + "section", new XElement(xmlns + "title", new XText("Schema Properties")),
                                                                new XElement(xmlns + "content",
                                                                    new XElement(xmlns + "table",
                                                                        new XElement(xmlns + "tableHeader",
                                                                            new XElement(xmlns + "row",
                                                                                new XElement(xmlns + "entry", new XText("Property")),
                                                                                new XElement(xmlns + "entry", new XText("Value")))),
                                                                        new XElement(xmlns + "row",
                                                                            new XElement(xmlns + "entry", new XText("Application")),
                                                                            new XElement(xmlns + "entry", new XElement(xmlns + "token", new XText(CleanAndPrep(appName))))),
                                                                        new XElement(xmlns + "row",
                                                                            new XElement(xmlns + "entry", new XText("Always Track All Properties")),
                                                                            new XElement(xmlns + "entry", new XText(schema.Type == SchemaType.Property ? "does not apply to property schemas." : schema.AlwaysTrackAllProperties.ToString()))),
                                                                        new XElement(xmlns + "row",
                                                                            new XElement(xmlns + "entry", new XText("Assembly Qualified Name")),
                                                                            new XElement(xmlns + "entry", new XText(assemblyName))),
                                                                        new XElement(xmlns + "row",
                                                                            new XElement(xmlns + "entry", new XText("Properties")),
                                                                            new XElement(xmlns + "entry", schema.Properties.Count > 0 ? DictionaryToTable(schema.Properties, "Property Name", "Property Type") : new XElement(xmlns + "legacyItalic", new XText("(N/A)")))),
                                                                        new XElement(xmlns + "row",
                                                                            new XElement(xmlns + "entry", new XText("Root Name")),
                                                                            new XElement(xmlns + "entry", new XText(schema.RootName ?? "(None)"))),
                                                                        new XElement(xmlns + "row",
                                                                            new XElement(xmlns + "entry", new XText("Target Namespace")),
                                                                            new XElement(xmlns + "entry", new XText(schema.TargetNameSpace ?? "N/A"))),
                                                                        new XElement(xmlns + "row",
                                                                            new XElement(xmlns + "entry", new XText("Tracked Property Names")),
                                                                            schema.TrackedPropertyNames.Count > 0 ? CollectionToList(schema.TrackedPropertyNames) : new XElement(xmlns + "legacyItalic", new XText("(N/A)"))),
                                                                        new XElement(xmlns + "row",
                                                                            new XElement(xmlns + "entry", new XText("Type")),
                                                                            new XElement(xmlns + "entry", new XText(schema.Type.ToString()))))));

            var content = new XElement(xmlns + "codeExample",
                new XElement(xmlns + "code", new XAttribute("language", "xml"), new XText(schema.XmlContent)));

            var test = new XDocument(
                new XElement("topic",
                             new XAttribute("id", Id),
                             new XAttribute("revision", "1")));
            writer.WriteRaw(test.ToString());
            //writer.StartIntroduction();
            //writer.StartParagraph();
            writer.WriteRaw(intro.ToString());
            //writer.EndParagraph();
            //writer.EndIntroduction();

            //writer.StartSection("","");
            //writer.StartParagraph();
            writer.WriteRaw(section.ToString());
            //writer.EndParagraph();
            //writer.EndIntroduction();

            writer.WriteRaw(content.ToString());

        }

        private XsltArgumentList CreateTransformParameterSets()
        {
            XsltArgumentList xsltArgs = new XsltArgumentList();
            xsltArgs.AddParam("GenDate", "", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
            xsltArgs.AddParam("DocVersion", "", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            return xsltArgs;
        }

    }
}
