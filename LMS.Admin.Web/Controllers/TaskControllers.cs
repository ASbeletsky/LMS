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
        private readonly TaskService taskService;
        private readonly TaskTypeService taskTypeService;
        private readonly CategoryService categoryService;

        private readonly IMapper dtoMapper;

        public TaskController(
            TaskService tasks, 
            TaskTypeService taskTypes,
            CategoryService taskCategories,
            IMapper mapper)
        {
            taskService = tasks;
            taskTypeService = taskTypes;
            categoryService = taskCategories;
            dtoMapper = mapper;
        }

        public IActionResult Create()
        {
            ViewData["AvailableTypes"] = taskTypeService.GetAll().Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });
            ViewData["AvailableCategories"] = categoryService.GetAll().Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]AnswerDTO task)
        {
            await taskService.CreateAsync(task);

            return RedirectToAction(nameof(List));
        }
       
        public IActionResult Edit(int id)
        {
            var task = taskService.GetById(id);

            ViewData["AvailableTypes"] = taskTypeService.GetAll().Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });
            ViewData["AvailableCategories"] = categoryService.GetAll().Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm]AnswerDTO task)
        {
            await taskService.UpdateAsync(task);

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult List()
        {
            var tasks = taskService.GetAll();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var task = taskService.GetById(id);
            return View(task);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await taskService.MarkAsDeletedByIdAsync(id);
            var tasks = taskService.GetAll();
            return View("List", tasks);
        }
    }
}
