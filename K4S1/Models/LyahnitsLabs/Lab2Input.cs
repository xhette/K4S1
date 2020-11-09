using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace K4S1.Models.LyahnitsLabs
{
	public class Lab2Input
	{
		[Display(Name = "Файл с ключом")]
		[Required(ErrorMessage = "Укажите путь к файлу с ключом")]
		public HttpPostedFileBase Key { get; set; }

		[Display(Name = "Файл с сообщением")]
		[Required(ErrorMessage = "Укажите путь к файлу с сообщением")]
		public HttpPostedFileBase File { get; set; }
	}
}