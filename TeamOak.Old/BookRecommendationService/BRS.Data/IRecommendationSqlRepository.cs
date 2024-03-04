using System;
using BRS.Logic;

namespace BRS.Data
{
    public interface IRecommendationSqlRepository
    {
        //only function that user should use
        public Task <List<Book>> GetRecommenedBookList(int userId);
        public Task <HashSet<string>> updateRawCategoriesSet(int userId);
        public Task <HashSet<int>> getBookIdFromHistroy(int userId);
        public Task <int> getCategoryIdListByBookId(int bookid);
        public Task <HashSet<int>> getCategoryIdFromCategories(int userId);
        public Task <string> updateRawListByCategoryID(int catId);
        public Task <List<Book>> updateTopBooksList(HashSet<string> _categoriesSet);
        public Task <List<Book>> GetTopBooks(int id);
        public Task <HashSet<int>> GetCategoriesID(string s);
        public HashSet <string> splitAndEliminateSpace(HashSet<string> _rawCategoriesSet);
        public Task <Category> GetCategoryByIdAsync(int id);
    }


}

