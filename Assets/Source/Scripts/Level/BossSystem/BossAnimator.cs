using Scripts.Level.MoveCounterSystem;
using UnityEngine;

namespace Scripts.Level.BossSystem
{
    public class BossAnimator : MonoBehaviour
    {
        [SerializeField] private Boss _boss;
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _particleSystem;

        private readonly int _wakesUpAnimation = Animator.StringToHash("Boss Wakes Up");
        private readonly int _dieAnimation = Animator.StringToHash("Boss Die");

        private MoveCounter _moveCounter;

        public void Init(MoveCounter moveCounter)
        {
            _moveCounter = moveCounter;

            _moveCounter.Ended += OnGameMovesEnded;
            _boss.BossHealth.Died += OnBossDied;
        }

        private void OnDestroy()
        {
            _moveCounter.Ended -= OnGameMovesEnded;
            _boss.BossHealth.Died -= OnBossDied;
        }

        private void OnGameMovesEnded()
        {
            _animator.Play(_wakesUpAnimation);
        }

        private void OnBossDied()
        {
            _animator.Play(_dieAnimation);
            _particleSystem.Play();
        }
    }
}