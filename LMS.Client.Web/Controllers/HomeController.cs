using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LMS.Client.Web.Models;
using LMS.Entities;
using System.Threading;
using System;
using LMS.Dto;
using LMS.Business.Services;
using LMS.Interfaces;
using LMS.Identity;

namespace LMS.Client.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly TestSessionUserService testSessionUserService;
        private readonly TestSessionService testSessionService;
        private readonly TestService testService;
        private readonly IdentityService identityService;
        private readonly IMapper dtoMapper;
        private static IDictionary<int, TestClientDTO> TestDictionary = new Dictionary<int, TestClientDTO>();
        private static Timer timerTestChecker = new Timer(new TimerCallback(TestChecker), null, 0, 1000 * 60 * 30);

        //private static TestClientDTO DBTest;
        TaskInfo info = new TaskInfo();

        public HomeController(TestSessionUserService _testSessionUserService,
            TestSessionService _testSessionService,TestService _testService, IMapper mapper,
            IdentityService _identityService)
        {
            testSessionUserService = _testSessionUserService;
            testSessionService = _testSessionService;
            testService = _testService;
            dtoMapper = mapper;
            identityService = _identityService;
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
            var UserSession = testSessionUserService.GetByUserId("2524e8bf-67a7-4693-ba3b-8c1a7d60fb2a");
            identityService.LogInClient("2524e8bf-67a7-4693-ba3b-8c1a7d60fb2a");
            if (UserSession == null)
            {
                return View("Baned");
            };
            TestToStore(UserSession);
            var DBTest = dtoMapper.Map<Entities.TestSessionUser, TestSessionUserDTO>(UserSession);
            var test = TestDictionary[DBTest.TestId.Value];
            for (int i = 0; i < test.Tasks.Count -1; i++)
            {
                DBTest.Categories += test.Tasks[i].Category.Title + ", ";
            }
            DBTest.Categories += test.Tasks[test.Tasks.Count - 1].Category.Title+".";
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
        public ActionResult ShowProblem(int number,int sessionId)
        {
            var UserSession = testSessionUserService.GetById(sessionId, "2524e8bf-67a7-4693-ba3b-8c1a7d60fb2a");
            TestToStore(UserSession);
            var test = TestDictionary[UserSession.TestId.Value];
            info.OurTask = test.Tasks[number];
            info.CurrentQuestionNumber = number;
            info.TaskCount = test.Tasks.Count;
            info.Category = test.Tasks[number].Category.Title;
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
        public RedirectToActionResult Start(int sessionId)
        {
            var number = 0;
            return RedirectToAction("ShowProblem", new { number, sessionId });
        }
        public RedirectToActionResult Navigate(int number,string mode, List<string> result,int got,int testId,int sessionId)
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
            return RedirectToAction("ShowProblem", new { number,sessionId });
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

        public void TestToStore(TestSessionUser UserSession)
        {
            if (!TestDictionary.ContainsKey(UserSession.TestId.Value))
            {
                var testDto = testService.GetByIdClient(UserSession.TestId.Value);
                testDto.EndTime = testSessionService.GetById(UserSession.SessionId).EndTime.AddMinutes(15);
                TestDictionary.Add(UserSession.TestId.Value, testDto);
            }
            else
            {
                var newTime = testSessionService.GetById(UserSession.SessionId).EndTime.AddMinutes(15);
                var testId = UserSession.TestId.Value;
                TestDictionary[testId].EndTime = newTime.AddMinutes(15) > TestDictionary[testId].EndTime ?
                    newTime : TestDictionary[testId].EndTime;
            }
        }

        public static void TestChecker(object obj)
        {
            lock (TestDictionary)
            {
                if (TestDictionary.Count > 0)
                {
                    foreach (var item in TestDictionary)
                    {
                        if (item.Value.EndTime <= DateTimeOffset.Now)
                        {
                            TestDictionary.Remove(item.Key);
                        }
                    }
                }
            }
        }

    }
}
