using System.Security.Cryptography;
using System.Text;
using Lit.Server.Api;
using Lit.Server.Data;
using Lit.Server.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lit.Server.Api
{

	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly ApplicationContext _context;
		private readonly IConfiguration _configuration;
		private readonly ILogger<AuthController> _logger;
		private readonly ITokenService _tokenService;

		public AuthController(ILogger<AuthController> logger, ApplicationContext context, IConfiguration configuration, ITokenService tokenService)
		{
			_logger = logger;
			_context = context;
			_configuration = configuration;
			_tokenService = tokenService;
		}
		[HttpPost("register")] // POST: api/account/register
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{

			if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
			using var hmac = new HMACSHA512(); // using to dispose of this class automatically when were done with it, if a class implements IDispoable it must implement a Dispose method - when we're done with this class, it will call dispose
			var user = new User
			{
				Username = registerDto.Username.ToLower(), // can be null
				PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
				PasswordSalt = hmac.Key,
				Email = registerDto.Email,
				FirstName = registerDto.FirstName,
				LastName = registerDto.LastName,


			};
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return new UserDto
			{
				Username = user.Username,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Token = _tokenService.CreateToken(user),
			};

		}
		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await _context.Users.SingleOrDefaultAsync(user => user.Username == loginDto.Username);

			if (user == null) return Unauthorized("invalid username");

			using var hmac = new HMACSHA512(user.PasswordSalt); // returns Byte array
			var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

			for (int i = 0; i < computedHash.Length; i++)
			{
				if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
			}
			return new UserDto
			{
				Username = user.Username,
				Token = _tokenService.CreateToken(user)
			};
		}

		private async Task<bool> UserExists(string username)
		{
			return await _context.Users.AnyAsync(user => user.Username == username.ToLower());
		}
	}
}