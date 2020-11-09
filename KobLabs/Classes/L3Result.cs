using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobLabs.Classes
{
	public class L3Result
	{
		public int[] Tasks { get; set; }
 
		public int[,] StartCritical { get; set; }

		public int[,] StartRandom { get; set; }

		public int [,] CriticalSchedule { get; set; }

		public int[,] RandomSchedule { get; set; }

		public int[] CriticalLoad { get; set; }

		public int[] RandomLoad { get; set; }

		public List<MethodStatistic> MethodStatistics { get; set; }
	}
}
