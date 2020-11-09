using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K4S1.Models.KobakLabs
{
	public class FinalStatisticModel
	{
		public int[,] Matrix { get; set; }

		public List<StatisticElementModel> StatisticElements { get; set; }

		public FinalStatisticModel()
		{
			StatisticElements = new List<StatisticElementModel>();
		}
	}
}