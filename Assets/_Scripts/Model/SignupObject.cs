using System;

namespace Model
{
    [Serializable]
    public class SignupObject
    {
        public string username;
        public string password;
        public string email;
        public string dob;

        public string Username
        {
            get { return username; }
            set { username = value; } // You might add validation logic here
        }

        public string Password
        {
            get { return password; }
            set { password = value; } 
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Dob
        {
            get { return dob; }
            set { dob = value; } // You might add validation logic here
        }

        public SignupObject(string username, string password, string email, string dob)
        {
            Username = username;
            Password = password;
            Email = email;
            Dob = dob;
        }
        
        
    }
}