using System;

namespace Model
{
    [Serializable]
    public class UsernameObject
    {
        public string username;

        public UsernameObject(string username)
        {
            this.username = username;
        }

        public string Username
        {
            get => username;
            set => username = value;
        }

        public override string ToString()
        {
            return $"Username: {username}";
        }
    }
}