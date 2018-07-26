using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Admin.Web.Controllers
{
    public class TestSessionController : Controller
    {
        public IActionResult List()
        {
            var session = new List<TestSessionDTO>
            {
                new TestSessionDTO()
                {
                    Id = 0,
                    StartTime = DateTimeOffset.Now,
                    Duration = TimeSpan.FromHours(2),
                    Title = "Session #1"
                },
                new TestSessionDTO()
                {
                    Id = 1,
                    StartTime = DateTimeOffset.Now - TimeSpan.FromDays(2),
                    Duration = TimeSpan.FromHours(1.4),
                    Title = "Session #2"
                },
                new TestSessionDTO()
                {
                    Id = 2,
                    StartTime = DateTimeOffset.Now + TimeSpan.FromDays(2),
                    Duration = TimeSpan.FromHours(2.3),
                    Title = "Session #3"
                }
            };

            return View(session);
        }
    }
}
