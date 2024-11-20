using System;

namespace Model
{
    [Serializable]
    public class LoginObject
    {
        public string username;
        public string password;

        public string Username
        {
            get { return username; } 
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public LoginObject(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public override string ToString()
        {
            return $"Username: {Username}, Password: {Password}";
        }
    }
}