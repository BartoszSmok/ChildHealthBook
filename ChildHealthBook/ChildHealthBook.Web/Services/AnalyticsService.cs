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

        internal async Task<WebSharedStats> GetAnalysis()
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            WebVaccinationFactorRecord webVaccinationFactorRecord = await _httpClient.GetFromJsonAsync<WebVaccinationFactorRecord>($"/Gateway/Analytics/vaccinationFactor");
            WebChildrenAverageAgeRecord webChildrenAverageAgeRecord = await _httpClient.GetFromJsonAsync<WebChildrenAverageAgeRecord>($"/Gateway/Analytics/childrenAverageCountPerParent");
            WebChildrenAverageCountPerParentRecord webChildrenAverageCountPerParentRecord = await _httpClient.GetFromJsonAsync<WebChildrenAverageCountPerParentRecord>($"/Gateway/Analytics/childrenAverageCountPerParent");

            WebSharedStats result = new WebSharedStats
            {
                VaccinationFactor = webVaccinationFactorRecord.Factor,
                DateOfRecordCreationVaccinationFactor = webVaccinationFactorRecord.DateOfRecordCreation,
                ChildrenAverageAge = webChildrenAverageAgeRecord.Average,
                DateOfRecordCreationChildrenAverageAge = webChildrenAverageAgeRecord.DateOfRecordCreation,
                AverageChildrenCountPerParent = webChildrenAverageCountPerParentRecord.Average,
                DateOfRecordCreationAverageChildrenCountPerParent = webChildrenAverageCountPerParentRecord.DateOfRecordCreation
            };

            return result;
        }

        public async Task<IEnumerable<WebVaccinationFactorRecord>> GetVaccinationFactorHistory()
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"/Gateway/Analytics/vaccinationFactorHistory";
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<WebVaccinationFactorRecord>>(url);
            return response;
        }

        public async Task<IEnumerable<WebChildrenAverageAgeRecord>> GetChildrenAverageAgeHistory()
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"/Gateway/Analytics/childrenAverageAgeHistory";
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
            string url = $"/Gateway/Analytics/childrenAverageCountPerParentHistory";
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
