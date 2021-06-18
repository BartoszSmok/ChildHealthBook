using ChildHealthBook.Common.WebDtos.ChildDtos;
using ChildHealthBook.Common.WebDtos.EventDtos;
using ChildHealthBook.Gateway.API.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Services
{
    public class GatewayService : IGatewayService
    {
        private ChildClient _childClient;
        private IAzureQueueClient _azureQueueClient;

        public GatewayService(ChildClient childClient, IAzureQueueClient azureQueueClient)
        {
            _childClient = childClient;
            _azureQueueClient = azureQueueClient;
        }

        public async Task AddNewChild(ChildCreateDto childCreateDto)
        {
            await _azureQueueClient.AddNewChild(childCreateDto);
        }

        public async Task AddNewExamination(MedicalExaminationCreateDto medicalExaminationCreateDto)
        {
            Console.WriteLine("1");
            await _azureQueueClient.AddNewExamination(medicalExaminationCreateDto);
            Console.WriteLine("2");
            ChildReadDto childReadDto = await _childClient.GetChildById(medicalExaminationCreateDto.ChildId);
            Console.WriteLine("3");
            ExaminationNotificationDto examinationNotificationDto = new ExaminationNotificationDto
            {
                ParentId = childReadDto.ParentId,
                ChildFullName = childReadDto.FullName,
                DateOfMedicalExamination = medicalExaminationCreateDto.DateOfMedicalExamination,
                ExaminationType = medicalExaminationCreateDto.ExaminationType,
                ExaminationTitle = medicalExaminationCreateDto.ExaminationTitle,
                Comment = medicalExaminationCreateDto.Comment,
                SpecialistFullName = medicalExaminationCreateDto.SpecialistFullName,
                Address = medicalExaminationCreateDto.Address
            };
            Console.WriteLine("4");
            await _azureQueueClient.SendToNotificationService(examinationNotificationDto);
        }

        public async Task AddNewMedicalEvent(MedicalEventCreateDto medicalEventCreateDto)
        {
            await _azureQueueClient.AddNewMedicalEvent(medicalEventCreateDto);
        }

        public async Task AddNewPersonalEvent(PersonalEventCreateDto personalEventCreateDto)
        {
            await _azureQueueClient.AddNewPersonalEvent(personalEventCreateDto);
        }

        public async Task<IEnumerable<ChildReadDto>> GetAllChildren()
        {
            IEnumerable<ChildReadDto> result = await _childClient.GetAllChildren();
            return result;
        }

        public async Task<IEnumerable<ChildReadDto>> GetAllChildrenByParentId(Guid parentId)
        {
            IEnumerable<ChildReadDto> result = await _childClient.GetAllChildrenByParentId(parentId);
            return result;
        }

        public async Task<ChildWithEventsReadDto> GetChildByIdWithEvents(Guid childId)
        {
            ChildWithEventsReadDto result = await _childClient.GetChildByIdWithEvents(childId);
            return result;
        }


    }
}
