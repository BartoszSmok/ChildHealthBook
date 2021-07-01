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
        Task AddNewChild(string messageText);
        Task<ChildWithEventsReadDto> GetChildByIdWithEvents(Guid childId);
        Task<IEnumerable<ChildReadDto>> GetAllChildrenByParentId(Guid parentId);
        Task<IEnumerable<ChildWithEventsReadDto>> GetAllChildrenWithEvents();
        Task<ChildReadDto> GetChildById(Guid childId);
        Task<string> GetChildFullNameById(Guid childId);
    }
}
