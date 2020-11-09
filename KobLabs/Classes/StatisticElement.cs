using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobLabs.Classes
{
	public class StatisticElement
	{
		public int[,] Matrix { get; set; }

		public ScheduleResult Result { get; set; }

		public string MethodName { get; set; }
	}
}
