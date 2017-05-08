using System;

namespace inSyca.foundation.framework.configuration
{
    public class RegistryKeySourceAttribute : Attribute
    {
        internal string _name;

        public RegistryKeySourceAttribute(string name)
        {
            _name = name;
        }
    }
}
