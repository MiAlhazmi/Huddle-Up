namespace Model
{
    public class UserSaveData
    {
        public UserInfo userInfo;
        public bool isLoggedIn;

        public UserInfo UserInfo
        {
            get { return userInfo; }
            set { userInfo = value; }
        }

        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set { isLoggedIn = value; }
        }

        public UserSaveData(UserInfo userInfo, bool isLoggedIn)
        {
            this.userInfo = userInfo;
            this.isLoggedIn = isLoggedIn;
        }

        public override string ToString()
        {
            return $"UserInfo: {userInfo}, IsLoggedIn: {isLoggedIn}";
        }
    }
}