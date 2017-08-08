using System;

namespace inSyca.foundation.framework.configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class RegistryKeySourceAttribute : Attribute
    {
        internal string _name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public RegistryKeySourceAttribute(string name)
        {
            _name = name;
        }
    }
}
