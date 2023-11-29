using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardView _leaderboardView;

    public const string AnonymousName = "AnonymousName";

    private const string LeaderboardName = "LeaderboardName";

    private readonly List<LeaderboardPlayerData> _leaderboardPlayers = new List<LeaderboardPlayerData>();

    public void SetPlayer(int score)
    {
        if (Application.isEditor || PlayerAccount.IsAuthorized == false)
            return;

        Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardName, _ =>
        {
            Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, score);
        });
    }

    public void Fill()
    {
        _leaderboardPlayers.Clear();

        if (PlayerAccount.IsAuthorized == false)
            return;

        Agava.YandexGames.Leaderboard.GetEntries(LeaderboardName, result =>
        {
            for (int i = 0; i < result.entries.Length; i++)
            {
                string name = result.entries[i].player.publicName;
                int rank = result.entries[i].rank;
                int coins = result.entries[i].score;

                if (string.IsNullOrEmpty(name))
                    name = AnonymousName;

                _leaderboardPlayers.Add(new LeaderboardPlayerData(name, rank, coins));
            }

            _leaderboardView.CreateLeaderboard(_leaderboardPlayers);
        });
    }
}