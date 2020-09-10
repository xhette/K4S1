using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace K4S1.Models.LyahnitsLabs
{
	public class LyahLab1Model
	{
		[Required(ErrorMessage = "Требуется ввести ключ")]
		[Display(Name = "Ключ")]
		public string Key { get; set; }

		[Display(Name = "Файл с сообщением")]
		[Required(ErrorMessage = "Укажите путь к файлу с сообщением")]
		public HttpPostedFileBase File { get; set; }
	}
}