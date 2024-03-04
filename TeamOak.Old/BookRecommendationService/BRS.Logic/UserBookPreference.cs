using System;
namespace BRS.Logic
{
	public class UserBookPreference
	{
		//Fields
		public int _id { get; set; }
		public Users _user { get; set; }
		public Category _category { get; set; }

		//constructor
		public UserBookPreference()
		{
		}

		public UserBookPreference(Users user, Category category)
		{
			this._user = user;
			this._category = category;
		
		}

        public UserBookPreference(int id, Users user, Category category)
        {
			this._id = id;
            this._user = user;
			this._category = category;
        }


        //Methods
    }
}

