using ChildHealthBook.Common.Identity.DTOs;
using ChildHealthBook.Web.CookieServices.Validator;
using ChildHealthBook.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ChildHealthBook.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserSessionCookieValidator _cookieValidator;

        public HomeController(ILogger<HomeController> logger,
            UserSessionCookieValidator userSessionValidator)
        {
            _logger = logger;
            _cookieValidator = userSessionValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            if (!_cookieValidator.IsCookiePresent(Request))
            {
                return View(new UserLoginDTO());
            }
            return RedirectToAction("Index", "Home");

        }
        public IActionResult RegisterParent()
        {
            if (!_cookieValidator.IsCookiePresent(Request))
            {
                return View(new ParentRegisterDTO());
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult RegisterScientist()
        {
            if (!_cookieValidator.IsCookiePresent(Request))
            {
                return View(new UserRegisterDTO());
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
