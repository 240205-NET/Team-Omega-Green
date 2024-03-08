using System;

namespace Lit.Server.Logic
{
	public class Book
	{
		[Key]
		public int Isbn { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}