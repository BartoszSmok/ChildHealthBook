using ChildHealthBook.Web.Models.ChildDtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Services
{
    public class ParentService
    {
        HttpClient _http;
        public ParentService()
        {
            _http = new HttpClient();
        }

        public async Task<IEnumerable<ChildReadDto>> GetMyChildren(Guid parentId)
        {
            string url = "" + parentId;

            var response = await _http.GetAsync(url);
            if(response.IsSuccessStatusCode)
            {
                var children = await response.Content.ReadAsStringAsync();
                var childrenConverted = JsonConvert.DeserializeObject<IEnumerable<ChildReadDto>>(children);
                return childrenConverted;
            }
            return null;
        }
    }


}
