using System;
using BRS.Logic;

namespace BRS.Data
{
	public interface IUserBookPreferenceRepository
	{
		public Task<IEnumerable<UserBookPreference>> GetAllUsersPreferencesAsync();

		public Task AddUserBookPreferenceAsync(UserBookPreference preference);

		public Task<UserBookPreference> GetUserBookPreferenceByUserIdAsync(int userId);
	}
}

