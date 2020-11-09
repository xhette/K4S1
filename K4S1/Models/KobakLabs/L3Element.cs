using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KobLabs.Classes;

namespace K4S1.Models.KobakLabs
{
	public class L3Element
	{
		public int[] Tasks { get; set; }

		public int[,] StartCritical { get; set; }

		public int[,] StartRandom { get; set; }

		public int[,] CriticalSchedule { get; set; }

		public int[,] RandomSchedule { get; set; }

		public int[] CriticalLoad { get; set; }

		public int[] RandomLoad { get; set; }

		public List<MethodModel> MethodStatistics { get; set; }

		public static explicit operator L3Element(L3Result l3)
		{
			if (l3 == null)
			{
				return null;
			}
			else
			{
				L3Element result = new L3Element
				{
					Tasks = l3.Tasks,
					StartCritical = l3.StartCritical,
					StartRandom = l3.StartRandom,
					CriticalSchedule = l3.CriticalSchedule,
					RandomSchedule = l3.RandomSchedule,
					CriticalLoad = l3.CriticalLoad,
					RandomLoad = l3.RandomLoad
				};
				result.MethodStatistics = new List<MethodModel>();
				foreach (var c in l3.MethodStatistics)
				{
					result.MethodStatistics.Add(new MethodModel(c.MethodName, c.MaxLoad));
				}

				return result;
			}
		}
	}
}