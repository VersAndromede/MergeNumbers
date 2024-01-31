namespace PowerSystem
{
    public class PlayerPowerPresenter
    {
        private readonly Power _power;
        private readonly PlayerPowerView _powerView;

        public PlayerPowerPresenter(Power power, PlayerPowerView powerView)
        {
            _power = power;
            _powerView = powerView;
        }

        public void Enable()
        {
            _power.Changed += OnPowerChanged;
            _powerView.UpdateUI(_power.Value);
        }

        public void Disable()
        {
            _power.Changed -= OnPowerChanged;
        }

        private void OnPowerChanged()
        {
            _powerView.UpdateUI(_power.Value);
        }
    }
}