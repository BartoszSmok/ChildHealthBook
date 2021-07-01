using ChildHealthBook.Web.Models.Session;
using Microsoft.AspNetCore.Http;

namespace ChildHealthBook.Web.CookieServices.Validator
{
    public class UserSessionCookieValidator
    {
        private ICookieDeserializer<AuthUserSession> _userSessionDeserializer;

        public UserSessionCookieValidator(
            ICookieDeserializer<AuthUserSession> userSessionDeserializer)
        {
            _userSessionDeserializer = userSessionDeserializer;
        }
        public bool IsCookiePresent(HttpRequest requestContext)
        {
            return !string.IsNullOrEmpty(GetCookie(requestContext));
        }


        public bool IsRoleValid(HttpRequest requestContext, string role)
        {
            return _userSessionDeserializer.Deserialize(GetCookie(requestContext)).AccountType == role;
        }

        private string GetCookie(HttpRequest requestContext)
        {
            return requestContext.Cookies["UserData"] ?? string.Empty;
        }
    }
}
