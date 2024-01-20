using UnityEngine;

public class PowerSetup : MonoBehaviour
{
    [SerializeField] private PowerView _powerView;

    private PowerPresenter _powerPresenter;

    private void OnDestroy()
    {
        _powerPresenter.Disable();
    }

    public void Init(Power power)
    {
        _powerPresenter = new PowerPresenter(power, _powerView);
        _powerPresenter.Enable();
    }
}
