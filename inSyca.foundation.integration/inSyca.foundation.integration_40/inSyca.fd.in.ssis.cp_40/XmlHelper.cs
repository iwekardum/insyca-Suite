using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using inSyca.foundation.integration.ssis.components.diagnostics;
using inSyca.foundation.framework;

namespace inSyca.foundation.integration.ssis.components
{
    static internal class XmlHelper
    {
        // Remove forbidden chars that could damage your XML document
        static internal string removeForbiddenXmlChars(object columnValue)
        {
            Log.DebugFormat("removeForbiddenXmlChars(object columnValue {0})", columnValue);

            try
            {
                switch (columnValue.GetType().FullName)
                {
                    case "System.Decimal":
                        return ((System.Decimal)columnValue).ToString(CultureInfo.InvariantCulture);
                    case "System.Double":
                        return ((System.Double)columnValue).ToString(CultureInfo.InvariantCulture);
                    case "System.Single":
                        return ((System.Single)columnValue).ToString(CultureInfo.InvariantCulture);
                    default:
                        try
                        {
                            decimal dValue = decimal.Parse(columnValue.ToString(), CultureInfo.CreateSpecificCulture("de-DE"));

                            return dValue.ToString(CultureInfo.InvariantCulture);
                        }
                        catch(Exception ex)
                        {
                            Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { columnValue }, ex));

                            return columnValue.ToString().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
                        }
                }
            }
            catch (Exception ex)
            {
                Log.Error(new LogEntry(System.Reflection.MethodBase.GetCurrentMethod(), new object[] { columnValue }, ex));

                return "#ERROR#";
            }

        }

    }
}
