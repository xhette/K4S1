using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyahLabs
{
	public class Ghistogramm
	{
		private long m;
		private long length = 0;
		long[] intervals;

		public Ghistogramm (int n)
		{
			m = (int)Math.Pow(2, n);
			length = m - 1;

			intervals = new long[100];

			long step = length / 100;

			intervals[0] = step;

			for (int i = 1; i < 100; i++)
			{
				intervals[i] = intervals[i - 1] + step;
			}

			if (intervals[99] > m)
			{
				long razn = intervals[99] - m;
				intervals[99] -= razn;
			}

			if (intervals[99] < m)
			{
				long razn = m - intervals[99];
				intervals[99] += razn;
			}
		}

		public double[] GetGhist (List<long> randoms)
		{
			double[] gist = new double[100];

			foreach (var r in randoms)
			{
				for (int i = 0; i < 100; i++)
				{
					if (i > 0)
					{
						if (r < intervals[i] && r > intervals[i - 1])
						{
							gist[i]++;
						}
					}
					else
					{
						if (r < intervals[i] && r > 0)
						{
							gist[i]++;
						}
					}
				}
			}

			for (int i = 0; i < 100; i++)
			{
				gist[i] = gist[i] / (double)randoms.Count();
			}

			return gist;
		}

		public long[] GetIntervarls()
		{
			return intervals;
		}
	}
}
