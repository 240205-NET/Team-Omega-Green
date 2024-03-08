using System;

namespace Lit.Server.Logic
{
	public class Cart
	{
		[Key]
		public int CartId { get; set; }
		[ForeignKey("User")]
		public int UserId { get; set; }
		public User User { get; set; }
		public List<Book> Books { get; set; } = new List<Book>();
	}
}