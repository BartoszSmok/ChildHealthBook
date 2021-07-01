using ChildHealthBook.Common.Identity.DTOs;
using Common.Identity.Setup;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChildHealthBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<TokensController> _logger;

        public TokensController(IOptionsSnapshot<JwtSettings> jwtSettings, UserManager<User> userManager,
            ILogger<TokensController> logger)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// This POST method creates new JWT token
        /// </summary>
        /// <param name="userName">Username for who create token</param>
        /// <param name="password">User password for who create token</param>
        /// <returns></returns>
        [Route("token")]
        [HttpPost]
        public async Task<IActionResult> Create(UserLoginDTO credentials)
        {

            if (await IsValidUserNameAndPassword(credentials))
            {
                _logger.LogInformation($"Login for user {credentials.UserName} is successful. Generating token...");
                return new ObjectResult(await GenerateToken(credentials.UserName));
            }

            _logger.LogWarning($"User {credentials.UserName} provided invalid credential/s. Failed to login.");
            return BadRequest();
        }
        private async Task<bool> IsValidUserNameAndPassword(UserLoginDTO credentials)
        {
            var user = await _userManager.FindByNameAsync(credentials.UserName);
            return await _userManager.CheckPasswordAsync(user, credentials.Password);
        }

        private async Task<dynamic> GenerateToken(string userName)
        {
            var claims = await GenerateClaims(userName);

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretCode)),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims.Value));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ActionResult<List<Claim>>> GenerateClaims(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(CustomClaimTypes.CustomName, user.Name),
                new Claim(CustomClaimTypes.CustomSurname, user.Surname),
                new Claim(CustomClaimTypes.CustomAge, user.DateOfBirth.ToString()),
                new Claim(CustomClaimTypes.CustomPhone, user.Phone),
                new Claim(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud, _jwtSettings.Issuer),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes)).ToUnixTimeSeconds().ToString()),
            };

            claims.Add(new Claim(ClaimTypes.Role, user.AccountType));

            return claims;
        }

    }
}
