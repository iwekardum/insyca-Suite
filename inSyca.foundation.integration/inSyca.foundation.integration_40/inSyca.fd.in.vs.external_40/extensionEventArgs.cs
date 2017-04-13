using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inSyca.foundation.integration.visualstudio.external
{
    public class OutputWindowEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
