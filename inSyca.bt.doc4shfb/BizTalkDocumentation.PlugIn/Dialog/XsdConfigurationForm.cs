using System;

namespace BizTalkDocumentation.PlugIn
{
    internal sealed partial class BizTalkPlugInConfigurationForm : HelpAwareForm
    {
        public BizTalkPlugInConfigurationForm(BizTalkPlugInConfiguration configuration)
        {
            InitializeComponent();

            NewConfiguration = configuration.Clone();

            _propertyGrid.SelectedObject = NewConfiguration;
        }

        public BizTalkPlugInConfiguration NewConfiguration { get; private set; }
    }
}