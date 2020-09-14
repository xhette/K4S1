using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobLabs.Enums
{
	public enum MethodEnum
	{
		[Description("Случайный")]
		RandomMethod = 1,

		[Description("По возрастанию")]
		Ascending = 2,

		[Description("По убыванию")]
		Descending = 3
	}
}
