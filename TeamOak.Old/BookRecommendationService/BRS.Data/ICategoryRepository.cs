using System;
using BRS.Logic;

namespace BRS.Data
{
	public interface ICategoryRepository
	{

		public Task<IEnumerable<Category>> GetAllCategoriesAsync();

		public Task AddNewCategoriesAsync(Category cat);

		public Task<Category> GetCategoryByIdAsync(int id);

		public Task<Category> checkUniqueCategoryNameExistsorNotAsync(string name);

		public Task<Category> GetCategoryByNameAsync(string name);

    }
}

