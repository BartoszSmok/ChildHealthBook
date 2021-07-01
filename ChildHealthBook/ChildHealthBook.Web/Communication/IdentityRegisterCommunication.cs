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
            return response.IsSuccessStatusCode;
        }
    }
}
