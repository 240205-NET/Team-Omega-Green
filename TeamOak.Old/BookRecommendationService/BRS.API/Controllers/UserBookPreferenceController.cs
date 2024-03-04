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
    public class UserBookPreferenceController : ControllerBase
    {
        //fields
        private readonly IUserBookPreferenceRepository _repo;
        private readonly ILogger<UserBookPreferenceController> _logger;

        //constructor
        public UserBookPreferenceController(IUserBookPreferenceRepository repo, ILogger<UserBookPreferenceController> logger)
        {
            this._repo = repo;
            this._logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBookPreference>>> GetAllUserBookPreferecesAsync()
        {
            IEnumerable<UserBookPreference> preferences;
            try
            {
                preferences = await _repo.GetAllUsersPreferencesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return preferences.ToList();
        }

        // GET api/values/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserBookPreference>> GetUserBookPreferenceByUserIdAsync(int userId)
        {
            UserBookPreference preference;
            try
            {
                preference = await _repo.GetUserBookPreferenceByUserIdAsync(userId);
            }catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return preference;

        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> AddUserBookPreferenceAsync([FromBody] UserBookPreference preference)
        {
            try
            {
                await _repo.AddUserBookPreferenceAsync(preference);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }
    }
}

