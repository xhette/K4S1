using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K4S1.Models.KobakLabs
{
	public class ScheduleResultModel
	{
		public int[,] Schedule { get; set; }

		public int[] Loads { get; set; }

		public string MethodName { get; set; }
	}
}