using ChildHealthBook.Common.Identity.DTOs;
using ChildHealthBook.Gateway.API.Communication.Strategy.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Communication.Strategy
{
    public class HttpClientIdentityCommunication : IIdentityCommunicationStrategy
    {
        private HttpClient _http;

        public HttpClientIdentityCommunication()
        {
            _http = new HttpClient();
        }

        public async Task<string> GetToken(string url, UserLoginDTO credentials)
        {
            var json = JsonConvert.SerializeObject(credentials);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(url, data);

            return await ValidateResponseStatusAndGetContent(response);
        }

        public async Task RegisterParent(string url, ParentRegisterDTO parentData)
        {
            var json = JsonConvert.SerializeObject(parentData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(url, data);

            await ValidateResponseStatusAndGetContent(response);
        }

        public async Task RegisterScientist(string url, UserRegisterDTO userData)
        {
            var json = JsonConvert.SerializeObject(userData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(url, data);

            await ValidateResponseStatusAndGetContent(response);
        }

        private async Task<string> ValidateResponseStatusAndGetContent(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}
