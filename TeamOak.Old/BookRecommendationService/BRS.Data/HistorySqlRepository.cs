using System;
using System.Data.SqlClient;
using BRS.Logic;
using Microsoft.Extensions.Logging;

namespace BRS.Data
{
	public class HistorySqlRepository : IHistoryRepository
    {
        // Fields
        private readonly string _connectionString;
        private readonly ILogger<HistorySqlRepository> _logger;

        public HistorySqlRepository(string connectionString, ILogger<HistorySqlRepository> logger) 
		{
            this._connectionString = connectionString;
            this._logger = logger;
		}

        public async Task<IEnumerable<History>> GetAllHistoryOfUserAsync(int userId)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);

            await connection.OpenAsync();
            string cmdText = @"SELECT TOP 10 * FROM [book].[history] WHERE userId=@uId  ORDER BY created_on DESC;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@uId", userId);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            //get user from userId
            Users user = await GetUsersByIdAsync(userId);
            List<History> listHistories = new List<History>();
            //get book from bookId
            while (await reader.ReadAsync())
            {
                int historyId = (int)reader["id"];
                int bookId = (int)reader["bookid"];
                Book book = await GetBookByIdAsync(bookId);
                string desc = reader["description"].ToString() ?? "";
                DateTime createdOn = DateTime.Parse(reader["created_on"].ToString()!);
                listHistories.Add(new History(historyId, book, user, desc, createdOn));
            }
            await connection.CloseAsync();
            return listHistories;
        }

        public async Task AddHistoryAsync(History history, int userId)
        {
            Users user = history._user;
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string cmdText = @"INSERT INTO [book].[history] (bookId, userId, description, created_on) VALUES (@bookId, @userId, @desc, @dateCreated)";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@bookId", history._book.id);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@desc", history._description);
            cmd.Parameters.AddWithValue("@dateCreated", DateTime.Now);

            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();
            //_logger.LogInformation(message: "Executed AddHistoryAsync, history  #{0}: {1} - {2} created",history._description);
            //throw new NotImplementedException();
        }

        public async Task<Users> GetUsersByIdAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            string query = @"SELECT * FROM [book].[user] WHERE id=@id";
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

        public async Task<Book> GetBookByIdAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            string query = @"SELECT * FROM [book].[books] WHERE id=@id;";
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            Book book = new Book();
            Category cat = new Category();
            while (await reader.ReadAsync())
            {
                int bookId = (int)reader["id"];
                string title = reader["title"].ToString() ?? "";
                string author = reader["author"].ToString() ?? "";
                string isbn = reader["isbn"].ToString() ?? "";
                int categoryId = (int) reader["categoryId"];
                cat = new Category(categoryId, "");
                book = new Book(bookId, title, author, isbn, cat);
            }
            await connection.CloseAsync();
            return book;
        }

        

        
    }
}

