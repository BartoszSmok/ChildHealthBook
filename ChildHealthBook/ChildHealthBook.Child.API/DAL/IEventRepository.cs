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
    }
}
