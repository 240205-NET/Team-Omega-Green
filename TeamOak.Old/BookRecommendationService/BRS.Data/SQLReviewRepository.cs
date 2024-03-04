using System;
using System.Data.SqlClient;
using BRS.Logic;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Cryptography;

namespace BRS.Data
{
    public class SQLReviewRepository : IReviewRepository
    {
        // Fields
        private readonly string _connectionString;
        private readonly ILogger<SQLReviewRepository> _logger;

        // Constructors
        public SQLReviewRepository(string connectionString, ILogger<SQLReviewRepository> logger)
        {
            this._connectionString = connectionString;
            this._logger = logger;
        }

        // Methods
        public async Task AddNewReviewAsync(Reviews review)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            //string cmdText = @"INSERT INTO [book].[reviews] (userId, comment, bookId) VALUES (@uId @review, @bId) WHERE bookId = @bId;";
            string cmdText = @"INSERT INTO [book].[reviews] (userId, comment, bookId) VALUES (@uId, @review, @bId);";

            using SqlCommand cmd = new(cmdText, connection);


            cmd.Parameters.AddWithValue("@uId", review.uId);
            cmd.Parameters.AddWithValue("@review", review.userReview);
            cmd.Parameters.AddWithValue("@bId", review.bId);

            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();


            _logger.LogInformation("Executed AddNewReviewAsync, user id :{0} - user review : {1} - book reviewed : {2} created", review.uId, review.userReview, review.bId);
        }
        public async Task<IEnumerable<Reviews>> GetAllReviewsByBookIdAsync(int bId)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);

            await connection.OpenAsync();
            string cmdText = "SELECT * FROM [book].[reviews] WHERE bookId = @bId;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@bId", bId);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<Reviews> tmpReview = new List<Reviews>();
            Users user = new Users();
            while (await reader.ReadAsync())
            {
                int Id = (int)reader["id"];
                int userId = (int)reader["userId"];
                string userReview = reader["comment"].ToString();
                

                tmpReview.Add(new Reviews(Id, userId, userReview, bId));
            }
            await connection.CloseAsync();
            _logger.LogInformation("Executed GetAllReviewsByTitleAsync, returned {0} results.", tmpReview.Count);
            return tmpReview;
        }

        public async Task<IEnumerable<Reviews>> GetAllReviewsByUserIdAsync(int uId)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);

            await connection.OpenAsync();
            string cmdText = "SELECT * FROM [book].[reviews] WHERE userId = @uId;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@uId", uId);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<Reviews> tmpReview = new List<Reviews>();
            Users user = new Users();
            while (await reader.ReadAsync())
            {
                int Id = (int)reader["id"];
                string userReview = reader["comment"].ToString();
                int bId = (int)reader["bookId"];

                tmpReview.Add(new Reviews(Id, uId, userReview, bId));
            }
            await connection.CloseAsync();
            _logger.LogInformation("Executed GetAllReviewsByUserIdAsync, returned {0} results.", tmpReview.Count);
            return tmpReview;
        }

        public async Task<Reviews> GetReviewByReviewIdAsync(int rId)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);

            await connection.OpenAsync();

            string cmdText = "SELECT * FROM [book].[reviews] WHERE id = @rId;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@rId", rId);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            Reviews tmpReview = new Reviews();

            while (await reader.ReadAsync())
            {
                int uId = (int)reader["userId"];
                string userReview = reader["comment"].ToString();
                int bId = (int)reader["bookId"];
                tmpReview = new Reviews(rId, uId, userReview, bId);
            }
            await connection.CloseAsync();
            return tmpReview;
        }

        public async Task DeleteReviewByReviewIdAsync(int rId)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            string cmdText = "DELETE FROM [book].[reviews] WHERE id = @rId;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@rId", rId);

            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();

            _logger.LogInformation("Executed DeleteReviewByReviewIdAsync, Review with id #{0} deleted", rId);
        }

        public async Task DeleteReviewByBookIdAsync(int bId)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            string cmdText = "DELETE FROM [book].[reviews] WHERE bookId = @bId;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@bId", bId);

            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();

            _logger.LogInformation("Executed DeleteReviewByBookIdAsync, Review with id #{0} deleted", bId);
        }

        public async Task DeleteReviewByUserIdAsync(int uId)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            string cmdText = "DELETE FROM [book].[reviews] WHERE userId = @uId;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@uId", uId);

            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();

            _logger.LogInformation("Executed DeleteReviewByUserIdAsync, Review with id #{0} deleted", uId);
        }

        //shift+alt+F will take care of formatting
    }
}