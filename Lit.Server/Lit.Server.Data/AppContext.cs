using System;
using Microsoft.EntityFrameworkCore;
using Lit.Server.Logic;

namespace Lit.Server.Data
{
	public class AppContext : DbContext
	{
		// public AppContext(DbContextOptions<AppContext> options) : base(options)
		// {

		// }
		public DbSet<Book> Books { get; set; }
		public DbSet<Cart> Cart { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<History> Histories { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("");
		}
	}



}