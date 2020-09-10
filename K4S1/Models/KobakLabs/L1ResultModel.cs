using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KobLabs.Classes;

namespace K4S1.Models.KobakLabs
{
	public class L1ResultModel
	{
		public int[] Tasks { get; set; }

		public int[,] Schedule { get; set; }

		public int[] Results { get; set; }

		public int [] ResultStatistics
		{
			get; set;
		}

		public int [] IndexesStatistics
		{
			get; set;
		}

		public float ResultAverage
		{
			get; set;
		}

		public float IndexAverage
		{
			get; set;
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
			get; set;
		}

		public float IndexAverageAsc
		{
			get; set;
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
			get; set;
		}

		public float IndexAverageDesc
		{
			get; set;
		}

		public int[] Methods { get; set; }
		public L1ResultModel()
		{
		}

		public static explicit operator L1ResultModel(L1Result result)
		{
			if (result == null) return null;
			else return new L1ResultModel
			{
				Tasks = result.Tasks,
				Schedule = result.Schedule,
				Results = result.Results,
				ResultStatistics = result.ResultStatistics,
				IndexesStatistics = result.IndexesStatistics,
				ResultAverage = result.ResultAverage,
				IndexAverage = result.IndexAverage,
				TasksAsc = result.TasksAsc,
				ScheduleAsc = result.ScheduleAsc,
				ResultsAsc = result.ResultsAsc,
				ResultStatisticsAsc = result.ResultStatisticsAsc,
				IndexesStatisticsAsc = result.IndexesStatisticsAsc,
				ResultAverageAsc = result.ResultAverageAsc,
				IndexAverageAsc = result.IndexAverageAsc,
				TasksDesc = result.TasksDesc,
				ScheduleDesc = result.ScheduleDesc,
				ResultsDesc = result.ResultsDesc,
				ResultStatisticsDesc = result.ResultStatisticsDesc,
				IndexesStatisticsDesc = result.IndexesStatisticsDesc,
				ResultAverageDesc = result.ResultAverageDesc,
				IndexAverageDesc = result.IndexAverageDesc,
				Methods = result.Methods,
			};
		}
	}
}
