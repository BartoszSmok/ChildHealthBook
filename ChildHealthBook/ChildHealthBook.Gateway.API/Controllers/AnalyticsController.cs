using ChildHealthBook.Common.AnalyticsDtos;
using ChildHealthBook.Gateway.API.Communication.Bridge;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildHealthBook.Gateway.API.Controllers
{
    [Route("/Gateway/[controller]")]
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
        private ILogger<AnalyticsController> _logger;
        public AnalyticsController(AnalyticsCommunicationBridge<VaccinationFactorRecord> currVaccinationFactor,
            AnalyticsCommunicationBridge<IEnumerable<VaccinationFactorRecord>> vaccinationFactorHistory,
            AnalyticsCommunicationBridge<ChildrenAverageAgeRecord> currChildrenAverageAge,
            AnalyticsCommunicationBridge<IEnumerable<ChildrenAverageAgeRecord>> childrenAverageAgeHistory,
            AnalyticsCommunicationBridge<ChildrenAverageCountPerParentRecord> currChildrenAverageCountPerParent,
            AnalyticsCommunicationBridge<IEnumerable<ChildrenAverageCountPerParentRecord>> childrenAverageCountPerParentHistory,
            IConfiguration config,
            ILogger<AnalyticsController> logger)
        {
            _currVaccinationFactor = currVaccinationFactor;
            _vaccinationFactorHistory = vaccinationFactorHistory;
            _currChildrenAverageAge = currChildrenAverageAge;
            _childrenAverageAgeHistory = childrenAverageAgeHistory;
            _currChildrenAverageCountPerParent = currChildrenAverageCountPerParent;
            _childrenAverageCountPerParentHistory = childrenAverageCountPerParentHistory;
            _analyticsApiBaseUrl = config.GetSection("Gateway:AnalyticsAPI:BaseUrl").Value;
            _logger = logger;
        }

        [HttpGet]
        [Route("vaccinationFactor")]
        public async Task<ActionResult<VaccinationFactorRecord>> GetVaccinationFactor()
        {
            try
            {
                _logger.LogInformation("Successfull connect");
                return Ok(await _currVaccinationFactor.Get($"{_analyticsApiBaseUrl}/api/analytics/vaccinationFactor"));
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Exception {e}");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, e.Message);
            }

        }
        [HttpGet]
        [Route("childrenAverageAge")]
        public async Task<ActionResult<ChildrenAverageAgeRecord>> GetChildrenAverageAge()
        {
            try
            {
                _logger.LogInformation("Successfull connect");
                return Ok(await _currChildrenAverageAge.Get($"{_analyticsApiBaseUrl}/api/analytics/childrenAverageAge"));
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Exception {e}");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, e.Message);
            }
        }

        [HttpGet]
        [Route("childrenAverageCountPerParent")]
        public async Task<ActionResult<ChildrenAverageCountPerParentRecord>> GetChildrenAverageCountPerParent()
        {
            try
            {
                _logger.LogInformation("Successfull connect");
                return Ok(await _currChildrenAverageCountPerParent.Get($"{_analyticsApiBaseUrl}/api/analytics/childrenAverageCountPerParent"));
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Exception {e}");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, e.Message);
            }
        }

        [HttpGet]
        [Route("vaccinationFactorHistory")]
        public async Task<ActionResult<IEnumerable<VaccinationFactorRecord>>> GetVaccinationFactorHistory()
        {
            try
            {
                _logger.LogInformation("Successfull connect");
                return Ok(await _vaccinationFactorHistory.Get($"{_analyticsApiBaseUrl}/api/history/vaccinationFactor"));
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Exception {e}");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, e.Message);
            }
        }
        [HttpGet]
        [Route("childrenAverageAgeHistory")]
        public async Task<ActionResult<IEnumerable<ChildrenAverageAgeRecord>>> GetChildrenAverageAgeHistory()
        {
            try
            {
                _logger.LogInformation("Successfull connect");
                return Ok(await _childrenAverageAgeHistory.Get($"{_analyticsApiBaseUrl}/api/history/childrenAverageAge"));
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Exception {e}");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, e.Message);
            }
        }

        [HttpGet]
        [Route("childrenAverageCountPerParentHistory")]
        public async Task<ActionResult<IEnumerable<ChildrenAverageCountPerParentRecord>>> GetChildrenAverageCountPerParentHistory()
        {
            try
            {
                _logger.LogInformation("Successfull connect");
                return Ok(await _childrenAverageCountPerParentHistory.Get($"{_analyticsApiBaseUrl}/api/history/childrenAverageCountPerParent"));
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Exception {e}");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, e.Message);
            }
        }
    }
}
