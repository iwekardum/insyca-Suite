using inSyca.foundation.framework.configuration;
using System.IO;
using System;

namespace inSyca.foundation.framework.diagnostics
{
    [LogSource(typeof(Configuration))]
    internal class Log : LogBase<Log>
    {
    }
}
