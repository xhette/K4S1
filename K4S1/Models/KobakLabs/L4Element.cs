using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K4S1.Models.KobakLabs
{
	public class L4Element
	{
		public string Barier { get; set; }

		public List<int[]> Result { get; set; }

		public L4Element()
		{
			Result = new List<int[]>();
		}
	}
}