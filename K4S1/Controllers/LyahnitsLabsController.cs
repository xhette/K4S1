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

        public ActionResult Lab2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetRandoms(int n)
		{
            Lab2Model model = new Lab2Model
            {
                Randoms = new List<long>(),
                Ghistogramm = new List<double>()
            };

            FuckingRandom fuckingRandom = new FuckingRandom(24);

            for (int i = 0; i < n; i++)
            {
                model.Randoms.Add(fuckingRandom.Next());
            }

            Ghistogramm ghistogramm = new Ghistogramm(24);
            model.Ghistogramm = ghistogramm.GetGhist(model.Randoms).ToList();
            model.Intervals = ghistogramm.GetIntervarls();

            return View(model);
        }

        public ActionResult DownloadKey(string key)
		{
            var bytes = Encoding.ASCII.GetBytes(key); //Data to be downloaded

            return File(bytes, "text/plain", "random-key.txt");
        }

        [HttpGet]
        public ActionResult GhistoCode()
		{
            Lab2Input model = new Lab2Input();

            return View(model);
        }

        [HttpPost]
        public ActionResult GhistoCode(Lab2Input model)
        {
            StreamReader reader = new StreamReader(model.File.InputStream);
            string text = reader.ReadToEnd();

            StreamReader readerKey = new StreamReader(model.Key.InputStream);
            List<string> key = readerKey.ReadToEnd().Split(' ').ToList();

            string code = CodeMethods.Lab2Code(key, text);
            var bytes = Encoding.UTF8.GetBytes(code);

            return File(bytes, "text/plain", "random-code.txt");
        }

        [HttpGet]
        public ActionResult GhistoDecode()
        {
            Lab2Input model = new Lab2Input();

            return View(model);
        }

        [HttpPost]
        public ActionResult GhistoDecode(Lab2Input model)
        {
            StreamReader reader = new StreamReader(model.File.InputStream);
            string text = reader.ReadToEnd();

            StreamReader readerKey = new StreamReader(model.Key.InputStream);
            List<string> key = readerKey.ReadToEnd().Split(' ').ToList();

            string code = CodeMethods.Lab2Code(key, text);
            var bytes = Encoding.Unicode.GetBytes(code);

            return File(bytes, "text/plain", "random-decode.txt");
        }
    }
}