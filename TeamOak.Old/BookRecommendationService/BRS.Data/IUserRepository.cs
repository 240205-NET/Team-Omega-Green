using System;
using BRS.Logic;

namespace BRS.Data
{
    public interface IUserRepository
	{
        //Nabin Repository starts
        public Task<IEnumerable<Users>> GetAllUsersAsync();

        public Task<IEnumerable<UserDetails>> GetAllUsersDetailsAsync();

        public Task AddUser(Users user);

        public Task AddUserDetails(UserDetails ud);

        public Task UpdateUserAsync(int id, Users user);

        public Task UpdateUserDetailsAsync(int userDetailsId, UserDetails ud);

        public Task<Users> GetUsersByIdAsync(int id);

        public Task<UserDetails> GetUserDetailsByIdAsync(int uid);

        public Task<bool> CheckUserExistsOrNotByEmail(string email);

        public Task<Users> GetUsersByEmail(string email);

        public Task<bool> LoginUser(string username, string password);

        


    }
}

