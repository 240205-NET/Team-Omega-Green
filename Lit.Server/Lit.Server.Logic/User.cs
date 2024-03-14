using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Lit.Server.Logic
{
	public class User
	{
		[Key]
		public int UserId { get; set; }
		public string Username { get; set; } // Ensure uniqueness via Fluent API
		public string Email { get; set; } // Ensure uniqueness via Fluent API
		public string Location { get; set; } // Changed type to string assuming it's a location name. Use a complex type or related entity for detailed addresses.
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }

		// Navigation properties if needed
		public List<Review> Reviews { get; set; } = new List<Review>();
		public Cart Cart { get; set; }
	}
}