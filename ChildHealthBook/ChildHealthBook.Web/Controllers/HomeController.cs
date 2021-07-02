using ChildHealthBook.Common.Identity.DTOs;
using ChildHealthBook.Web.CookieServices.Validator;
using ChildHealthBook.Web.Models;
using ChildHealthBook.Web.Models.Errors;
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

        public IActionResult CustomError(CustomErrorModel errorInfo)
        {
            _logger.LogInformation($"{errorInfo.Details}, {errorInfo.ErrorHeader}, {errorInfo.PossibleSolutions}");
            return View(errorInfo);
        }
        public IActionResult Login()
        {
            if (!_cookieValidator.IsCookiePresent(Request))
            {
                _logger.LogInformation("Rendering login form...");
                return View(new UserLoginDTO());
            }
            _logger.LogInformation("User tried to render login view but cookie is already present, redirect...");
            return RedirectToAction("Index", "Home");

        }
        public IActionResult RegisterParent()
        {
            if (!_cookieValidator.IsCookiePresent(Request))
            {
                _logger.LogInformation("Rendering register parent form...");
                return View(new ParentRegisterDTO());
            }

            _logger.LogInformation("User tried to render register parent view but cookie is already present, redirect...");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult RegisterScientist()
        {
            if (!_cookieValidator.IsCookiePresent(Request))
            {
                if (_cookieValidator.IsRoleValid(Request, "Scientist"))
                {
                    _logger.LogInformation("Rendering register scientist form...");
                    return View(new UserRegisterDTO());
                }
                _logger.LogInformation("User tried to render register scientist form but his role is invalid.");
            }
            _logger.LogInformation("Register scientist form fail due to user invalid access, redirect...");
            return RedirectToAction("Index", "Home");
        }
    }
}
