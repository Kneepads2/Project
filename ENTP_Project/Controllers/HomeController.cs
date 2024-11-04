using Auth0.AspNetCore.Authentication;
using ENTP_Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

//Dylan Tran, 11/3/2024
namespace ENTP_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login() //login function
        {
            var redirectUri = Url.Action("Homepage", "App"); //once logged in, redirects to /App/Homepagw
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUri }, "Auth0"); //we use Auth0 to help with the login
        }

        public IActionResult Logout()
        {
            var callbackUrl = Url.Action("LoggedOut", "Home"); //redirects to the Logout page once logged out
            return SignOut(new AuthenticationProperties { RedirectUri = callbackUrl },
                           CookieAuthenticationDefaults.AuthenticationScheme, "Auth0");
        }

        public IActionResult LoggedOut() //returns logout page
        {
            return View();
        }

        public IActionResult Index() //returns frontpage view
        {
            DefineAdmin();
            return View();
        }

        public IActionResult Privacy() //returns privacy view
        {
            DefineAdmin();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
