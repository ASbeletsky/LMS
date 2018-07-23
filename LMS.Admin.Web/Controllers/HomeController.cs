using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LMS.Admin.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LMS.Admin.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {  
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
