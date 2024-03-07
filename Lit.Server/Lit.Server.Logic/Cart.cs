using System;

namespace Lit.Server.Logic
{
	public class Cart
	{
		List<Book> books { set; get; }
		public int userId { set; get; }
		public User user { set; get; } // ?
	}
}