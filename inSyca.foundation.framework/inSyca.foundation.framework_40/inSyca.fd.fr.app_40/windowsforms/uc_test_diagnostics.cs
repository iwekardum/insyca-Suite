using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inSyca.foundation.framework.application.windowsforms
{
    public partial class uc_test_diagnostics : UserControl
    {
        internal enum DiagnosticLevel { Unknown, Test_Maillog_Information, Test_Maillog_Warning, Test_Maillog_Error, Test_Eventlog_Information, Test_Eventlog_Warning, Test_Eventlog_Error };

        public uc_test_diagnostics()
        {
            InitializeComponent();
        }

        internal DiagnosticLevel Level()
        {
            if (rb_maillog.Checked)
            {
                if (rb_information.Checked)
                    return DiagnosticLevel.Test_Maillog_Information;
                else if (rb_warning.Checked)
                    return DiagnosticLevel.Test_Maillog_Warning;
                else if (rb_error.Checked)
                    return DiagnosticLevel.Test_Maillog_Error;
            }
            else if (rb_eventlog.Checked)
            {
                if (rb_information.Checked)
                    return DiagnosticLevel.Test_Eventlog_Information;
                else if (rb_warning.Checked)
                    return DiagnosticLevel.Test_Eventlog_Warning;
                else if (rb_error.Checked)
                    return DiagnosticLevel.Test_Eventlog_Error;
            }

            return DiagnosticLevel.Unknown;
        }
    }
}
