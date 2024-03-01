using Scripts.Level.HealthSystems;
using Scripts.Level.PlayerSystem;
using UnityEngine;

namespace Scripts.Level.PowerSystem
{
    public class PowerInitialization : MonoBehaviour
    {
        [SerializeField] private PowerSetup _powerSetup;
        [SerializeField] private Player _player;
        [SerializeField] private PlayerMergeAbility _playerMergeAbility;

        public Power Power { get; private set; }

        public void Init(Health playerHealth)
        {
            Power = new Power(0);
            _powerSetup.Init(Power);
            _player.Init(playerHealth, Power);
            _playerMergeAbility.Init(Power);
        }
    }
}