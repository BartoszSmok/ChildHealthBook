using ChildHealthBook.Common.WebDtos.ChildDtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Clients
{
    public class ChildClient
    {

        private readonly HttpClient _httpClient;

        public ChildClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        internal async Task<IEnumerable<ChildReadDto>> GetAllChildren()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ChildReadDto>>($"/api/child");
        }

        internal async Task<IEnumerable<ChildReadDto>> GetAllChildrenByParentId(Guid parentId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ChildReadDto>>($"/api/child/parent/{parentId}");
        }

        internal async Task<ChildWithEventsReadDto> GetChildByIdWithEvents(Guid childId)
        {
            return await _httpClient.GetFromJsonAsync<ChildWithEventsReadDto>($"/api/child/{childId}");
        }
    }
}
