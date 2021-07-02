using ChildHealthBook.Common.Identity.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Communication
{
    public class IdentityTokenCommunication
    {
        private HttpClient _http;

        public IdentityTokenCommunication(IConfiguration config)
        {
            _http = new HttpClient();
            _http.BaseAddress = new Uri(config.GetSection("WebClient:GatewayAPI").Value);
        }
        public async Task<string> Login(UserLoginDTO credentials)
        {
            string url = "api/identity/login";
            var json = JsonConvert.SerializeObject(credentials);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(url, data);

            return await ValidateResponseStatusAndGetContent(response);
        }

        private async Task<string> ValidateResponseStatusAndGetContent(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                return token;
            }

            return string.Empty;
        }
    }
}
