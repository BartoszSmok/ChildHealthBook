using ChildHealthBook.Web.CookieServices.Validator;
using ChildHealthBook.Web.Models.AnalyticsDtos;
using ChildHealthBook.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Controllers
{
    public class AnalyticsController : Controller
    {
        private AnalyticsService _analyticsService;
        private UserSessionCookieValidator _cookieValidator;

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
                    WebSharedStats sharedStats = await _analyticsService.GetAnalysis();

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
