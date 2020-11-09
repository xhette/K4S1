using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K4S1.Models.LyahnitsLabs
{
	public class Lab2Model
	{
		public List<long> Randoms { get; set; }

		public List<double> Ghistogramm { get; set; }

		public string Key
		{
			get
			{
				string key = "";

				foreach (var r in Randoms)
				{
					key += r.ToString();
					key += " ";
				}

				return key;
			}
		}

		public long[] Intervals { get; set; }

		public double GhistAll
		{
			get
			{
				double sum = Ghistogramm.Sum() / (double)Ghistogramm.Count;

				return Math.Round(sum, 4);
			}
		}
	}
}