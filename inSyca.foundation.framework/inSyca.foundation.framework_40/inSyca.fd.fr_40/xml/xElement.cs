using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace inSyca.foundation.framework.xml
{
    public static class xElement
    {
        public static void RemoveEmptyElements(XElement xElement)
        {
            IEnumerable<XElement> empties;
            do
            {
                empties = from n in xElement.Descendants()
                          where n.IsEmpty || string.IsNullOrWhiteSpace(n.Value)
                          select n;

                empties.ToList().ForEach(x => x.Remove());
            }
            while (empties.Count() > 0);
        }
    }
}
