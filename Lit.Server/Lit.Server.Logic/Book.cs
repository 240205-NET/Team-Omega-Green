using System;

namespace Lit.Server.Logic
{
	public class Book
	{
		[Key]
		public int isbn { get; set; } // unique 
		public string title { get; set; }
		public string author { get; set; }
		public int categoryId { get; set; }
		public Category category
	}
}