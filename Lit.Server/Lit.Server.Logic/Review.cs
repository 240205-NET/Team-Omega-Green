using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
		public string BookId { get; set; }
		public Book Book { get; set; }
	}
}