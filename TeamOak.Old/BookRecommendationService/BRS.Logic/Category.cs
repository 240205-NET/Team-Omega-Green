using System;
namespace BRS.Logic
{
	public class Category
	{
		//fields
		public int _id { get; set; }
		public string _name { get; set; } = string.Empty;

		public Category()
		{

		}

		public Category(string name)
		{
			this._name = name;
		}

		public Category(int id, string name)
        {
			this._id = id;
            this._name = name;
        }
    }
}

