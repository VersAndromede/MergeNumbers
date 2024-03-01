using Scripts.Level.BossSystem;
using Scripts.Level.HealthSystems;
using Scripts.Level.PowerSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Level.GameOver
{
    public class GameOverHandler : MonoBehaviour
    {
        [SerializeField] private float _delayToResult;
        [SerializeField] private UnityEvent _gameOver;
        [SerializeField] private UnityEvent _winning;
        [SerializeField] private UnityEvent _defeat;

        private WaitForSeconds _waitTime;
        private Boss _boss;
        private Power _power;
        private Health _playerHealth;

        public event Action<Winner> GameOver;

        private void OnDestroy()
        {
            _playerHealth.Died -= OnPlayerDied;
            _power.Over -= OnPlayerDied;
            _boss.BossHealth.Died -= OnBossDied;
        }

        public void Init(Boss boss, Power power, Health playerHealth)
        {
            _boss = boss;
            _power = power;
            _playerHealth = playerHealth;
            _waitTime = new WaitForSeconds(_delayToResult);

            _playerHealth.Died += OnPlayerDied;
            _power.Over += OnPlayerDied;
            _boss.BossHealth.Died += OnBossDied;
        }

        private IEnumerator AssignVictory(Winner winner)
        {
            yield return _waitTime;
            GameOver?.Invoke(winner);
            _gameOver?.Invoke();
            yield return new WaitForFixedUpdate();

            if (winner == Winner.Player)
                _winning?.Invoke();
            else
                _defeat?.Invoke();
        }

        private void OnBossDied()
        {
            StartCoroutine(AssignVictory(Winner.Player));
        }

        private void OnPlayerDied()
        {
            _boss.BossHealth.MakeInvulnerable();
            StartCoroutine(AssignVictory(Winner.Boss));
        }
    }
}