using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS.Client.Web.Models;
using LMS.Data.Models;
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Greetings()
        {
            Test helptest = new Test
            {
                Id = 1,
                Title = "Тест для принятия на работу",
                Category = new TestCategory { Id = 1, Title = ".Net" },
                Duration = new System.TimeSpan(1, 0, 0),
                Problems = new List<TestProblem>(20)
            };
            return View(helptest);
        }
        public ActionResult ShowProblem(int number)
        {
            ViewBag.Index = number;
            List<TestProblem> helpProblem = new List<TestProblem>
            {
                new TestProblem
                {
                    Id = 1,
                    Complexity = 2,
                    Content = "Будет true or false?",
                    Type = new ProblemType { Id = 0, Title = "Тест на выбор одного правильного ответа" },
                    Test = new Test(),
                    Choices = new List<Choice> { new Choice { Id = 1, Answer = "True", IsRight = true, Problem = new TestProblem() }, new Choice { Id = 2, Answer = "False", IsRight = false, Problem = new TestProblem() } }
                },
                new TestProblem
                {
                    Id = 3,
                    Complexity = 2,
                    Content = "Доброе утро?",
                    Type = new ProblemType { Id = 0, Title = "Тест на выбор одного правильного ответа" },
                    Test = new Test(),
                    Choices = new List<Choice> { new Choice { Id = 1, Answer = "Да", IsRight = true, Problem = new TestProblem() }, new Choice { Id = 2, Answer = "Нет", IsRight = false, Problem = new TestProblem() }, new Choice { Id = 3, Answer = "ХЗ", IsRight = true, Problem = new TestProblem() } }
                },
                new TestProblem
                {
                    Id = 2,
                    Complexity = 2,
                    Content = "Как дела?",
                    Type = new ProblemType { Id = 1, Title = "Тест на выбор нескольких ответов" },
                    Test = new Test(),
                    Choices = new List<Choice> { new Choice { Id = 1, Answer = "Хорошо", IsRight = true, Problem = new TestProblem() }, new Choice { Id = 2, Answer = "Плохо", IsRight = false, Problem = new TestProblem() }, new Choice { Id = 3, Answer = "Норм", IsRight = true, Problem = new TestProblem() } }
                }
            };
            switch (helpProblem[number].Type.Id)
            {
                case 0:
                    return PartialView("_OneAnswerProblem",helpProblem[number]);
                case 1:
                    return PartialView("_MultipleAnswerProblem", helpProblem[number]);
            }
            return PartialView("_OneAnswerProblem");
        }
        public RedirectToActionResult Start()
        {
            int number = 0;
            return RedirectToAction("ShowProblem", new { number});
        }
        [HttpPost]
        public RedirectToActionResult GetResult(int number,List<string> result)
        {
            number++;
            return RedirectToAction("ShowProblem", new { number });
        }
    }
}
