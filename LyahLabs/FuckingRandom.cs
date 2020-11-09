using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LyahLabs
{
	public class FuckingRandom
	{
		int m = 0;
		int a = 0;
		int b = 0;
		int c0 = 0;
		long c = 0;

		public FuckingRandom (int n = 1)
		{
			m = (int)Math.Pow(2, n);

			int second = DateTime.Now.Millisecond;

			while (!(second % 4 == 1))
			{
				second++;
			}

			a = second;


			b = FindCoprime(m);
			c0 = DateTime.Now.Day;
			c = c0;
		}

		public long Next (bool fromFirst = false)
		{
			if (!fromFirst)
			{
				c = (a * c + b) % m;
			}
			else
			{
				c = (a * c0 + b) % m;
			}
			return c;
		}

		public long Next(long c)
		{
			this.c = (a * c + b) % m;
			return this.c;
		}

		public static int NOD(int a, int b)
		{
			while (b != 0)
				b = a % (a = b);
			return a;
		}

		int FindCoprime (int m)
		{
			int i;

			for (i = DateTime.Today.Day; i < m; ++i)
				if (NOD(m, i) == 1)
				{
					return i;
				}

			return 1;
		}
	}
}
