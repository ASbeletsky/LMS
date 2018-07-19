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
                },
                new TestProblem
                {
                    Id = 4,
                    Complexity = 2,
                    Content = "Франция чемпион?",
                    Type = new ProblemType { Id = 0, Title = "Тест на выбор одного правильного ответа" },
                    Test = new Test(),
                    Choices = new List<Choice> { new Choice { Id = 1, Answer = "Да", IsRight = true, Problem = new TestProblem() }, new Choice { Id = 2, Answer = "Нет", IsRight = false, Problem = new TestProblem() }, new Choice { Id = 3, Answer = "ХЗ", IsRight = true, Problem = new TestProblem() } }
                },
                new TestProblem
                {
                    Id = 5,
                    Complexity = 2,
                    Content = "Объясните что такое полиморфизм",
                    Type = new ProblemType { Id = 2, Title = "Тест на написание развёрнутого ответа" },
                    Test = new Test(),
                    Choices = new List<Choice>()
                },
                new TestProblem
                {
                    Id = 6,
                    Complexity = 2,
                    Content = "Напишите Hello World",
                    Type = new ProblemType { Id = 3, Title = "Тест на написание кода" },
                    Test = new Test(),
                    Choices = new List<Choice>()
                },
                new TestProblem
                {
                    Id = 7,
                    Complexity = 2,
                    Content = "Покажите схему заказа в Маке",
                    Type = new ProblemType { Id = 4, Title = "Тест на диаграмму" },
                    Test = new Test(),
                    Choices = new List<Choice>()
                }
            };
            ViewBag.Index = number;
            ViewBag.Kolvo = helpProblem.Count;
            switch (helpProblem[number].Type.Id)
            {
                case 0:
                    return PartialView("_OneAnswerProblem",helpProblem[number]);
                case 1:
                    return PartialView("_MultipleAnswerProblem", helpProblem[number]);
                case 2:
                    return PartialView("_ToWriteTextProblem", helpProblem[number]);
                case 3:
                    return PartialView("_ToWriteCodeProblem", helpProblem[number]);
                case 4:
                    return PartialView("_ToDrawDiagramProblem", helpProblem[number]);
            }
            return PartialView("_OneAnswerProblem");
        }
        public RedirectToActionResult Start()
        {
            int number = 0;
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
    }
}
