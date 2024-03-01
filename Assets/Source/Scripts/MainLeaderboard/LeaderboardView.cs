using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MainLeaderboard
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private LeaderboardElement _leaderboardElementPrefab;

        private List<LeaderboardElement> _spawnedElements = new List<LeaderboardElement>();

        public void CreateLeaderboard(List<LeaderboardPlayerData> leaderboardPlayers)
        {
            ClearLeaderboard();

            foreach (LeaderboardPlayerData leaderboardPlayer in leaderboardPlayers)
            {
                LeaderboardElement leaderboardElement = Instantiate(_leaderboardElementPrefab, _container);
                leaderboardElement.Init(leaderboardPlayer.Rank, leaderboardPlayer.Name, leaderboardPlayer.Score);
                _spawnedElements.Add(leaderboardElement);
            }
        }

        public void ClearLeaderboard()
        {
            foreach (LeaderboardElement spawnedElement in _spawnedElements)
                Destroy(spawnedElement.gameObject);

            _spawnedElements = new List<LeaderboardElement>();
        }
    }
}