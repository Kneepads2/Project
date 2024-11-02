using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            new { title = "Zoowemama", start = "2024-10-10" },
            new { title = "Urban Nightmare", start = "2024-10-20"},
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

            //check if the claims are being correctly retrieved
            var name = claims.FirstOrDefault(c => c.Type == "name")?.Value ?? claims.FirstOrDefault(c => c.Type == "email")?.Value;
            //var name = claims.FirstOrDefault(c => c.Type == "name")?.Value;
            //var email = claims.FirstOrDefault(c => c.Type == "https://dev-y6sgst5cqueqdc0x.us.auth0.com/email")?.Value;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            var phone = claims.FirstOrDefault(c => c.Type == "phone")?.Value;
            var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;
            var diet = claims.FirstOrDefault(c => c.Type == "diet")?.Value;
            var plan = claims.FirstOrDefault(c => c.Type == "plan")?.Value;
            var weightStr = claims.FirstOrDefault(c => c.Type == "weight")?.Value;

            int? weight = null;
            if (int.TryParse(weightStr, out int parsedWeight))
            {
                weight = parsedWeight;
            }
           
            Console.WriteLine($"Name: {name}, Email: {email}, Phone: {phone}, Role: {role}, Diet: {diet}, Plan: {plan}, Weight: {weight}");       

            if (email == "tradylan@sheridancollege.ca") //making an admin role
            {
                var adminModel = new RegistrationModel
                {
                    Name = "Dylan Tran",
                    Email = email,
                    Phone = "+1 647-806-8297",
                    Role = "Admin",
                    Diet = "None",
                    Plan = "Premium",
                    Weight = 140,
                };
                return View(adminModel);
            }

            else {
                var model = new RegistrationModel
                {
                    Name = name,
                    Email = email,
                    Phone = phone,
                    Role = role,
                    Diet = diet,
                    Plan = plan,
                    Weight = weight,
                };
                return View(model);
            }
            
        }

        public IActionResult ProfileChange()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }

        public IActionResult Database()
        {
            return View();
        }
    }
}
