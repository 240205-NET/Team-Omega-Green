using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using BRS.Logic;
using System.Security.Cryptography;

namespace WishList.Data
{
    public class WishSQLRepository : IWishRepository
    {
        //Fields 
        private readonly string _connectionString;
        private readonly ILogger<WishSQLRepository> _logger;

        //constructors
        public WishSQLRepository(string connectionString, ILogger<WishSQLRepository> logger)
        {
            this._connectionString = connectionString;
            this._logger = logger;
        }

        public async Task<Book> GetBookAsyncById(int bookId)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            string query = @"SELECT * FROM [book].[books] WHERE id=@id;";
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", bookId);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            Book book = new Book();
            while (await reader.ReadAsync())
            {
                int Id = (int)reader["id"];
                string title = reader["title"].ToString() ?? "";
                string author = reader["author"].ToString() ?? "";
                string isbn = reader["isbn"].ToString() ?? "";
                int categoryId = (int)reader["categoryId"];
                Category c = new Category(categoryId, "");
                book = new Book(Id, title, author, isbn, c);
            }
            await connection.CloseAsync();
            return book;
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
        public async Task<IEnumerable<Wishlist>> GetWishListAsync(int userId)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string cmdText = @"SELECT * FROM [book].[wishlist] WHERE userId=@userId;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue(@"userId", userId);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            
            List<Wishlist> wishlist = new List<Wishlist>();
            while (reader.Read())
            {
                int id = (int)reader["id"];
                int bookId = (int)reader["bookId"];
                int uId = (int)reader["userId"];
                Book book = await GetBookAsyncById(bookId);
                Users user = await GetUsersByIdAsync(uId);
                wishlist.Add(new Wishlist(id,user,book));

            }
            await connection.CloseAsync();
            return wishlist;
        }
        public async Task DeleteBookFromWIshLostAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string cmdText = "DELETE FROM [book].[wishlist] WHERE [book].[wishlist].id =@id;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@id", id);

            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();
            _logger.LogInformation("Delete Execution completed, book id#{0}", id);
        }

        public async Task AddTOWishListAsync(Wishlist wish)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string cmdText = @"INSERT INTO [book].[wishlist] (userId, bookId) VALUES (@userId, @bookId);";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@userId",wish.user._id);
            cmd.Parameters.AddWithValue("@bookId", wish.book.id);
            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();
            _logger.LogInformation($"Executed AddTOWishListAsync on {DateTime.Now}");

        }

        //public async Task<Wishlist> GetWishListOfUserByUserId(int userId)
        //{
        //    using SqlConnection connection = new SqlConnection(this._connectionString);
        //    await connection.OpenAsync();

        //    string query = @"SELECT * FROM [book].[wishlist] WHERE userId=@userId;";
        //    using SqlCommand cmd = new SqlCommand(query, connection);
        //    cmd.Parameters.AddWithValue("@userId", userId);

        //    using SqlDataReader reader = await cmd.ExecuteReaderAsync();
        //    Users user;
        //    Wishlist wish = new Wishlist();
        //    while (await reader.ReadAsync())
        //    {
        //        int userDetailsId = (int)reader["id"];
        //        int uId =(int) reader["userId"];
        //        int bookId = (int)reader["bookId"];
        //        user = await GetUsersByIdAsync(uId);
        //        Book book = await GetBookAsyncById(bookId);
        //        wish = new Wishlist(user, book);
        //    }
        //    await connection.CloseAsync();
        //    return wish;
        //}
    }
}
