using ChildHealthBook.Common.WebDtos.EventDtos;
using ChildHealthBook.Web.CookieServices.Validator;
using ChildHealthBook.Web.Models.ChildDtos;
using ChildHealthBook.Web.Models.EventDtos;
using ChildHealthBook.Web.Models.IdentityDtos;
using ChildHealthBook.Web.Models.Session;
using ChildHealthBook.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Controllers
{
    public class ParentController : Controller
    {
        ParentService _parentService;
        private UserSessionCookieValidator _cookieValidator;

        public ParentController(ParentService parentService,
            UserSessionCookieValidator cookieValidator)
        {
            _parentService = parentService;
            _cookieValidator = cookieValidator;
        }

        [HttpGet]
        public async Task<IActionResult> ChildrenIndex()
        {
            if (_cookieValidator.IsCookiePresent(Request))
            {
                if (_cookieValidator.IsRoleValid(Request, "Parent"))
                {
                    var userData = JsonConvert.DeserializeObject<AuthUserSession>(Request.Cookies["UserData"]);
                    var parentId = userData.Id;
                    var childrens = await _parentService.GetMyChildren(parentId);
                    if (childrens != null)
                    {
                        return View(childrens);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ChildCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChildCreate(WebChildCreateDto childCreateDto)
        {
            if (_cookieValidator.IsCookiePresent(Request))
            {
                if (_cookieValidator.IsRoleValid(Request, "Parent"))
                {
                    var userData = JsonConvert.DeserializeObject<AuthUserSession>(Request.Cookies["UserData"]);
                    var parentId = userData.Id;
                    if (ModelState.IsValid)
                    {
                        var result = await _parentService.CreateChild(childCreateDto, parentId);
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("ChildrenIndex", "Parent");
                        }
                    }
                }
            }
            return View(childCreateDto);
            
        }

        [HttpGet]
        public async Task<IActionResult> ChildDetails([FromRoute] Guid id)
        {
            if (_cookieValidator.IsCookiePresent(Request))
            {
                if (_cookieValidator.IsRoleValid(Request, "Parent"))
                {
                    var child = await _parentService.GetChildByIdWithEvents(id);
                    if (child != null)
                    {
                        return View(child);
                    }
                }
            }
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> ExaminationCreate(Guid id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExaminationCreate([FromRoute] Guid id, WebMedicalExaminationCreateDto medicalExaminationCreateDto)
        {
            if (_cookieValidator.IsCookiePresent(Request))
            {
                if (_cookieValidator.IsRoleValid(Request, "Parent"))
                {
                    MedicalExaminationCreateDto medicalExaminationCreate = new MedicalExaminationCreateDto
                    {
                        ChildId = id,
                        Comment = medicalExaminationCreateDto.Comment,
                        Address = medicalExaminationCreateDto.Address,
                        DateOfMedicalExamination = medicalExaminationCreateDto.DateOfMedicalExamination,
                        ExaminationTitle = medicalExaminationCreateDto.ExaminationTitle,
                        ExaminationType = medicalExaminationCreateDto.ExaminationType,
                        SpecialistFullName = medicalExaminationCreateDto.SpecialistFullName
                    };
                    if (ModelState.IsValid)
                    {
                        var result = await _parentService.AddNewExamination(medicalExaminationCreate);
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("ChildDetails", "Parent", new { id = id });
                        }
                    }
                }
            }
            return View(medicalExaminationCreateDto);
        }

        public async Task<IActionResult> PersonalEventCreate(Guid id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PersonalEventCreate([FromRoute] Guid id, WebPersonalEventCreateDto personalEventCreateDto)
        {
            if (_cookieValidator.IsCookiePresent(Request))
            {
                if (_cookieValidator.IsRoleValid(Request, "Parent"))
                {
                    PersonalEventCreateDto personalEventCreate = new PersonalEventCreateDto
                    {
                        ChildId = id,
                        DateOfEvent = personalEventCreateDto.DateOfEvent,
                        Comment = personalEventCreateDto.Comment,
                        EventTitle = personalEventCreateDto.EventTitle,
                        EventType = personalEventCreateDto.EventType
                    };
                    if (ModelState.IsValid)
                    {
                        var result = await _parentService.CreatePersonalEvent(personalEventCreate);
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("ChildDetails", "Parent", new { id = id });
                        }
                    }
                }
            }
            return View(personalEventCreateDto);
        }
        
        public async Task<IActionResult> MedicalEventCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MedicalEventCreate([FromRoute] Guid id, WebMedicalEventCreateDto webMedicalEventCreateDto)
        {
            if (_cookieValidator.IsCookiePresent(Request))
            {
                if (_cookieValidator.IsRoleValid(Request, "Parent"))
                {
                    MedicalEventCreateDto medicalEventCreateDto = new MedicalEventCreateDto
                    {
                        ChildId = id,
                        Comment = webMedicalEventCreateDto.Comment,
                        DateOfEvent = webMedicalEventCreateDto.DateOfEvent,
                        EventTitle = webMedicalEventCreateDto.EventTitle,
                        EventType = webMedicalEventCreateDto.EventType
                    };
                    if (ModelState.IsValid)
                    {
                        var result = await _parentService.AddNewMedicalEvent(medicalEventCreateDto);
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("ChildDetails", "Parent", new { id = id });
                        }
                    }
                }
            }
            return View(webMedicalEventCreateDto);
        }
        
        [HttpGet] // id -> eventId
        public async Task<IActionResult> ShareEvent([FromRoute] Guid id)
        {
            if (_cookieValidator.IsCookiePresent(Request))
            {
                if (_cookieValidator.IsRoleValid(Request, "Parent"))
                {
                    IEnumerable<WebParentReadDto> parentList = await _parentService.GetParentList();

                    return View(parentList);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet] // id -> eventid, val -> parent id
        public async Task<IActionResult> Share([FromRoute] Guid id, [FromRoute] Guid val)
        {
            if (_cookieValidator.IsCookiePresent(Request))
            {
                if (_cookieValidator.IsRoleValid(Request, "Parent"))
                {
                    _parentService.AddNewShare(id, val);
                    return RedirectToAction("ChildrenIndex", "Parent");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SharedEvent()
        {
            //IEnumerable<WebSharedEventReadDto> vaccinationFactorHistory = new List<WebSharedEventReadDto>
            //{
            //    new WebSharedEventReadDto{ChildFullName = "Adas", EventType="medical", Comment="succes", DateOfEvent=DateTime.Now, EventTitle="title"},
            //    new WebSharedEventReadDto{ChildFullName = "Ewa", EventType="inny", Comment="succes2", DateOfEvent=DateTime.Now, EventTitle="title2"},
            //    new WebSharedEventReadDto{ChildFullName = "marek", EventType="medical3", Comment="succes", DateOfEvent=DateTime.Now, EventTitle="3title"}
            //};
            //    return View(vaccinationFactorHistory);
            if (_cookieValidator.IsCookiePresent(Request))
            {
                if (_cookieValidator.IsRoleValid(Request, "Parent"))
                { 
                    var userData = JsonConvert.DeserializeObject<AuthUserSession>(Request.Cookies["UserData"]);
                    var parentId = userData.Id;
                    var sharedEvents = await _parentService.GetSharedEventByParentId(parentId);
                    if (sharedEvents != null)
                    {
                        return View(sharedEvents);
                    }
                }
            }
            return RedirectToAction("ChildrenIndex", "Parent");
        }
    }
}
