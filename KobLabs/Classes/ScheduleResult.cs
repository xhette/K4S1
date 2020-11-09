using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobLabs.Classes
{
	public class ScheduleResult
	{
		public int[,] Schedule { get; set; }

		public int[] Loads { get; set; }
	}
}
