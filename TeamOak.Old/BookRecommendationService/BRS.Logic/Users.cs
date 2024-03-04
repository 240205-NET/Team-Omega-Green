using System;

namespace BRS.Logic
{
    public class Users
    {
        //Fields
        public int _id { get; set; }
        public string? _email { get; set; } 
        public string? _password { get; set; }
        public string? _name { get; set; }


        //constructor 
        public Users() { }

        public Users(int id, string email, string password, string name)
        {
            this._id = id;
            this._email = email;
            this._password = password;
            this._name = name;
        }
        public Users(int id, string email)
        {
            this._id = id;
            this._email = email;

        }
        public Users(int id, string email, string name)
        {
            this._id = id;
            this._email = email;
            this._name = name;

        }
        public Users(string email, string password, string name)
        {
            this._email = email;
            this._password = password;
            this._name = name;
        }
         public Users(string email, string password)
        {
            this._email = email;
            this._password = password;
        }
        //methods
        public override string ToString()
        {
            return @$"\nUser ID: {this._id}\n Email: {this._email}\n Name: {this._name}\n";
        }
    }               
}

