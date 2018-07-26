using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LMS.Client.Web.Models;
using LMS.Entities;
namespace LMS.Client.Web.Controllers
{
    public class HomeController : Controller
    {
        Test test = new Test
        {
                Id = 1,
                Title = "Тест для принятия на работу",
                TestCategory = new Category { Id = 1, Title = ".Net" },
                Duration = new System.TimeSpan(1, 0, 0),
                Problems = new List<Task>
                {
                    new Task
                    {
                        Id = 1,
                        Complexity = 2,
                        Content = "Будет true or false?",
                        Type = new TaskType { Id = 0, Title = "Тест на выбор одного правильного ответа" },
                        Choices = new List<Choice> { new Choice { Id = 1, Answer = "True", IsRight = true, Problem = new Task() }, new Choice { Id = 2, Answer = "False", IsRight = false, Problem = new Task() } }
                    },
                    new Task
                    {
                        Id = 3,
                        Complexity = 2,
                        Content = "Доброе утро?",
                        Type = new TaskType { Id = 0, Title = "Тест на выбор одного правильного ответа" },
                        Choices = new List<Choice> { new Choice { Id = 1, Answer = "Да", IsRight = true, Problem = new Task() }, new Choice { Id = 2, Answer = "Нет", IsRight = false, Problem = new Task() }, new Choice { Id = 3, Answer = "ХЗ", IsRight = true, Problem = new Task() } }
                    },
                    new Task
                    {
                        Id = 2,
                        Complexity = 2,
                        Content = "Как дела?",
                        Type = new TaskType { Id = 1, Title = "Тест на выбор нескольких ответов" },
                        Choices = new List<Choice> { new Choice { Id = 1, Answer = "Хорошо", IsRight = true, Problem = new Task() }, new Choice { Id = 2, Answer = "Плохо", IsRight = false, Problem = new Task() }, new Choice { Id = 3, Answer = "Норм", IsRight = true, Problem = new Task() } }
                    },
                    new Task
                    {
                        Id = 4,
                        Complexity = 2,
                        Content = "Франция чемпион?",
                        Type = new TaskType { Id = 0, Title = "Тест на выбор одного правильного ответа" },
                        Choices = new List<Choice> { new Choice { Id = 1, Answer = "Да", IsRight = true, Problem = new Task() }, new Choice { Id = 2, Answer = "Нет", IsRight = false, Problem = new Task() }, new Choice { Id = 3, Answer = "ХЗ", IsRight = true, Problem = new Task() } }
                    },
                    new Task
                    {
                        Id = 5,
                        Complexity = 2,
                        Content = "Объясните что такое полиморфизм",
                        Type = new TaskType { Id = 2, Title = "Тест на написание развёрнутого ответа" },
                        Choices = new List<Choice>()
                    },
                    new Task
                    {
                        Id = 6,
                        Complexity = 2,
                        Content = "Напишите Hello World",
                        Type = new TaskType { Id = 3, Title = "Тест на написание кода" },
                        Choices = new List<Choice>()
                    },
                    new Task
                    {
                        Id = 7,
                        Complexity = 2,
                        Content = "Покажите схему заказа в Маке",
                        Type = new TaskType { Id = 4, Title = "Тест на диаграмму" },
                        Choices = new List<Choice>()
                    }
                }
        };
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Greetings()
        {
            return View(test);
        }
        public ActionResult ShowProblem(int number)
        {
            info.OurTask = test.Problems[number];
            info.CurrentQuestionNumber = number;
            info.TaskCount = test.Problems.Count;
            info.Category = test.TestCategory.Title;
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
        public List<int> ConvertToIdList(List<string>result)
        {
            var IdList = new List<int>();
            foreach(string s in result)
            {
                IdList.Add(Convert.ToInt32(s));
            }
            return IdList;
        }
    }
}
