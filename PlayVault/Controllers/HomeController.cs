using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PlayVault.Models;

namespace PlayVault.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var name = HttpContext.Session.GetString(SessionKeys.SessionKeyName);
            var age = HttpContext.Session.GetInt32(SessionKeys.SessionKeyAge);

            if (!string.IsNullOrEmpty(name))
            {
                ViewData["UserName"] = name;
                ViewData["UserAge"] = age;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Terms()
        {
            return View();
        }

        public IActionResult Ufficio()
        {
            return View();
        }

        public IActionResult Donazioni()
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
