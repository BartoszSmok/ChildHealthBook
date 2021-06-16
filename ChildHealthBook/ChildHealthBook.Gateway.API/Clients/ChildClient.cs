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

        public async Task<IEnumerable<ChildReadDto>> GetAllChildren()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ChildReadDto>>($"/api/child");
        }

        public async Task AddNewChild(ChildCreateDto childCreateDto)
        {
            //await _httpClient.PostAsJsonAsync($"/api/child", JsonContent.Create<ChildCreateDto>(childCreateDto).ToString());
            await _httpClient.PostAsJsonAsync($"/api/child",
                new ChildCreateDto { 
                    ParentId = childCreateDto.ParentId,
                    DateOfBirth = childCreateDto.DateOfBirth,
                    FullName = childCreateDto.FullName,
                    CurrentWeight = childCreateDto.CurrentWeight,
                    CurrentHeight = childCreateDto.CurrentHeight
                });
        }
    }
}
