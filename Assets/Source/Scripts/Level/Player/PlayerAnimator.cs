using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private MoveCounter _moveCounter;
    [SerializeField] private GameOverHandler _gameOverHandler;
    [SerializeField] private PlayerRotation _playerRotation;

    private const string JumpAnimationTrigger = "Jump";

    private void OnEnable()
    {
        _moveCounter.Ended += OnGameMovesEnded;
        _gameOverHandler.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _moveCounter.Ended -= OnGameMovesEnded;
        _gameOverHandler.GameOver -= OnGameOver;
    }

    private void OnGameMovesEnded()
    {
        _playerRotation.StartRotationJob(Vector3.forward);
    }

    private void OnGameOver(Winner winner)
    {
        if (winner == Winner.Player)
            _animator.SetTrigger(JumpAnimationTrigger);
    }
}
