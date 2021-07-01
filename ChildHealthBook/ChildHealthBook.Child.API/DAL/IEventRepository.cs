using ChildHealthBook.Child.API.Models;
using ChildHealthBook.Common.WebDtos.EventDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Child.API.DAL
{
    public interface IEventRepository
    {
        Task<IEnumerable<MedicalExaminationReadDto>> GetChildExaminations(Guid childId);
        Task<IEnumerable<PersonalEventReadDto>> GetChildPersonalEvents(Guid childId);
        Task<IEnumerable<MedicalEventReadDto>> GetChildMedicalEvents(Guid childId);
        Task AddNewPersonalEvent(string messageText);
        Task AddNewMedicalEvent(string messageText);
        Task AddNewExamination(string messageText);
        Task ShareEvent(string messageText);
        Task<IEnumerable<ShareEventModel>> GetSharedEventByParentId(Guid parentId);
        Task<PersonalEventModel> GetChildPersonalEventById(Guid eventId);
    }
}
