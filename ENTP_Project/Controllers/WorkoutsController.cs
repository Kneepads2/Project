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

        public async Task<IActionResult> ViewWorkout(int workoutId)
        {
            var claims = User.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            var userCheck = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            var userPlan = userCheck.Plan;
            var userRole = userCheck.Role;
            DefineAdmin();
            if (userPlan == "Free" && userRole == "User")
            {
                return Ok(new { message = "To view this fitness routine, please upgrade your Subscription Plan! This may be accomplished by visiting your profile!" });
            }
            else
            {
                var workout = _context.Workouts.Find(workoutId);
                Console.WriteLine(workout);
                if (workout == null)
                {
                    return NotFound();
                }
                return View(workout);
            }
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

        [HttpPost]
        public async Task<IActionResult> AddToLibrary(int workoutId)
        {
            var claims = User.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;
            var userCheck = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            var userId = userCheck.Id;
            var user = await _context.Users
                                     .Include(u => u.MyWorkouts)  // Ensure meals are loaded
                                     .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var workout = await _context.Workouts.FindAsync(workoutId);
            if (workout == null)
            {
                return NotFound("Meal not found");
            }

            // Check if the meal is already added
            if (!user.MyWorkouts.Any(m => m.Id == workoutId))
            {
                user.MyWorkouts.Add(workout);
                await _context.SaveChangesAsync();
            }

            return Ok(new { message = "Workout added to library!" });
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
