using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BRS.Data;
using BRS.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BRS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        //Fields
        private readonly IUserRepository _Irepo;

        private readonly ILogger<UserController> _logger;

        //constructor
        public UserController(IUserRepository Irepo, ILogger<UserController> logger)
        {
            this._Irepo = Irepo;
            this._logger = logger;
        }

        //get all user details
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUsersAsync()
        {
            IEnumerable<Users> details;
            try
            {
                details = await _Irepo.GetAllUsersAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return details.ToList();
        }

        [HttpGet("[action]/{email}")]
        public async Task<ActionResult<bool>> CheckUserExistsOrNotByEmail(string email)
        {
            Console.WriteLine("email is " + email);
            bool exists;
            try
            {
               exists = await _Irepo.CheckUserExistsOrNotByEmail(email);
            }catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(200);
            }
            return exists;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserByIdAsync(int id)
        {
            Users user;
            
            try
            {
                user = await _Irepo.GetUsersByIdAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return user;
        }

        [HttpGet("[action]/{email}")]
        public async Task<ActionResult<Users>> GetUsersByEmailAsync(string email)
        {
            Users user;
            try
            {
                user = await _Irepo.GetUsersByEmail(email);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return user;
        }
            // GET api/values/5
            [HttpGet("/user-details/{udId}")]
        public async Task<ActionResult<UserDetails>> GetUserDetailsByIdAsync(int udId)
        {
            UserDetails ud;

            try
            {
                ud = await _Irepo.GetUserDetailsByIdAsync(udId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return ud;
        }



        // POST api/values
        [HttpPost]
        public async Task<ActionResult> AddUserAsync([FromBody] Users value)
        {   
            try
            {
               await _Irepo.AddUser(value);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }

        // POST api/values
        [HttpPost("/user-details")]
        public async Task<ActionResult> AddUserDetailsAsync([FromBody] UserDetails value)
        {
            try
            {
                await _Irepo.AddUserDetails(value);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }

        [HttpPut("/user-details/{userDetailsId}")]
        public async Task<ActionResult> UpdateUserDetailsAsync(int userDetailsId, [FromBody] UserDetails ud)
        {
            try
            {
                await _Irepo.UpdateUserDetailsAsync(userDetailsId, ud);
            }catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }

        [HttpPut("[action]/{userId}")]
        public async Task<ActionResult> UpdateUsersAsync(int userId, [FromBody] Users user)
        {
            try
            {
                await _Irepo.UpdateUserAsync(userId, user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(Users user)
        {
            Console.WriteLine("user login info are: " + user._email + "password: " + user._password);
;
           bool result =  await _Irepo.LoginUser(user._email, user._password);
            if (result)
            {
                return StatusCode(200);
            }
            else
            {
                return StatusCode(403);
            }
           
        }


        //get all user details
        [HttpGet("user-details")]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetAllUserDetailsAsync()
        {
            IEnumerable<UserDetails> details;
            try
            {
                details = await _Irepo.GetAllUsersDetailsAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return details.ToList();
        }



    }
}

