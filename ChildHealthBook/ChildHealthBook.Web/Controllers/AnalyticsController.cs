using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Controllers
{
    public class AnalyticsController : Controller
    {
        public async Task<IActionResult> AnalysisIndex()
        {
            return View();
        }

        public async Task<IActionResult> VaccinationHistory()
        {
            return View();
        }
        
        public async Task<IActionResult> AvarageAgeHistory()
        {
            return View();
        }
        public async Task<IActionResult> ChildrenCountHistory()
        {
            return View();
        }
    }
}
