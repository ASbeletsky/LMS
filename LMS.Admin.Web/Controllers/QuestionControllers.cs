using LMS.Business.Services;
using LMS.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Admin.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly QuestionService questionService;
        public QuestionController(QuestionService service)
        {
            questionService = service;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreatOrEdit([FromForm]Question question)
        //{
        //    await questionService.NewQuestion(question);
        //    return View("EditOrCreate", );
        //}

        [HttpGet]
        public IActionResult GetAllQuestions()
        {
            var questions = questionService.GetAllQuestions();
            return View("List", questions);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteQuestion([FromForm]Question question)
        {
            await questionService.DeleteQuestion(question);
            var questions = questionService.GetAllQuestions();
            return View("List", questions);
        }
    }
}
