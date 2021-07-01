using ChildHealthBook.Common.Identity.DTOs;
using ChildHealthBook.Web.CookieServices.Token;
using ChildHealthBook.Web.Models.Session;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;

namespace ChildHealthBook.Web.CookieServices.Serializers
{
    public class AuthenticatedUserSessionBuilder
    {
        private TokenHelper _tokenHelper;

        public AuthenticatedUserSessionBuilder(TokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }
        public string BuildCookieString(string token)
        {
            var claims = _tokenHelper.GetClaims(token);

            var user = new AuthUserSession()
            {
                Id = Guid.Parse(claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value),
                AccountType = claims.Where(claim => claim.Type == ClaimTypes.Role).FirstOrDefault().Value,
                Name = claims.Where(claim => claim.Type == CustomClaimTypes.CustomName).FirstOrDefault().Value,
                Surname = claims.Where(claim => claim.Type == CustomClaimTypes.CustomSurname).FirstOrDefault().Value,
                DateOfBirth = DateTime.Parse(claims.Where(claim => claim.Type == CustomClaimTypes.CustomAge).FirstOrDefault().Value),
                Phone = claims.Where(claim => claim.Type == CustomClaimTypes.CustomPhone).FirstOrDefault().Value,
                Email = claims.Where(claim => claim.Type == ClaimTypes.Email).FirstOrDefault().Value
            };
            return JsonConvert.SerializeObject(user);
        }
    }
}
