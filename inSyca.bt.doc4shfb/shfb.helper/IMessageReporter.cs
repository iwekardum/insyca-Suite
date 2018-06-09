using System;

namespace shfb.helper
{
    public interface IMessageReporter
    {
        void ReportWarning(string warningCode, string message);
    }
}