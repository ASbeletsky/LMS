//using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LMS.Client.Web.Models;
using LMS.Entities;
using System;
using LMS.Dto;
using LMS.Business.Services;
using LMS.Interfaces;

namespace LMS.Client.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly TestSessionUserService testSessionUserService;
        private readonly TestService testService;
        private readonly IMapper dtoMapper;

        private static TestClientDTO DBTest;
        TaskInfo info = new TaskInfo();

        public HomeController( TestService _testService,TestSessionUserService _testSessionUserService, IMapper mapper)
        {
            testSessionUserService = _testSessionUserService;
            testService = _testService;
            dtoMapper = mapper;
        }

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
            var UserSession = testSessionUserService.GetByUserId("65546306-32a5-48ee-8a0a-a384d9ad2063");
            if (UserSession == null)
            {
                return View("Baned");
            };
            DBTest = dtoMapper.Map<Entities.Test, TestClientDTO>(UserSession.Test);
            return View(DBTest);
        }

        public IActionResult TimerTest()
        {
            return View();
        }

        //public IActionResult TestAjax()
        //{
        //    return View(test);
        //}
        public ActionResult ShowProblem(int number)
        {
            info.OurTask = DBTest.Tasks[number];
            info.CurrentQuestionNumber = number;
            info.TaskCount = DBTest.Tasks.Count;
            info.Category = DBTest.Tasks[number].Category.Title;
            info.Result = new string[]{""};
            switch (info.OurTask.Type.Id)
            {
                case 1:
                    return PartialView("_ToWriteTextProblem", info);
                case 2:
                    return PartialView("_OneAnswerProblem", info);
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
