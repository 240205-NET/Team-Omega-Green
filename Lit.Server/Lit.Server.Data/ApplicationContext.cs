using Lit.Server.Logic;
using Microsoft.EntityFrameworkCore;

namespace Lit.Server.Data
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Cart> Cart { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<History> Histories { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<User> Users { get; set; }
	}
}