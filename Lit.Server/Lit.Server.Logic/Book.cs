using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Lit.Server.Logic
{
	public class Book
	{
		[Key]
		public string Isbn { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}

}