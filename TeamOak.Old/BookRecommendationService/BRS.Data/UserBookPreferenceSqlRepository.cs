using System;
using System.Data.SqlClient;
using BRS.Logic;
using Microsoft.Extensions.Logging;

namespace BRS.Data
{
	public class UserBookPreferenceSqlRepository : IUserBookPreferenceRepository
	{
        // Fields
        private readonly string _connectionString;
        private readonly ILogger<UserBookPreferenceSqlRepository> _logger;

        // Constructor
        public UserBookPreferenceSqlRepository(string connectionString, ILogger<UserBookPreferenceSqlRepository> logger)
        {
            this._connectionString = connectionString;
            this._logger = logger;
        }

        public async Task<Users> GetUsersByIdAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            string query = @"SELECT * FROM [book].[user] WHERE id=@id;";
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            Users users = new Users();
            while (await reader.ReadAsync())
            {
                int userId = (int)reader["id"];
                string email = reader["email"].ToString() ?? "";
                string password = reader["password"].ToString() ?? "";
                string name = reader["name"].ToString() ?? "";

                users = new Users(userId, email, password, name);
            }
            await connection.CloseAsync();
            return users;
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

        public async Task AddUserBookPreferenceAsync(UserBookPreference preference)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            Users user = new Users(preference._user._id, preference._user._email!, preference._user._name!);
            Category c = new Category(preference._category._id, preference._category._name);
            
            try 
            {
              await GetUsersByIdAsync(user._id);

            }catch(Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            try
            {
                await GetCategoryByIdAsync(c._id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, preference._user._id);
            }
            //int id = userId.Id;
            
            string cmdText = @"INSERT INTO [book].[userBookPreference] (userId, categoryId) VALUES (@userId, @categoryId);";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@userId", user._id);
            cmd.Parameters.AddWithValue("@categoryId", c._id );
            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();

            //_logger.LogInformation("Executed AddUserDetails, userDetails id #{0} created.");

        }

        public async Task<IEnumerable<UserBookPreference>> GetAllUsersPreferencesAsync()
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string cmdText = @"SELECT * FROM [book].[userBookPreference];";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<UserBookPreference> preferencesList = new List<UserBookPreference>();
            Users user=new Users();
            Category category = new Category();
            while (await reader.ReadAsync())
            {
                int preferenceId = (int)reader["id"];
                 user = await GetUsersByIdAsync((int)reader["userId"]);
                category = await GetCategoryByIdAsync((int)reader["categoryId"]); 
               
                preferencesList.Add(new UserBookPreference(preferenceId,user, category));
            }
            await connection.CloseAsync();
            _logger.LogInformation("Executed GetAllUsersDetails, returned {0} results.", preferencesList.Count);

            return preferencesList;
        }

        public async Task<UserBookPreference> GetUserBookPreferenceByUserIdAsync(int userId)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string cmdText = @"SELECT * FROM [book].[userBookPreference] WHERE userId=@userId;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@userId", userId);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            UserBookPreference preference = new UserBookPreference();
            Users user;
            Category category;
            while (await reader.ReadAsync())
            {
                int preferenceId = (int)reader["id"];
                user = await GetUsersByIdAsync((int)reader["userId"]);
                category = await GetCategoryByIdAsync((int)reader["categoryId"]);
                preference = new UserBookPreference(preferenceId, user, category);
            }
            await connection.CloseAsync();
            _logger.LogInformation("Executed GetAllUsersDetails, returned {0} results.", preference._id);
            return preference;
        }
    }
}

