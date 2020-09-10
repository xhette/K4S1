using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KobLabs.Classes;

namespace KobLabs
{
	public static class CommonMatrixMetthods
	{
		public static int[] FillArrayRandom(int min, int max, int m)
		{
			int[] array = new int[m];
			Random random = new Random();

			for (int i = 0; i < array.Length; i++)
			{
					array[i] = random.Next(min, max);
			}

			return array;
		}

		public static Array ResizeMatrix(Array arr, int n, int m)
		{
			var temp = Array.CreateInstance(arr.GetType().GetElementType(), n, m);
			int length = arr.Length <= temp.Length ? arr.Length : temp.Length;
			Array.ConstrainedCopy(arr, 0, temp, 0, length);

			return temp;
		}

		public static void ConsoleWriteMatrix (Matrix matrix)
		{
			Console.WriteLine();

			for (int i = 0; i < matrix.Elements.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.Elements.GetLength(1); j++)
				{
					if (matrix.Elements[i, j] != 0)
					{
						Console.Write("{0,3}", matrix.Elements[i, j]);
					}
					else
					{
						Console.Write("{0,3}", "-");
					}
				}

				Console.WriteLine();
			}
		}

		public static void ConsoleWriteTasks (TasksArray tasks)
		{
			Console.Write("[ ");
			for (int i = 0; i < tasks.Elements.Length; i++)
			{
				Console.Write("{0}, ", tasks.Elements[i]);
			}
			Console.Write("]");
			Console.WriteLine();
		}

		public static void ConsoleWriteResults (TasksArray result)
		{
			for (int i = 0; i < result.Elements.Length; i++)
			{
				Console.Write("{0,3}", "___");
			}

			Console.WriteLine();

			for (int i = 0; i < result.Elements.Length; i++)
			{
				Console.Write("{0,3}", result.Elements[i]);
			}

			Console.WriteLine();
		}
	}
}
