using System;
using BRS.Logic;

namespace BRS.Data
{
    public interface IReviewRepository
    {
        //public Task<Reviews> GetAllReviewsByBookTitleAsync(string bookTitle);

        //Nabin Repository starts


        //Nabin IRepository Ends

        //Jessirae's code starts here
        public Task AddNewReviewAsync(Reviews review);
        public Task<IEnumerable<Reviews>> GetAllReviewsByBookIdAsync(int bId);
        public Task<IEnumerable<Reviews>> GetAllReviewsByUserIdAsync(int uId);
        public Task<Reviews> GetReviewByReviewIdAsync(int rId);        
        public Task DeleteReviewByReviewIdAsync(int rId);
        public Task DeleteReviewByBookIdAsync(int bId);
        public Task DeleteReviewByUserIdAsync(int uId);

        //Jessirae's code ends here

        //Hemanta's IRepository Starts here
        //public Task<Book> SearchBookByBookId(int id);

        //Hemanta's IRepository ends here
    }
}