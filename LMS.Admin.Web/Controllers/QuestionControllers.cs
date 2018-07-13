using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS.Dto;
using LMS.Interfaces;
using LMS.Admin.Web.Models;
using LMS.Business.Services;

namespace LMS.Admin.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly QuestionService questionService;
        private readonly QuestionTypeService questionTypeService;
        private readonly CategoryService questionCategoryService;

        private readonly IMapper dtoMapper;

        public QuestionController(
            QuestionService questions, 
            QuestionTypeService questionTypes,
            CategoryService questionCategories,
            IMapper mapper)
        {
            questionService = questions;
            questionTypeService = questionTypes;
            questionCategoryService = questionCategories;
            dtoMapper = mapper;
        }

        public IActionResult Create()
        {
            var vm = new EditOrCreateQuestionViewModel
            {
                AvailableTypes = questionTypeService.GetAll().ToList(),
                AvailableCategories = questionCategoryService.GetAll().ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]QuestionDTO question)
        {
            await questionService.CreateQuestionAsync(question);

            return RedirectToAction(nameof(List));
        }
       
        public IActionResult Edit(int id)
        {
            var viewModel = new EditOrCreateQuestionViewModel
            {
                AvailableTypes = questionTypeService.GetAll(),
                AvailableCategories = questionCategoryService.GetAll()
            };

            dtoMapper.Map(
                questionService.GetQuestionById(id),
                viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm]QuestionDTO question)
        {
            await questionService.UpdateOrMarkQuestionAsync(question);

            return RedirectToAction(nameof(List));
        }
        
        [HttpGet]
        public IActionResult List()
        {
            var questions = questionService.GetAllQuestions();
            return View(questions);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var question = questionService.GetQuestionById(id);
            return View(question);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await questionService.DeleteOrMarkQuestionByIdAsync(id);
            var questions = questionService.GetAllQuestions();
            return View("List", questions);
        }
    }
}
