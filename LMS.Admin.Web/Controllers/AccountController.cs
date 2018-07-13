using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LMS.Admin.Web.ViewModels;
using LMS.Business.Services;
using LMS.Entries;

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
                var user = new User { Email = model.Email, Name = model.Name, Surname = model.Surname, UserName = model.Email };
                // добавляем пользователя
                var errors = await _identityService.Register(user, model.Password);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
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
                    // проверяем, принадлежит ли URL приложению
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
        public async Task<IActionResult> LogOff()
        {
            await _identityService.LogOff();
            return RedirectToAction("Index", "Home");
        }
    }
}
