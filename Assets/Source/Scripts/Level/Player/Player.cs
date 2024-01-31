using HealthSystem;
using MoveCounterSystem;
using PowerSystem;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerSystem
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private MoveCounter _moveCounter;
        [SerializeField] private HealthSetup _healthSetup;
        [SerializeField] private UnityEvent _died;

        private Health _health;
        private Power _power;

        private void OnDestroy()
        {
            _power.Over -= OnDied;
            _moveCounter.Ended -= OnGameMovesEnded;

            if (_health != null)
                _health.Died -= OnDied;
        }

        public void Init(Health health, Power power)
        {
            _health = health;
            _power = power;

            _power.Over += OnDied;
            _moveCounter.Ended += OnGameMovesEnded;
        }

        private void OnDied()
        {
            _died?.Invoke();
        }

        private void OnGameMovesEnded()
        {
            _health.SetMax((uint)_power.Value);
            _healthSetup.Init(_health);
            _health.Died += OnDied;
        }
    }
}