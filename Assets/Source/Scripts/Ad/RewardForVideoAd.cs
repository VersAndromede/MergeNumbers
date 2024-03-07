using System;
using Scripts.UpgradeSystem;
using UnityEngine;

namespace Scripts.Ad
{
    public class RewardForVideoAd
    {
        private const uint MinRewardCount = 150;

        private readonly UpgradeWithMultiplicationValuePolicy _incomeUpgrade;
        private readonly float _defaultRewardCount;

        public RewardForVideoAd(UpgradeWithMultiplicationValuePolicy incomeUpgrade)
        {
            _incomeUpgrade = incomeUpgrade;
            OnIncomeUpgradeLevelChanged();
        }

        public event Action Changed;

        public uint Count { get; private set; }

        public void Enable()
        {
            _incomeUpgrade.LevelChanged += OnIncomeUpgradeLevelChanged;
        }

        public void Disable()
        {
            _incomeUpgrade.LevelChanged -= OnIncomeUpgradeLevelChanged;
        }

        private void OnIncomeUpgradeLevelChanged()
        {
            uint rewardCount = (uint)Mathf.CeilToInt(_defaultRewardCount * _incomeUpgrade.BonusValue);

            if (rewardCount < MinRewardCount)
                rewardCount = MinRewardCount;

            Count = rewardCount;
            Changed?.Invoke();
        }
    }
}