using ChildHealthBook.Common.WebDtos.ChildDtos;
using ChildHealthBook.Common.WebDtos.EventDtos;
using ChildHealthBook.Web.Models.ChildDtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Services
{
    public class ParentService
    {

        private readonly HttpClient _httpClient;
        private readonly IWebSettings _webSettings;

        public ParentService(IWebSettings webSettings)
        {
            _httpClient = new HttpClient();
            _webSettings = webSettings;
        }

        public async Task<IEnumerable<WebChildReadDto>> GetMyChildren(Guid parentId)
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"/Gateway/Parent/{parentId}";
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<WebChildReadDto>>(url);
            return response;
        }

        public async Task<HttpResponseMessage> CreateChild(WebChildCreateDto childCreateDto, Guid parentId)
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"/Gateway/Parent/Child";
            var responseMessage = await _httpClient.PostAsJsonAsync(url,
                new WebChildCreateDto
                {
                    ParentId = parentId,
                    DateOfBirth = childCreateDto.DateOfBirth,
                    FullName = childCreateDto.FullName,
                    CurrentWeight = childCreateDto.CurrentWeight,
                    CurrentHeight = childCreateDto.CurrentHeight,

                });
            return responseMessage;
        }

        public async Task<WebChildWithEventsReadDto> GetChildByIdWithEvents(Guid childId)
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"/Gateway/Parent/Child/{childId}";

            var response = await _httpClient.GetFromJsonAsync<WebChildWithEventsReadDto>(url);
           
            return response;
        }

        public async Task<HttpResponseMessage> CreatePersonalEvent(PersonalEventCreateDto personalEventCreate)
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"/Gateway/Parent/Child/PersonalEvent";

            var responseMessage = await _httpClient.PostAsJsonAsync(url,
                new PersonalEventCreateDto
                {
                   ChildId = personalEventCreate.ChildId,
                   DateOfEvent = personalEventCreate.DateOfEvent,
                   EventType = personalEventCreate.EventType,
                   EventTitle = personalEventCreate.EventTitle,
                   Comment = personalEventCreate.Comment
                });
            return responseMessage;
        }

        public async Task<HttpResponseMessage> AddNewExamination(MedicalExaminationCreateDto medicalExaminationCreateDto)
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"/Gateway/Parent/Child/Examination";
            var responseMessage = await _httpClient.PostAsJsonAsync(url,
                new MedicalExaminationCreateDto
                {
                    ChildId = medicalExaminationCreateDto.ChildId,
                    SpecialistFullName = medicalExaminationCreateDto.SpecialistFullName,
                    Address = medicalExaminationCreateDto.Address,
                    Comment = medicalExaminationCreateDto.Comment,
                    DateOfMedicalExamination = medicalExaminationCreateDto.DateOfMedicalExamination,
                    ExaminationTitle = medicalExaminationCreateDto.ExaminationTitle,
                    ExaminationType = medicalExaminationCreateDto.ExaminationType

                });
            return responseMessage;
        }

        public async Task<HttpResponseMessage> AddNewMedicalEvent(MedicalEventCreateDto medicalEventCreateDto)
        {
            _httpClient.BaseAddress = new Uri(_webSettings.GatewayAPI);
            string url = $"/Gateway/Parent/Child/MedicalEvent";
            var responseMessage = await _httpClient.PostAsJsonAsync(url,
                new MedicalEventCreateDto
                {
                    ChildId = medicalEventCreateDto.ChildId,
                    DateOfEvent = medicalEventCreateDto.DateOfEvent,
                    Comment = medicalEventCreateDto.Comment,
                    EventTitle = medicalEventCreateDto.EventTitle,
                    EventType = medicalEventCreateDto.EventType

                });
            return responseMessage;
        }
    }


}
