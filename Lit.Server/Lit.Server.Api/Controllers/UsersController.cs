using Microsoft.AspNetCore.Mvc;
using Lit.Server.Data;
using Microsoft.EntityFrameworkCore;
using Lit.Server.Logic;

public class UserDTO
{
	public int UserId { get; set; }
	public string Username { get; set; } // Ensure uniqueness via Fluent API
	public string Password { get; set; } // Ensure uniqueness via Fluent API
	public string Email { get; set; } // Ensure uniqueness via Fluent API
	public string FirstName { get; set; }
	public string LastName { get; set; }

}

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	private readonly ApplicationContext _context;
	private readonly IConfiguration _configuration;
	private readonly ILogger<UsersController> _logger;

	public UsersController(ILogger<UsersController> logger, ApplicationContext context, IConfiguration configuration)
	{
		_logger = logger;
		_context = context;
		_configuration = configuration;
	}

	// GET: api/users
	[HttpGet]
	public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
	{
		var users = await _context.Users
			.Include(user => user.Cart)
			.Select(user => new UserDTO
			{
				UserId = user.UserId,
				Username = user.Username,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
			})
			.ToListAsync();

		return users;
	}

	// GET: api/users/1
	[HttpGet("{UserId}")]
	public async Task<ActionResult<UserDTO>> GetUserById(int UserId)
	{
		var user = await _context.Users
			.Include(user => user.Cart)
			.Select(user => new UserDTO
			{
				UserId = user.UserId,
				Username = user.Username,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
			})
			.FirstOrDefaultAsync(user => user.UserId == UserId);

		if (user == null)
		{
			return NotFound();
		}

		return user;
	}

	// POST: api/users
	[HttpPost]
	public async Task<ActionResult<User>> CreateUser(UserDTO userDto)
	{
		User user = new User
		{
			Username = userDto.Username,
			Password = userDto.Password,
			Email = userDto.Email,
			FirstName = userDto.FirstName,
			LastName = userDto.LastName,
			// Since Category is a navigation property, EF will handle it based on the CategoryId
		};

		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(GetUserById), new { UserId = user.UserId }, user);
	}

	[HttpPut("{UserId}")]
	public async Task<IActionResult> EditUser(int UserId, [FromBody] UserDTO userDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		if (UserId != userDto.UserId)
		{
			return BadRequest("The User ID in the URL does not match the User ID in the body.");
		}

		var user = await _context.Users.FindAsync(UserId);
		if (user == null)
		{
			return NotFound();
		}

		user.Username = userDto.Username;
		user.Email = userDto.Email;
		user.FirstName = userDto.FirstName;
		user.LastName = userDto.LastName;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!_context.Users.Any(user => user.UserId == UserId))
			{
				return NotFound();
			}
			else
			{
				throw;
			}
		}
		return NoContent();
	}

	// DELETE: api/users/5
	[HttpDelete("{UserId}")]
	public async Task<IActionResult> DeleteUser(int UserId)
	{
		var user = await _context.Users.FindAsync(UserId);
		if (user == null)
		{
			return NotFound();
		}

		_context.Users.Remove(user);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}