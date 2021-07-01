using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChildHealthBook.Common.WebDtos.ChildDtos;
using ChildHealthBook.Child.API.DAL;
using ChildHealthBook.Child.API.Clients;
using ChildHealthBook.Common.WebDtos.EventDtos;
using ChildHealthBook.Child.API.Models;

namespace ChildHealthBook.Child.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChildController : ControllerBase
    {
        private readonly ILogger<ChildController> _logger;
        private readonly IChildRepository _childRepository;
        private readonly IEventRepository _eventRepository;

        public ChildController(ILogger<ChildController> logger, IChildRepository childRepository, IEventRepository eventRepository)
        {
            _logger = logger;
            _childRepository = childRepository;
            _eventRepository = eventRepository;
        }

        //api/child/ - Get all children / GET
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ChildReadDto>>> GetAllChildren()
        {
            IEnumerable<ChildReadDto> result = await _childRepository.GetAllChildren();
            return result == null ? NotFound() : Ok(result);
        }

        //api/child/parent/{parentId} - Get all children by query string parentId / GET
        [HttpGet("Parent/{parentId}")]
        public async Task<ActionResult<IEnumerable<ChildReadDto>>> GetAllChildrenByParentId(Guid parentId)
        {
            IEnumerable<ChildReadDto> result = await _childRepository.GetAllChildrenByParentId(parentId);
            return result == null ? NotFound() : Ok(result);
        }

        //api/child/{childId} - Get child with events by query string childId / GET
        [HttpGet("{childId}")]
        public async Task<ActionResult<ChildReadDto>> GetChildById(Guid childId)
        {
            Console.WriteLine("2b");
            ChildReadDto result = await _childRepository.GetChildById(childId);
            Console.WriteLine("2e");
            return result == null ? NotFound() : Ok(result);
        }

        //api/child/WithEvents/{childId} - Get child with events by query string childId / GET
        [HttpGet("WithEvents/{childId}")]
        public async Task<ActionResult<ChildWithEventsReadDto>> GetChildByIdWithEvents(Guid childId)
        {
            ChildWithEventsReadDto result = await _childRepository.GetChildByIdWithEvents(childId);
            result.MedicalExaminations = await _eventRepository.GetChildExaminations(childId);
            result.PersonalEvents = await _eventRepository.GetChildPersonalEvents(childId);
            result.MedicalEvents = await _eventRepository.GetChildMedicalEvents(childId);
            return result == null ? NotFound() : Ok(result);
        }

        //api/analytics/ - Get all children data required for anylitcs purposes / GET
        [HttpGet("Analytics")]
        public async Task<ActionResult<IEnumerable<ChildWithEventsReadDto>>> GetAllChildrenWithEvents()
        {
            IEnumerable<ChildWithEventsReadDto> result = await _childRepository.GetAllChildrenWithEvents();

            foreach (var item in result)
            {
                item.MedicalExaminations =  await _eventRepository.GetChildExaminations(item.Id);
                item.PersonalEvents = await _eventRepository.GetChildPersonalEvents(item.Id);
                item.MedicalEvents = await _eventRepository.GetChildMedicalEvents(item.Id);
            }

            return result == null ? NotFound() : Ok(result);
        }

        //GetSharedEventByParentId
        [HttpGet("ShareEvent/{parentId}")]
        public async Task<ActionResult<IEnumerable<SharedEventReadDto>>> GetSharedEventByParentId(Guid parentId)
        {
            IEnumerable<ShareEventModel> shareEventModels = await _eventRepository.GetSharedEventByParentId(parentId);

            List <SharedEventReadDto> result = new List<SharedEventReadDto>();

            //if (shareEventModels != null)
            //{
                foreach (var item in shareEventModels)
                {
                    PersonalEventModel personalEventModel = await _eventRepository.GetChildPersonalEventById(item.EventId);

                    SharedEventReadDto sharedEventReadDto = new SharedEventReadDto { 
                    ChildFullName = await _childRepository.GetChildFullNameById(personalEventModel.ChildId),
                    DateOfEvent = personalEventModel.DateOfEvent,
                    EventType = personalEventModel.EventType,
                    EventTitle = personalEventModel.EventTitle,
                    Comment = personalEventModel.Comment
                    };

                    result.Add(sharedEventReadDto);
                }
            //}

            return result == null ? NotFound() : Ok(result);
        }
    }
}
