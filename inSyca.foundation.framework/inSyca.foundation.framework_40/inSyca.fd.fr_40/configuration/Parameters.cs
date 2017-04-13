using inSyca.foundation.framework.diagnostics;
using System;
using System.Xml;

namespace inSyca.foundation.framework.configuration
{
    public class Parameters
    {
        public Parameters()
        {
        }

        #region Properties
        public Parameter[] SimpleParameter;
        public Parameter[] RegExParameter;

        private static string nodeSimpleReplace_XPath = @"//SimpleReplaces/SimpleReplace";
        private static string nodeRegEx_XPath = @"//RegExs/RegEx";
        private static string nodeSearch_XPath = @"/Search";
        private static string nodeReplaceTo_XPath = @"/ReplaceTo";
        private static XmlDocument configXmlDocument = new XmlDocument();
        #endregion Properties

        #region Methods

        /// <summary>
        /// Load and initialize the config parameters
        /// </summary>
        /// <param name="configFileName"></param>
        public void Initialize(string configFileName)
        {
            XmlTextReader txtReader = null;
            try
            {
                txtReader = new XmlTextReader(configFileName);
                configXmlDocument.Load(txtReader);

                // SimpleReplaces:
                GetNodeLists(ref SimpleParameter, nodeSimpleReplace_XPath, false);
                // RegExs:
                GetNodeLists(ref RegExParameter, nodeRegEx_XPath, true);
            }
            finally
            {
                if (txtReader != null)
                    txtReader.Close();
            }
        }

        private static void GetNodeLists(ref Parameter[] namedParameter, string parentNodeXPath, bool isRegEx)
        {
            Log.DebugFormat("GetNodeLists(ref Parameter[] namedParameter {0}, string parentNodeXPath {1}, bool isRegEx {2})", namedParameter, parentNodeXPath, isRegEx);

            XmlNodeList nodeList_Search = configXmlDocument.SelectNodes(parentNodeXPath + nodeSearch_XPath);
            XmlNodeList nodeList_ReplaceTo = configXmlDocument.SelectNodes(parentNodeXPath + nodeReplaceTo_XPath);

            if (nodeList_Search.Count != nodeList_ReplaceTo.Count)
                throw new Exception("In record " + parentNodeXPath + "nodeList_Search.Count != nodeList_ReplaceTo.Count");

            namedParameter = new Parameter[nodeList_Search.Count];

            for (int i = 0; i < nodeList_Search.Count; i++)
            {
                namedParameter[i].Search = ConvertFromHexadecimal(nodeList_Search[i].InnerText);
                namedParameter[i].ReplaceTo = ConvertFromHexadecimal(nodeList_ReplaceTo[i].InnerText);

                namedParameter[i].Trace();
            }
        }

        private static string ConvertFromHexadecimal(string source)
        {
            Log.DebugFormat("ConvertFromHexadecimal(string source {0})", source);

            if (source == null) return source;

            byte retVal = 0;
            if (source.Length > 2)
                if (source[0] == '#'
                    || source[1] == 'x')
                {
                    try
                    {
                        retVal = byte.Parse(source.Substring(2, source.Length - 2), System.Globalization.NumberStyles.HexNumber);
                        source = System.Convert.ToChar(retVal).ToString();
                    }
                    catch (Exception ex)
                    {
                        diagnostics.Log.Warn(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), null, ex));
                        // do not convert! do not throw exception!
                    }

                }
            return source;
        }
        #endregion Methods
    }

    public struct Parameter
    {
        public string Search;
        public string ReplaceTo;
        public void Trace()
        {
            Log.DebugFormat("Trace() - Search=[{0}] ReplaceTo=[{1}]", (this.Search != null ? Search : "null"), (this.ReplaceTo != null ? ReplaceTo : "null") );
        }
    }
}
