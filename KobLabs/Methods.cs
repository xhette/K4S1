using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using KobLabs.Classes;

namespace KobLabs
{
	public static class Methods
	{
		public static L1Result CriticalMethodPath(int m, int n, int min, int max)
		{
			int[] tasks = new int[m];
			Random random = new Random();

			for (int j = 0; j < tasks.Length; j++)
			{
				tasks[j] = random.Next(min, max);
			}

			int[,] matrices = new int[1, n];
			int[] results = new int[n];

			for (int i = 0; i < tasks.Length; i++)
			{
				// находим минимальную нагрузку
				int resultMin = results.Min();
				int minIndex = Array.IndexOf(results, resultMin);

				// находим последнюю строку в расписании
				int matrixRow = matrices.GetLength(0) - 1;

				// если ячейка занята, добавляем новую строку в матрицу
				if (matrices[matrixRow, minIndex] != 0)
				{
					int newRowsCount = matrices.GetLength(0) + 1;
					matrixRow++;

					matrices = (int[,])CommonMatrixMetthods.ResizeMatrix(matrices, newRowsCount, matrices.GetLength(1));
				}

				// записываем элемент из списка заданий в ячейку расписаний
				matrices[matrixRow, minIndex] = tasks[i];
				int newResult = resultMin + tasks[i];

				// изменяем нагрузку
				results[minIndex] = newResult;
			}

			L1Result result = new L1Result()
			{
				Tasks = tasks,
				Schedule = matrices,
				Results = results,
				TasksAsc = new int[m],
				TasksDesc = new int[m],
				ScheduleAsc = new int[1,n],
				ScheduleDesc = new int[1, n],
				ResultsAsc = new int[n],
				ResultsDesc = new int[n],
			};

			result.ResultStatistics = new int[result.Results.Length];
			result.IndexesStatistics = new int[result.Results.Length];

			for (int i = 0; i < result.Results.Length; i++)
			{
				result.ResultStatistics[i] = result.Results[i];
				result.IndexesStatistics[i] = i;
			}
			for (int i = 0; i < result.ResultStatistics.Length - 1; i++)
			{
				for (int j = 0; j < result.ResultStatistics.Length - 1; j++)
				{
					if (result.ResultStatistics[j] > result.ResultStatistics[j + 1])
					{
						int tmp = result.ResultStatistics[j];
						int tmpIndex = result.IndexesStatistics[j];

						result.ResultStatistics[j] = result.ResultStatistics[j + 1];
						result.ResultStatistics[j + 1] = tmp;

						result.IndexesStatistics[j] = result.IndexesStatistics[j + 1];
						result.IndexesStatistics[j + 1] = tmpIndex;
					}
				}
			}

			tasks.CopyTo(result.TasksAsc, 0);
			tasks.CopyTo(result.TasksDesc, 0);
			Array.Sort(result.TasksAsc);
			Array.Sort(result.TasksDesc);
			Array.Reverse(result.TasksDesc);

			for (int i = 0; i < result.TasksAsc.Length; i++)
			{
				// находим минимальную нагрузку
				int resultMin = result.ResultsAsc.Min();
				int minIndex = Array.IndexOf(result.ResultsAsc, resultMin);

				// находим последнюю строку в расписании
				int matrixRow = result.ScheduleAsc.GetLength(0) - 1;

				// если ячейка занята, добавляем новую строку в матрицу
				if (result.ScheduleAsc[matrixRow, minIndex] != 0)
				{
					int newRowsCount = result.ScheduleAsc.GetLength(0) + 1;
					matrixRow++;

					result.ScheduleAsc = (int[,])CommonMatrixMetthods.ResizeMatrix(result.ScheduleAsc, newRowsCount, result.ScheduleAsc.GetLength(1));
				}

				// записываем элемент из списка заданий в ячейку расписаний
				result.ScheduleAsc[matrixRow, minIndex] = result.TasksAsc[i];
				int newResult = resultMin + result.TasksAsc[i];

				// изменяем нагрузку
				result.ResultsAsc[minIndex] = newResult;
			}

			result.ResultStatisticsAsc = new int[result.ResultsAsc.Length];
			result.IndexesStatisticsAsc = new int[result.ResultsAsc.Length];

			for (int i = 0; i < result.ResultsAsc.Length; i++)
			{
				result.ResultStatisticsAsc[i] = result.ResultsAsc[i];
				result.IndexesStatisticsAsc[i] = i;
			}
			for (int i = 0; i < result.ResultStatisticsAsc.Length - 1; i++)
			{
				for (int j = 0; j < result.ResultStatisticsAsc.Length - 1; j++)
				{
					if (result.ResultStatisticsAsc[j] > result.ResultStatisticsAsc[j + 1])
					{
						int tmp = result.ResultStatisticsAsc[j];
						int tmpIndex = result.IndexesStatisticsAsc[j];

						result.ResultStatisticsAsc[j] = result.ResultStatisticsAsc[j + 1];
						result.ResultStatisticsAsc[j + 1] = tmp;

						result.IndexesStatisticsAsc[j] = result.IndexesStatisticsAsc[j + 1];
						result.IndexesStatisticsAsc[j + 1] = tmpIndex;
					}
				}
			}


			for (int i = 0; i < result.TasksDesc.Length; i++)
			{
				// находим минимальную нагрузку
				int resultMin = result.ResultsDesc.Min();
				int minIndex = Array.IndexOf(result.ResultsDesc, resultMin);

				// находим последнюю строку в расписании
				int matrixRow = result.ScheduleDesc.GetLength(0) - 1;

				// если ячейка занята, добавляем новую строку в матрицу
				if (result.ScheduleDesc[matrixRow, minIndex] != 0)
				{
					int newRowsCount = result.ScheduleDesc.GetLength(0) + 1;
					matrixRow++;

					result.ScheduleDesc = (int[,])CommonMatrixMetthods.ResizeMatrix(result.ScheduleDesc, newRowsCount, result.ScheduleDesc.GetLength(1));
				}

				// записываем элемент из списка заданий в ячейку расписаний
				result.ScheduleDesc[matrixRow, minIndex] = result.TasksDesc[i];
				int newResult = resultMin + result.TasksDesc[i];

				// изменяем нагрузку
				result.ResultsDesc[minIndex] = newResult;
			}

			result.ResultStatisticsDesc = new int[result.ResultsDesc.Length];
			result.IndexesStatisticsDesc = new int[result.ResultsDesc.Length];

			for (int i = 0; i < result.ResultsDesc.Length; i++)
			{
				result.ResultStatisticsDesc[i] = result.ResultsDesc[i];
				result.IndexesStatisticsDesc[i] = i;
			}
			for (int i = 0; i < result.ResultStatisticsDesc.Length - 1; i++)
			{
				for (int j = 0; j < result.ResultStatisticsDesc.Length - 1; j++)
				{
					if (result.ResultStatisticsDesc[j] > result.ResultStatisticsDesc[j + 1])
					{
						int tmp = result.ResultStatisticsDesc[j];
						int tmpIndex = result.IndexesStatisticsDesc[j];

						result.ResultStatisticsDesc[j] = result.ResultStatisticsDesc[j + 1];
						result.ResultStatisticsDesc[j + 1] = tmp;

						result.IndexesStatisticsDesc[j] = result.IndexesStatisticsDesc[j + 1];
						result.IndexesStatisticsDesc[j + 1] = tmpIndex;
					}
				}
			}

			result.Methods = new List<MethodMax>();

			result.Methods.Add(new MethodMax
			{
				MethodName = Enums.MethodEnum.RandomMethod,
				MaxValue = result.Results.Max()
			});
			result.Methods.Add(new MethodMax
			{
				MethodName = Enums.MethodEnum.Ascending,
				MaxValue = result.ResultsAsc.Max()
			});
			result.Methods.Add(new MethodMax
			{
				MethodName = Enums.MethodEnum.Descending,
				MaxValue = result.ResultsDesc.Max()
			});

			result.Methods = result.Methods.OrderBy(c => c.MaxValue).ToList();

			return result;
		}

		public static StatisticElement Zverev (int[,] matrix)
		{
			StatisticElement element = new StatisticElement();
			element.Matrix = matrix;
			element.MethodName = "Алгоритм Плотникова-Зверева";

			int[,] sortedMatrix = CommonMatrixMetthods.SortMatrixByCol(matrix);

			int[,] resultMatrix = new int[sortedMatrix.GetLength(0) + 1, sortedMatrix.GetLength(1)];
			int[] subStep = new int[sortedMatrix.GetLength(1)];

			for (int i = 0; i < sortedMatrix.GetLength(1); i++)
			{
				resultMatrix[0, i] = 0;
			}

			int step = 0;

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					subStep[j] = resultMatrix[step, j] + matrix[i, j];
				}

				int min = subStep[0], minIndex = 0;

				for (int j = 0; j < subStep.Length; j++)
				{
					if (min > subStep[j])
					{
						min = subStep[j];
						minIndex = j;
					}
				}

				step++;

				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					if (j != minIndex)
					{
						resultMatrix[step, j] = resultMatrix[step - 1, j];
					}
					else
					{
						resultMatrix[step, minIndex] = subStep[minIndex];
					}
				}
			}

			element.Result = new ScheduleResult();
			element.Result.Schedule = resultMatrix;
			element.Result.Loads = new int[matrix.GetLength(1)];

			for (int j = 0; j < resultMatrix.GetLength(1); j++)
			{
				element.Result.Loads[j] = resultMatrix[resultMatrix.GetLength(0) - 1, j];
			}

			return element;
		}

		public static StatisticElement MinElements(int[,] matrix)
		{
			StatisticElement element = new StatisticElement();
			element.Matrix = matrix;
			element.MethodName = "Минимальных элементов";

			int[,] resultMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
			int[] subStep = new int[matrix.GetLength(1)];

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					subStep[j] = matrix[i, j];
				}

				int min = subStep[0], minIndex = 0;

				for (int j = 0; j < subStep.Length; j++)
				{
					if (min > subStep[j])
					{
						min = subStep[j];
						minIndex = j;
					}
				}


				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					if (j != minIndex)
					{
						resultMatrix[i, j] = 0;
					}
					else
					{
						resultMatrix[i, minIndex] = subStep[minIndex];
					}
				}
			}

			element.Result = new ScheduleResult();
			element.Result.Schedule = resultMatrix;
			element.Result.Loads = new int[matrix.GetLength(1)];

			for (int i = 0; i < resultMatrix.GetLength(0); i++)
			{
				for (int j = 0; j < resultMatrix.GetLength(1); j++)
				{
					element.Result.Loads[j] += resultMatrix[i, j];
				}
			}

			return element;
		}

		public static StatisticElement Barier (int[,] matrix)
		{
			StatisticElement element = new StatisticElement();
			element.Matrix = matrix;
			element.MethodName = "Поиск с барьером";

			int[,] firstStep = MinElements(matrix).Result.Schedule;
			int barier = 0;

			int[,] resultMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
			int[] subStep = new int[matrix.GetLength(1)];

			for (int i = 0; i < firstStep.GetLength(0); i++)
			{
				for (int j = 0; j < firstStep.GetLength(1); j++)
				{
					barier += firstStep[i, j];
				}
			}

			barier = barier / matrix.GetLength(1);

			bool isBarier = false;

			for (int i = 0; i < firstStep.GetLength(0); i++)
			{
				if (!isBarier)
				{
					for (int j = 0; j < matrix.GetLength(1); j++)
					{
						subStep[j] = matrix[i, j];
					}

					int min = subStep[0], minIndex = 0;

					for (int j = 0; j < subStep.Length; j++)
					{
						if (min > subStep[j])
						{
							min = subStep[j];
							minIndex = j;
						}
					}


					for (int j = 0; j < matrix.GetLength(1); j++)
					{
						if (j != minIndex)
						{
							if (i > 0)
							{
								resultMatrix[i, j] = resultMatrix[i - 1, j];
							}
					
						}
						else
						{

							if (i > 0)
							{
								resultMatrix[i, minIndex] = resultMatrix[i - 1, j] + subStep[minIndex];
							}
							else
							{
								resultMatrix[i, minIndex] = subStep[minIndex];
							}
						}
					}

					for (int j = 0; j < matrix.GetLength(1); j++)
					{
						subStep[j] = matrix[i + 1, j];
					}

					min = subStep[0]; minIndex = 0;

					for (int j = 0; j < subStep.Length; j++)
					{
						if (min > subStep[j])
						{
							min = subStep[j];
							minIndex = j;
						}
					}

					int nextStep = min + resultMatrix[i, minIndex];

					if (nextStep >= barier)
					{
						isBarier = true;
					}
				}
				else
				{
					for (int j = 0; j < matrix.GetLength(1); j++)
					{
						subStep[j] = resultMatrix[i - 1, j] + matrix[i, j];
					}

					int min = subStep[0], minIndex = 0;

					for (int j = 0; j < subStep.Length; j++)
					{
						if (min > subStep[j])
						{
							min = subStep[j];
							minIndex = j;
						}
					}

					for (int j = 0; j < matrix.GetLength(1); j++)
					{
						if (j != minIndex)
						{
							resultMatrix[i, j] = resultMatrix[i - 1, j];
						}
						else
						{
							resultMatrix[i, minIndex] = subStep[minIndex];
						}
					}
				}

			}

			element.Result = new ScheduleResult();
			element.Result.Schedule = resultMatrix;
			element.Result.Loads = new int[matrix.GetLength(1)];

			for (int j = 0; j < resultMatrix.GetLength(1); j++)
			{
				element.Result.Loads[j] = resultMatrix[resultMatrix.GetLength(0) - 1, j];
			}

			return element;
		}

		public static int[,] RandomSchedule (int[] array, int n)
		{
			int[,] result = new int[1, n];

			Random random = new Random();

			for (int i = 0; i < array.Length; i++)
			{

				// находим последнюю строку в расписании
				int matrixRow = result.GetLength(0) - 1;
				int minIndex = random.Next(0, n - 1);

				// если ячейка занята, добавляем новую строку в матрицу
				if (result[matrixRow, minIndex] != 0)
				{
					int newRowsCount = result.GetLength(0) + 1;
					matrixRow++;

					result= (int[,])CommonMatrixMetthods.ResizeMatrix(result, newRowsCount, result.GetLength(1));
				}

				// записываем элемент из списка заданий в ячейку расписаний
				result[matrixRow, minIndex] = array[i];
			}

			return result;
		}

		public static L3Result Kron (int m, int n, int min, int max)
		{
			L3Result result = new L3Result();

			L1Result l1 = CriticalMethodPath(m, n, min, max);
			result.Tasks = l1.Tasks;
			result.StartCritical = l1.Schedule;
			result.StartRandom = RandomSchedule(result.Tasks, n);

			result.RandomLoad = new int[n];
			result.CriticalLoad = new int[n];

			for (int i = 0; i < result.StartRandom.GetLength(0); i++)
			{
				for (int j = 0; j < result.StartRandom.GetLength(1); j++)
				{
					result.RandomLoad[j] += result.StartRandom[i, j];
				}
			}

			for (int i = 0; i < result.StartCritical.GetLength(0); i++)
			{
				for (int j = 0; j < result.StartCritical.GetLength(1); j++)
				{
					result.CriticalLoad[j] += result.StartCritical[i, j];
				}
			}

			Thread.Sleep(1000);

			result.CriticalSchedule = KronAlgorytm(result.StartCritical, result.CriticalLoad);
			result.RandomSchedule = KronAlgorytm(result.StartRandom, result.RandomLoad);

			for (int i = 0; i < result.RandomLoad.Length; i++)
			{
				result.RandomLoad[i] = 0;
				result.CriticalLoad[i] = 0;
			}

			for (int i = 0; i < result.RandomSchedule.GetLength(0); i++)
			{
				for (int j = 0; j < result.RandomSchedule.GetLength(1); j++)
				{
					result.RandomLoad[j] += result.RandomSchedule[i, j];
				}
			}

			for (int i = 0; i < result.CriticalSchedule.GetLength(0); i++)
			{
				for (int j = 0; j < result.CriticalSchedule.GetLength(1); j++)
				{
					result.CriticalLoad[j] += result.CriticalSchedule[i, j];
				}
			}

			result.MethodStatistics = new List<MethodStatistic>();
			result.MethodStatistics.Add(new MethodStatistic
			{
				MethodName = "Критический путь",
				MaxLoad = result.CriticalLoad.Max()
			});
			result.MethodStatistics.Add(new MethodStatistic
			{
				MethodName = "Случайный",
				MaxLoad = result.RandomLoad.Max()
			});

			result.MethodStatistics = result.MethodStatistics.OrderBy(c => c.MaxLoad).ToList();

			return result;
		}

		private static int[,] KronAlgorytm (int[,] matrix, int[] array)
		{
			while (true)
			{

				int delta = array.Max() - array.Min();
				int tMax = Array.IndexOf(array, array.Max());
				int tMin = Array.IndexOf(array, array.Min());
				int tMaxElement = 0;

				for (int i = 0; i < matrix.GetLength(0); i++)
				{
					if (matrix[i, tMax] < delta && matrix[i, tMax] != 0)
					{
						tMaxElement = matrix[i, tMax];
						matrix[i, tMax] = 0;
						break;
					}
				}

				if (tMaxElement == 0)
				{
					break;
				}

				int index = -1;

				for (int i = 0; i < matrix.GetLength(0); i++)
				{
					if (matrix[i, tMin] == 0)
					{
						index = i;
						break;
					}
				}

				if (index == -1)
				{
					int newRowsCount = matrix.GetLength(0) + 1;
					index = matrix.GetLength(0);
					matrix = (int[,])CommonMatrixMetthods.ResizeMatrix(matrix, newRowsCount, matrix.GetLength(1));
				}

				matrix[index, tMin] = tMaxElement;

				for (int i = 0; i < array.Length; i++)
				{
					array[i] = 0;
				}

				for (int i = 0; i < matrix.GetLength(0); i++)
				{
					for (int j = 0; j < matrix.GetLength(1); j++)
					{
						array[j] += matrix[i, j];
					}
				}
			}

				

			for (int i = 0; i < array.Length; i++)
			{
				int delta = array.Max() - array.Min();
				int tMax = Array.IndexOf(array, array.Max());
				int tMin = Array.IndexOf(array, array.Min());

				for (int j = 0; j < array.Length; j++)
				{
					if (matrix[i, tMax] - matrix[i, tMin] < delta && matrix[i, tMax] - matrix[i, tMin] > 0 && matrix[i, tMax] != 0 && matrix[i, tMin] != 0)
					{
						if (i < matrix.GetLength(0))
						{
							int temp = matrix[i, tMax];
							matrix[i, tMax] = matrix[i, tMin];
							matrix[i, tMin] = temp;

							break;
						}
						else
						{
							matrix = (int[,])CommonMatrixMetthods.ResizeMatrix(matrix, i+1, matrix.GetLength(1));

							int temp = matrix[i, tMax];
							matrix[i, tMax] = matrix[i, tMin];
							matrix[i, tMin] = temp;
						}
					}
				}
			}

			return matrix;
		}

		public static int Barier1(int[,] matrix)
		{
			StatisticElement element = new StatisticElement();
			element.Matrix = matrix;
			element.MethodName = "Поиск с барьером по среднему элементов";

			double[] barier = new double[matrix.GetLength(0)];

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					barier[i] += matrix[i, j];
				}
			}

			for (int i = 0; i < barier.Length; i++)
			{
				barier[i] /= (double)matrix.GetLength(1);
			}

			double b = barier.Sum() / barier.Length;
			b = Math.Ceiling(b);

			return (int)b;
		}

		public static int Barier2(int[,] matrix)
		{
			StatisticElement element = new StatisticElement();
			element.Matrix = matrix;
			element.MethodName = "Поиск с барьером по минимальным элементам";

			int[] barier = new int[matrix.GetLength(0)];

			for (int i = 0; i < barier.Length; i++)
			{
				barier[i] = matrix[i, 0];
			}

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					if (barier[i] > matrix[i, j])
					{
						barier[i] = matrix[i, j];
					}
				}
			}

			double b = (double)barier.Sum() / (double)barier.Length;
			b = Math.Ceiling(b);

			return (int)b;
		}

		public static List<int[]> BarierMethod(int[,] matrix, int barier)
		{
			int[,] list = new int[1, matrix.GetLength(1)];

			int rowStoped = -1;

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				int matrixRow = list.GetLength(0) - 1;

				int[] loads = new int[matrix.GetLength(1)];
				int min = matrix[i, 0];
				int minInd = 0;

				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					if (min > matrix[i, j])
					{
						min = matrix[i, j];
						minInd = j;
					}
				}

				if (min + loads[minInd] >= barier)
				{
					rowStoped = i;
					break;
				}
				else
				{
					loads[minInd] += min;
					int newRowsCount = list.GetLength(0) + 1;
					matrixRow++;

					list = (int[,])CommonMatrixMetthods.ResizeMatrix(list, newRowsCount, loads.GetLength(1));


				}
			}

			if (rowStoped != -1 && rowStoped < matrix.GetLength(0))
			{
				int[] loads = new int[matrix.GetLength(1)];
				for (int i = rowStoped; i < matrix.GetLength(0); i++)
				{
					int[] subStep = new int[matrix.GetLength(1)];

					for (int j = 0; j < matrix.GetLength(1); j++)
					{
						subStep[j] = loads[j] + matrix[i, j];
					}

					int min = subStep[0];
					int minInd = 0;

					for (int j = 0; j < subStep.Length; j++)
					{
						if (min > subStep[j])
						{
							min = subStep[j];
							minInd = j;
						}
					}

					loads[minInd] += min;
					list.Add(loads);
				}
			}

			int[,] result = new int[list.Count, matrix.GetLength(1)];

			int index = 0;

			foreach(var e in list)
			{
				for (int i = 0; i < e.Length; i++)
				{
					result[index, i] = e[i];
				}

				index++;
			}

			return list;
		}
	}
}
