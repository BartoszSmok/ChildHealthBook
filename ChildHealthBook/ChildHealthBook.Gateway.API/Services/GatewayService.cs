using ChildHealthBook.Common.WebDtos.ChildDtos;
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

        public GatewayService(ChildClient childClient)
        {
            _childClient = childClient;
        }

        public async Task AddNewChild(ChildCreateDto childCreateDto)
        {
            await _childClient.AddNewChild(childCreateDto);
        }

        public async Task<IEnumerable<ChildReadDto>> GetAllChildren()
        {
            IEnumerable<ChildReadDto> result = await _childClient.GetAllChildren();
            return result;
        }

        public async Task<ChildWithEventsReadDto> GetChildByIdWithEvents(Guid childId)
        {
            ChildWithEventsReadDto result = await _childClient.GetChildByIdWithEvents(childId);
            return result;
        }
    }
}
