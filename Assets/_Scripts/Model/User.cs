using System;

namespace Model
{
    [Serializable]
    public class User
    {
        public int id;
        public string username;
        public int xp;
        public string registeredDate;
        public int ID
        {
            get => id;
            set => id = value;
        }
        public string Username
        {
            get => username;
            set => username = value;
        }
        public int Xp
        {
            get => xp;
            set => xp = value;
        }
        public string RegisteredDate
        {
            get => registeredDate;
            set => registeredDate = value;
        }
        public override string ToString()
        {
            return
                $"id: {id}, username: {username}, xp: {xp}, registeredDate: {registeredDate}";
        }

        public User(string username, string password, string email, string dob, string registeredDate, int xp, bool authenticated, int id)
        {
            this.id = id;
            this.username = username;
            this.registeredDate = registeredDate;
            this.id = id;
        }
    }
}