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
            new { title = "Event 1", start = "2024-10-01" },
            new { title = "Event 2", start = "2024-10-10" },
        };

            return new JsonResult(events);
        }
    }
}
