using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K4S1.Models.KobakLabs
{
	public enum MethodEnum
	{
		[Description("Случайный")]
		RandomMethod = 1,

		[Description("По возрастанию")]
		Ascending = 2,

		[Description("По убыванию")]
		Descending = 3,

		[Description("Критический путь")]
		Critical = 4
	}
}
