using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K4S1.Models.KobakLabs
{
	public class L4Model
	{
		public List<L4Result> Results { get; set; }

		public List<TopMethod> Tops { get; set; }

		public L4Model()
		{
			Results = new List<L4Result>();
			Tops = new List<TopMethod>();
		}
	}
}