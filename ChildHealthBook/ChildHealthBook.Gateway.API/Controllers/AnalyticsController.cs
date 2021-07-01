using ChildHealthBook.Common.AnalyticsDtos;
using ChildHealthBook.Gateway.API.Communication.Bridge;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private string _analyticsApiBaseUrl;
        private AnalyticsCommunicationBridge<VaccinationFactorRecord> _currVaccinationFactor
        { get; set; }
        private AnalyticsCommunicationBridge<IEnumerable<VaccinationFactorRecord>> _vaccinationFactorHistory { get; set; }

        private AnalyticsCommunicationBridge<ChildrenAverageAgeRecord> _currChildrenAverageAge { get; set; }
        private AnalyticsCommunicationBridge<IEnumerable<ChildrenAverageAgeRecord>> _childrenAverageAgeHistory { get; set; }

        private AnalyticsCommunicationBridge<ChildrenAverageCountPerParentRecord> _currChildrenAverageCountPerParent { get; set; }
        private AnalyticsCommunicationBridge<IEnumerable<ChildrenAverageCountPerParentRecord>> _childrenAverageCountPerParentHistory { get; set; }

        public AnalyticsController(AnalyticsCommunicationBridge<VaccinationFactorRecord> currVaccinationFactor,
            AnalyticsCommunicationBridge<IEnumerable<VaccinationFactorRecord>> vaccinationFactorHistory,
            AnalyticsCommunicationBridge<ChildrenAverageAgeRecord> currChildrenAverageAge,
            AnalyticsCommunicationBridge<IEnumerable<ChildrenAverageAgeRecord>> childrenAverageAgeHistory,
            AnalyticsCommunicationBridge<ChildrenAverageCountPerParentRecord> currChildrenAverageCountPerParent,
            AnalyticsCommunicationBridge<IEnumerable<ChildrenAverageCountPerParentRecord>> childrenAverageCountPerParentHistory,
            IConfiguration config)
        {
            _currVaccinationFactor = currVaccinationFactor;
            _vaccinationFactorHistory = vaccinationFactorHistory;
            _currChildrenAverageAge = currChildrenAverageAge;
            _childrenAverageAgeHistory = childrenAverageAgeHistory;
            _currChildrenAverageCountPerParent = currChildrenAverageCountPerParent;
            _childrenAverageCountPerParentHistory = childrenAverageCountPerParentHistory;
            _analyticsApiBaseUrl = config.GetSection("Gateway:AnalyticsAPI:BaseUrl").Value;
        }

        [HttpGet]
        [Route("vaccinationFactor")]
        public async Task<ActionResult<VaccinationFactorRecord>> GetVaccinationFactor()
        {
            try
            {
                return Ok(await _currVaccinationFactor.Get($"{_analyticsApiBaseUrl}/api/analytics/vaccinationFactor"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet]
        [Route("childrenAverageAge")]
        public async Task<ActionResult<ChildrenAverageAgeRecord>> GetChildrenAverageAge()
        {
            try
            {
                return Ok(await _currChildrenAverageAge.Get($"{_analyticsApiBaseUrl}/api/analytics/childrenAverageAge"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("childrenAverageCountPerParent")]
        public async Task<ActionResult<ChildrenAverageCountPerParentRecord>> GetChildrenAverageCountPerParent()
        {
            try
            {
                return Ok(await _currChildrenAverageCountPerParent.Get($"{_analyticsApiBaseUrl}/api/analytics/childrenAverageCountPerParent"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("vaccinationFactorHistory")]
        public async Task<ActionResult<IEnumerable<VaccinationFactorRecord>>> GetVaccinationFactorHistory()
        {
            try
            {
                return Ok(await _vaccinationFactorHistory.Get($"{_analyticsApiBaseUrl}/api/history/vaccinationFactor"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet]
        [Route("childrenAverageAgeHistory")]
        public async Task<ActionResult<IEnumerable<ChildrenAverageAgeRecord>>> GetChildrenAverageAgeHistory()
        {
            try
            {
                return Ok(await _childrenAverageAgeHistory.Get($"{_analyticsApiBaseUrl}/api/history/childrenAverageAge"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("childrenAverageCountPerParentHistory")]
        public async Task<ActionResult<IEnumerable<ChildrenAverageCountPerParentRecord>>> GetChildrenAverageCountPerParentHistory()
        {
            try
            {
                return Ok(await _childrenAverageCountPerParentHistory.Get($"{_analyticsApiBaseUrl}/api/history/childrenAverageCountPerParent"));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
