using System;
using Microsoft.EntityFrameworkCore;
using Lit.Server.Logic;
using Microsoft.Extensions.Configuration;

namespace Lit.Server.Data
{
	public class ApplicationContext : DbContext
	{
		// public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		// {

		// }
		IConfiguration _config;
		private readonly string _connectionString;


		public ApplicationContext(IConfiguration config)
		{
			_config = config;
			_connectionString = _config.GetConnectionString("DefaultConnection");
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);
		}
		public DbSet<Book> Books { get; set; }
		public DbSet<Cart> Cart { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<History> Histories { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<User> Users { get; set; }
	}
}