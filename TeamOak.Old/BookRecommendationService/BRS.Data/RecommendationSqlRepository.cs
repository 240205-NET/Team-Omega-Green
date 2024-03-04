using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using BRS.Logic;
using Microsoft.Extensions.Logging;

namespace BRS.Data
{
    public class RecommendationSqlRepository : IRecommendationSqlRepository
    {
        // Field
        private readonly string _connectionString;
        private readonly ILogger<RecommendationSqlRepository> _logger;

        // Constructor
        public RecommendationSqlRepository(string connectionString, ILogger<RecommendationSqlRepository> logger)
        {
            this._connectionString = connectionString;
            this._logger = logger;
        }

        // Method

        //input userID = 1
        public async Task<List<Book>> GetRecommenedBookList(int userId)
        {
            //initializing return list
            List<Book> _BooksList = new List<Book>();
            //some sets to handle data convertion
            //type string : storing the name of category type
            HashSet<string> _categoriesSet = new HashSet<string>();
            HashSet<string> _rawcategoriesSet = new HashSet<string>();

            //get _rawCategoriesSet : input 1
            _rawcategoriesSet = await updateRawCategoriesSet(userId);
            //example data in _rawcategoriesSet : {A & B & C,D,E F}
            //get _categoriesSet
            _categoriesSet = splitAndEliminateSpace(_rawcategoriesSet);
            //example data in _categoriesSet : {A,B,C,D,E,EF}
            //get _BookList
            _BooksList = await updateTopBooksList(_categoriesSet);
            return _BooksList;
        }


        //retrieve top 10 from reading history(what kind of book interested in recently), user's preferences from user(habbit/used to),
        public async Task<HashSet<string>> updateRawCategoriesSet(int userId)
        {
            //tmp set for retrieving purpose
            HashSet<string> _rawCategoriesSet = new HashSet<string>();

            //Start read from user's preference side
            //Mission: Get Category Name in String type and put it in _rawCategoriesSet(Data will not be duplicated)

            //update rawList from user preference side 
            HashSet<int> _cateogoryId = await getCategoryIdFromCategories(userId);

            //getting catergories name by catergoryID
            foreach (int index in _cateogoryId)
            {
                _rawCategoriesSet.Add(await updateRawListByCategoryID(index));
            }

            //Start read from user's history side
            //Get BookID from user reading history ,at least has 1 record if user's history exist
            HashSet<int> _bookId = await getBookIdFromHistroy(userId);

            //Getting Catergory ID by bookId
            HashSet<int> _cateId = new HashSet<int>();
            foreach (int index in _bookId)
            {
                _cateId.Add(await getCategoryIdListByBookId(index));
            }
            //Once we have Category Id
            //We can get the category name again
            foreach (int index in _cateId)
            {
                _rawCategoriesSet.Add(await updateRawListByCategoryID(index));
            }
            //Mission complete : read from both history and preference and store category name in string
            //return the updated set
            return _rawCategoriesSet;
        }
        public async Task<HashSet<int>> getBookIdFromHistroy(int userId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT TOP 10 bookid FROM [book].[history] WHERE userId = @userId;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@userId", userId);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            HashSet<int> tmp = new HashSet<int>();
            while (await reader.ReadAsync())
            {
                int bookid = (int)reader["bookid"];
                tmp.Add(bookid);
            }
            await connection.CloseAsync();
            _logger.LogInformation("Executed getBookIdFromHistroy, returned {0} results.", tmp.Count);
            return tmp;
        }
        //getting categoryId by Bookid ----#############################################
        public async Task<int> getCategoryIdListByBookId(int bookid)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT categoryId FROM [book].[books] WHERE id = @bookid;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@bookid", bookid);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            await reader.ReadAsync();
            int categoryId = (int)reader["categoryId"];
            Category c1 = await GetCategoryByIdAsync(categoryId);

            await connection.CloseAsync();
            _logger.LogInformation("Executed getCategoryIdListByBookId, returned {0} results.", c1.ToString);

            return c1._id;

        }


        //Getting User's preference by userid
        public async Task<HashSet<int>> getCategoryIdFromCategories(int userId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT categoryId FROM [book].[userBookPreference] WHERE userId = @userId;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@userId", userId);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            HashSet<int> _categoryId = new HashSet<int>();
            while (await reader.ReadAsync())
            {
                int catId = (int)reader["categoryId"];
                _categoryId.Add(catId);
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed getCategoryIdFromCategories, returned {0} results.", _categoryId.Count);

            return _categoryId;
        }
        //Getting categories name by categoryID
        public async Task<string> updateRawListByCategoryID(int catId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT name FROM [book].[categories] WHERE id = @catId;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@catId", catId);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            // each category Id are unique
            await reader.ReadAsync();
            string name = reader["name"].ToString() ?? "";

            await connection.CloseAsync();
            _logger.LogInformation("Executed updateRawListByCategoryID, returned {0} results.", name);

            return name;

        }
        public async Task<List<Book>> updateTopBooksList(HashSet<string> _categoriesSet)
        {
            //Ready for returning a Book List
            List<Book> tmpBook = new List<Book>();

            HashSet<int> _finalIdSet = new HashSet<int>();
            //Getting categories ID
            foreach (string s in _categoriesSet)
            {
                _finalIdSet.UnionWith(await GetCategoriesID(s));
            }
            //Getting each categories top 2 books 
            foreach (int index in _finalIdSet)
            {
                tmpBook.AddRange(await GetTopBooks(index));
            }
            return tmpBook;
        }
        //input an catId then return the top views of certain category books
        public async Task<List<Book>> GetTopBooks(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string cmdText = "SELECT TOP 2 * FROM [book].[books] WHERE categoryId = @catId ORDER BY views DESC";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@catId", id);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            List<Book> tmpBook = new List<Book>();

            while (await reader.ReadAsync())
            {
                int idd = (int)reader["id"];
                string str = reader["title"].ToString() ?? "";
                string author = reader["author"].ToString() ?? "";
                string isbn = reader["isbn"].ToString() ?? "";
                int catId = (int)reader["categoryId"];
                Category cat = new Category(catId, "");
                tmpBook.Add(new Book(idd, str, author, isbn, cat));
            }
            _logger.LogInformation("Executed GetTopBooks, returned {0} results.", tmpBook.Count);
            await connection.CloseAsync();

            return tmpBook;
        }
        //example input: "A" -> List<int> of id cotains "A";
        public async Task<HashSet<int>> GetCategoriesID(string s)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            //Example: {A B, A C , A}
            string cmdText = "SELECT id FROM [book].[categories] name Like @string";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@string", s);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            HashSet<int> ids = new HashSet<int>();
            while (await reader.ReadAsync())
            {
                int id = (int)reader["id"];
                ids.Add(id);
            }

            await connection.CloseAsync();
            _logger.LogInformation("Executed GetCategoriesID, returned {0} results.", ids.Count);

            return ids;
        }


        public HashSet<string> splitAndEliminateSpace(HashSet<string> _rawCategoriesSet)
        {
            HashSet<string> tmp = new HashSet<string>();
            string[] x;
            string a;

            //example input A & B & C 
            //example input: A B
            foreach (string i in _rawCategoriesSet)
            {
                if (i.Contains("&"))
                {
                    //a = A&B&C
                    a = i.Replace(" ", "");
                    //x = {A,B,C}
                    x = a.Split('&');
                    foreach (string j in x)
                    {
                        tmp.Add(j);
                    }
                }
                else //A B
                {
                    //{A B}
                    tmp.Add(i);
                }
            }
            return tmp;
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
    }
}