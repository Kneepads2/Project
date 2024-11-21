using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using ENTP_Project.Models;
using ENTP_Project.Data;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Auth0.ManagementApi.Models;
using Microsoft.Extensions.Options;

//Dylan Tran
namespace ENTP_Project.Controllers
{
    public class AppController : Controller
    {
        private readonly AppDbContext _context;
        public AppController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Homepage() //returns homepage
        {

            var claims = User.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            var userCheck = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (userCheck == null)
            {
                return Redirect("Welcome");
            }

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

        public IActionResult Workouts()
        {// returns Workouts view
            DefineAdmin();
            return View();
        }

        public IActionResult Meals() //returns Meals view
        {
            DefineAdmin();
            return View();
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

        // [HttpGet("App/Database")]
        // public IActionResult Database() //returns Database, unfinished because my partner didnt do any work
        // {
        //     var users = _context.Users.ToList();
        //     DefineAdmin();
        //     return View(users);
        // }

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

        public IActionResult MyLibrary()
        {
            DefineAdmin();
            return View();
        }

        [HttpGet]
        public IActionResult Welcome()
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
                var adminModel = new UserModel
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

            else
            { //if your email isnt an admin email, the form you filled out will be your profile
                var model = new UserModel
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
            //return View();
        }

        [HttpPost("App/Welcome")]
        public async Task<IActionResult> Welcomed(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var claims = User.Claims;
                model.Name = claims.FirstOrDefault(c => c.Type == "name")?.Value ?? claims.FirstOrDefault(c => c.Type == "email")?.Value;
                model.Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
                model.Phone = claims.FirstOrDefault(c => c.Type == "phone")?.Value;

                if (model.Email == "tradylan@sheridancollege.ca" || model.Email == ("oU80WIkcvSUW@fakemailserver.com").ToLower())//TO BE AN ADMIN, YOUR EMAIL MUST BE LISTED HERE
                {
                    model.Role = "Admin";
                }
                else
                {
                    model.Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value;
                }

                model.Diet = claims.FirstOrDefault(c => c.Type == "diet")?.Value;
                model.Plan = claims.FirstOrDefault(c => c.Type == "plan")?.Value;
                var weightStr = claims.FirstOrDefault(c => c.Type == "weight")?.Value;
                model.Weight = Int32.Parse(weightStr);
                try
                {
                    _context.Users.Add(model);  // Add newUser to the database
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Homepage", "App");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving data: {ex.Message}");
                    Console.WriteLine($"Error: {ex.Message}, Inner Exception: {ex.InnerException?.Message}");
                    return StatusCode(500, new { message = "Internal server error" });
                }
            }
            return View(model); // Return the same view if ModelState is invalid
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
