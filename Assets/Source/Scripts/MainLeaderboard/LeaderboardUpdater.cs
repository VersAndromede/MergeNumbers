using Scripts.Level.GameOver;
using Scripts.WalletSystem;
using UnityEngine;

namespace Scripts.MainLeaderboard
{
    public class LeaderboardUpdater : MonoBehaviour
    {
        [SerializeField] private Leaderboard _leaderboard;
        [SerializeField] private GameOverHandler _gameOverHandler;

        private Wallet _wallet;

        private void OnDestroy()
        {
            _gameOverHandler.GameOver -= OnGameOver;
        }

        public void Init(Wallet wallet)
        {
            _wallet = wallet;
            _gameOverHandler.GameOver += OnGameOver;
        }

        public void UpdateLeaderboard()
        {
            _leaderboard.FetchScore(score =>
            {
                if (_wallet.Coins > score)
                    _leaderboard.SetPlayer(_wallet.Coins);
            });
        }

        private void OnGameOver(Winner winner)
        {
            UpdateLeaderboard();
        }
    }
}