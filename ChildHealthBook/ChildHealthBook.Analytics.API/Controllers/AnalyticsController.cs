using ChildHealthBook.Analytics.API.Analytics;
using ChildHealthBook.Analytics.API.Communication.Bridge;
using ChildHealthBook.Common.AnalyticsDtos;
using ChildHealthBook.Common.Identity.DTOs;
using ChildHealthBook.Common.WebDtos.ChildDtos;
using Common.Identity.Setup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Analytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private HistoryController _historyController { get; set; }
        private AverageCounter _averageCounter { get; set; }

        private IAnalyticsCounter _factorCounter { get; set; }
        private ICommunicationBridge<UserData> _identityApiCommunication { get; set; }
        private string ChildApiUrl { get; set; }
        private string IdentityApiUrl { get; set; }

        private ILogger<AnalyticsController> _logger;

        private ICommunicationBridge<ChildWithEventsReadDto> _childApiCommunication { get; set; }

        public AnalyticsController(HistoryController historyController, 
            AverageCounter averageCounter, 
            FactorCounter factorCounter,
            ICommunicationBridge<ChildWithEventsReadDto> childCommunication,
            ICommunicationBridge<UserData> identityCommunication, IConfiguration config, ILogger<AnalyticsController> logger)
        {
            _historyController = historyController;
            _averageCounter = averageCounter;
            _factorCounter = factorCounter;
            _childApiCommunication = childCommunication;
            _identityApiCommunication = identityCommunication;
            ChildApiUrl = config.GetSection("AnalyticsAPI:ChildApiUrl").Value; ;
            IdentityApiUrl = config.GetSection("AnalyticsAPI:IdentityApiUrl").Value;
            _logger = logger;
        }

        [HttpGet]
        [Route("vaccinationFactor")]
        public async Task<ActionResult<VaccinationFactorRecord>> GetVaccinationFactor()
        {
            try
            {
                
                string apiEndpoint = $"{ChildApiUrl}/api/child/Analytics";
                _logger.LogInformation($"Request url: {apiEndpoint}");
                var children = await _childApiCommunication.GetAll(apiEndpoint);
                float factor = _factorCounter.GetClassSpecifiedFactor(children);
                var factorRecord = new VaccinationFactorRecord() { Factor = factor, DateOfRecordCreation = DateTime.Now };
                _historyController.CreateVaccinationFactorHistoryRecord(factorRecord);
                LogSuccess("Vaccination factor", factor);
                return Ok(factorRecord);
            }catch(Exception e)
            {
                LogError("GetVaccinationFactor", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet]
        [Route("childrenAverageAge")]
        public async Task<ActionResult<ChildrenAverageAgeRecord>> GetChildrenAverageAge()
        {
            try
            {
                string apiEndpoint = $"{ChildApiUrl}/api/child";
                var children = await _childApiCommunication.GetAll(apiEndpoint);
                float average = _averageCounter.GetClassSpecifiedFactor(children);
                var averageRecord = new ChildrenAverageAgeRecord() { Average = average, DateOfRecordCreation = DateTime.Now };
                _historyController.CreateChildrenAverageAgeHistoryRecord(averageRecord);
                LogSuccess("Children average age", average);
                return Ok(averageRecord);
            }
            catch (Exception e)
            {
                LogError("GetChildrenAverageAge", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("childrenAverageCountPerParent")]
        public async Task<ActionResult<ChildrenAverageCountPerParentRecord>> GetChildrenAverageCountPerParent()
        {
            try
            {
                string childApiEndpoint = $"{ChildApiUrl}/api/child";
                string identityApiEndpoint = $"{IdentityApiUrl}/api/accounts/user/getUsers";
                var children = await _childApiCommunication.GetAll(childApiEndpoint);
                var parents = await _identityApiCommunication.GetAll(identityApiEndpoint);
                float average = _averageCounter.GetAverageChildrenCountPerParent(children.ToList().Count, parents.ToList().Count);
                var factorRecord = new ChildrenAverageCountPerParentRecord() { Average = average, DateOfRecordCreation = DateTime.Now };
                _historyController.CreateChildrenAverageCountPerParentHistoryRecord(factorRecord);
                LogSuccess("children average count per parent", average);
                return Ok(factorRecord);
            }
            catch (Exception e)
            {
                LogError("GetChildrenAverageCountPerParent", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        private void LogError(string location, string message)
        {
            _logger.LogError($"Exception thrown at {location} in AnalyticsController, exception message: {message}");
        }

        private void LogSuccess(string factorName, float factorValue)
        {
            _logger.LogInformation($"Successfully counted {factorName} as {factorValue} and created record.");
        }
    }
}
