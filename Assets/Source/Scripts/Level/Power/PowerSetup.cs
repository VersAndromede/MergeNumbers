using UnityEngine;

namespace PowerSystem
{
    public class PowerSetup : MonoBehaviour
    {
        [SerializeField] private PlayerPowerView _powerView;

        private PlayerPowerPresenter _powerPresenter;

        private void OnDestroy()
        {
            _powerPresenter.Disable();
        }

        public void Init(Power power)
        {
            _powerPresenter = new PlayerPowerPresenter(power, _powerView);
            _powerPresenter.Enable();
        }
    }
}