using System;

namespace Lit.Server.Logic
{
	public class User
	{
		public string userId { set; get; }
		public string username { set; get; } // unique
		public string email { set; get; } // unique?
		public int location { set; get; }
		public string firstName { set; get; }
		public string lastName { set; get; }
		public string password { set; get; }
		// TODO
	}
}