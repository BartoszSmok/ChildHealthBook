using ChildHealthBook.Web.Models.ChildDtos;
using ChildHealthBook.Web.Models.EventDtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Services
{
    public class ParentService
    {
        HttpClient _httpClient;
        public ParentService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<ChildReadDto>> GetMyChildren()
        {
            string url = "http://childhealthbook.gateway.api/Gateway/Parent/Child";

            var response = await _httpClient.GetAsync(url);
            if(response.IsSuccessStatusCode)
            {
                var children = await response.Content.ReadAsStringAsync();
                var childrenConverted = JsonConvert.DeserializeObject<IEnumerable<ChildReadDto>>(children);
                return childrenConverted;
            }
            return null;
        }

        public async Task<HttpResponseMessage> CreateChild(ChildCreateDto childCreateDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync($"http://childhealthbook.gateway.api/Gateway/Parent/Child",
                new ChildCreateDto
                {
                    ParentId = new Guid(),
                    DateOfBirth = childCreateDto.DateOfBirth,
                    FullName = childCreateDto.FullName,
                    CurrentWeight = childCreateDto.CurrentWeight,
                    CurrentHeight = childCreateDto.CurrentHeight,

                });
            return responseMessage;
        }

        public async Task<ChildWithEventsReadDto> GetChildByIdWithEvents(Guid childId)
        {
            string url = $"http://childhealthbook.gateway.api/Gateway/Child/{childId}";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var child = await response.Content.ReadAsStringAsync();
                var childConverted = JsonConvert.DeserializeObject<ChildWithEventsReadDto>(child);
                return childConverted;
            }
            return null;
        }

        public async Task<HttpResponseMessage> CreatePersonalEvent(PersonalEventCreateDto personalEventCreateDto, Guid childId)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync($"http://childhealthbook.gateway.api/Gateway/Child/PersonalEvent",
                new PersonalEventCreateDto
                {
                   ChildId = childId,
                   DateOfEvent = personalEventCreateDto.DateOfEvent,
                   EventType = "Personal Event",
                   EventTitle = personalEventCreateDto.EventTitle,
                   Comment = personalEventCreateDto.Comment

                });
            return responseMessage;
        }
    }


}
