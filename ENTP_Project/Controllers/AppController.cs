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

        public IActionResult Workouts() { 
            return View();
        }

        public IActionResult Meals()
        {
            return View();
        }
        public IActionResult Profile()
        {
            var claims = User.Claims;

            // Check if the claims are being correctly retrieved
            var name = claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var email = claims.FirstOrDefault(c => c.Type == "email")?.Value;
            var phone = claims.FirstOrDefault(c => c.Type == "phone")?.Value;
            var role = claims.FirstOrDefault(c => c.Type == "role")?.Value;
            var diet = claims.FirstOrDefault(c => c.Type == "diet")?.Value;
            var plan = claims.FirstOrDefault(c => c.Type == "plan")?.Value;

            // Log the values for debugging
            Console.WriteLine($"Name: {name}, Email: {email}, Phone: {phone}, Role: {role}, Diet: {diet}, Plan: {plan}");

            // Create the model with the claims data
            var model = new RegistrationModel
            {
                Name = name,
                Email = email,
                Phone = phone,
                Role = role,
                Diet = diet,
                Plan = plan
            };

            // Pass the model to the view
            return View(model);
        }


        public IActionResult Settings()
        {
            return View();
        }
    }
}
