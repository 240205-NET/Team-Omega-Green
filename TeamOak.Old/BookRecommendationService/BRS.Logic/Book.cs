using System;

namespace BRS.Logic
{
    public class Book
    {
        //Fields 
        public int id {  get; set; }
        public string? title { get; set; }
        public string? author { get; set; }
        public string? isbn { get; set; }
        public Category categoryId { get; set; }
		public int views{get;set;}
		Random rmd = new Random();
        //Constructors

        public Book( ) { }

        public Book(int id, string title, string author, string isbn, Category categoryId)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.isbn = isbn;
            this.categoryId = categoryId;
			this.views = rmd.Next(20,100);
        }

        public Book(string title, string author, string isbn, Category categoryId)
        {
            this.title = title;
            this.author = author;
            this.isbn = isbn;
            this.categoryId = categoryId;
			this.views = rmd.Next(20,100);

        }

        public override string ToString()
        {
            return ($"\\n Id: {this.id}\n title: {this.title}\n author: {this.author}\n isbn: {this.isbn}\n categoryId: {this.categoryId} ");
        }
    }
}
