using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameMoves _gameMoves;
    [SerializeField] private GameOverController _gameOverController;
    [SerializeField] private PlayerRotation _playerRotation;

    private void OnEnable()
    {
        _gameMoves.Ended += OnGameMovesEnded;
        _gameOverController.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _gameMoves.Ended -= OnGameMovesEnded;
        _gameOverController.GameOver -= OnGameOver;
    }

    private void OnGameMovesEnded()
    {
        _playerRotation.StartRotationJob(Vector3.forward);
    }

    private void OnGameOver(Winner winner)
    {
        if (winner == Winner.Player)
            _animator.SetTrigger("Jump");
    }
}
