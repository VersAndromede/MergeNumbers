using UnityEngine;

namespace HealthSystem
{
    public class HealthSetup : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;

        private HealthPresenter _healthPresenter;

        private void OnDestroy()
        {
            _healthPresenter?.Disable();
        }

        public void Init(Health health)
        {
            _healthPresenter = new HealthPresenter(health, _healthBar);
            _healthPresenter.Enable();
        }
    }
}