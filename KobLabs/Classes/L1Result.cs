using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobLabs.Classes
{
	public class L1Result
	{
		public int[] Tasks { get; set; }

		public int[,] Schedule { get; set; }

		public int[] Results { get; set; }

		public int[] ResultStatistics
		{
			get; set;
		}

		public int[] IndexesStatistics
		{
			get; set;
		}

		public float ResultAverage
		{
			get
			{
				return (float)ResultStatistics.Sum() / (float)ResultStatistics.Count();
			}
		}

		public float IndexAverage
		{
			get
			{
				return (float)IndexesStatistics.Sum() / (float)IndexesStatistics.Count();
			}
		}

		public int[] TasksDesc { get; set; }

		public int[,] ScheduleDesc { get; set; }

		public int[] ResultsDesc { get; set; }

		public int[] ResultStatisticsDesc
		{
			get; set;
		}

		public int[] IndexesStatisticsDesc
		{
			get; set;
		}

		public float ResultAverageDesc
		{
			get
			{
				return (float)ResultStatisticsDesc.Sum() / (float)ResultStatisticsDesc.Count();
			}
		}

		public float IndexAverageDesc
		{
			get
			{
				return (float)IndexesStatisticsDesc.Sum() / (float)IndexesStatisticsDesc.Count();
			}
		}

		public int[] TasksAsc { get; set; }

		public int[,] ScheduleAsc { get; set; }

		public int[] ResultsAsc { get; set; }

		public int[] ResultStatisticsAsc
		{
			get; set;
		}

		public int[] IndexesStatisticsAsc
		{
			get; set;
		}

		public float ResultAverageAsc
		{
			get
			{
				return (float)ResultStatisticsAsc.Sum() / (float)ResultStatisticsAsc.Count();
			}
		}

		public float IndexAverageAsc
		{
			get
			{
				return (float)IndexesStatisticsAsc.Sum() / (float)IndexesStatisticsAsc.Count();
			}
		}

		//public int[] Methods { get; set; }

		public List<MethodMax> Methods { get; set; }

		public L1Result() { }
	}
}
