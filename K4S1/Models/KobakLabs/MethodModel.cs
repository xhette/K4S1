using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KobLabs.Classes;
using KobLabs.Enums;

namespace K4S1.Models.KobakLabs
{
	public class MethodModel
	{
		public MethodEnum MethodName { get; set; }

		public int MaxValue { get; set; }

		public string Method { 
			get
			{
				if (MethodName == MethodEnum.RandomMethod)
				{
					return "Случайный";
				}
				else if (MethodName == MethodEnum.Ascending)
				{
					return "По возрастанию";
				}
				else if (MethodName == MethodEnum.Descending)
				{
					return "По убыванию";
				}
				else if (MethodName == MethodEnum.Critical)
				{
					return "Критический путь";
				}
				else
				{
					return "";
				}
			} 
		}

		public MethodModel() { }

		public MethodModel (string name, int max)
		{
			if (name == "Случайный")
			{
				MethodName = MethodEnum.RandomMethod;
			}
			if (name == "Критический путь")
			{
				MethodName = MethodEnum.Critical;
			}

			MaxValue = max;
		}

		public static explicit operator MethodModel(MethodMax method)
		{
			if (method == null) return null;
			else return new MethodModel
			{
				MethodName = (MethodEnum)(int)method.MethodName,
				MaxValue = method.MaxValue
			};
		}
	}
}
