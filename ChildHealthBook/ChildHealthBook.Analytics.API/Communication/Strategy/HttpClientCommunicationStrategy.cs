using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChildHealthBook.Analytics.API.Communication.Strategy
{
    public class HttpClientCommunicationStrategy<T> : ICommunicationStrategy<T>
    {
        private HttpClient _http;
        public HttpClientCommunicationStrategy()
        {
            _http = new HttpClient();
        }
        public async Task<IEnumerable<T>> GetAll(string connectionKey = "")
        {
            var result = await _http.GetAsync(connectionKey);
            return await ValidateResponseStatusAndGetListContent(result);
        }

        private async Task<IEnumerable<T>> ValidateResponseStatusAndGetListContent(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(data);
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}
