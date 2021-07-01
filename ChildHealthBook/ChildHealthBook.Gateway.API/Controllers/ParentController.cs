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

        //AddNewPersonalEvent - Add new event to a child / POST
        [HttpPost("Child/PersonalEvent")]
        public async Task<ActionResult> AddNewPersonalEvent(PersonalEventCreateDto personalEventCreateDto)
        {
            await _gatewayService.AddNewPersonalEvent(personalEventCreateDto);
            return Ok();
        }

        //AddNewMedicalEvent - Add new event to a child / POST
        [HttpPost("Child/MedicalEvent")]
        public async Task<ActionResult> AddNewMedicalEvent(MedicalEventCreateDto medicalEventCreateDto)
        {
            await _gatewayService.AddNewMedicalEvent(medicalEventCreateDto);
            return Ok();
        }

        //api/event/child/examination/{childId} - Add new examination to child with Id / POST
        //AddNewExamination - Add new examination to a child / POST
        [HttpPost("Child/Examination")]
        public async Task<ActionResult> AddNewExamination(MedicalExaminationCreateDto medicalExaminationCreateDto)
        {
            await _gatewayService.AddNewExamination(medicalExaminationCreateDto);
            return Ok();
        }

        //ShareEventWithParent - Share selected event with another parent
        [HttpPost("Child/ShareEvent")]
        public async Task<ActionResult> ShareEventWithParent(ShareEventCreateDto shareEventCreateDto)
        {
            await _gatewayService.ShareEventWithParent(shareEventCreateDto);
            return NoContent();
        }

        //public class SharedEventReadDto
        //    public string ChildFullName { get; set; }
        //    public DateTime DateOfEvent { get; set; }
        //    public string EventType { get; set; }
        //    public string EventTitle { get; set; }
        //    public string Comment { get; set; }

        //GetSharedEventByParentId
        [HttpGet("Child/ShareEvent/{parentId}")]
        public async Task<ActionResult<IEnumerable<SharedEventReadDto>>> GetSharedEventByParentId(Guid parentId)
        {
            IEnumerable<SharedEventReadDto> result = await _gatewayService.GetSharedEventByParentId(parentId);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
