using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KobLabs.Classes;

namespace K4S1.Models.KobakLabs
{
	public class StatisticElementModel
	{
		public ScheduleResultModel Result { get; set; }

		public StatisticElementModel()
		{
			Result = new ScheduleResultModel();
		}
	}
}