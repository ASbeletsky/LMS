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
        TestClientDTO test = new TestClientDTO
        {
            Id = 1,
            Title = "Тест для принятия на работу",
            Category = new CategoryDTO
            {
                Id = 1,
                Title = "Some title",
                TasksCount = 1
            },
            Levels = new List<TestLevelClientDTO>
            {
                new TestLevelClientDTO
                {
                    Id=1,
                    TestId=1,
                    Description="Some description about test",
                    Tasks = new List<TaskClientDTO>
                    {
                        new TaskClientDTO
                        {
                            Id=1,
                            Complexity = 2,
                            Content = "Поддерживает ли C# множественное наследование классов",
                            Type = new TaskTypeDTO { Id = 0, Title = "Тест на выбор одного правильного ответа" },
                            Category = new CategoryDTO
                            {
                                Id=1,
                                Title="Random test",
                                TasksCount=1
                            },
                            AnswerOptions= new List<TaskAnswerOptionDTO>
                            {
                                new TaskAnswerOptionDTO
                                {
                                    Id=0,
                                    Content="True",
                                    IsCorrect = false
                                },
                                new TaskAnswerOptionDTO
                                {
                                    Id=1,
                                    Content="False",
                                    IsCorrect = true
                                }
                            }
                        },
                        new TaskClientDTO
                        {
                            Id=2,
                            Complexity = 2,
                            Content = "Правильное определение массива",
                            Type = new TaskTypeDTO { Id = 1, Title = "Тест на выбор нескольких правильных ответов" },
                            Category = new CategoryDTO
                            {
                                Id=1,
                                Title="Random test",
                                TasksCount=1
                            },
                            AnswerOptions= new List<TaskAnswerOptionDTO>
                            {
                                new TaskAnswerOptionDTO
                                {
                                    Id=2,
                                    Content="int[] nums = new int[4];",
                                    IsCorrect = true
                                },
                                new TaskAnswerOptionDTO
                                {
                                    Id=3,
                                    Content="int array[] = new int[];",
                                    IsCorrect = false
                                },
                                new TaskAnswerOptionDTO
                                {
                                    Id=4,
                                    Content="int[] array={3,4,5}",
                                    IsCorrect = false
                                },
                                new TaskAnswerOptionDTO
                                {
                                    Id=4,
                                    Content="int[] nums = new[] { 1, 2, 3, 5 }",
                                    IsCorrect = false
                                },                               
                            }
                        },
                        new TaskClientDTO
                        {
                            Id=1,
                            Complexity = 2,
                            Content = "Объясните что такое полиморфизм",
                            Type = new TaskTypeDTO { Id = 2, Title = "Тест на написание открытого ответа" },
                            Category = new CategoryDTO
                            {
                                Id=1,
                                Title="Random test",
                                TasksCount=1
                            }
                        },
                        new TaskClientDTO
                        {
                            Id=1,
                            Complexity = 2,
                            Content = "Напишите Hello World!",
                            Type = new TaskTypeDTO { Id = 3, Title = "Тест на написание кода" },
                            Category = new CategoryDTO
                            {
                                Id=1,
                                Title="Html",
                                TasksCount=1
                            }
                        },
                        new TaskClientDTO
                        {
                            Id=1,
                            Complexity = 2,
                            Content = "Схема получения информации о продукте используя QR-код",
                            Type = new TaskTypeDTO { Id = 4, Title = "Тест на диаграмму" },
                            Category = new CategoryDTO
                            {
                                Id=1,
                                Title="Random test",
                                TasksCount=1
                            }
                        },
                    }
                }
            }
        /*{
                Task = new List<Task>
                {
                    new Task
                    {
                        Id = 1,
                        Complexity = 2,
                        Content = "Будет true or false?",
                        Type = new TaskType { Id = 0, Title = "Тест на выбор одного правильного ответа" },
                        Choices = new List<Task> { new Task { Id = 1, Answer = "True", IsRight = true, Problem = new Task() }, new Task { Id = 2, Answer = "False", IsRight = false, Problem = new Task() } }
                    },
                    new Task
                    {
                        Id = 3,
                        Complexity = 2,
                        Content = "Доброе утро?",
                        Type = new TaskType { Id = 0, Title = "Тест на выбор одного правильного ответа" },
                        Choices = new List<Task> { new Task { Id = 1, Answer = "Да", IsRight = true, Problem = new Task() }, new Task { Id = 2, Answer = "Нет", IsRight = false, Problem = new Task() }, new Task { Id = 3, Answer = "ХЗ", IsRight = true, Problem = new Task() } }
                    },
                    new Task
                    {
                        Id = 2,
                        Complexity = 2,
                        Content = "Как дела?",
                        Type = new TaskType { Id = 1, Title = "Тест на выбор нескольких ответов" },
                        Choices = new List<Task> { new Task { Id = 1, Answer = "Хорошо", IsRight = true, Problem = new Task() }, new Task { Id = 2, Answer = "Плохо", IsRight = false, Problem = new Task() }, new Task { Id = 3, Answer = "Норм", IsRight = true, Problem = new Task() } }
                    },
                    new Task
                    {
                        Id = 4,
                        Complexity = 2,
                        Content = "Франция чемпион?",
                        Type = new TaskType { Id = 0, Title = "Тест на выбор одного правильного ответа" },
                        Choices = new List<Task> { new Task { Id = 1, Answer = "Да", IsRight = true, Problem = new Task() }, new Task { Id = 2, Answer = "Нет", IsRight = false, Problem = new Task() }, new Task { Id = 3, Answer = "ХЗ", IsRight = true, Problem = new Task() } }
                    },
                    new Task
                    {
                        Id = 5,
                        Complexity = 2,
                        Content = "Объясните что такое полиморфизм",
                        Type = new TaskType { Id = 2, Title = "Тест на написание развёрнутого ответа" },
                        Choices = new List<Task>()
                    },
                    new Task
                    {
                        Id = 6,
                        Complexity = 2,
                        Content = "Напишите Hello World",
                        Type = new TaskType { Id = 3, Title = "Тест на написание кода" },
                        Choices = new List<Task>()
                    },
                    new Task
                    {
                        Id = 7,
                        Complexity = 2,
                        Content = "Покажите схему заказа в Маке",
                        Type = new TaskType { Id = 4, Title = "Тест на диаграмму" },
                        Choices = new List<Task>()
                    }
                }*/
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

        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        public IActionResult Greetings()
        {
            return View(test);
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
