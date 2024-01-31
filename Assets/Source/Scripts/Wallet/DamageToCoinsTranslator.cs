using BossSystem;
using UnityEngine;
using Upgrades;

namespace WalletSystem
{
    public class DamageToCoinsTranslator : MonoBehaviour
    {
        [SerializeField] private UpgradeWithMultiplicationValuePolicy _incomeUpgrade;

        private Wallet _wallet;
        private BossHealth _bossHealth;

        public void Init(Wallet wallet, BossHealth bossHealth)
        {
            _wallet = wallet;
            _bossHealth = bossHealth;
            _bossHealth.DamageReceived += OnDamageReceived;
        }

        private void OnDestroy()
        {
            _bossHealth.DamageReceived -= OnDamageReceived;
        }

        private void OnDamageReceived(int damageTaken)
        {
            uint bonusValue = (uint)_incomeUpgrade.BonusValue;

            if (bonusValue >= _incomeUpgrade.MinBonusValue)
                _wallet.AddCoins(bonusValue + _incomeUpgrade.MinBonusValue);
            else
                _wallet.AddCoins(_incomeUpgrade.MinBonusValue);
        }
    }
}
