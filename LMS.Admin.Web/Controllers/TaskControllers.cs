using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using LMS.Dto;
using LMS.Business.Services;

namespace LMS.Admin.Web.Controllers
{
    [Authorize(Roles = "admin, moderator, reviewer")]
    public class TaskController : Controller
    {
        private readonly TaskService taskService;
        private readonly TaskTypeService taskTypeService;
        private readonly CategoryService categoryService;

        public TaskController(
            TaskService tasks,
            TaskTypeService taskTypes,
            CategoryService taskCategories)
        {
            taskService = tasks;
            taskTypeService = taskTypes;
            categoryService = taskCategories;
        }

        [Authorize(Roles = "admin, moderator")]
        public IActionResult Create()
        {
            ViewData["AvailableTypes"] = taskTypeService.GetAll().Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });
            ViewData["AvailableCategories"] = categoryService.GetAll().Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Create([FromForm]TaskDTO task)
        {
            await taskService.CreateAsync(task);

            return RedirectToAction(nameof(List));
        }

        [Authorize(Roles = "admin, moderator")]
        public IActionResult Edit(int id)
        {
            var task = taskService.GetById(id);

            ViewData["AvailableTypes"] = taskTypeService.GetAll().Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });
            ViewData["AvailableCategories"] = categoryService.GetAll().Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.Title });
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Edit([FromForm]TaskDTO task)
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
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Delete(int id)
        {
            await taskService.MarkAsDeletedByIdAsync(id);
            var tasks = taskService.GetAll();
            return View("List", tasks);
        }

        [HttpPost]
        public IActionResult Filter([FromForm]TaskFilterDTO filter)
        {
            var filtered = taskService.Filter(filter).ToList();
            return Json(new
            {
                tasks = filtered,
                count = filtered.Count
            });
        }
    }
}
