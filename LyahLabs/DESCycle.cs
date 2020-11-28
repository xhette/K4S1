using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyahLabs
{
	public class DESCycle
	{
		#region tables
		private readonly int[,] _IPTable =
		{
			{ 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4 },
			{ 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8 },
			{ 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3 },
			{ 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 }
		};

		private readonly int[,] _ETable =
		{
			{ 32, 1, 2, 3, 4, 5 },
			{ 4, 5, 6, 7, 8, 9, },
			{ 8, 9, 10, 11, 12, 13 },
			{ 12, 13, 14, 15, 16, 17 },
			{ 16, 17, 18, 19, 20, 21 },
			{ 20, 21, 22, 23, 24, 25 },
			{ 24, 25, 26, 27, 28, 29 },
			{ 28, 29, 30, 31, 32, 1 }
		};

		private readonly int[,] _S1 =
		{
			{ 0, 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
			{ 1, 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
			{ 2, 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
			{ 3, 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 }
		};

		private readonly int[,] _S2 =
		{
			{ 0, 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
			{ 1, 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
			{ 2, 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
			{ 3, 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 }
		};

		private readonly int[,] _S3 =
		{
			{ 0, 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
			{ 1, 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
			{ 2, 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
			{ 3, 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 }
		};

		private readonly int[,] _S4 =
		{
			{ 0, 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
			{ 1, 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
			{ 2, 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
			{ 3, 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 }
		};

		private readonly int[,] _S5 =
		{
			{ 0, 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
			{ 1, 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
			{ 2, 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
			{ 3, 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 }
		};

		private readonly int[,] _S6 =
		{
			{ 0, 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
			{ 1, 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
			{ 2, 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
			{ 3, 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 }
		};

		private readonly int[,] _S7 =
		{
			{ 0, 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
			{ 1, 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
			{ 2, 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
			{ 3, 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 }
		};

		private readonly int[,] _S8 =
		{
			{ 0, 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
			{ 1, 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
			{ 2, 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
			{ 3, 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 }
		};

		private readonly int[,] _PTable =
		{
			{ 16, 7, 20, 21, 29, 12, 28, 17 },
			{ 1, 15, 23, 26, 5, 18, 31, 10 },
			{ 2, 8, 24, 14, 32, 27, 3, 9 },
			{ 19, 13, 30, 6, 22, 11, 4, 25 }
		};

		private readonly int[,] _C0 =
		{
			{ 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34, 26, 18 },
			{ 10, 2, 59, 51, 43, 35, 27, 19, 11, 3, 60, 52, 44, 36 }
		};

		private readonly int[,] _D0 =
		{
			{ 63, 55, 47, 39, 31, 23, 15, 7, 62, 54, 46, 38, 30, 22 },
			{ 14, 6, 61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4 }
		};

		private readonly int[,] _GTable =
		{
			{ 57, 49, 41, 33, 25, 17, 9 },
			{ 1, 58, 50, 42, 34, 26, 18 },
			{ 10, 2, 59, 51, 43, 35, 27 },
			{ 19, 11, 3, 60, 52, 44, 36 },
			{ 63, 55, 47, 39, 31, 23, 15 },
			{ 7, 62, 54, 46, 38, 30, 22 },
			{ 14, 6, 61, 53, 45, 37, 29 },
			{ 21, 13, 5, 28, 20, 12, 4 }
		};

		private readonly int[] _ITable = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

		private readonly int[,] _HTable =
		{
			{ 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4 },
			{ 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40 },
			{ 51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32 }
		};

		private readonly int[,] _IP1Table =
		{
			{ 40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31 },
			{ 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29 },
			{ 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27 },
			{ 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25 }
		};
		#endregion

		private const int sizeOfBlock = 64; 
		private const int sizeOfChar = 8;

		string[] Blocks;
		string Key;
		string C, D;
		string _key;

		public DESCycle(string message, string key)
		{
			_key = key;

			string trueMessage = StringToRightLength(message);
			string binaryMessage = StringToBinaryFormat(trueMessage);
			Key = CorrectKeyWord(StringToBinaryFormat(key));
			CutBinaryStringIntoBlocks(binaryMessage);
		}

		public string GetKey()
		{
			return _key;
		}

		private string StringToRightLength(string input)
		{
			while (((input.Length * sizeOfChar) % sizeOfBlock) != 0)
			{
				input += "#";
			}

			return input;
		}

		private void CutBinaryStringIntoBlocks(string input)
		{
			Blocks = new string[input.Length / sizeOfBlock];

			int lengthOfBlock = sizeOfBlock;

			for (int i = 0; i < Blocks.Length; i++)
			{
				Blocks[i] = input.Substring(i * lengthOfBlock, lengthOfBlock);
			}
		}

		private string StringToBinaryFormat(string input)
		{
			return string.Join("", Encoding.GetEncoding("utf-16").GetBytes(input).Select(b => Convert.ToString(b, 2).PadLeft(sizeOfChar, '0')));
		}

		private string CorrectKeyWord(string input)
		{
			if (input.Length > 64)
				input = input.Substring(0, 64);
			else
				while (input.Length < 64)
					input = "0" + input;

			return input;
		}
		private string StringFromBinaryToNormalFormat(string input)
		{
			return Encoding.GetEncoding("utf-16").GetString(Enumerable.Range(0, input.Length / 8).Select(i => input.Substring(i * sizeOfChar, sizeOfChar)).Select(s => Convert.ToByte(s, 2)).ToArray());
		}

		public string Code()
		{
			string gKey = G(Key);

			CD(gKey);

			string coded = "";

			for (int i = 0; i < Blocks.Length; i++)
			{
				string block = CodeBlock(Blocks[i]);

				coded += StringFromBinaryToNormalFormat(block);
			}

			return coded;
		}

		public string Decode()
		{
			string gKey = G(Key);

			CD(gKey);

			string coded = "";

			for (int i = 0; i < Blocks.Length; i++)
			{
				string block = DecodeBlock(Blocks[i]);

				coded += StringFromBinaryToNormalFormat(block);
			}

			return coded;
		}

		private string CodeBlock (string input)
		{
			string ip = IP(input);

			string l0 = ip.Substring(0, ip.Length / 2);
			string r0 = ip.Substring(ip.Length / 2, ip.Length / 2);

			string r = r0, l = l0;

			for (int i = 0; i < 16; i++)
			{
				string li = r;

				string keyJ = Key_j(i);
				string f = F(r, keyJ);

				r = XOR(l, f);
				l = li;
			}

			string concat = string.Concat(r, l);
			string result = IP1(concat);

			return result;
		}
		private string DecodeBlock(string input)
		{
			string ip = IP(input);

			string l0 = ip.Substring(ip.Length / 2, ip.Length / 2);
			string r0 = ip.Substring(0, ip.Length / 2);

			string r = r0, l = l0;

			for (int i = 16; i > 16; i++)
			{
				string li = r;

				string keyJ = Key_j(i);
				string f = F(r, keyJ);

				r = XOR(l, f);
				l = li;
			}

			string concat = string.Concat(r, l);
			string result = IP1(concat);

			return result;
		}

		private string F (string R, string key)
		{
			string eStep = E(R);
			string xorStep = XOR(eStep, key);
			string[] sSteps = new string[8];

			for (int i = 0; i < 8; i++)
			{
				sSteps[i] += Sj(xorStep.Substring(i * 6, 6), i+1);
			}

			string sStep = string.Join("", sSteps);

			string pStep = P(sStep);

			return pStep;
		}

		private string Key_j (int step)
		{
			string keyC = LeftShift(C, _ITable[step]);
			string keyD = LeftShift(D, _ITable[step]);

			string key = H(keyC, keyD);

			return key;
		}

		#region Blocks
		private string IP (string input)
		{
			char[] bitArray = new char[input.Length];
			int bitIndex = 0;

			for (int i = 0; i < _IPTable.GetLength(0); i++)
			{
				for (int j = 0; j < _IPTable.GetLength(1); j++)
				{
					bitArray[bitIndex] = input[_IPTable[i, j] - 1];
					bitIndex++;
				}
			}

			return string.Join("", bitArray);
		}

		private string IP1(string input)
		{
			char[] bitArray = new char[input.Length];
			int bitIndex = 0;

			for (int i = 0; i < _IP1Table.GetLength(0); i++)
			{
				for (int j = 0; j < _IP1Table.GetLength(1); j++)
				{
					bitArray[bitIndex] = input[_IP1Table[i, j] - 1];
					bitIndex++;
				}
			}

			return string.Join("", bitArray);
		}

		private string E (string input)
		{
			char[] bitArray = new char[48];
			int bitIndex = 0;

			for (int i = 0; i < _ETable.GetLength(0); i++)
			{
				for (int j = 0; j < _ETable.GetLength(1); j++)
				{
					bitArray[bitIndex] = input[_ETable[i, j] - 1];
					bitIndex++;
				}
			}

			return string.Join("", bitArray);
		}

		private string XOR (string input, string key)
		{
			string output = "";

			for (int i = 0; i < input.Length; i++)
			{
				bool bit1 = input[i].Equals('1'); ;
				bool bit2 = key[i].Equals('1'); ;

				bool bitResult = bit1 ^ bit2;

				string xorResult = bitResult ? "1" : "0";

				output += xorResult;
			}

			return output;
		}

		private string[] SBlocks (string input)
		{
			string[] output = new string[8];

			for (int i = 0; i < 8; i++)
			{
				output[i] = input.Substring(i * 6, 6);
			}

			return output;
		}

		private string Sj(string input, int sIndex)
		{
			int[,] sTable = null;

			switch (sIndex)
			{
				case 1:
					sTable = _S1;
					break;
				case 2:
					sTable = _S2;
					break;
				case 3:
					sTable = _S3;
					break;
				case 4:
					sTable = _S4;
					break;
				case 5:
					sTable = _S5;
					break;
				case 6:
					sTable = _S6;
					break;
				case 7:
					sTable = _S7;
					break;
				case 8:
					sTable = _S8;
					break;
				default:
					sTable = _S1;
					break;
			}

			string rowIndex = String.Format("{0}{1}", input[0], input[5]);
			string colIndex = input.Substring(1, 4);

			int row = Convert.ToInt32(rowIndex, 2);
			int col = Convert.ToInt32(colIndex, 2);

			string newBit = Convert.ToString(sTable[row, col], 2);

			if (newBit.Length < 4)
			{
				int dif = 4 - newBit.Length;
				string sub = "";

				for (int i = 0; i < dif; i++)
				{
					sub += "0";
				}

				newBit = newBit.Insert(0, sub);
			}

			string output = newBit;

			return output;
		}

		private string P (string input)
		{
			char[] bitArray = new char[32];
			int bitIndex = 0;

			for (int i = 0; i < _PTable.GetLength(0); i++)
			{
				for (int j = 0; j < _PTable.GetLength(1); j++)
				{
					bitArray[bitIndex] = input[_PTable[i, j] - 1];
					bitIndex++;
				}
			}

			return string.Join("", bitArray);
		}
		#endregion

		#region Keys

		private string G (string key)
		{
			char[] bitArray = new char[56];
			int bitIndex = 0;

			for (int i = 0; i < _GTable.GetLength(0); i++)
			{
				for (int j = 0; j < _GTable.GetLength(1); j++)
				{
					bitArray[bitIndex] = key[_GTable[i, j] - 1];
					bitIndex++;
				}
			}

			return string.Join("", bitArray);
		}

		private void CD(string key)
		{
			C = key.Substring(0, key.Length / 2);
			D = key.Substring(key.Length / 2, key.Length / 2);
		}

		private string H (string keyC, string keyD)
		{
			string cdKey = "";

			for (int i = 0; i < keyC.Length; i++)
			{
				cdKey += keyC[i];
				cdKey += keyD[i];
			}

			char[] bitArray = new char[48];
			int bitIndex = 0;

			for (int i = 0; i < _HTable.GetLength(0); i++)
			{
				for (int j = 0; j < _HTable.GetLength(1); j++)
				{
					bitArray[bitIndex] = cdKey[_HTable[i, j] - 1];
					bitIndex++;
				}
			}

			return string.Join("", bitArray);
		}

		private string LeftShift (string key, int step)
		{
			int razsdv = key.Length - step;
			var newKey = key.Substring(razsdv) + key.Substring(0, razsdv);

			return newKey;
		}
		#endregion
	}
}