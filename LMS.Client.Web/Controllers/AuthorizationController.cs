using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS.Identity;
using LMS.Client.Web.Models;
namespace LMS.Client.Web.Controllers
{
    public class AuthorizationController : Controller
    {
        private IdentityService _identityService;

        public AuthorizationController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult AccessDenied(Uri ReturnUrl)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _identityService.LogInClient(model.Code);

                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    else
                        return RedirectToAction("Greetings", "Home");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _identityService.Logout();
            return RedirectToAction("Login", "Authorization");
        }
    }
}
