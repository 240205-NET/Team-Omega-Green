using System;
using System.ComponentModel;

namespace BRS.Logic
{
	public class History
	{
		public int _id { get; }

		public Book _book { get; set; }

        public Users _user { get; set; }

		public string? _description { get; set; } = string.Empty;

		public DateTime _createdOn { get; set; }

        public History()
		{
		}
        public History(Book book, Users user, string description, DateTime createdOn)
        {
            this._book = book;
            this._user = user;
            this._description = description;
			this._createdOn = createdOn;
        }

        public History(int id, Book book, Users user, string description, DateTime createdOn)
		{
			this._id = id;
			this._book = book;
			this._user = user;
			this._description = description;
            this._createdOn = createdOn;

        }


    }
}

