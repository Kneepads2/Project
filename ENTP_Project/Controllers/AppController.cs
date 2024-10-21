using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ENTP_Project.Models;

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
            var claims = User.Claims;

            // Extract the 'name' claim (if you stored the name in user metadata)
            var name = claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var email = claims.FirstOrDefault(c => c.Type == "email")?.Value;
            var phone = claims.FirstOrDefault(c => c.Type == "phone")?.Value;
            var role = claims.FirstOrDefault(c => c.Type == "role")?.Value;
            var diet = claims.FirstOrDefault(c => c.Type == "diet")?.Value;
            var plan = claims.FirstOrDefault(c => c.Type == "plan")?.Value;

            // Pass the name to the view or use it directly in your logic
            var model = new RegistrationModel
            {
                Name = name,
                Phone = phone,
                Role = role,
                Diet = diet,
                Plan = plan
            };
            return View(model);
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}
