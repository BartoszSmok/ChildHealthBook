using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChildHealthBook.Common.WebDtos.ChildDtos;
using ChildHealthBook.Child.API.DAL;

namespace ChildHealthBook.Child.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ChildController : ControllerBase
    {
        private readonly ILogger<ChildController> _logger;
        private readonly IChildRepository _repository;

        public ChildController(ILogger<ChildController> logger, IChildRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        //api/child/ - Get all children / GET
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ChildReadDto>>> GetAllChildren()
        {
            IEnumerable<ChildReadDto> result = await _repository.GetAllChildren();
            return result == null ? NotFound() : Ok(result);
        }

        //api/child/  - Add new child / POST
        [HttpPost("")]
        public async Task<ActionResult> AddNewChild(ChildCreateDto childCreateDto)
        {
            await _repository.AddNewChild(childCreateDto);
            return NoContent();
        }


        /*
            api/child/parent/{parentId} - Get all children by query string parentId / GET
            api/child/{childId} - Get child with events by query string childId / GET
            api/child/  - Add new child / POST
            -----------------------------------------
            api/analytics/ - Get all children data required for anylitcs purposes / GET
            -----------------------------------------
            api/event/shared/{parentId} - Get all shared events by query string parentId / GET
            api/event/child/{childId} - Add new event to child with id / POST
            api/event/child/examination/{childId} - Add new examination to child with Id / POST
            api/event/examination/child/{childId} - Get all child examination by query string childId / GET 
            api/event/examination/{examinationId} - Get examination by Id / GET
            api/event/{eventId} - Update event with Id / PUT
        */
    }
}
