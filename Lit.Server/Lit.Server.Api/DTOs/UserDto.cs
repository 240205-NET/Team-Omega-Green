namespace Lit.Server.Api
{

	public class UserDto
	{
		public int? UserId { get; set; }
		public string? Username { get; set; } // Ensure uniqueness via Fluent API
		public string? Password { get; set; } // Ensure uniqueness via Fluent API
		public string? Email { get; set; } // Ensure uniqueness via Fluent API
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Token { get; set; }

	}

}