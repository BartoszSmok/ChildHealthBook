using ChildHealthBook.Common.Identity.DTOs;
using ChildHealthBook.Gateway.API.Communication.Bridge;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private IdentityCommunicationBridge _identityCommunicationBrige { get; set; }
        private string _identityApiBaseUrl;
        public IdentityController(IdentityCommunicationBridge identityCommunicationBrige,
            IConfiguration config)
        {
            _identityCommunicationBrige = identityCommunicationBrige;
            _identityApiBaseUrl = config.GetSection("Gateway:IdentityAPI:BaseUrl").Value;
        }

        [HttpPost]
        [Route("registerParent")]
        public async Task<IActionResult> RegisterNewParent(ParentRegisterDTO parentUpsertDto)
        {
            try
            {
                string url = _identityApiBaseUrl + "/api/accounts/user/registerParent";
                await _identityCommunicationBrige.RegisterParent(url, parentUpsertDto); 
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("registerScientist")]
        public async Task<IActionResult> RegisterNewScientist(UserRegisterDTO userUpsertDto)
        {
            try
            {
                string url = _identityApiBaseUrl + "/api/accounts/user/registerScientist";
                await _identityCommunicationBrige.RegisterScientist(url, userUpsertDto);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            try
            {
                string url = _identityApiBaseUrl + "/api/tokens/token";
                return Ok(await _identityCommunicationBrige.GetTokenAsString(url, userLoginDTO));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
