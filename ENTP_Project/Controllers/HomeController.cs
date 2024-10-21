using Auth0.AspNetCore.Authentication;
using ENTP_Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ENTP_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            var redirectUri = Url.Action("Homepage", "App");
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUri }, "Auth0");
        }

        public IActionResult Logout()
        {
            var callbackUrl = Url.Action("LoggedOut", "Home");
            return SignOut(new AuthenticationProperties { RedirectUri = callbackUrl },
                           CookieAuthenticationDefaults.AuthenticationScheme, "Auth0");
        }

        public IActionResult LoggedOut()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ViewResult Onboarding()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
