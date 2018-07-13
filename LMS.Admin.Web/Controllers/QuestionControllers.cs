using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS.Dto;
using LMS.Interfaces;
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
            ViewData["AvailableTypes"] = questionTypeService.GetAll();
            ViewData["AvailableCategories"] = questionCategoryService.GetAll();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]QuestionDTO question)
        {
            await questionService.CreateAsync(question);

            return RedirectToAction(nameof(List));
        }
       
        public IActionResult Edit(int id)
        {
            var question = questionService.GetById(id);

            ViewData["AvailableTypes"] = questionTypeService.GetAll();
            ViewData["AvailableCategories"] = questionCategoryService.GetAll();
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm]QuestionDTO question)
        {
            await questionService.UpdateAsync(question);

            return RedirectToAction(nameof(List));
        }
        
        [HttpGet]
        public IActionResult List()
        {
            var questions = questionService.GetAll();
            return View(questions);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var question = questionService.GetById(id);
            return View(question);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await questionService.MarkAsDeletedByIdAsync(id);
            var questions = questionService.GetAll();
            return View("List", questions);
        }
    }
}
