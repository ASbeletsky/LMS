using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using LMS.Dto;
using LMS.Identity;
using LMS.Business.Services;

namespace LMS.Admin.Web.Controllers
{
    [Authorize(Roles = "admin, moderator, reviewer")]
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

            if (testSession.StartTime < DateTimeOffset.Now
                && !HttpContext.User.IsInRole(Roles.Admin))
            {
                return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
            }

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
            if (testSession.StartTime < DateTimeOffset.Now
                && !HttpContext.User.IsInRole(Roles.Admin))
            {
                return RedirectToAction(nameof(AccountController.AccessDenied), "Account");
            }

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
        public IActionResult Details(int id)
        {
            var testSessionResult = testSessionService.GetResults(id);
            return View(testSessionResult);
        }

        [HttpGet]
        public IActionResult ResultDetails(int sessionId, string id)
        {
            var testSessionResult = testSessionService.GetExameneeResult(sessionId, id);
            return View(testSessionResult);
        }

        [HttpPost]
        public async Task<IActionResult> SaveResultDetails(ICollection<TaskAnswerScoreDTO> taskAnswerScores)
        {
            var reviewerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await testSessionService.SaveAnswerScoresAsync(taskAnswerScores, reviewerId);

            return RedirectToAction(nameof(List));
        }
    }
}
