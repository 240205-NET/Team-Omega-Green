using System;
using System.Data.SqlClient;
using BRS.Logic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace BRS.Data
{
    public class UserSqlRepository : IUserRepository
	{
        // Fields
        private readonly string _connectionString;
        private readonly ILogger<UserSqlRepository> _logger;

        // Constructor
        public UserSqlRepository(string connectionString, ILogger<UserSqlRepository> logger)
        {
            this._connectionString = connectionString;
            this._logger = logger;
        }


        //Methods
        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);

            await connection.OpenAsync();
            string query = @"SELECT * FROM [book].[user];";
            using SqlCommand cmd1 = new SqlCommand(query, connection);
            using SqlDataReader reader = await cmd1.ExecuteReaderAsync();
            List<Users> users = new List<Users>();
            while (await reader.ReadAsync())
            {
                int Id = (int)reader["id"];
                string email = reader["email"].ToString() ?? "";
                string password = reader["password"].ToString() ?? "";
                string name = reader["name"].ToString() ?? "";

                users.Add(new Users(Id, email, password, name ));
            }
            await connection.CloseAsync();
            _logger.LogInformation("Executed GetAllUsers, returned {0} results.", users.Count);
            return users;
        }

        public async Task<IEnumerable<UserDetails>> GetAllUsersDetailsAsync()
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);

            await connection.OpenAsync();
            string cmdText = @"SELECT * FROM [book].[userDetails];";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<UserDetails> userDetail = new List<UserDetails>();
            while (await reader.ReadAsync())
            {
                int Id = (int)reader["id"];
                Users user = await GetUsersByIdAsync((int)reader["userId"]);
                string address1 = reader["address1"].ToString() ?? "";
                string address2 = reader["address2"].ToString() ?? "";
                string city = reader["city"].ToString() ?? "";
                string state = reader["state"].ToString() ?? "";
                string phoneNo = reader["phoneNo"].ToString() ?? "";
                int zip = (int)reader["zip"];
                userDetail.Add(new UserDetails(Id, address1, address2, city, state, phoneNo, zip, user));
            }
            await connection.CloseAsync();
            _logger.LogInformation("Executed GetAllUsersDetails, returned {0} results.", userDetail.Count);

            return userDetail;

        }

        //this only adds user
        public async Task AddUser(Users user)
        {
            bool checkUser = await CheckUserExistsOrNotByEmail(user._email!);
         
            if(checkUser==false)
            {
                using SqlConnection connection = new SqlConnection(this._connectionString);
                await connection.OpenAsync();
                string cmdText = @"INSERT INTO [book].[user] (email, password, name) VALUES (@email, @password, @name);";
                using SqlCommand cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.AddWithValue("@email", user._email);
                cmd.Parameters.AddWithValue("@password", user._password);
                cmd.Parameters.AddWithValue("@name", user._name);
                await cmd.ExecuteNonQueryAsync();
                await connection.CloseAsync();

                _logger.LogInformation($"Executed AddUser {user._email} ");
            }
            else
            {
                throw new Exception("Email Already Exists!!");
               
            }
        }

        //Adds userDetails and UserTable 
        public async Task AddUserDetails(UserDetails ud)
        {
            
            using SqlConnection connection = new SqlConnection(this._connectionString);
            Users user = new Users(ud._user._email, ud._user._password, ud._user._name);
            Users userData = await GetUsersByEmail(ud._user._email);
            int userId;
            Console.WriteLine("object is null: "+user.Equals(null));
            Console.WriteLine("object is null: " + userData);

            if (userData._id==0)
            {
                await AddUser(user);
                Users u = await GetUsersByEmail(user._email!);
                userId = u._id;
                Console.WriteLine("Inside User Id: " + userId);
            }
            else
            {
                userId = userData._id;
                Console.WriteLine("User Id is: " + userId);
                Console.WriteLine("User email is: " + ud._user._email);
            }

            //int id = userId.Id;
            await connection.OpenAsync();
            string cmdText = @"INSERT INTO [book].[userDetails]
                (address1, address2, city, state, phoneNo, zip, userId) VALUES
                (@address1, @address2, @city, @state, @phoneNo, @zip, @userId);";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@address1",ud._address1);
            cmd.Parameters.AddWithValue("@address2", ud._address2);
            cmd.Parameters.AddWithValue("@city", ud._city);
            cmd.Parameters.AddWithValue("@state", ud._state);
            cmd.Parameters.AddWithValue("@phoneNo", ud._phoneNo);
            cmd.Parameters.AddWithValue("@zip", ud._zip);
            cmd.Parameters.AddWithValue("@userId", userId);
            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();

            _logger.LogInformation("Executed AddUserDetails, userDetails id #{0} created.", ud._id);
        }

        public async Task<UserDetails> GetUserDetailsByIdAsync(int uid)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            string query = @"SELECT * FROM [book].[userDetails] WHERE id=@id;";
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", uid);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            Users user;
            UserDetails userDetails = new UserDetails();
            while (await reader.ReadAsync())
            {
                //Users u = new Users();
                //u._id;
                int userDetailsId = (int)reader["id"];
                string address1 = reader["address1"].ToString() ?? "";
                string address2 = reader["address2"].ToString() ?? "";
                string city = reader["city"].ToString() ?? "";
                string state = reader["state"].ToString() ?? "";
                string phoneNo = reader["phoneNo"].ToString()?? "";
                int zip = (int)reader["zip"];
                int userId = (int) reader["userId"];

                user = await GetUsersByIdAsync(userId);
                userDetails = new UserDetails(userDetailsId, address1, address2, city, state, phoneNo, zip, user);

            }
            await connection.CloseAsync();
            return userDetails;
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

        public async Task<bool> CheckUserExistsOrNotByEmail(string email)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string query = @"SELECT * FROM [book].[user] WHERE email = @eId;";
            using SqlCommand cmd = new SqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@eId", email);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            bool result = false;
            if (reader.HasRows)
            {
                result = true;
            }
            await connection.CloseAsync();
            return result;
        }

        public async Task<Users> GetUsersByEmail(string email)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();

            string query = @"SELECT * FROM [book].[user] WHERE email = @eId;";
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@eId", email);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            Users user = new Users();
            while (await reader.ReadAsync())
            {
                int id = (int)reader["id"];
                string email_address = reader["email"].ToString() ?? "";
                string password = reader["password"].ToString() ?? "";
                string name = reader["name"].ToString() ?? "";


                user = new Users(id, email_address, password, name);
            }
            await connection.CloseAsync();
            return user;
        }

        public async Task UpdateUserAsync(int id, Users user)
        {
            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string queryString = @"UPDATE [book].[user] SET email=@email, password=@password, name=@name WHERE id=@id;";
            using SqlCommand cmd = new SqlCommand(queryString, connection);
            cmd.Parameters.AddWithValue("@email", user._email);
            cmd.Parameters.AddWithValue("@password", user._password);
            cmd.Parameters.AddWithValue("@name", user._name);
            cmd.Parameters.AddWithValue("@id",id);
            await cmd.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        //public async Task<Users> GetUserByUserDetailsId(int userDetailsId)
        //{
            
        //}

        public async Task UpdateUserDetailsAsync(int userId, UserDetails ud)
        {

            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            //find user details as well from table
            Users user = await GetUsersByIdAsync(userId);
            if (user._id == userId) {
                string queryString = @"UPDATE [book].[userDetails]
                SET address1=@address1, address2=@address2, city=@city, state=@state, phoneNo=@phoneNo, zip=@zip
                WHERE userId=@userId;";
                using SqlCommand cmd = new SqlCommand(queryString, connection);
                cmd.Parameters.AddWithValue("@address1", ud._address1);
                cmd.Parameters.AddWithValue("@address2", ud._address2);
                cmd.Parameters.AddWithValue("@city", ud._city);
                cmd.Parameters.AddWithValue("@state", ud._state);
                cmd.Parameters.AddWithValue("@phoneNo", ud._phoneNo);
                cmd.Parameters.AddWithValue("@zip", ud._zip);
                cmd.Parameters.AddWithValue("@userId", userId);
                await cmd.ExecuteNonQueryAsync();
                await connection.CloseAsync();
                _logger.LogInformation($"User Details updated for {user._name}");
            }
            else
            {
                _logger.LogInformation($"User Details Cannot be Updated");
            }
            
        }

        public async Task<bool> LoginUser(string email, string password) {

            using SqlConnection connection = new SqlConnection(this._connectionString);
            await connection.OpenAsync();
            string query = @"SELECT * FROM [book].[user] WHERE email=@email AND password=@password;";

            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            bool result = false;
            if (reader.HasRows)
            {
                result = true;
            }
            await connection.CloseAsync();
            return result;
        }

        //public async Task<IEnumerable<Users>> GetAllUsersAsync()
        //{
        //    using SqlConnection connection = new SqlConnection(this._connectionString);

        //    await connection.OpenAsync();
        //    string cmdText = @"SELECT * FROM [book].[user];";
        //    using SqlCommand cmd = new SqlCommand(cmdText, connection);
        //    using SqlDataReader reader = await cmd.ExecuteReaderAsync();
        //    List<Users> users = new List<Users>();
        //    while(await reader.ReadAsync())
        //    {
        //        int Id = (int)reader["id"];
        //        string email = reader["email"].ToString() ?? "";
        //        string password = reader["password"].ToString() ?? "";
        //        users.Add(new Users(email, password));
        //    }
        //    await connection.CloseAsync();
        //    _logger.LogInformation("Executed GetAllUsers, returned {0} results.", users.Count);
        //    return users;
        //}


        //public async Task<IEnumerable<UserDetails>> GetAllUsersDetailsAsync()
        //{
        //    using SqlConnection connection = new SqlConnection(this._connectionString);

        //    await connection.OpenAsync();
        //    string cmdText = @"SELECT * FROM [book].[userDetails];";
        //    using SqlCommand cmd = new SqlCommand(cmdText, connection);
        //    using SqlDataReader reader = await cmd.ExecuteReaderAsync();
        //    List<UserDetails> userDetail = new List<UserDetails>();
        //    while (await reader.ReadAsync())
        //    {
        //        int Id = (int) reader["id"];
        //        Users user = await GetUsersByIdAsync((int)reader["userId"]);
        //        string name = reader["name"].ToString() ?? "";
        //        string address1 = reader["address1"].ToString() ?? "";
        //        string address2 = reader["address2"].ToString() ?? "";
        //        string city = reader["city"].ToString() ?? "";
        //        string state = reader["state"].ToString() ?? "";
        //        string phoneNo = reader["phoneNo"].ToString() ?? "";
        //        int zip = (int) reader["zip"];
        //        userDetail.Add(new UserDetails(Id, name, address1, address2, city, state, phoneNo, zip, user));
        //    }
        //    await connection.CloseAsync();
        //    _logger.LogInformation("Executed GetAllUsersDetails, returned {0} results.", userDetail.Count);

        //    return userDetail;

        //}
    }
}

