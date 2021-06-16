using ChildHealthBook.Child.API.Models;
using ChildHealthBook.Common.WebDtos.ChildDtos;
using ChildHealthBook.Common.WebDtos.EventDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Child.API.DAL
{
    public interface IChildRepository
    {
        Task<IEnumerable<ChildReadDto>> GetAllChildren();
        Task AddNewChild(ChildCreateDto childCreateDto);
        Task<ChildWithEventsReadDto> GetChildByIdWithEvents(Guid childId);
        Task<IEnumerable<MedicalExaminationReadDto>> GetChildExaminations(Guid childId);
        Task<IEnumerable<PersonalEventReadDto>> GetChildPersonalEvents(Guid childId);
        Task<IEnumerable<MedicalEventReadDto>> GetChildMedicalEvents(Guid childId);
    }
}
