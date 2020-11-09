using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyahLabs
{
	public static class CodeMethods
	{
		public static string Lab1Code(string key, string message)
		{
			List<int> keyArray = key.Split(' ').Select(c => Convert.ToInt32(c)).ToList();

			char[,] messageMatrix = new char[1, keyArray.Count];

			int i = 0;
			int j = 0;

			foreach (var c in message)
			{
				messageMatrix[i, j] = c;
				j++;

				if (j >= messageMatrix.GetLength(1))
				{
					i++;
					j = 0;
				}

				if (i >= messageMatrix.GetLength(0))
				{
					messageMatrix = (char[,])ResizeMatrix(messageMatrix, messageMatrix.GetLength(0) + 1, messageMatrix.GetLength(1));
				}
			}

			List<int> sortedKey = keyArray.OrderBy(c => c).ToList();

			List<int> sortedIndexes = new List<int>();

			foreach (var k in sortedKey)
			{
				sortedIndexes.Add(keyArray.IndexOf(k));
			}

			StringBuilder codeMessage = new StringBuilder();

			foreach (var index in sortedIndexes)
			{
				for (int z = 0; z < messageMatrix.GetLength(0); z++)
				{
					codeMessage.Append(messageMatrix[z, index]);
				}
			}

			return codeMessage.ToString().Replace("\0", "*").Replace(" ", "*");
		}

		public static string Lab1Decode(string key, string codeMessage)
		{
			List<int> keyArray = key.Split(' ').Select(c => Convert.ToInt32(c)).ToList();

			List<int> sortedKey = keyArray.OrderBy(c => c).ToList();

			List<int> sortedIndexes = new List<int>();

			foreach (var k in sortedKey)
			{
				sortedIndexes.Add(keyArray.IndexOf(k));
			}

			int cols = keyArray.Count();
			int rows = codeMessage.Length / keyArray.Count();

			char[,] matrix = new char[rows, cols];

			int index = 0;

			foreach (var keyIndex in sortedIndexes)
			{
				for (int i = 0; i < rows; i++)
				{
					matrix[i, keyIndex] = codeMessage[index];
					index++;
				}
			}

			StringBuilder decodeMessage = new StringBuilder();
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					decodeMessage.Append(matrix[i, j]);
				}
			}

			return decodeMessage.ToString();
		}

		private static Array ResizeMatrix(Array arr, int n, int m)
		{
			var temp = Array.CreateInstance(arr.GetType().GetElementType(), n, m);
			int length = arr.Length <= temp.Length ? arr.Length : temp.Length;
			Array.ConstrainedCopy(arr, 0, temp, 0, length);

			return temp;
		}

		public static string Lab2Code (List<string> key, string message)
		{
			List<byte> bitMessage = Encoding.Unicode.GetBytes(message).ToList();

			List<byte> bitKey = new List<byte>();

			foreach (var k in key)
			{
				var step = Encoding.Unicode.GetBytes(k).ToList();

				foreach (var ch in step)
				{
					bitKey.Add(ch);
				}
			}

			List<byte> coded = new List<byte>();
			int index = 0;

			foreach (var ch in bitMessage)
			{
				if (!(index < bitKey.Count))
				{
					index = 0;
				}

				var m = (byte)(ch ^ bitKey[index]);
				index++;

				coded.Add(m);
			}

			var mes = coded.ToArray();

			return Encoding.Unicode.GetString(mes);
		}
	}
}
