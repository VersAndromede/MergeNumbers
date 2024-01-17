using UnityEngine;

public class LeaderboardUpdater : MonoBehaviour
{
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private GameOverController _gameOverController;

    private Wallet _wallet;

    private void OnDestroy()
    {
        _gameOverController.GameOver -= OnGameOver;
    }

    public void Init(Wallet wallet)
    {
        _wallet = wallet;
        _gameOverController.GameOver += OnGameOver;
    }

    public void UpdateLeaderboard()
    {
        _leaderboard.GetScore(score =>
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
