//using System;
//using System.Data.SqlClient;
//using BRS.Logic;
//using System.Text;
//using Microsoft.Extensions.Logging;

//namespace BRS.Data
//{
//    public class SQLSearchRepository : IRepository
//    {
//        // Fields
//        private readonly string _connectionString;
//        private readonly ILogger<SQLSearchRepository> _logger;

//        // Constructors
//        public SQLSearchRepository(string connectionString, ILogger<SQLSearchRepository> logger)
//        {
//            this._connectionString = connectionString;
//            this._logger = logger;
//        }

//        // Methods        
//        //Hemanta Code Starts
//        //Search Book by BookId
//        public async Task<Book> SearchBookByBookId(int id)
//        {
//            using SqlConnection connection = new SqlConnection(this._connectionString);
//            await connection.OpenAsync();
//            string cmdText = "SELECT * FROM [book].[books] WHERE title=@id;";

//            using SqlCommand cmd = new SqlCommand(cmdText, connection);
//            cmd.Parameters.AddWithValue(@"id", id);

//            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
//            //for List of books by title
//            // List<Book> books = new List<Book>();

//            //gives only one book 
//            Book tempBook = new Book();
//            while (await reader.ReadAsync())
//            {
//                int id = (int)reader["id"];
//                string title = reader["title"].ToString() ?? "";
//                string author = reader["author"].ToString() ?? "";
//                int isbn = (int)reader["isbn"];
//                int categoryId = (int)reader["categoryId"];

//                //adding books to the list by title
//                // books.Add(new Book(id, title, author, isbn, categoryId));

//                //for only one book
//                tempBook = new Book(id, title, author, isbn, categoryId);

//                //Add logic here for WishList
//            }
//            await connection.CloseAsync();
//        }


//        //Search Book By Title returns Book object
//        public async Task<Book> SearchBookByTitle(string title)
//        {
//            using SqlConnection connection = new SqlConnection(this._connectionString);
//            await connection.OpenAsync();
//            string cmdText = "SELECT * FROM [book].[books] WHERE title=@title;";

//            using SqlCommand cmd = new SqlCommand(cmdText, connection);
//            cmd.Parameters.AddWithValue(@"titile", title);

//            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
//            //for List of books by title
//            List<Book> books = new List<Book>();

//            //gives only one book 
//            //Book tempBook = new Book();
//            while (await reader.ReadAsync())
//            {
//                int id = (int)reader["id"];
//                string title = reader["title"].ToString() ?? "";
//                string author = reader["author"].ToString() ?? "";
//                int isbn = (int)reader["isbn"];
//                int categoryId = (int)reader["categoryId"];

//                //adding books to the list by title
//                books.Add(new Book(id, title, author, isbn, categoryId));

//                //for only one book
//                //tempBook= new Book (id, title, author, isbn, categoryId);
//            }
//            await connection.CloseAsync();
//        }

//        //Search Book by author returns List of Books by author
//        public async Task<Book> SearchBookByAuthor(string author)
//        {
//            using SqlConnection connection = new SqlConnection(this._connectionString);
//            await connection.OpenAsync();
//            string cmdText = "SELECT * FROM [book].[books] WHERE author=@author;";

//            using SqlCommand cmd = new SqlCommand(cmdText, connection);
//            cmd.Parameters.AddWithValue(@"author", author);

//            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

//            List<Book> books = new List<Book>();
//            //Book tempBook = new Book();
//            while (await reader.ReadAsync())
//            {
//                int id = (int)reader["id"];
//                string title = reader["title"].ToString() ?? "";
//                string author = reader["author"].ToString() ?? "";
//                int isbn = (int)reader["isbn"];
//                int categoryId = (int)reader["categoryId"];

//                books.Add(new Book(id, title, author, isbn, categoryId));
//                // tempBook= new Book (id, title, author, isbn, categoryId);
//            }
//            await connection.CloseAsync();
//        }
//        //Hemanta code ends..

//    }
//}