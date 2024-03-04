using BRS.Logic;

namespace WishList.Data
{
    public interface IWishRepository
    {
        public Task<IEnumerable<Wishlist>> GetWishListAsync(int userId);
        public Task AddTOWishListAsync(Wishlist wish);
        public Task DeleteBookFromWIshLostAsync(int bookId);
        //public Task<Wishlist> GetWishListOfUserByUserId(int userId);
    }
}
