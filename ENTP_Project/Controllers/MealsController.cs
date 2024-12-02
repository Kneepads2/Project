﻿using ENTP_Project.Data;
using ENTP_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ENTP_Project.Controllers
{
    public class MealsController : Controller
    {
        private readonly AppDbContext _context;
        public MealsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateMeal()
        {
            DefineAdmin();
            return View();
        }

        [HttpPost("Meals/CreateMeal")]
        public async Task<IActionResult> PostMeal(MealModel meal)
        {
            Console.WriteLine("in postmeal");
            var claims = User.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            var userCheck = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            meal.UserId = userCheck.Id;
            //if (ModelState.IsValid) {

            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction("Meals", "Meals");
            //}
            Console.WriteLine($"{meal.Title},{meal.Diet}, {meal.ImageUrl}, {meal.UserId}, {meal.Description}");
            //return View("CreateMeal", meal);
        }

        [HttpGet]
        public IActionResult Meals() //returns Meals view
        {
            var meals = _context.Meals.ToList();
            DefineAdmin();
            return View(meals);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal != null)
            {
                _context.Meals.Remove(meal);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Meals");
        }

        [HttpGet]
        public IActionResult EditMeal(int id)
        {
            var meal = _context.Meals.Find(id);
            DefineAdmin();
            return View(meal);
        }

        [HttpPost]
        public async Task<IActionResult> EditMeal(MealModel meal)
        {
            var claims = User.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            var userCheck = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            meal.UserId = userCheck.Id;
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction("Meals");
        }

        public IActionResult ViewMeal()
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