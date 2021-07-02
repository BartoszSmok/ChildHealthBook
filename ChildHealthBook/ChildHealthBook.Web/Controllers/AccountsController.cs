using ChildHealthBook.Common.Identity.DTOs;
using ChildHealthBook.Web.Communication;
using ChildHealthBook.Web.CookieServices.Serializers;
using ChildHealthBook.Web.CookieServices.Validator;
using ChildHealthBook.Web.Models.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public AccountsController(IIdentityRegisterCommunication<ParentRegisterDTO> parentRegisterHandler,
            IIdentityRegisterCommunication<UserRegisterDTO> userRegisterHandler,
            IdentityTokenCommunication tokenFetcherHandler,
            AuthenticatedUserSessionBuilder authCookieBuilder,
            UserSessionCookieValidator cookieValidator)
        {
            _parentRegisterHandler = parentRegisterHandler;
            _userRegisterHandler = userRegisterHandler;
            _tokenFetcherHandler = tokenFetcherHandler;
            _authCookieBuilder = authCookieBuilder;
            _cookieValidator = cookieValidator;
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
                return RedirectToAction("Index", "Home");
            }
            string token = await _tokenFetcherHandler.Login(loginDTO);
            if (!string.IsNullOrEmpty(token))
            {
                string authUserCookie = _authCookieBuilder.BuildCookieString(token);
                var options = new CookieOptions() { Expires = DateTime.Now.AddMinutes(30) };
                Response.Cookies.Append("UserData", authUserCookie, options);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterParent(ParentRegisterDTO parentRegisterDto)
        {
            string apiEndpoint = "api/identity/registerParent";
            if (_cookieValidator.IsCookiePresent(Request))
            {
                return RedirectToAction("Index", "Home");
            }

            if (await _parentRegisterHandler.Register(parentRegisterDto, apiEndpoint))
            {
                return RedirectToAction("Login", "Home");
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterScientist(UserRegisterDTO userRegisterDto)
        {
            string apiEndpoint = "api/identity/registerScientist";
            if (_cookieValidator.IsCookiePresent(Request) )
            {
                if(_cookieValidator.IsRoleValid(Request, "Scientist"))
                {
                    return await TryToRegisterScientist(userRegisterDto, apiEndpoint);
                }
                return RedirectToAction("Index", "Home");
            }

            return await TryToRegisterScientist(userRegisterDto, apiEndpoint);
        }

        public IActionResult Logout()
        {

            if (Request.Cookies["UserData"] != null)
            {
                Response.Cookies.Delete("UserData");
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task<IActionResult> TryToRegisterScientist(UserRegisterDTO userRegisterDto, string apiEndpoint)
        {
            if (await _userRegisterHandler.Register(userRegisterDto, apiEndpoint))
            {
                return RedirectToAction("Login", "Home");
            }
            return RedirectToAction("Error", "Home");
        }
    }
}
