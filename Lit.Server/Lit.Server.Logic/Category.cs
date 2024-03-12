using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Lit.Server.Logic
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }
		public string Name { get; set; }

		// If you want to establish a relationship from Category to Books
		public List<Book> Books { get; set; } = new List<Book>();
	}

}