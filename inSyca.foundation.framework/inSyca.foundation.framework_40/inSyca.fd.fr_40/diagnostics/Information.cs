using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace inSyca.foundation.framework.diagnostics
{
    public class Information
    {
        Type _informationClassType;

        public Information()
        {
            _informationClassType = typeof(Information);
        }

        public Information(Type informationClassType)
        {
            _informationClassType = informationClassType;
        }

        public string Version
        {
            get
            {
                return System.Reflection.Assembly.GetAssembly(_informationClassType).GetName().Version.ToString();
            }
        }

        public string Location
        {
            get
            {
                return System.Reflection.Assembly.GetAssembly(_informationClassType).Location.ToString();
            }
        }
    }
}
