using System;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using BRS.Logic;
using Microsoft.Extensions.Logging;
using static System.Reflection.Metadata.BlobBuilder;

namespace BRS.Data
{
	public class BookSqlRepository : IBookRepository
    {

        private readonly string _connectionString;
        private readonly ILogger<IBookRepository> _logger;

        public BookSqlRepository(string connectionString, ILogger<IBookRepository> logger) 
		{
            this._connectionString = connectionString;
            this._logger = logger;
        }

        public async Task AddBook(Book book)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string cmdText = @"INSERT INTO [book].[books] (title, author, isbn, categoryId) VALUES (@title, @author, @isbn, @categoryId)";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@title", book.title);
            cmd.Parameters.AddWithValue("@author", book.author);
            cmd.Parameters.AddWithValue("@isbn", book.isbn);
            cmd.Parameters.AddWithValue("@categoryId", book.categoryId._id);
            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();
            _logger.LogInformation("Executed Add Book");
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);

            await connection.OpenAsync();
            string query = @"SELECT * FROM [book].[books];";
            using SqlCommand cmd1 = new SqlCommand(query, connection);
            using SqlDataReader reader = await cmd1.ExecuteReaderAsync();
            List<Book> books = new List<Book>();
            while (await reader.ReadAsync())
            {
                int Id = (int)reader["id"];
                string title = reader["title"].ToString() ?? "";
                string author = reader["author"].ToString() ?? "";
                string isbn = reader["isbn"].ToString() ?? "";
                int categoryId = (int) reader["categoryId"];
                Category c = new Category(categoryId, "");
                books.Add(new Book(Id, title, author, isbn, c));
            }
            await connection.CloseAsync();
            _logger.LogInformation($"Executed GetAllBooksAsync, {books.Count}");
            return books;
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

        public async Task<Book> GetBookByIsbnAsync(string isbn)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            string query = @"SELECT * FROM [book].[books] WHERE isbn=@isbn;";
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@isbn", isbn);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            Book book = new Book();
            while (await reader.ReadAsync())
            {
                int Id = (int)reader["id"];
                string title = reader["title"].ToString() ?? "";
                string author = reader["author"].ToString() ?? "";
                string isbnBook = reader["isbn"].ToString() ?? "";
                int categoryId = (int)reader["categoryId"];
                Category c = new Category(categoryId, "");
                book = new Book(Id, title, author, isbnBook, c);
            }
            await connection.CloseAsync();
            return book;
        }

        public async Task<Book> GetBookByTitleAsync(string title)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            string query = @"SELECT * FROM [book].[books] WHERE title=@title;";
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@title", title);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            Book book = new Book();
            while (await reader.ReadAsync())
            {
                int Id = (int)reader["id"];
                string bookTitle = reader["title"].ToString() ?? "";
                string author = reader["author"].ToString() ?? "";
                string isbn = reader["isbn"].ToString() ?? "";
                int categoryId = (int)reader["categoryId"];
                Category c = new Category(categoryId, "");
                book = new Book(Id, bookTitle, author, isbn, c);
            }
            await connection.CloseAsync();
            return book;
        }
    }
}

