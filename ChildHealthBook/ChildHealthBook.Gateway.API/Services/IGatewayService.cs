using ChildHealthBook.Common.WebDtos.ChildDtos;
using ChildHealthBook.Common.WebDtos.EventDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Services
{
    public interface IGatewayService
    {
        Task<IEnumerable<ChildReadDto>> GetAllChildren();
        Task AddNewChild(ChildCreateDto childCreateDto);
        Task<ChildWithEventsReadDto> GetChildByIdWithEvents(Guid childId);
        Task<IEnumerable<ChildReadDto>> GetAllChildrenByParentId(Guid parentId);
    }
}
