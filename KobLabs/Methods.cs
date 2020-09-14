using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
	}
}
