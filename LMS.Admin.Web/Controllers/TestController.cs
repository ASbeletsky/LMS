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
    public class TestController : Controller
    {
        private readonly TestService testService;
        private readonly TestTemplateService testTemplateService;

        public TestController(
            TestService tests,
            TestTemplateService testTemplates)
        {
            testService = tests;
            testTemplateService = testTemplates;
        }

        [Authorize(Roles = "admin, moderator")]
        public IActionResult Create(int? templateId = null)
        {
            var test = new TestDTO();

            var templates = testTemplateService
                .GetTemplatesSummary()
                .ToArray();

            ViewData["Templates"] = templates
                .Select(template => new SelectListItem
                {
                    Value = template.Id.ToString(),
                    Text = template.Title
                });

            templateId = templateId ?? templates.FirstOrDefault()?.Id;
            if (templateId.HasValue)
            {
                testService.BindToTemplate(test, templateId.Value);
            }

            return View(test);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Create([FromForm] TestDTO test)
        {
            await testService.CreateAsync(test);

            return RedirectToAction(nameof(List));
        }

        [Authorize(Roles = "admin, moderator")]
        public IActionResult Edit(int id, int? templateId = null)
        {
            var test = testService.GetById(id);

            var templates = testTemplateService
                .GetTemplatesSummary()
                .ToArray();

            ViewData["Templates"] = templates
                .Select(template => new SelectListItem
                {
                    Value = template.Id.ToString(),
                    Text = template.Title
                });
             
            templateId = templateId ?? test.TestTemplateId ?? templates.FirstOrDefault()?.Id;
            if (templateId.HasValue)
            {
                testService.BindToTemplate(test, templateId.Value);
            }

            return View(test);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Edit([FromForm] TestDTO test)
        {
            await testService.UpdateAsync(test);

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult List()
        {
            var tests = testService.GetAll();
            return View(tests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Delete(int id)
        {
            await testService.DeleteByIdAsync(id);

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Filter(int templateId)
        {
            var tests = testService.FilterByTemplate(templateId);
            return Json(tests);
        }
    }
}
