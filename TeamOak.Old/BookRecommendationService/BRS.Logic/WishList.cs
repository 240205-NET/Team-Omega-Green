namespace BRS.Logic
{
    public class Wishlist
    {
        public Users user {  get; set; }
        public int id { get; set; }
        //public Book book { get; set; }
        public Book book { get; set; }

        public Wishlist(int id, Users user, Book book)
        {
            this.id = id;
            this.book = book;
            this.user = user;
        }

       /* public Wishlist(int userID, int id, Book book)
        {
            this.userID = userID;
            this.id = id;
            this.book = book;
            
        }*/

        public Wishlist(Users user, Book book){
            this.book = book;
            this.user = user;
        }
        public Wishlist(){
            this.book = book;
            this.user = user;
        }

    }
}
