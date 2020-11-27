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
			//Stopwatch startTime;
			//TimeSpan resultTime;

			Matrix A = new Matrix(10, 12);
			Matrix B = new Matrix(12, 10);
			Matrix C = new Matrix(12, 10);

			Random random = new Random();

			A.Fill(random);
			B.Fill(random);
			C.Fill(random);

			Console.WriteLine("A:");
			Console.WriteLine();
			A.WriteToConsole();

			Console.WriteLine("B:");
			Console.WriteLine();
			B.WriteToConsole();

			Console.WriteLine("C:");
			Console.WriteLine();
			C.WriteToConsole();

			#region Сложение

			Matrix Summ = C + B;
			Matrix SummParallel = Matrix.AddParallel(C, B);
			Matrix SummAsync = Matrix.AddAsync(C, B).Result;

			#endregion

			#region Умножение
			Matrix Mult = A * B;
			Matrix MultParallel = Matrix.MultipleParallel(A, B);
			Matrix MultAsync = Matrix.MultipleAsync(A, B).Result;

			#endregion

			Console.ReadLine();
		}
	}
}
