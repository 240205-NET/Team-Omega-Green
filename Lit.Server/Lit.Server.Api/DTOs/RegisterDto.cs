using System.ComponentModel.DataAnnotations;

namespace Lit.Server.Api
{
	public class RegisterDto
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}