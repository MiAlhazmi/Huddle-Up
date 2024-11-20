using System;

namespace Model
{
    [Serializable]
    public class TagSwapResponse
    {
        public int gamesPlayed;
        public int wins;
        public int loses;

        public TagSwapResponse(int gamesPlayed, int wins, int loses)
        {
            this.gamesPlayed = gamesPlayed;
            this.wins = wins;
            this.loses = loses;
        }

        public int GamesPlayed
        {
            get => gamesPlayed;
            set => gamesPlayed = value;
        }

        public int Wins
        {
            get => wins;
            set => wins = value;
        }

        public int Loses
        {
            get => loses;
            set => loses = value;
        }

        public override string ToString()
        {
            return $"GamesPlayed: {gamesPlayed}, Wins: {wins}, Loses: {loses}";
        }
    }
}