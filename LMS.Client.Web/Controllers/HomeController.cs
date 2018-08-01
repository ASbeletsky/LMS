//using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LMS.Client.Web.Models;
using LMS.Entities;
using System;
using LMS.Dto;

namespace LMS.Client.Web.Controllers
{
    public class HomeController : Controller
    {
        TaskInfo info = new TaskInfo();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        public IActionResult Greetings()
        {
            return View();
        }

        public IActionResult TimerTest()
        {
            return View();
        }

        public IActionResult TestAjax()
        {
            return View(test);
        }
        public ActionResult ShowProblem(int number)
        {
            var tasks = new List<TaskClientDTO>();
            foreach (var item in test.Levels)
            {
                foreach (var item2 in item.Tasks)
                {
                    tasks.Add(item2);
                }
            }
            info.OurTask = tasks[number];
            info.CurrentQuestionNumber = number;
            info.TaskCount = tasks.Count;
            info.Category = tasks[number].Category.Title;
            info.Result = new string[]{""};
            switch (info.OurTask.Type.Id)
            {
                case 0:
                    return PartialView("_OneAnswerProblem",info);
                case 1:
                    return PartialView("_MultipleAnswerProblem", info);
                case 2:
                    return PartialView("_ToWriteTextProblem", info);
                case 3:
                    return PartialView("_ToWriteCodeProblem", info);
                case 4:
                    return PartialView("_ToDrawDiagramProblem", info);
            }
            return PartialView("_OneAnswerProblem");
        }
        public RedirectToActionResult Start()
        {
            var number = 0;
            return RedirectToAction("ShowProblem", new { number});
        }
        public RedirectToActionResult Navigate(int number,string mode, List<string> result,int got)
        {
            switch (mode)
            {
                case "prev":
                    if (number > 0) number--;
                    break;
                case "res":
                    number++;
                    break;
                case "next":
                    number++;
                    break;
                default:
                    number = got;
                    break;
            }
            return RedirectToAction("ShowProblem", new { number });
        }
        public List<int> ConvertToIdList(List<string>result)
        {
            var IdList = new List<int>();
            foreach(var s in result)
            {
                IdList.Add(Convert.ToInt32(s));
            }
            return IdList;
        }

    }
}
