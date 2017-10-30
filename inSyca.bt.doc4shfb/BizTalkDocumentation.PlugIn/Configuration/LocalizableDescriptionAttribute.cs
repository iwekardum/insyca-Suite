using System;
using System.ComponentModel;

using BizTalkDocumentation.PlugIn.Properties;

namespace BizTalkDocumentation.PlugIn
{
    public sealed class LocalizableDescriptionAttribute : DescriptionAttribute
    {
        public LocalizableDescriptionAttribute(string description)
            : base(description)
        {
        }

        public override string Description
        {
            get { return Resources.ResourceManager.GetString(DescriptionValue); }
        }
    }
}