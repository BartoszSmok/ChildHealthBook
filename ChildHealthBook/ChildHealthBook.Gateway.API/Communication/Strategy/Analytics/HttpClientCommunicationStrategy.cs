using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Communication.Strategy
{
    public class HttpClientCommunicationStrategy<T> : IAnalyticsCommunicationStrategy<T>
    {
        private HttpClient _http;
        public HttpClientCommunicationStrategy()
        {
            _http = new HttpClient();
        }
        public async Task<T> Get(string connectionKey = "")
        {
            var result = await _http.GetAsync(connectionKey);
            return await ValidateResponseStatusAndGetListContent(result);
        }

        private async Task<T> ValidateResponseStatusAndGetListContent(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                throw new Exception("Request was not successful");
            }
        }

    }
}
