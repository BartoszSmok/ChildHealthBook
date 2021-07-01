using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChildHealthBook.Web.CookieServices.Token
{
    public class TokenHelper
    {

        public IEnumerable<Claim> GetClaims(string token)
        {
            return ReadToken(token).Claims;
        }

        private JwtSecurityToken ReadToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var data = handler.ReadJwtToken(token);
            return data;
        }
    }
}
