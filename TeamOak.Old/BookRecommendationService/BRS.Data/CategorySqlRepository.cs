using System;
using System.Data.SqlClient;
using BRS.Logic;
using Microsoft.Extensions.Logging;

namespace BRS.Data
{
	public class CategorySqlRepository : ICategoryRepository
	{
        //fields
        private readonly string _connectionString;
        private readonly ILogger<CategorySqlRepository> _logger;
		//constructor
		public CategorySqlRepository(string connectionString ,ILogger<CategorySqlRepository> logger)
		{
            this._connectionString = connectionString;
            this._logger = logger;
		}

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string sqlText = @"SELECT * FROM [book].[categories];";
            using SqlCommand cmd = new SqlCommand(sqlText, connection);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<Category> categories = new List<Category>();

            while(await reader.ReadAsync())
            {
                int Id = (int)reader["id"];
                string name = reader["name"].ToString() ?? "";
                categories.Add(new Category(Id, name));
            }
            await connection.CloseAsync();
            return categories;
        }

        public async Task AddNewCategoriesAsync(Category cat)
        {
            string inputCategoryToLowerCase = cat._name.ToLower();
            Category category = await checkUniqueCategoryNameExistsorNotAsync(cat._name);
            string databaseCategoryToLowerCase = category._name.ToLower();
            if(inputCategoryToLowerCase != databaseCategoryToLowerCase)
            {
                using SqlConnection connection = new SqlConnection(this._connectionString);
                await connection.OpenAsync();
                string sqlText = @"INSERT INTO [book].[categories] (name) VALUES (@name);";
                using SqlCommand cmd = new SqlCommand(sqlText, connection);
                cmd.Parameters.AddWithValue("@name", cat._name);
                await cmd.ExecuteNonQueryAsync();
                await connection.CloseAsync();
                _logger.LogInformation($"The Category {cat._name} is added to database");
            }
        
            else
            {
                Console.WriteLine("Category Exists");
                _logger.LogInformation($"The Category {cat._name} already exists in database");
            }
            
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string sqlText = @"SELECT * FROM [book].[categories] WHERE id=@id;";
            using SqlCommand cmd = new SqlCommand(sqlText, connection);
            cmd.Parameters.AddWithValue("@id", id);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            Category category = new Category();
            while (await reader.ReadAsync())
            {
                int Id = (int)reader["id"];
                string name = reader["name"].ToString() ?? "";
                category = new Category(Id, name);
            }
            await connection.CloseAsync();
            return category;
        }


        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string sqlText = @"SELECT * FROM [book].[categories] WHERE name=@name;";
            using SqlCommand cmd = new SqlCommand(sqlText, connection);
            cmd.Parameters.AddWithValue("@name", name);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            Category category = new Category();
            while (await reader.ReadAsync())
            {
                int Id = (int)reader["id"];
                string catName = reader["name"].ToString() ?? "";
                category = new Category(Id, catName);
            }
            await connection.CloseAsync();
            return category;
        }
        public async Task<Category> checkUniqueCategoryNameExistsorNotAsync(string name) {

            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string sqlText = @"SELECT * FROM [book].[categories] WHERE name=@catName;";
            using SqlCommand cmd = new SqlCommand(sqlText, connection);
            cmd.Parameters.AddWithValue("@catName", name);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            Category category = new Category();
            while (await reader.ReadAsync())
            {
                int Id = (int)reader["id"];
                string catName = reader["name"].ToString() ?? "";
                category = new Category(Id, catName);
            }
            await connection.CloseAsync();
            return category;
        }
    }
}

