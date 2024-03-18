using System.ComponentModel.DataAnnotations;

namespace Lit.Server.Api
{
	public class LoginDto
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}