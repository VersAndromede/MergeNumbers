public class PowerPresenter
{
    private readonly Power _power;
    private readonly PowerView _powerView;

    public PowerPresenter(Power power, PowerView powerView)
    {
        _power = power;
        _powerView = powerView;
    }

    public void Enable()
    {
        _power.Changed += OnPowerChanged;
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
