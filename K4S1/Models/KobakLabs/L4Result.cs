using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K4S1.Models.KobakLabs
{
	public class L4Result
	{
		public int[,] Matrix { get; set; }

		public List<L4Element> Methods { get; set; }

		public L4Result()
		{
			Methods = new List<L4Element>();
		}
	}
}