using Scripts.Level.GameOver;
using UnityEngine;

namespace Scripts.Level.PlayerSystem
{
    public class PlayerAnimator : MonoBehaviour
    {
        private const string JumpAnimationTrigger = "Jump";

        [SerializeField] private Animator _animator;
        [SerializeField] private GameOverHandler _gameOverHandler;
        [SerializeField] private PlayerRotation _playerRotation;

        private void OnEnable()
        {
            _gameOverHandler.GameOver += OnGameOver;
        }

        private void OnDisable()
        {
            _gameOverHandler.GameOver -= OnGameOver;
        }

        private void OnGameOver(Winner winner)
        {
            if (winner == Winner.Player)
                _animator.SetTrigger(JumpAnimationTrigger);
        }
    }
}