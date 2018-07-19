using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LMS.Admin.Web.ViewModels;


namespace LMS.Admin.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if(!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
