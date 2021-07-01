using ChildHealthBook.Web.Models.AnalyticsDtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Services
{
    public class AnalyticsService

    {
        private readonly HttpClient _httpClient;
        private readonly IWebSettings _webSettings;
        public AnalyticsService(IWebSettings webSettings)
        {
            _httpClient = new HttpClient();
            _webSettings = webSettings;
        }

        public async Task<WebVaccinationFactorRecord> GetVaccinationFactor()
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"Analytics/vaccinationFactor";
            var response = await _httpClient.GetFromJsonAsync<WebVaccinationFactorRecord>(url);
            //if (response.IsSuccessStatusCode)
            //{
            //    var vaccinationFactor = await response.Content.ReadAsStringAsync();
            //    var vaccinationFactorConverted = JsonConvert.DeserializeObject<WebVaccinationFactorRecord>(vaccinationFactor);
            //    return vaccinationFactorConverted;
            //}
            return response;
        }

        public async Task<WebChildrenAverageAgeRecord> GetChildrenAverageAge()
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"Analytics/childrenAverageAge";
            var response = await _httpClient.GetFromJsonAsync<WebChildrenAverageAgeRecord>(url);
            //if (response.IsSuccessStatusCode)
            //{
            //    var childrenAverageAge = await response.Content.ReadAsStringAsync();
            //    var childrenAverageAgeConverted = JsonConvert.DeserializeObject<WebChildrenAverageAgeRecord>(childrenAverageAge);
            //    return childrenAverageAgeConverted;
            //}
            return response;
        }

        public async Task<WebChildrenAverageAgeRecord> GetChildrenAverageCountPerParent()
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"childrenAverageCountPerParent";
            var response = await _httpClient.GetFromJsonAsync<WebChildrenAverageAgeRecord>(url);
            return response;
        }

        public async Task<IEnumerable<WebVaccinationFactorRecord>> GetVaccinationFactorHistory()
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"Analytics/vaccinationFactorHistory";
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<WebVaccinationFactorRecord>>(url);
            return response;
        }

        public async Task<IEnumerable<WebChildrenAverageAgeRecord>> GetChildrenAverageAgeHistory()
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"Analytics/childrenAverageAgeHistory";
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<WebChildrenAverageAgeRecord>>(url);

            //var response = await _httpClient.GetAsync(url);
            //if (response.IsSuccessStatusCode)
            //{
            //    var childrenAverageAgeHistory = await response.Content.ReadAsStringAsync();
            //    var childrenAverageAgeHistoryConverted = JsonConvert.DeserializeObject<IEnumerable<WebChildrenAverageAgeRecord>> (childrenAverageAgeHistory);
            //    return childrenAverageAgeHistoryConverted;
            //}
            return response;
        }

        public async Task<IEnumerable<WebChildrenAverageCountPerParentRecord>> GetChildrenAverageCountPerParentHistory()
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"Analytics/childrenAverageCountPerParentHistory";
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<WebChildrenAverageCountPerParentRecord>>(url);


            //if (response.IsSuccessStatusCode)
            //{
            //    var childrenAverageCountPerParentHistory = await response.Content.ReadAsStringAsync();
            //    var childrenAverageCountPerParentHistoryConverted = JsonConvert.DeserializeObject<IEnumerable<WebChildrenAverageCountPerParentRecord>>(childrenAverageCountPerParentHistory);
            //    return childrenAverageCountPerParentHistoryConverted;
            //}
            return response;
        }


    }
}
