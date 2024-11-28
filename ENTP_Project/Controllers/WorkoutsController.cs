using ENTP_Project.Data;
using ENTP_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ENTP_Project.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly AppDbContext _context;
        public WorkoutsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateWorkout()
        {
            DefineAdmin();
            return View();
        }

        [HttpPost("Workouts/CreateWorkout")]
        public async Task<IActionResult> PostWorkout(WorkoutModel workout)
        {

            var claims = User.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            var userCheck = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            workout.UserId = userCheck.Id;

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return RedirectToAction("Workouts", "Workouts");

        }

        public IActionResult Workouts()
        {// returns Workouts view
            var workouts = _context.Workouts.ToList();
            DefineAdmin();
            return View(workouts);
        }

        [HttpGet]
        public IActionResult EditWorkout(int id)
        {
            var workout = _context.Workouts.Find(id);
            DefineAdmin();
            return View(workout);
        }

        [HttpPost]
        public async Task<IActionResult> EditWorkout(WorkoutModel workout)
        {
            var claims = User.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            var userCheck = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            workout.UserId = userCheck.Id;
            _context.Workouts.Update(workout);
            await _context.SaveChangesAsync();
            return RedirectToAction("Workouts");
        }

        public IActionResult ViewWorkout()
        {
            DefineAdmin();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout != null)
            {
                _context.Workouts.Remove(workout);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Workouts");
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
