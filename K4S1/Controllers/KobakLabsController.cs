using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser;

using K4S1.Models.KobakLabs;

using KobLabs;
using KobLabs.Classes;

namespace Controllers
{
    public class KobakLabsController : Controller
    {
        // GET: KobakLabs
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Lab1()
		{
            Lab1Input model = new Lab1Input();
            return View(model);
		}

        [HttpPost]
        public ActionResult Lab1(Lab1Input model)
        {
            if (ModelState.IsValid)
            {
                L1StatisticModel statisticModel = new L1StatisticModel();
                statisticModel.Results = new List<L1ResultModel>();

                var task = new int[model.M];
                Random random = new Random();

                for (int i = 0; i < model.Q; i++)
                {
                    statisticModel.Results.Add((L1ResultModel)Methods.CriticalMethodPath(model.M, model.N, model.Min, model.Max));

                    Thread.Sleep(500);
                }


                foreach (var c in statisticModel.Results)
                {
                    statisticModel.Staticstics += c.Results.Max();
                    statisticModel.StaticsticsDesc += c.ResultsDesc.Max();
                    statisticModel.StaticsticsAsc += c.ResultStatisticsAsc.Max();
                }

                statisticModel.Staticstics = statisticModel.Staticstics / model.Q;
                statisticModel.StaticsticsAsc = statisticModel.StaticsticsAsc / model.Q;
                statisticModel.StaticsticsDesc = statisticModel.StaticsticsDesc / model.Q;

                statisticModel.Methods = new float[3];

                statisticModel.Methods[0] = (float)statisticModel.Results.Sum
                    (
                    c => c.Methods.IndexOf(c.Methods.FirstOrDefault(n => n.MethodName == MethodEnum.RandomMethod)) + 1
                    ) / (float)statisticModel.Results.Count();

                statisticModel.Methods[1] = (float)statisticModel.Results.Sum
                    (
                    c => c.Methods.IndexOf(c.Methods.FirstOrDefault(n => n.MethodName == MethodEnum.Ascending)) + 1
                    ) / (float)statisticModel.Results.Count();

                statisticModel.Methods[2] = (float)statisticModel.Results.Sum
                    (
                    c => c.Methods.IndexOf(c.Methods.FirstOrDefault(n => n.MethodName == MethodEnum.Descending)) + 1
                    ) / (float)statisticModel.Results.Count();

                return View("~/Views/KobakLabs/Lab1Result.cshtml", statisticModel);
            }
            else return View(model);

        }
    }
}