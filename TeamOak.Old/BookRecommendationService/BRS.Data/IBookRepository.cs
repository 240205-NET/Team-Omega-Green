using System;
using BRS.Logic;

namespace BRS.Data
{
	public interface IBookRepository
	{

		public Task<IEnumerable<Book>> GetAllBooksAsync();

		public Task AddBook(Book book);

		public Task<Book> GetBookAsyncById(int bookId);

        public Task<Book> GetBookByTitleAsync(string title);

		public Task<Book> GetBookByIsbnAsync(string isbn);
    }
}

