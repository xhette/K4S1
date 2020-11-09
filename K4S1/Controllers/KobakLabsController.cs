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

        public ActionResult Lab2()
        {
            Lab1Input model = new Lab1Input();
            return View(model);
        }

        [HttpPost]
        public ActionResult Lab2(Lab1Input model)
        {
            ResultModel result = new ResultModel();

            for (int i = 0; i < model.Q; i++)
            {
                FinalStatisticModel finalStatistic = new FinalStatisticModel();
                StatisticElementModel element1 = new StatisticElementModel();
                StatisticElementModel element2 = new StatisticElementModel();
                StatisticElementModel element3 = new StatisticElementModel();
                finalStatistic.Matrix = new int[model.M, model.N];

                Random random = new Random();

                for (int m = 0; m < model.M; m++)
                {
                    for (int n = 0; n < model.N; n++)
                    {
                        finalStatistic.Matrix[m, n] = random.Next(model.Min, model.Max);
                    }
                }

                var result1 = Methods.Zverev(finalStatistic.Matrix);
                var result2 = Methods.MinElements(finalStatistic.Matrix);
                var result3 = Methods.Barier(finalStatistic.Matrix);

                element1.Result.Schedule = result1.Result.Schedule;
                element1.Result.MethodName = result1.MethodName;
                element1.Result.Loads = result1.Result.Loads;

                element2.Result.Schedule = result2.Result.Schedule;
                element2.Result.MethodName = result2.MethodName;
                element2.Result.Loads = result2.Result.Loads;

                element3.Result.Schedule = result3.Result.Schedule;
                element3.Result.MethodName = result3.MethodName;
                element3.Result.Loads = result3.Result.Loads;

                finalStatistic.StatisticElements = finalStatistic.StatisticElements.OrderBy(c => c.Result.Loads.Max()).ToList();

                result.FinalStatistic.Add(new FinalStatisticModel
                {
                    Matrix = finalStatistic.Matrix,
                    StatisticElements = new List<StatisticElementModel>
                    {
                        element1, element2, element3
                    }
                });

                Thread.Sleep(500);
            }


            foreach (var e in result.FinalStatistic)
            {
                e.StatisticElements = e.StatisticElements.OrderBy(c => c.Result.Loads.Max()).ToList();
            }


            TopMethod topMethod1 = new TopMethod();
            TopMethod topMethod2 = new TopMethod();
            TopMethod topMethod3 = new TopMethod();

            topMethod1.Method = "Алгоритм Плотникова-Зверева";

            foreach (var e in result.FinalStatistic)
            {
                topMethod1.Method = "Алгоритм Плотникова-Зверева";
                topMethod1.Index += (float)e.StatisticElements.IndexOf(e.StatisticElements.FirstOrDefault(c => c.Result.MethodName == "Алгоритм Плотникова-Зверева")) + 1;

                topMethod2.Method = "Минимальных элементов";
                topMethod2.Index += (float)e.StatisticElements.IndexOf(e.StatisticElements.FirstOrDefault(c => c.Result.MethodName == "Минимальных элементов")) + 1;

                topMethod3.Method = "Поиск с барьером";
                topMethod3.Index += (float)e.StatisticElements.IndexOf(e.StatisticElements.FirstOrDefault(c => c.Result.MethodName == "Поиск с барьером")) + 1;

            }

            topMethod1.Index /= model.Q;

            topMethod2.Index /= model.Q;

            topMethod3.Index /= model.Q;


            result.TopMethod.Add(topMethod1);
            result.TopMethod.Add(topMethod2);
            result.TopMethod.Add(topMethod3);

            return View("~/Views/KobakLabs/Lab2Result.cshtml", result);
        }

        public ActionResult Lab3()
        {
            Lab1Input model = new Lab1Input();
            return View(model);
        }

        [HttpPost]
        public ActionResult Lab3(Lab1Input model)
        {
            L3ResultModel result = new L3ResultModel();
            result.Elements = new List<L3Element>();
            result.Top = new List<TopMethod>();

            for (int i = 0; i < model.Q; i++)
            {
                result.Elements.Add((L3Element)Methods.Kron(model.M, model.N, model.Min, model.Max));

                Thread.Sleep(500);
            }

            result.Top.Add(new TopMethod
            {
                Index = 0,
                Method = "Критический путь"
            });
            result.Top.Add(new TopMethod
            {
                Index = 0,
                Method = "Случайный"
            });

            foreach (var e in result.Elements)
            {
                result.Top[0].Index += (float)e.MethodStatistics.IndexOf(e.MethodStatistics.FirstOrDefault(c => c.MethodName == MethodEnum.Critical)) + 1;
                result.Top[1].Index += (float)e.MethodStatistics.IndexOf(e.MethodStatistics.FirstOrDefault(c => c.MethodName == MethodEnum.RandomMethod)) + 1;
            }

            result.Top[0].Index /= (float)model.Q;
            result.Top[1].Index /= (float)model.Q;

            return View("~/Views/KobakLabs/Lab3Result.cshtml", result);
        }

        public ActionResult Lab4()
        {
            Lab1Input model = new Lab1Input();
            return View(model);
        }

        [HttpPost]
        public ActionResult Lab4(Lab1Input model)
        {
            L4Model result = new L4Model();

            for (int q = 0; q < model.Q; q++)
            {
                int[,] matrix = new int[model.M, model.N];

                Random random = new Random();
                for (int i = 0; i < model.M; i++)
				{
                    for (int j = 0; j < model.N; j++)
					{
                        matrix[i, j] = random.Next(model.Min, model.Max);
					}
				}

                L4Result step = new L4Result();

                step.Matrix= new int[model.M, model.N];

                for (int i = 0; i < model.M; i++)
                {
                    for (int j = 0; j < model.N; j++)
                    {
                        step.Matrix[i, j] = matrix[i, j];
                    }
                }

                int barier1 = 0;
                int barier2 = 0;

                L4Element element1 = new L4Element();
                element1.Barier = "Поиск с барьером по среднему элементов";

                barier1 = Methods.Barier1(matrix);
                var result1 = Methods.BarierMethod(step.Matrix, barier1);

                foreach (var e in result1)
                {
                    int[] step2 = new int[e.Length];

                    for (int i = 0; i < e.Length; i++)
					{
                        step2[i] = e[i];
					}

                    element1.Result.Add(step2);
				}

                L4Element element2 = new L4Element();
                element2.Barier = "Поиск с барьером по минимальным элементам";

                barier2 = Methods.Barier2(matrix);
                var result2 = Methods.BarierMethod(step.Matrix, barier2);

                foreach (var e in result2)
                {
                    int[] step2 = new int[e.Length];

                    for (int i = 0; i < e.Length; i++)
                    {
                        step2[i] = e[i];
                    }

                    element2.Result.Add(step2);
                }

                step.Methods.Add(element1);
                step.Methods.Add(element2);

                step.Methods = step.Methods.OrderBy(c => c.Result[c.Result.Count - 1].Max()).ToList();


                result.Results.Add(step);
            }

            int index1 = 0;
            int index2 = 0;

            index1 = result.Results.Select(c => c.Methods.IndexOf(c.Methods.FirstOrDefault(x => x.Barier == "Поиск с барьером по среднему элементов"))).ToList().Sum() / model.Q;
            index2 = result.Results.Select(c => c.Methods.IndexOf(c.Methods.FirstOrDefault(x => x.Barier == "Поиск с барьером по минимальным элементам"))).ToList().Sum() / model.Q;

            result.Tops.Add(new TopMethod
            {
                Index = index1,
                Method = "Поиск с барьером по среднему элементов"
            });

            result.Tops.Add(new TopMethod
            {
                Index = index2,
                Method = "Поиск с барьером по минимальным элементам"
            });

            return View("~/Views/KobakLabs/Lab4Result.cshtml", result);
        }
    }
}