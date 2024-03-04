using System;
using BRS.Logic;

namespace BRS.Data
{
    public interface ISearchRepository
    {
        public Task<Book> SearchBookByBookId(int id);

        public Task<Book> SearchBookByTitle(string title);
        public Task<IEnumerable<Book>> SearchBookByAuthor(string author);
    }
}