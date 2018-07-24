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
    public class TestVariantController : Controller
    {
        private readonly TestVariantService testVariantService;
        private readonly TestTemplateService testTemplateService;

        public TestVariantController(
            TestVariantService testVariants,
            TestTemplateService testTemplates)
        {
            testVariantService = testVariants;
            testTemplateService = testTemplates;
        }

        [Authorize(Roles = "admin, moderator")]
        public IActionResult Create(int? templateId = null)
        {
            var variant = new TestVariantDTO();

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
                testVariantService.BindToTemplate(variant, templateId.Value);
            }

            return View(variant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Create([FromForm] TestVariantDTO variant)
        {
            await testVariantService.CreateAsync(variant);

            return RedirectToAction(nameof(List));
        }

        [Authorize(Roles = "admin, moderator")]
        public IActionResult Edit(int id, int? templateId = null)
        {
            var variant = testVariantService.GetById(id);

            var templates = testTemplateService
                .GetTemplatesSummary()
                .ToArray();

            ViewData["Templates"] = templates
                .Select(template => new SelectListItem
                {
                    Value = template.Id.ToString(),
                    Text = template.Title
                });
             
            templateId = templateId ?? variant.TestTemplateId ?? templates.FirstOrDefault()?.Id;
            if (templateId.HasValue)
            {
                testVariantService.BindToTemplate(variant, templateId.Value);
            }

            return View(variant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Edit([FromForm] TestVariantDTO variant)
        {
            await testVariantService.UpdateAsync(variant);

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult List()
        {
            var variants = testVariantService.GetAll();
            return View(variants);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Delete(int id)
        {
            await testVariantService.DeleteByIdAsync(id);

            return RedirectToAction(nameof(List));
        }
    }
}
