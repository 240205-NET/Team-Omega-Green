using System;
using Lit.Server.Logic;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lit.Server.Data
{
	public class ApplicationContext : DbContext
	{
		IConfiguration _config;
		private readonly string _connectionString;

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{

		}

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

	public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
	{
		public ApplicationContext CreateDbContext(string[] args)
		{
			IConfiguration configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				 .Build();


			var build = new DbContextOptionsBuilder<ApplicationContext>();
			var connectionString = configuration.GetConnectionString("DefaultConnection");

			build.UseSqlServer(connectionString);

			return new ApplicationContext(build.Options);
		}
	}
}