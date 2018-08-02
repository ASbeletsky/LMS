using System;
using System.Linq;
using System.Threading.Tasks;
using LMS.Business.Services;
using LMS.Dto;
using LMS.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace LMS.Admin.Web.Controllers
{
    [Authorize(Roles = "admin, moderator, reviewer, examenee")]
    public class TestSessionController : Controller
    {
        private readonly TestSessionService testSessionService;
        private readonly TestTemplateService testTemplateService;
        private readonly IdentityService identityService;

        public TestSessionController(
            TestSessionService testSessions,
            TestTemplateService testTemplates,
            IdentityService identity)
        {
            testSessionService = testSessions;
            testTemplateService = testTemplates;
            identityService = identity;
        }

        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Create(DateTimeOffset? startTime = null)
        {
            var test = new TestSessionDTO()
            {
                StartTime = startTime ?? DateTimeOffset.Now
            };

            ViewData["Templates"] = testTemplateService
                .GetAll()
                .Select(template => new SelectListItem
                {
                    Value = template.Id.ToString(),
                    Text = template.Title
                });
            ViewData["Users"] = (await identityService
                .GetAllAsync(Roles.Examinee))
                .Select(user => new SelectListItem
                {
                    Value = user.Id,
                    Text = user.Name
                });

            return View(test);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Create([FromForm] TestSessionDTO testSession)
        {
            await testSessionService.CreateAsync(testSession);

            return RedirectToAction(nameof(List));
        }

        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Edit(int id)
        {
            var testSession = testSessionService.GetById(id);

            ViewData["Templates"] = testTemplateService
                .GetAll()
                .Select(template => new SelectListItem
                {
                    Value = template.Id.ToString(),
                    Text = template.Title
                });
            ViewData["Users"] = (await identityService
                .GetAllAsync(Roles.Examinee))
                .Select(user => new SelectListItem
                {
                    Value = user.Id,
                    Text = user.Name
                });

            return View(testSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Edit([FromForm] TestSessionDTO testSession)
        {
            await testSessionService.UpdateAsync(testSession);

            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Delete(int id)
        {
            await testSessionService.DeleteByIdAsync(id);

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult List()
        {
            var testSessions = testSessionService.GetAll();
            return View(testSessions);
        }

        [HttpGet]
        public IActionResult Interactive(int id)
        {
            var testSession = testSessionService.GetById(id);
            return View(testSession);
        }
    }
}
