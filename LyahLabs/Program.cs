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
			string result = CodeMethods.Lab1Code("5 4 3 6", "TYI YFDN JGGCD");
			string result2 = CodeMethods.Lab1Decode("5 4 3 6", result);
		}
	}
}
