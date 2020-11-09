using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChystLabs
{
	public class KoshiClasses
	{
	}

	public class CauchyConditions
	{
		public double X0 { get; private set; }
		public double Y0 { get; private set; }
		public Func<double, double, double> FirstDeritative { get; private set; }
		public CauchyConditions(double x0, double y0, Func<double, double, double> firstDeritative)
		{
			this.X0 = x0;
			this.Y0 = y0;
			this.FirstDeritative = firstDeritative;
		}
	}
}