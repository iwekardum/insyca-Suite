using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Xml;
using System.Xml.XPath;

using SandcastleBuilder.Utils;
using SandcastleBuilder.Utils.Design;

namespace BizTalkDocumentation.PlugIn
{
    internal sealed class BizTalkPlugInConfiguration
    {
        private BizTalkPlugInConfiguration(BizTalkPlugInConfiguration other)
        {
            BizTalkDbInstance = other.BizTalkDbInstance;
            MgmtDatabaseName = other.MgmtDatabaseName;
            RulesServer = other.RulesServer;
            RulesDatabase = other.RulesDatabase;
        }

        private BizTalkPlugInConfiguration(IBasePathProvider basePathProvider, XPathNavigator navigator)
        {
            BizTalkDbInstance = GetString(navigator, "configuration/biztalk/@dbInstance", string.Empty);
            MgmtDatabaseName = GetString(navigator, "configuration/biztalk/@mgmtDatabaseName", string.Empty);
            RulesServer = GetString(navigator, "configuration/biztalk/@rulesServer", string.Empty);
            RulesDatabase = GetString(navigator, "configuration/biztalk/@rulesDatabase", string.Empty);
        }

        [LocalizableCategory("ConfigCategoryBizTalk")]
        [LocalizableDescription("ConfigDescriptionBizTalkDbInstance")]
        [DefaultValue("")]
        public string BizTalkDbInstance { get; set; }

        [LocalizableCategory("ConfigCategoryBizTalk")]
        [LocalizableDescription("ConfigDescriptionMgmtDatabaseName")]
        [DefaultValue("BizTalkMgmtDb")]
        public string MgmtDatabaseName { get; set; }

        [LocalizableCategory("ConfigCategoryBizTalk")]
        [LocalizableDescription("ConfigDescriptionRulesServer")]
        [DefaultValue("")]
        public string RulesServer { get; set; }

        [LocalizableCategory("ConfigCategoryBizTalk")]
        [LocalizableDescription("ConfigDescriptionRulesDatabase")]
        [DefaultValue("BizTalkRuleEngineDb")]
        public string RulesDatabase { get; set; }


        public static BizTalkPlugInConfiguration FromXml(IBasePathProvider basePathProvider, XPathNavigator configuration)
        {
            return new BizTalkPlugInConfiguration(basePathProvider, configuration);
        }

        public static BizTalkPlugInConfiguration FromXml(IBasePathProvider basePathProvider, string configuration)
        {
            var stringReader = new StringReader(configuration);
            var document = new XPathDocument(stringReader);
            var navigator = document.CreateNavigator();
            return FromXml(basePathProvider, navigator);
        }

        private static bool GetBoolean(XPathNavigator navigator, string xpath, bool defaultValue)
        {
            var value = navigator.SelectSingleNode(xpath);
            return (value == null)
                    ? defaultValue
                    : value.ValueAsBoolean;
        }

        private static int GetInt32(XPathNavigator navigator, string xpath, int defaultValue)
        {
            var value = navigator.SelectSingleNode(xpath);
            return (value == null)
                    ? defaultValue
                    : value.ValueAsInt;
        }

        private static string GetString(XPathNavigator navigator, string xpath, string defaultValue)
        {
            var value = navigator.SelectSingleNode(xpath);
            return (value == null)
                    ? defaultValue
                    : value.Value;
        }

        private static FilePath GetFilePath(IBasePathProvider basePathProvider, XPathNavigator navigator, string xpath)
        {
            var path = navigator.SelectSingleNode(xpath);
            return path == null
                       ? new FilePath(string.Empty, basePathProvider)
                       : new FilePath(path.Value, basePathProvider);
        }

        public static string ToXml(BizTalkPlugInConfiguration configuration)
        {
            var doc = new XmlDocument();
            var configurationNode = doc.CreateElement("configuration");
            doc.AppendChild(configurationNode);

            var biztalk = doc.CreateElement("biztalk");
            biztalk.SetAttribute("dbInstance", configuration.BizTalkDbInstance);
            biztalk.SetAttribute("mgmtDatabaseName", configuration.MgmtDatabaseName);
            biztalk.SetAttribute("rulesServer", configuration.RulesServer);
            biztalk.SetAttribute("rulesDatabase", configuration.RulesDatabase);
            configurationNode.AppendChild(biztalk);

            var schemaFilesNode = doc.CreateElement("schemaFiles");
            configurationNode.AppendChild(schemaFilesNode);

            var dependencyFilesNode = doc.CreateElement("dependencyFiles");
            configurationNode.AppendChild(dependencyFilesNode);

            var docFilesNode = doc.CreateElement("docFiles");
            configurationNode.AppendChild(docFilesNode);

            return doc.OuterXml;
        }

        public BizTalkPlugInConfiguration Clone()
        {
            return new BizTalkPlugInConfiguration(this);
        }
    }
}