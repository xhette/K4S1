using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using KobLabs.Classes;
using KobLabs.Enums;

namespace KobLabs
{
	class Program
	{
		static void Main(string[] args)
		{

			int m; int q; int n; int min; int max;

			Console.WriteLine("Число задач: ");
			m = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Число процессоров: ");
			n = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Число массивов: ");
			q = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Минимальная граница: ");
			min = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Максимальная граница: ");
			max = Convert.ToInt32(Console.ReadLine());

			int[,] matrix = new int[10, 3];
            Random rnd = new Random();

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					matrix[i, j] = rnd.Next(15, 25);
				}
			}

			Methods.Barier(matrix);

            Console.ReadKey();
        }
		
	}

}
