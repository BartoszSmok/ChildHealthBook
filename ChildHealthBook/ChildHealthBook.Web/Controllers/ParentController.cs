using ChildHealthBook.Web.Models.ChildDtos;
using ChildHealthBook.Web.Models.EventDtos;
using ChildHealthBook.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Controllers
{
    public class ParentController : Controller
    {
        ParentService _parentService;
        public ParentController(ParentService parentService)
        {
            _parentService = parentService;
        }

        public async Task<IActionResult> ChildrenIndex()
        {
            var childrens = await _parentService.GetMyChildren();
            if (childrens != null)
            {
                return View(childrens);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ChildCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChildCreate(ChildCreateDto childCreateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _parentService.CreateChild(childCreateDto);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ChildrenIndex", "Parent");
                }
            }
            return View(childCreateDto);
        }

        public async Task<IActionResult> ChildDetails(Guid childId)
        {
            var child = await _parentService.GetChildByIdWithEvents(childId);
            if (child != null)
            {
                return View(child);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ExaminationCreate()
        {
            return View();
        }

        public async Task<IActionResult> PersonalEventCreate(PersonalEventCreateDto personalEventCreateDto, Guid childId)
        {
            if (ModelState.IsValid)
            {
                var result = await _parentService.CreatePersonalEvent(personalEventCreateDto, childId);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ChildDetails", "Parent");
                }
            }
            return View(personalEventCreateDto);
        }
        public async Task<IActionResult> MedicalEventCreate()
        {
            return View();
        }
        
        public async Task<IActionResult> ShareEvent(Guid eventId)
        {
            return View();
        }

        public async Task<IActionResult> Share(Guid eventId)
        {
            return RedirectToAction(nameof(ChildDetails));
        }

        public async Task<IActionResult> SharedEvent()
        {
            return View();
        }

    }
}
