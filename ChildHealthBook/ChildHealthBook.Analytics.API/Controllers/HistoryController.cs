using ChildHealthBook.Analytics.API.Repository;
using ChildHealthBook.Common.AnalyticsDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ChildHealthBook.Analytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private IHistoryRecordRepository<ChildrenAverageAgeRecord> _averageAgeHistory { get; set; }
        private IHistoryRecordRepository<ChildrenAverageCountPerParentRecord> _averageChildrenCountPerParentHistory { get; set; }
        private IHistoryRecordRepository<VaccinationFactorRecord> _vaccinationFactorHistory { get; set; }

        private ILogger<HistoryController> _logger;

        public HistoryController(IHistoryRecordRepository<ChildrenAverageAgeRecord> averageAgeHistory, 
            IHistoryRecordRepository<ChildrenAverageCountPerParentRecord> averageChildrenCountPerParentHistory, 
            IHistoryRecordRepository<VaccinationFactorRecord> vaccinationFactorHistory,
            ILogger<HistoryController> logger)
        {
            _averageAgeHistory = averageAgeHistory;
            _averageChildrenCountPerParentHistory = averageChildrenCountPerParentHistory;
            _vaccinationFactorHistory = vaccinationFactorHistory;
            _logger = logger;
        }

        [HttpGet]
        [Route("vaccinationFactor")]
        public ActionResult<IEnumerable<VaccinationFactorRecord>> GetVaccinationFactorHistory()
        {
            try
            {
                _logger.LogInformation("Vaccination factor history fetch attempt...");
                return Ok(_vaccinationFactorHistory.GetAll());
            }
            catch (Exception e)
            {
                LogError("GetVaccinationFactorHistory", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet]
        [Route("childrenAverageAge")]
        public ActionResult<IEnumerable<ChildrenAverageAgeRecord>> GetChildrenAverageAgeHistory()
        {
            try
            {
                _logger.LogInformation("Children average age history fetch attempt...");
                return Ok(_averageAgeHistory.GetAll());
            }
            catch (Exception e)
            {
                LogError("GetChildrenAverageAgeHistory", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("childrenAverageCountPerParent")]
        public ActionResult<IEnumerable<ChildrenAverageCountPerParentRecord>> GetChildrenAverageCountPerParentHistory()
        {
            try
            {
                _logger.LogInformation("Children average count per parent history fetch attempt...");
                return Ok(_averageChildrenCountPerParentHistory.GetAll());
            }
            catch (Exception e)
            {
                LogError("GetChildrenAverageCountPerParentHistory", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("vaccinationFactor")]
        public ActionResult CreateVaccinationFactorHistoryRecord(VaccinationFactorRecord vaccinationFactor)
        {
            try
            {
                _logger.LogInformation("Attempt of creating vaccination factory history record...");
                return CreatedAtAction("vaccinationFactor", _vaccinationFactorHistory.Insert(vaccinationFactor));
            }
            catch (Exception e)
            {
                LogError("CreateVaccinationFactorHistoryRecord", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("childrenAverageAge")]
        public ActionResult CreateChildrenAverageAgeHistoryRecord(ChildrenAverageAgeRecord childrenAverageAgeRecord)
        {
            try
            {
                _logger.LogInformation("Attempt of creating children average age history record...");
                return CreatedAtAction("childrenAverageAge", _averageAgeHistory.Insert(childrenAverageAgeRecord));
            }
            catch (Exception e)
            {
                LogError("CreateChildrenAverageAgeHistoryRecord", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("childrenAverageCountPerParent")]
        public ActionResult CreateChildrenAverageCountPerParentHistoryRecord(ChildrenAverageCountPerParentRecord childrenAverageCountPerParentRecord)
        {
            try
            {
                _logger.LogInformation("Attempt of creating children average count per parent history record...");
                return CreatedAtAction("childrenAverageCountPerParent", _averageChildrenCountPerParentHistory.Insert(childrenAverageCountPerParentRecord));
            }
            catch (Exception e)
            {
                LogError("CreateChildrenAverageCountPerParentHistoryRecord", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        private void LogError(string location, string message)
        {
            _logger.LogError($"Exception thrown at {location} in HistoryController, exception message: {message}");
        }
    }
}
