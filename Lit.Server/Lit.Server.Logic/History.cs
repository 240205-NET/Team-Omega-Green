using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Lit.Server.Logic
{
	public class History
	{
		[Key]
		public int HistoryId { get; set; }
		[ForeignKey("User")]
		public int UserId { get; set; }
		public List<Book> Purchases { get; set; } = new List<Book>();
	}
}