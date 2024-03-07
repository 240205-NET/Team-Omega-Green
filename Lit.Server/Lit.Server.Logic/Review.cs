using System;

namespace Lit.Server.Logic
{
	public class Review
	{
		public int rating { set; get; }
		public string body { set; get; }
		public int userId { get; set; }
		public User user { get; set; } // ? 
		public int bookId { get; set; }
		public Book book { get; set; } // ?
	}
}