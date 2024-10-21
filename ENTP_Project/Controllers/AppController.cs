using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;


namespace ENTP_Project.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Homepage()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            var events = new List<object>
        {
            new { title = "Ballet Class", start = "2024-10-01" },
            new { title = "Zwei Meeting", start = "2024-10-10" },
            new { title = "Urban Nightmare Extermination", start = "2024-10-20"},
        };

            return new JsonResult(events);
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}
