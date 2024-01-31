namespace HealthSystem
{
    public class HealthPresenter
    {
        private readonly Health _health;
        private readonly HealthBar _healthBar;

        public HealthPresenter(Health health, HealthBar healthBar)
        {
            _health = health;
            _healthBar = healthBar;
            _healthBar.UpdateUIWithInstantTransition(_health.Value, _health.MaxValue);
        }

        public void Enable()
        {
            _health.Changed += OnChanged;
        }

        public void Disable()
        {
            _health.Changed -= OnChanged;
        }

        private void OnChanged()
        {
            _healthBar.UpdateUIWithSmoothTransition(_health.Value, _health.MaxValue);
        }
    }
}