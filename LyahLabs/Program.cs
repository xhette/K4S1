using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyahLabs
{
	class Program
	{
		static void Main(string[] args)
		{
			DESCycle dES = new DESCycle("Покемоны в собственном соку для чайников", "agafadsfscdf");

			string code = dES.Code();
			string decode = dES.Decode();
		}
	}
}
