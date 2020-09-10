using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KobLabs.Classes;

namespace K4S1.Models.KobakLabs
{
	public class L1StatisticModel
	{
		public List<L1ResultModel> Results { get; set; }
		
		public float Staticstics { get; set; }

		public float Indexes { get; set; }

		public float StaticsticsAsc { get; set; }

		public float IndexesAsc { get; set; }

		public float StaticsticsDesc { get; set; }

		public float IndexesDesc { get; set; }

		public List<int[]> Methods { get; set; }
	}
}