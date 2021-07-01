using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Communication
{
    public class IdentityRegisterCommunication<T> : IIdentityRegisterCommunication<T>
    {
        private HttpClient _http;
        public IdentityRegisterCommunication(IConfiguration configuration)
        {
            _http = new HttpClient();
            _http.BaseAddress = new Uri(configuration.GetSection("WebClient:GatewayAPI").Value);
        }
        public async Task<bool> Register(T accountData, string url)
        {
            var json = JsonConvert.SerializeObject(accountData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(url, data);
            if (response.IsSuccessStatusCode)
            {
                return response.IsSuccessStatusCode;
            }

            if((int)response.StatusCode == StatusCodes.Status500InternalServerError)
            {
                throw new SystemException($"Internal server error, response details : {response.Content}");
            }

            throw new ArgumentException($"Argument exception, server returned status code that is different from 500 and 200, response details:" +
                $"{response.Content}");
        }
    }
}
