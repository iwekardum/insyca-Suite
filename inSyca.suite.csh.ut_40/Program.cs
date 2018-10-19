using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inSyca.suite.csh.ut_40
{
	public class Program
	{
		static void Main(string[] args)
		{
			inSyca.foundation.unittest_40.ConsoleServiceHost csh = new inSyca.foundation.unittest_40.ConsoleServiceHost();
			csh.StartServiceHost();
		}
	}
}
