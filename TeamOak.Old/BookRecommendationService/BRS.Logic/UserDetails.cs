using System;
namespace BRS.Logic
{
	public class UserDetails
	{
       

        //Fields
        public int _id { get; set; }
        public string? _address1 { get; set; }
		public string? _address2 { get; set; }
		public string? _city { get; set; }
		public string? _state { get; set; }
		public string? _phoneNo { get; set; }
		public int _zip { get; set; }
		public Users? _user { get; set; }
        //constructor
        public UserDetails() { }

		public UserDetails(int id, string address1, string  address2, string city, string state, string phoneNo, int zip, Users user )
		{
			this._id = id;
			this._address1 = address1;
			this._address2 = address2;
			this._city = city;
			this._state = state;
			this._phoneNo = phoneNo;
			this._zip = zip;
			this._user = user;

		}
        public UserDetails(string address1, string address2, string city, string state, string phoneNo, int zip, Users user)
        {
            this._address1 = address1;
            this._address2 = address2;
            this._city = city;
            this._state = state;
            this._phoneNo = phoneNo;
            this._zip = zip;
            this._user = user;

        }

        //methods
        public override string ToString()
        {
			return @$"\n{this._id}\n Address1: {this._address1}\n Address2: {this._address2}\n City: {this._city}\n State: {this._state}\n
			PhoneNo: {this._phoneNo}\n Zip: {this._zip}\n User: {this._user}\n";
		}
	}
}

