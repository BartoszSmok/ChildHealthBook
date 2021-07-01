using ChildHealthBook.Common.Identity.DTOs;
using ChildHealthBook.Web.Communication;
using ChildHealthBook.Web.CookieServices.Serializers;
using ChildHealthBook.Web.CookieServices.Validator;
using ChildHealthBook.Web.Models.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Controllers
{
    public class AccountsController : Controller
    {
        IIdentityRegisterCommunication<ParentRegisterDTO> _parentRegisterHandler;
        IIdentityRegisterCommunication<UserRegisterDTO> _userRegisterHandler;
        private IdentityTokenCommunication _tokenFetcherHandler;
        private AuthenticatedUserSessionBuilder _authCookieBuilder;
        private UserSessionCookieValidator _cookieValidator;
        private ILogger<AccountsController> _logger;

        public AccountsController(IIdentityRegisterCommunication<ParentRegisterDTO> parentRegisterHandler,
            IIdentityRegisterCommunication<UserRegisterDTO> userRegisterHandler,
            IdentityTokenCommunication tokenFetcherHandler,
            AuthenticatedUserSessionBuilder authCookieBuilder,
            UserSessionCookieValidator cookieValidator,
            ILogger<AccountsController> logger)
        {
            _parentRegisterHandler = parentRegisterHandler;
            _userRegisterHandler = userRegisterHandler;
            _tokenFetcherHandler = tokenFetcherHandler;
            _authCookieBuilder = authCookieBuilder;
            _cookieValidator = cookieValidator;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            if (_cookieValidator.IsCookiePresent(Request))
            {
                _logger.LogInformation("User tried to login but user session cookie is already present, redirect...");
                return RedirectToAction("Index", "Home");
            }
            try
            {
                _logger.LogInformation("Fetching user token...");
                string token = await _tokenFetcherHandler.Login(loginDTO);
                if (!string.IsNullOrEmpty(token))
                {
                    string authUserCookie = _authCookieBuilder.BuildCookieString(token);
                    var options = new CookieOptions() { Expires = DateTime.Now.AddMinutes(30) };
                    Response.Cookies.Append("UserData", authUserCookie, options);
                    _logger.LogInformation("Successfully fetched user token and added user session.");
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in Login in AccountsController: {e.Message}");
                return RedirectToAction("Error", "Home");
            }

            _logger.LogError("User provided invalid credentials, redirect to information view...");
            return RedirectToAction("CustomError", "Home",
                new
                {
                    errorInfo = new CustomErrorModel()
                    {
                        ErrorHeader = "Invalid credentials",
                        Details = "Invalid credentials, username or password are invalid",
                        PossibleSolutions = "Make sure you provided valid username and password"
                    }
                }
                );
        }

        [HttpPost]
        public async Task<IActionResult> RegisterParent(ParentRegisterDTO parentRegisterDto)
        {
            string apiEndpoint = "api/identity/registerParent";
            if (_cookieValidator.IsCookiePresent(Request))
            {
                _logger.LogInformation("User tried to register parent but user session cookie is already present, redirect...");
                return RedirectToAction("Index", "Home");
            }

            try
            {
                if (await _parentRegisterHandler.Register(parentRegisterDto, apiEndpoint))
                {
                    _logger.LogInformation($"Successfully registered new parent {parentRegisterDto.Name} {parentRegisterDto.Surname}");
                    return RedirectToAction("Login", "Home");
                }
            }catch(Exception e)
            {
                return HandleRegisterException(e);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterScientist(UserRegisterDTO userRegisterDto)
        {
            string apiEndpoint = "api/identity/registerScientist";
            if (!_cookieValidator.IsCookiePresent(Request))
            {
                _logger.LogInformation("User tried to register scientist but user session cookie is already present, redirect...");
                if (_cookieValidator.IsRoleValid(Request, "Scientist"))
                {
                    _logger.LogInformation("Registering new scientist...");
                    return await TryToRegisterScientist(userRegisterDto, apiEndpoint);
                }
                _logger.LogInformation("User tried to register scientist but his role is invalid, redirect...");
                return RedirectToAction("Index", "Home");
            }

            return await TryToRegisterScientist(userRegisterDto, apiEndpoint);
        }

        public IActionResult Logout()
        {
            _logger.LogInformation("User is trying to logout...");
            if (Request.Cookies["UserData"] != null)
            {
                _logger.LogInformation("User logout succeded");
                Response.Cookies.Delete("UserData");
            }
            _logger.LogInformation("User logout fail, cookie is not present.");
            return RedirectToAction("Index", "Home");
        }

        private async Task<IActionResult> TryToRegisterScientist(UserRegisterDTO userRegisterDto, string apiEndpoint)
        {
            try
            {
                if (await _userRegisterHandler.Register(userRegisterDto, apiEndpoint))
                {
                    _logger.LogInformation($"Successfully registered new scientist, {userRegisterDto.UserName}");
                    return RedirectToAction("Login", "Home");
                }
            }catch(Exception e)
            {
                return HandleRegisterException(e);
            }
            
            return RedirectToAction("Error", "Home");
        }


        private IActionResult HandleRegisterException(Exception e)
        {
            if (e is ArgumentException)
            {
                _logger.LogInformation("User provided invalid data for user registration, redirect to information page..., Exception message:" +
                    $"{e.Message}");
                return RedirectToAction("CustomError", "Home",
                    new
                    {
                        errorInfo = new CustomErrorModel()
                        {
                            ErrorHeader = "Invalid data",
                            Details = "Invalid data provided",
                            PossibleSolutions = "Make sure you provided valid registration data, that passwords match and username is not busy as well as email."
                        }
                    }
                    );
            }
            if (e is SystemException)
            {
                _logger.LogInformation($"Server error, {e.Message}");
            }

            return RedirectToAction("Error", "Home");
        }

    }
}
