using System.Collections.Generic;
using UnityEngine;
using Scripts.WalletSystem;

namespace Scripts.UpgradeSystem
{
    public class UpgradesInitializion : MonoBehaviour
    {
        [SerializeField] private UpgradeWithMultiplicationValuePolicy _damageUpgrade;
        [SerializeField] private UpgradeWithMultiplicationValuePolicy _incomeUpgrade;
        [SerializeField] private UpgradeWithAddedValuePolicy _movesUpgrade;
        [SerializeField] private List<UpgradeButton> _upgradeButtons;

        public IReadOnlyList<Upgrade> Upgrades { get; private set; }

        public void Init(UpgradeData[] upgradesData, Wallet wallet)
        {
            List<Upgrade> upgrades = new List<Upgrade>
            {
                _damageUpgrade,
                _incomeUpgrade,
                _movesUpgrade
            };

            Upgrades = upgrades;

            if (upgradesData[0] == null)
                for (int i = 0; i < upgradesData.Length; i++)
                    upgradesData[i] = new UpgradeData();

            for (int i = 0; i < upgrades.Count; i++)
                upgrades[i].Init(upgradesData[i].Level, upgradesData[i].Price, upgradesData[i].BonusValue);

            for (int i = 0; i < _upgradeButtons.Count; i++)
                _upgradeButtons[i].Init(wallet);
        }
    }
}