using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GabrLabs
{
	public class Matrix
	{
		public float[,] _matrix
		{
			get; set;
		}

		static Stopwatch startTime;
		static TimeSpan resultTime;

		#region Костыли
		private class Point
		{
			public int Row { get; set; }

			public int Col { get; set; }

			public Random Random { get; set; }
		}

		private class PointWithVals
		{
			public int Row { get; set; }

			public int Col { get; set; }

			public float Val1 { get; set; }

			public float Val2 { get; set; }

			public Matrix Matrix { get; set; }
		}
		#endregion

		public int Rows => _matrix.GetLength(0);

		public int Cols => _matrix.GetLength(1);


		public Matrix(int n, int m)
		{
			_matrix = new float[n, m];
		}

		#region Последовательное
		public void Fill(Random random)
		{
			for (int i = 0; i < _matrix.GetLength(0); i++)
			{
				for (int j = 0; j < _matrix.GetLength(1); j++)
				{
					_matrix[i, j] = (float)random.Next(100, 1000) / 10f;
				}
			}
		}

		public static Matrix operator + (Matrix m1, Matrix m2)
		{
			if ((m1._matrix.GetLength(0) != m2._matrix.GetLength(0)) || (m1._matrix.GetLength(1) != m2._matrix.GetLength(1)))
			{
				throw new Exception("Размерность матриц не совпадает, операция невозможна");
			}
			else
			{
				Matrix result = new Matrix(m1._matrix.GetLength(0), m2._matrix.GetLength(1));

				startTime = Stopwatch.StartNew();
				startTime.Stop();

				for (int i = 0; i < m1._matrix.GetLength(0); i++)
				{
					for (int j = 0; j < m1._matrix.GetLength(1); j++)
					{
						result._matrix[i, j] = m1._matrix[i, j] + m2._matrix[i, j];
					}
				}
				resultTime = startTime.Elapsed;

				Console.WriteLine();
				Console.WriteLine("Последовательное сложение");
				resultTime = startTime.Elapsed;
				result.WriteToConsole();
				Console.WriteLine("Время выполнения: {0}", resultTime);

				return result;
			}
		}

		public static Matrix operator - (Matrix m1, Matrix m2)
		{
			if ((m1._matrix.GetLength(0) != m2._matrix.GetLength(0)) || (m1._matrix.GetLength(1) != m2._matrix.GetLength(1)))
			{
				throw new Exception("Размерность матриц не совпадает, операция невозможна");
			}
			else
			{
				Matrix result = new Matrix(m1._matrix.GetLength(0), m2._matrix.GetLength(1));

				for (int i = 0; i < m1._matrix.GetLength(0); i++)
				{
					for (int j = 0; j < m1._matrix.GetLength(1); j++)
					{
						result._matrix[i, j] = m1._matrix[i, j] - m2._matrix[i, j];
					}
				}

				return result;
			}
		}

		public static Matrix operator * (Matrix m1, Matrix m2)
		{
			if (m1._matrix.GetLength(1) != m2._matrix.GetLength(0))
			{
				throw new Exception("Неверная размерность матриц, операция невозможна");
			}
			else
			{
				Matrix result = new Matrix(m1._matrix.GetLength(0), m2._matrix.GetLength(1));

				startTime = Stopwatch.StartNew();
				startTime.Stop();

				for (int i = 0; i < result._matrix.GetLength(0); i++)
				{
					for (int j = 0; j < result._matrix.GetLength(1); j++)
					{
						float c = 0;

						for (int t = 0; t < m1._matrix.GetLength(1); t++)
						{
							c += (m1._matrix[i, t] * m2._matrix[t, j]);
						}

						result._matrix[i, j] = c;
					}
				}

				resultTime = startTime.Elapsed;

				Console.WriteLine();
				Console.WriteLine("Последовательное умножение");
				result.WriteToConsole();
				Console.WriteLine("Время выполнения: {0}", resultTime);

				return result;
			}
		}

		public void WriteToConsole()
		{
			if (this.Rows < 20 && this.Cols < 20)
			{
				for (int i = 0; i < _matrix.GetLength(0); i++)
				{
					for (int j = 0; j < _matrix.GetLength(1); j++)
					{
						Console.Write(String.Format("{0,12}", _matrix[i, j]));
					}

					Console.WriteLine();
				}
			}
		}

		public void СopyFrom (Matrix source)
		{
			if (source._matrix.GetLength(0) == _matrix.GetLength(0) && source._matrix.GetLength(1) == _matrix.GetLength(1))
			{
				for (int i = 0; i < source._matrix.GetLength(0); i++)
				{
					for (int j = 0; j < source._matrix.GetLength(1); j++)
					{
						_matrix[i, j] = source._matrix[i, j];
					}
				}
			}
			else
			{
				throw new Exception("Неверная размерность матриц, операция невозможна");
			}
		}
		#endregion

		#region ThreadParts

		private void FillParallel_Thread(int row, int col, Random random)
		{
			this._matrix[row, col] = (float)random.Next(1, 100) / 10f;
		}

		private void FillParallel_Thread(object data)
		{
			if (data is Point point)
			{
				//Console.WriteLine("Начало заполнения [{0}, {1}]", point.Row, point.Col);

				FillParallel_Thread(point.Row, point.Col, point.Random);

				//Console.WriteLine("Конец заполнения [{0}, {1}]", point.Row, point.Col);
			}
		}

		private static void AddParallel_Thread (object data)
		{
			if (data is PointWithVals point)
			{
				//Console.WriteLine("Начало сложения [{0}, {1}]", point.Row, point.Col);

				AddParallel_Thread(point.Row, point.Col, point.Val1, point.Val2, point.Matrix);

				//Console.WriteLine("Конец сложения [{0}, {1}]", point.Row, point.Col);
			}
		}

		private static void AddParallel_Thread(int row, int col, float val1, float val2, Matrix matrix)
		{
			matrix._matrix[row, col] = val1 + val2;
		}

		private static void MultipleParallel_Thread(object data)
		{
			if (data is PointWithVals point)
			{
				//Console.WriteLine("Начало умножения [{0}, {1}]", point.Row, point.Col);

				MultipleParallel_Thread(point.Row, point.Col, point.Val1, point.Val2, point.Matrix);

				//Console.WriteLine("Конец умножения [{0}, {1}]", point.Row, point.Col);
			}
		}

		private static void MultipleParallel_Thread (int row, int col, float val1, float val2, Matrix matrix)
		{
			matrix._matrix[row, col] += val1 * val2;
		}

		#endregion

		#region Thread
		public void FillParallel(Random random)
		{
			List<Thread> threads = new List<Thread>();

			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Cols; j++)
				{
					Thread thread = new Thread(new ParameterizedThreadStart(FillParallel_Thread));
					thread.Start(new Point { Row = i, Col = j, Random = random });
					threads.Add(thread);
				}
			}

			foreach (Thread thread in threads)
				thread.Join();

			//Console.WriteLine("Конец всех потоков");
		}

		public static Matrix AddParallel (Matrix m1, Matrix m2)
		{
			if ((m1._matrix.GetLength(0) != m2._matrix.GetLength(0)) || (m1._matrix.GetLength(1) != m2._matrix.GetLength(1)))
			{
				throw new Exception("Размерность матриц не совпадает, операция невозможна");
			}
			else
			{
				List<Thread> threads = new List<Thread>();
				Matrix result = new Matrix(m1._matrix.GetLength(0), m2._matrix.GetLength(1));

				startTime = Stopwatch.StartNew();
				startTime.Stop();

				for (int i = 0; i < m1._matrix.GetLength(0); i++)
				{
					for (int j = 0; j < m1._matrix.GetLength(1); j++)
					{
						Thread thread = new Thread(new ParameterizedThreadStart(AddParallel_Thread));
						thread.Start(new PointWithVals { Row = i, Col = j,  Val1 = m1._matrix[i, j], Val2 = m2._matrix[i, j], Matrix = result });
						threads.Add(thread);
					}
				}

				foreach (Thread thread in threads)
					thread.Join();

				resultTime = startTime.Elapsed;

				Console.WriteLine();
				Console.WriteLine("Параллельное сложение");
				result.WriteToConsole();
				Console.WriteLine("Время выполнения: {0}", resultTime);

				return result;
			}
		}
		public static Matrix MultipleParallel(Matrix m1, Matrix m2)
		{
			if (m1._matrix.GetLength(1) != m2._matrix.GetLength(0))
			{
				throw new Exception("Неверная размерность матриц, операция невозможна");
			}
			else
			{
				Matrix result = new Matrix(m1._matrix.GetLength(0), m2._matrix.GetLength(1));
				List<Thread> threads = new List<Thread>();

				startTime = Stopwatch.StartNew();
				startTime.Stop();

				for (int i = 0; i < result._matrix.GetLength(0); i++)
				{
					for (int j = 0; j < result._matrix.GetLength(1); j++)
					{

						for (int t = 0; t < m1._matrix.GetLength(1); t++)
						{
							Thread thread = new Thread(new ParameterizedThreadStart(MultipleParallel_Thread));
							thread.Start(new PointWithVals { Row = i, Col = j, Val1 = m1._matrix[i, t], Val2 = m2._matrix[t, j], Matrix = result });
							threads.Add(thread);
						}
					}
				}

				foreach (Thread thread in threads)
					thread.Join();

				resultTime = startTime.Elapsed;

				Console.WriteLine();
				Console.WriteLine("Параллельное умножение");
				result.WriteToConsole();
				Console.WriteLine("Время выполнения: {0}", resultTime);

				return result;
			}
		}

		#endregion

		#region AsyncMethods
		public async static Task<Matrix> AddAsync(Matrix m1, Matrix m2)
		{
			if ((m1._matrix.GetLength(0) != m2._matrix.GetLength(0)) || (m1._matrix.GetLength(1) != m2._matrix.GetLength(1)))
			{
				throw new Exception("Размерность матриц не совпадает, операция невозможна");
			}
			else
			{
				try
				{
					Matrix result = new Matrix(m1._matrix.GetLength(0), m2._matrix.GetLength(1));

					startTime = Stopwatch.StartNew();
					startTime.Stop();

					for (int i = 0;  i < m1.Rows; i++)
					{
						for (int j = 0; j < m1.Cols; j ++)
						{
							await Task.Run(() =>
							{
								result._matrix[i, j] = m1._matrix[i, j] + m2._matrix[i, j];
							});
						}
					}

					resultTime = startTime.Elapsed;

					Console.WriteLine();
					Console.WriteLine("Асинхронное сложение");
					result.WriteToConsole();
					Console.WriteLine("Время выполнения: {0}", resultTime);

					return result;
				}
				catch (Exception ex)
				{
					return null;
				}
			}
		}

		public async static Task<Matrix> MultipleAsync(Matrix m1, Matrix m2)
		{
			if (m1._matrix.GetLength(1) != m2._matrix.GetLength(0))
			{
				throw new Exception("Неверная размерность матриц, операция невозможна");
			}
			else
			{
				try
				{
					startTime = Stopwatch.StartNew();
					startTime.Stop();

					Matrix result = new Matrix(m1._matrix.GetLength(0), m2._matrix.GetLength(1));

					for (int i = 0; i < result._matrix.GetLength(0); i++)
					{
						for (int j = 0; j < result._matrix.GetLength(1); j++)
						{

							for (int t = 0; t < m1._matrix.GetLength(1); t++)
							{
								await Task.Run(() => MultipleParallel_Thread(i, j, m1._matrix[i, t], m2._matrix[t, j], result));
							}
						}
					}

					resultTime = startTime.Elapsed;

					Console.WriteLine();
					Console.WriteLine("Асинхронное умножение");
					result.WriteToConsole();
					Console.WriteLine("Время выполнения: {0}", resultTime);

					return result;
				}

				catch (Exception ex)
				{
					return null;
				}
			}
		}
		#endregion

		private float NextFloat(Random random)
		{
			var buffer = new byte[4];
			random.NextBytes(buffer);
			return BitConverter.ToSingle(buffer, 0);
		}
	}
}
