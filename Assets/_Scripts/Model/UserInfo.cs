using System;

namespace Model
{
    [Serializable]
    public class UserInfo
    {
        public string username;
        public int xp;
        public long id;
        public long ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public int Xp
        {
            get { return xp; }
            set { xp = value; }
        }

        public override string ToString()
        {
            return $"Username: {username}, Xp: {xp}, ID: {id}";
        }

        public UserInfo(string username, int xp, long id)
        {
            this.username = username;
            this.xp = xp;
            this.id = id;

        }
    }
}
