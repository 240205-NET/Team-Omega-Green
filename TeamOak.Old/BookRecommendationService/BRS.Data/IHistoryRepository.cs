using System;
using BRS.Logic;

namespace BRS.Data
{
	public interface IHistoryRepository
	{
        public Task<IEnumerable<History>> GetAllHistoryOfUserAsync(int userId);

        public Task AddHistoryAsync(History history, int id);

        //public Task UpdateHistoryAsync(int id, Users user);

        //public Task<History> GetHistoryByIdAsync(int id);
    }

}

