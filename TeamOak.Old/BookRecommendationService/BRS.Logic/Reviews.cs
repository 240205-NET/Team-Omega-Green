using System;
using System.Runtime.CompilerServices;

namespace BRS.Logic
{
    public class Reviews
    {
        // Fields
        public int rId { get; set; }
        public int uId { get; set; }
        public string? userReview { get; set; } = string.Empty;
        public int bId { get; set; }
        // public List<string> allReviews;

        // Constructors
        public Reviews(int rId, int uId, string userReview, int bId)
        {
            this.rId = rId;
            this.uId = uId;
            this.userReview = userReview;
            this.bId = bId;

        }

        public Reviews(int uId, string userReview, int bId)
        {
            //this.rId = rId;
            this.uId = uId;
            this.userReview = userReview;
            this.bId = bId;


        }
        public Reviews()
        { }



        // Methods
    }

}