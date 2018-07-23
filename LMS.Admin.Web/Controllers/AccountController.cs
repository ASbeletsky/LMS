using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using LMS.Admin.Web.ViewModels;
using LMS.Identity;
using LMS.Entities;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace LMS.Admin.Web.Controllers
{
    public class AccountController : Controller
    {
        private  IdentityService _identityService;

        public AccountController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult AccessDenied(Uri ReturnUrl)
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Register()
        {
            ViewData["AllRoles"] = _identityService.GetAllRoles().Select(t => new SelectListItem() { Value = t.Name, Text = t.Name});
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { FirstName = model.FirstName, LastName = model.LastName, UserName = model.UserName };
                // add user
                try
                {
                    await _identityService.Register(user, model.Password, model.Role);
                    return View(model);
                }
                catch(AggregateException e)
                {
                    foreach (Exception ex in e.InnerExceptions)
                        ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewData["AllRoles"] = _identityService.GetAllRoles().Select(t => new SelectListItem() { Value = t.Name, Text = t.Name });
            return View(model);
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
                    await _identityService.LogIn(model.UserName, model.Password, model.RememberMe);

                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    else
                        return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Login", "Account");
        }
    }
}
