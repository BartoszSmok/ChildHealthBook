using ChildHealthBook.Web.CookieServices.Validator;
using ChildHealthBook.Web.Models.AnalyticsDtos;
using ChildHealthBook.Web.Models.Session;
using ChildHealthBook.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Controllers
{
    public class AnalyticsController : Controller
    {
        private UserSessionCookieValidator _cookieValidator;

        AnalyticsService _analyticsService;
        public AnalyticsController(AnalyticsService analyticsService, UserSessionCookieValidator cookieValidator)
        {
            _analyticsService = analyticsService;
            _cookieValidator = cookieValidator;
        }
        public async Task<IActionResult> AnalysisIndex()
        {
            if (_cookieValidator.IsCookiePresent(Request))
            {
                if (_cookieValidator.IsRoleValid(Request, "Scientist"))
                {
                    var vaccinationFactor = await _analyticsService.GetVaccinationFactor();
                    var childrenAverageAge = await _analyticsService.GetChildrenAverageAge();
                    var childrenAverageAgePerParent = await _analyticsService.GetChildrenAverageCountPerParent();
                    WebSharedStats sharedStats = new WebSharedStats
                    {
                        VaccinationFactor = vaccinationFactor.Factor,
                        DateOfRecordCreationVaccinationFactor = vaccinationFactor.DateOfRecordCreation,
                        ChildrenAverageAge = childrenAverageAge.Average,
                        DateOfRecordCreationChildrenAverageAge = childrenAverageAge.DateOfRecordCreation,
                        AverageChildrenCountPerParent = childrenAverageAgePerParent.Average,
                        DateOfRecordCreationAverageChildrenCountPerParent = childrenAverageAgePerParent.DateOfRecordCreation
                    };

                    if (sharedStats != null)
                    {
                        return View(sharedStats);
                    }
                }
            }
            return RedirectToAction("AnalysisIndex", "Analytics");

        }

        public async Task<IActionResult> VaccinationHistory()
        {
            if (_cookieValidator.IsCookiePresent(Request))
            {
                if (_cookieValidator.IsRoleValid(Request, "Scientist"))
                {
                    var vaccinationFactorHistory = await _analyticsService.GetVaccinationFactorHistory();
                    return View(vaccinationFactorHistory);
                }
            }
            return RedirectToAction("AnalysisIndex", "Analytics");
        }


          public async Task<IActionResult> AvarageAgeHistory()
          {
               if (_cookieValidator.IsCookiePresent(Request))
               {
                   if (_cookieValidator.IsRoleValid(Request, "Scientist"))
                   {
                      var avarageAgeHistory = await _analyticsService.GetChildrenAverageAgeHistory();
                        if (avarageAgeHistory != null)
                        {
                         return View(avarageAgeHistory);
                        
                        }
                   }
               }
           return RedirectToAction("AnalysisIndex", "Analytics");

          }
            
        public async Task<IActionResult> ChildrenCountHistory()
        {
            if (_cookieValidator.IsCookiePresent(Request))
            {
                if (_cookieValidator.IsRoleValid(Request, "Scientist"))
                {

                    var childrenAverageCountPerParentHistory = await _analyticsService.GetChildrenAverageCountPerParentHistory();
                    if (childrenAverageCountPerParentHistory != null)
                    {
                        return View(childrenAverageCountPerParentHistory);

                    }
                }
            }
            return RedirectToAction("AnalysisIndex", "Analytics");
        } 
    }
}
