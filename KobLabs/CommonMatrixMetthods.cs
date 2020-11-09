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

		public static void ConsoleWriteMatrix (int[,] matrix)
		{
			Console.WriteLine();

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					if (matrix[i, j] != 0)
					{
						Console.Write("{0,3}", matrix[i, j]);
					}
					else
					{
						Console.Write("{0,3}", "-");
					}
				}

				Console.WriteLine();
			}
		}

		public static void ConsoleWriteTasks (int[] tasks)
		{
			Console.Write("[ ");
			for (int i = 0; i < tasks.Length; i++)
			{
				Console.Write("{0}, ", tasks[i]);
			}
			Console.Write("]");
			Console.WriteLine();
		}

		public static void ConsoleWriteResults (int[] result)
		{
			for (int i = 0; i < result.Length; i++)
			{
				Console.Write("{0,3}", "___");
			}

			Console.WriteLine();

			for (int i = 0; i < result.Length; i++)
			{
				Console.Write("{0,3}", result[i]);
			}

			Console.WriteLine();
		}

		public static int[,] SortMatrixByCol (int[,] matrix)
		{
			SumMatrixElement[] summArray = new SumMatrixElement[matrix.GetLength(0)];

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				summArray[i] = new SumMatrixElement
				{
					Row = i,
					Sum = 0
				};

				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					summArray[i].Sum += matrix[i, j];
				}
			}

			summArray = summArray.OrderBy(c => c.Sum).ToArray();
			summArray.Reverse();

			int[,] resultMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];

			for (int i = 0; i < summArray.Length; i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					resultMatrix[i, j] = matrix[summArray[i].Row, j];
				}
			}

			return resultMatrix;
		} 
	}
}
