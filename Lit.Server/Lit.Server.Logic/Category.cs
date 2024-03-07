using System;

namespace Lit.Server.Logic
{
	public class Category
	{
		[Key]
		public int categoryId { set; get}// unique, private?
		public string name { set; get; }

	}
}