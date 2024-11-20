using System;

namespace Model
{
    [Serializable]
    public class GameStatRequest
    {
        public GameStatObject gameStat;
        public string usernameRequest;

        public GameStatRequest(GameStatObject gameStatObject, string usernameRequest)
        {
            this.gameStat = gameStatObject;
            this.usernameRequest = usernameRequest;
        }

        public GameStatObject GameStatObject
        {
            get => gameStat;
            set => gameStat = value;
        }

        public string UsernameRequest
        {
            get => usernameRequest;
            set => usernameRequest = value;
        }

        public override string ToString()
        {
            return $"GameStatObject: {gameStat}, UsernameRequest: {usernameRequest}";
        }
    }
}