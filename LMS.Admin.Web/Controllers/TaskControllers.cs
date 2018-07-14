using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LMS.Dto;
using LMS.Interfaces;
using LMS.Business.Services;

namespace LMS.Admin.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskService questionService;
        private readonly TaskTypeService questionTypeService;
        private readonly CategoryService questionCategoryService;

        private readonly IMapper dtoMapper;

        public TaskController(
            TaskService questions, 
            TaskTypeService questionTypes,
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
            ViewData["AvailableTypes"] = questionTypeService.GetAll().Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });
            ViewData["AvailableCategories"] = questionCategoryService.GetAll().Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]TaskDTO question)
        {
            await questionService.CreateAsync(question);

            return RedirectToAction(nameof(List));
        }
       
        public IActionResult Edit(int id)
        {
            var question = questionService.GetById(id);

            ViewData["AvailableTypes"] = questionTypeService.GetAll().Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });
            ViewData["AvailableCategories"] = questionCategoryService.GetAll().Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm]TaskDTO question)
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
