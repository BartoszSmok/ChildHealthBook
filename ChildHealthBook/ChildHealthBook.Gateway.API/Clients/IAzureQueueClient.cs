using ChildHealthBook.Common.WebDtos.ChildDtos;
using ChildHealthBook.Common.WebDtos.EventDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Clients
{
    public interface IAzureQueueClient
    {
        Task AddNewPersonalEvent(PersonalEventCreateDto personalEventCreateDto);
        Task AddNewChild(ChildCreateDto childCreateDto);
        Task AddNewMedicalEvent(MedicalEventCreateDto medicalEventCreateDto);
        Task AddNewExamination(MedicalExaminationCreateDto medicalExaminationCreateDto);
        Task SendToNotificationService(ExaminationNotificationDto examinationNotificationDto);
    }
}
