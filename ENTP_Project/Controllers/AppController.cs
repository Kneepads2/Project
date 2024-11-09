using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using ENTP_Project.Models;

//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

//Dylan Tran
namespace ENTP_Project.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Homepage() //returns homepage
        {
            DefineAdmin(); 
            return View();
        }

        public JsonResult GetEvents() //a list of events marked on the calendar, unfinished
        {
            var events = new List<object>
        {
            new { title = "Ballet Class", start = "2024-11-01" },
            new { title = "Zoowemama", start = "2024-11-10" },
            new { title = "Urban Nightmare", start = "2024-11-20"},
        };

            return new JsonResult(events);
        }

        public IActionResult Workouts() {// returns Workouts view
            DefineAdmin();
            return View();
        }

        public IActionResult Meals() //returns Meals view
        {
            DefineAdmin();
            return View();
        }
        public IActionResult Profile() //getting user data from Auth0 and displaying it
        {
            var claims = User.Claims; //Auth0 functions

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

            if (email == "tradylan@sheridancollege.ca" || email == "ou80wikcvsuw@fakemailserver.com") //making an admin role based on the email
            {
                var adminModel = new RegistrationModel
                {
                    Name = name,
                    Email = email,
                    Phone = phone,
                    Role = "Admin",
                    Diet = diet,
                    Plan = plan,
                    Weight = weight,
                };
                DefineAdmin();
                return View(adminModel);
            }

            else { //if your email isnt an admin email, the form you filled out will be your profile
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
                DefineAdmin();
                return View(model);
            }
            
        }

        public IActionResult ProfileChange() //returns profile change form
        {
            DefineAdmin();
            return View();
        }

        public IActionResult Settings() //returns settings view, unfinished
        {
            DefineAdmin();
            return View();
        }

        public IActionResult Database() //returns Database, unfinished because my partner didnt do any work
        {
            DefineAdmin();
            return View();
        }

        public IActionResult CreateMeal()
        {
            DefineAdmin();
            return View();
        }

        public IActionResult CreateWorkout()
        {
            DefineAdmin();
            return View();
        }

        public IActionResult ViewMeal()
        {
            DefineAdmin();
            return View();
        }

        public IActionResult ViewWorkout()
        {
            DefineAdmin();
            return View();
        }

        public IActionResult Chat()
        {
            DefineAdmin();
            return View();
        }
        private void DefineAdmin() //function to create an admin. Admins gain access to the Admin Panel. 
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            object role = null;
            if (email == "tradylan@sheridancollege.ca" || email == ("oU80WIkcvSUW@fakemailserver.com").ToLower())//TO BE AN ADMIN, YOUR EMAIL MUST BE LISTED HERE
            {
                role = "Admin";
            }
            else
            {
                role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;
            }
            
            ViewData["UserRole"] = role;
        }
    }
}
