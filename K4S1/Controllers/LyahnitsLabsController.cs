using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using K4S1.Models.LyahnitsLabs;

using LyahLabs;

namespace K4S1.Controllers
{
    public class LyahnitsLabsController : Controller
    {
        // GET: LyahnitsLab
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lab1()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Code()
		{
            LyahLab1Model model = new LyahLab1Model();

            return View(model);
		}

        [HttpPost]
        public ActionResult Code(LyahLab1Model model)
        {
            StreamReader reader = new StreamReader(model.File.InputStream);
            string text = reader.ReadToEnd();

            string result = CodeMethods.Lab1Code(model.Key, text);

            var bytes = Encoding.ASCII.GetBytes(result); //Data to be downloaded

            return File(bytes, "text/plain", "coding.txt");
        }

        [HttpGet]
        public ActionResult Decode()
        {
            LyahLab1Model model = new LyahLab1Model();

            return View(model);
        }

        [HttpPost]
        public ActionResult Decode(LyahLab1Model model)
        {
            StreamReader reader = new StreamReader(model.File.InputStream);
            string text = reader.ReadToEnd();

            string result = CodeMethods.Lab1Decode(model.Key, text);

            var bytes = Encoding.ASCII.GetBytes(result); //Data to be downloaded

            return File(bytes, "text/plain", "decoding.txt");
        }
    }
}