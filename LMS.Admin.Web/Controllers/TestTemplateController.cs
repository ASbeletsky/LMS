using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using LMS.Dto;
using LMS.Business.Services;
using Newtonsoft.Json;

namespace LMS.Admin.Web.Controllers
{
    [Authorize(Roles = "admin, moderator, reviewer")]
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

        [Authorize(Roles = "admin, moderator")]
        public IActionResult Create()
        {
            TestTemplateDTO template;
            if (TempData["EditTemplate0"] is string templateStr)
            {
                template = JsonConvert.DeserializeObject<TestTemplateDTO>(templateStr);
            }
            else
            {
                template = new TestTemplateDTO
                {
                    Levels =
                    {
                        new TestTemplateLevelDTO()
                    }
                };
            }
            
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
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Create([FromForm] TestTemplateDTO template)
        {
            await testTemplateService.CreateAsync(template);

            return RedirectToAction(nameof(List));
        }

        [Authorize(Roles = "admin, moderator")]
        public IActionResult Edit(int id)
        {
            TestTemplateDTO template;
            if (TempData["EditTemplate" + id] is string templateStr)
            {
                template = JsonConvert.DeserializeObject<TestTemplateDTO>(templateStr);
            }
            else
            {
                template = testTemplateService.GetById(id);
            }

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
        [Authorize(Roles = "admin, moderator")]
        public IActionResult NewLevel([FromForm] TestTemplateDTO template)
        {
            template.Levels.Add(new TestTemplateLevelDTO());
            TempData["EditTemplate" + template.Id] = JsonConvert.SerializeObject(template);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
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
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Delete(int id)
        {
            await testTemplateService.DeleteByIdAsync(id);
            var templateListItems = testTemplateService.GetListItems();
            return View("List", templateListItems);
        }
    }
}
