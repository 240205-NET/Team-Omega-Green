using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRS.Data;
using BRS.Logic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BRS.API.Controllers
{
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        //Fields
        private readonly IHistoryRepository _repo;

        private readonly ILogger<HistorySqlRepository> _logger;
        //Constructor
        public HistoryController(IHistoryRepository repo, ILogger<HistorySqlRepository> logger)
        {
            this._repo = repo;
            this._logger = logger;

        }
        // GET: api/values
        [HttpGet("/user/{uId}")]
        public async Task<ActionResult<IEnumerable<History>>> GetAllUserHistory(int uId)
        {
            IEnumerable<History> histories;
            try
            {
                histories = await _repo.GetAllHistoryOfUserAsync(uId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return histories.ToList();
        }

        // POST api/values
        [HttpPost("/user/{id}")]
        public async Task<IActionResult> AddHistoryOfUserAsync([FromBody] History value, int id)
        {
            try
            {
                await _repo.AddHistoryAsync(value, id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

