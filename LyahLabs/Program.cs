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
			List<long> randoms = new List<long>();
			List<long> randoms2 = new List<long>();
			FuckingRandom fuckingRandom = new FuckingRandom(24);
			for (int i = 0; i < 6; i++)
			{
				randoms.Add(fuckingRandom.Next());
			}
			randoms.Add(fuckingRandom.Next(true));
			for (int i = 0; i < 6; i++)
			{
				randoms.Add(fuckingRandom.Next());
			}

			Ghistogramm ghistogramm = new Ghistogramm(24);
			var g = ghistogramm.GetGhist(randoms);

			List<string> key = new List<string>();

			foreach (var r in randoms)
			{
				key.Add(r.ToString());
			}

			string code = CodeMethods.Lab2Code(key, "Съешь же ещё этих мягких французских булок да выпей чаю");
			string decode = CodeMethods.Lab2Code(key, code);
		}
	}
}
