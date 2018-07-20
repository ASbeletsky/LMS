using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LMS.Dto;
using LMS.Business.Services;
using Newtonsoft.Json;

namespace LMS.Admin.Web.Controllers
{
    public class TestTemplateController : Controller
    {
        private readonly TestTemplateService testTemplateService;
        private readonly TaskTypeService taskTypeService;
        private readonly CategoryService categoryService;

        public TestTemplateController(
            TestTemplateService testTemplates,
            TaskTypeService taskTypes,
            CategoryService taskCategories)
        {
            testTemplateService = testTemplates;
            taskTypeService = taskTypes;
            categoryService = taskCategories;
        }

        public IActionResult Create()
        {
            ViewData["AvailableTypes"] = taskTypeService.GetAll().Select(t => new SelectListItem()
            {
                Value = t.Id.ToString(),
                Text = t.Title
            });
            ViewData["AvailableCategories"] = categoryService.GetAll().Select(t => new SelectListItem()
            {
                Value = t.Id.ToString(),
                Text = t.Title
            });

            return View(new TestTemplateDTO()
            {
                Levels =
                {
                    new TestTemplateLevelDTO()
                }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] TestTemplateDTO template)
        {
            await testTemplateService.CreateAsync(template);

            return RedirectToAction(nameof(List));
        }

        public IActionResult Edit(int id)
        {
            var template = TempData["EditTemplate" + id] is string templateStr
                ? JsonConvert.DeserializeObject<TestTemplateDTO>(templateStr)
                : testTemplateService.GetById(id);

            ViewData["AvailableTypes"] = taskTypeService.GetAll().Select(t => new SelectListItem()
            {
                Value = t.Id.ToString(),
                Text = t.Title
            });
            ViewData["AvailableCategories"] = categoryService.GetAll().Select(t => new SelectListItem()
            {
                Value = t.Id.ToString(),
                Text = t.Title
            });

            return View(template);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewLevel([FromForm] TestTemplateDTO template)
        {
            template.Levels.Add(new TestTemplateLevelDTO());
            TempData["EditTemplate" + template.Id] = JsonConvert.SerializeObject(template);

            return RedirectToAction("Edit", new { id = template.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] TestTemplateDTO template)
        {
            await testTemplateService.UpdateAsync(template);

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult List()
        {
            var templateListItems = testTemplateService.GetListItems();
            return View(templateListItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await testTemplateService.DeleteByIdAsync(id);
            var templateListItems = testTemplateService.GetListItems();
            return View("List", templateListItems);
        }
    }
}
