using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace K4S1.Models.KobakLabs
{
	public class Lab1Input
	{
		 [Display(Name = "Число процессоров")]
		 [Required(ErrorMessage = "Поле должно быть заполнено")]
		 public int N { get; set; }
		[Display(Name = "Число заданий")]
		[Required(ErrorMessage = "Поле должно быть заполнено")]
		public int M { get; set; }

		[Display(Name = "Число массивов")]
		[Required(ErrorMessage = "Поле должно быть заполнено")]
		public int Q { get; set; }

		[Display(Name = "Минимальная граница")]
		[Required(ErrorMessage = "Поле должно быть заполнено")]
		public int Min { get; set; }

		[Display(Name = "Максимальная граница")]
		[Required(ErrorMessage = "Поле должно быть заполнено")]
		public int Max { get; set; }
	}
}