using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K4S1.Models.KobakLabs
{
	public class ResultModel
	{
		public List<FinalStatisticModel> FinalStatistic { get; set; }

		public List<TopMethod> TopMethod { get; set; }

		public ResultModel()
		{
			FinalStatistic = new List<FinalStatisticModel>();
			TopMethod = new List<TopMethod>();
		}
	}
}