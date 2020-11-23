using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrLabs
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch startTime = Stopwatch.StartNew();
			startTime.Stop();
			TimeSpan resultTime = startTime.Elapsed;

			Matrix A = new Matrix(3, 2);
			Matrix B = new Matrix(2, 2);
			Matrix E = new Matrix(2, 2);


			Console.ReadLine();
		}
	}
}
