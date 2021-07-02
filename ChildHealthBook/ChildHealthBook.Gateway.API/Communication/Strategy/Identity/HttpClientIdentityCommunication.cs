using ChildHealthBook.Common.Identity.DTOs;
using ChildHealthBook.Gateway.API.Communication.Strategy.Identity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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
        public async Task<IEnumerable<UserData>> GetParentsFromDb(string url)
        {
            return await _http.GetFromJsonAsync<IEnumerable<UserData>>(url);
        }

        public async Task<string> GetToken(string url, UserLoginDTO credentials)
        {
            var json = JsonConvert.SerializeObject(credentials);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(url, data);

            return await ValidateResponseStatusAndGetContent(response);
        }

        public async Task<bool> RegisterParent(string url, ParentRegisterDTO parentData)
        {
            var json = JsonConvert.SerializeObject(parentData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(url, data);

            return !string.IsNullOrEmpty(await ValidateResponseStatusAndGetContent(response));
        }

        public async Task<bool> RegisterScientist(string url, UserRegisterDTO userData)
        {
            var json = JsonConvert.SerializeObject(userData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(url, data);

            return !string.IsNullOrEmpty(await ValidateResponseStatusAndGetContent(response));
        }

        private async Task<string> ValidateResponseStatusAndGetContent(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            if((int)response.StatusCode == StatusCodes.Status400BadRequest)
            {
                return string.Empty;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        private async Task<UserData> ValidateResponseStatusAndGetListContent(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserData>(data);
            }
            else
            {
                throw new Exception("Request was not successful");
            }
        }
    }
}
