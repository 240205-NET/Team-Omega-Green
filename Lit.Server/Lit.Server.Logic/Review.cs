using System;

namespace Lit.Server.Logic
{
	public class Review
	{
		[Key]
		public int ReviewId { set; get; }
		public int Tating { set; get; }
		public string Body { set; get; }
		[ForeignKey("User")]
		public int UserId { get; set; }
		public User User { get; set; }
		[ForeignKey("Book")]
		public int BookId { get; set; }
		public Book Book { get; set; }
	}
}