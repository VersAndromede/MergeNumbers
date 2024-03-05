namespace Scripts.MainLeaderboard
{
    public class LeaderboardPlayerData
    {
        public LeaderboardPlayerData(string name, int rank, int score)
        {
            Name = name;
            Rank = rank;
            Score = score;
        }

        public string Name { get; private set; }

        public int Rank { get; private set; }

        public int Score { get; private set; }
    }
}