using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using LMS.Admin.Web.ViewModels;
using LMS.Identity;
using LMS.Entities;
using System;

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
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, UserName = model.Email };
                // add user
                try
                {
                    var result = await _identityService.Register(user, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch(AggregateException e)
                {
                    foreach (Exception ex in e.InnerExceptions)
                        ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
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
                var result =
                await _identityService.LogIn(model.Email, model.Password, model.RememberMe);

                if (result == true)
                {   
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
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
