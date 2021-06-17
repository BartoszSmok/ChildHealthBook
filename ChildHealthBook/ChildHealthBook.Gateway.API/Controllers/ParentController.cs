using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChildHealthBook.Common.WebDtos.ChildDtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ChildHealthBook.Gateway.API.Services;
using ChildHealthBook.Common.WebDtos.EventDtos;

namespace ChildHealthBook.Gateway.API.Controllers
{
    [ApiController]
    [Route("/Gateway/[controller]")]
    public class ParentController : ControllerBase
    {
        private readonly ILogger<ParentController> _logger;
        private readonly IGatewayService _gatewayService;

        public ParentController(ILogger<ParentController> logger, IGatewayService gatewayService)
        {
            _logger = logger;
            _gatewayService = gatewayService;
        }

        //GetAllChildren - Get all children / GET
        [HttpGet("Child")]
        public async Task<ActionResult<IEnumerable<ChildReadDto>>> GetAllChildren()
        {
            IEnumerable<ChildReadDto> result = await _gatewayService.GetAllChildren();
            return result == null ? NotFound() : Ok(result);
        }

        //GetAllChildrenByParentId - Get all children by query string parentId / GET
        [HttpGet("{parentId}")]
        public async Task<ActionResult<IEnumerable<ChildReadDto>>> GetAllChildrenByParentId(Guid parentId)
        {
            IEnumerable<ChildReadDto> result = await _gatewayService.GetAllChildrenByParentId(parentId);
            return result == null ? NotFound() : Ok(result);
        }

        //GetChildById - Get child pressed on web client / GET
        [HttpGet("Child/{childId}")]
        public async Task<ActionResult<ChildWithEventsReadDto>> GetChildByIdWithEvents(Guid childId)
        {
            ChildWithEventsReadDto result = await _gatewayService.GetChildByIdWithEvents(childId);
            return result == null ? NotFound() : Ok(result);
        }

        //AddNewChild - Add new child to parent / POST
        [HttpPost("Child")]
        public async Task<ActionResult> AddNewChild(ChildCreateDto childCreateDto)
        {
            await _gatewayService.AddNewChild(childCreateDto);
            return NoContent();
        }

        /*
            GetAllEvents - Get all child events / GET
            GetAllExaminations - Get all child examinations / GET
            AddNewExamination - Add new examination to a child / POST
            AddNewPersonalEvent - Add new event to a child / POST
            GetAllSharedEvents - Get all events shared for this parent by other parent / GET
            ShareEventWithParent - Share selected event with another parent / PUT
         */

    }
}
