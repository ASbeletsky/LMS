//using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS.Entities;
namespace LMS.Client.Web.Controllers
{
    public class HomeController : Controller
    {
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
        //public IActionResult Greetings()
        //{
        //    Test helptest = new Test
        //    {
        //        Id = 1,
        //        Title = "Тест для принятия на работу",
        //        Category = new TestCategory { Id = 1, Title = ".Net" },
        //        Duration = new System.TimeSpan(1, 0, 0),
        //        Problems = new List<TestProblem>(20)
        //    };
        //    return View(helptest);
        //}

        public IActionResult TimerTest()
        {
            return View();
        }

        public IActionResult TestAjax()
        {
            return PartialView();
        }

        public IActionResult Baned()
        {
            return View();
        }

    }
}
