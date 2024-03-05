using Agava.YandexGames;
using Scripts.Pause;
using Scripts.UpgradeSystem;
using Scripts.WalletSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Ad
{
    public class RewardButton : MonoBehaviour, IAd
    {
        private const uint MinRewardCount = 150;
        private const float RewardAdDisplayDelay = 0.04f;

        [SerializeField] private UpgradeWithMultiplicationValuePolicy _incomeUpgrade;
        [SerializeField] private Button _button;
        [SerializeField] private float _defaultRewardCount;
        [SerializeField] private UnityEvent _rewardReceived;

        private Wallet _wallet;
        private PauseSetter _pauseSetter;
        private WaitForSecondsRealtime _waitDisplayDelay;
        private bool _isRewarded;

        public event Action RewardReceived;

        public event Action RewardChanged;

        public event Action AdStarted;

        public event Action AdEnded;

        public uint RewardCount { get; private set; }

        private void OnDestroy()
        {
            _incomeUpgrade.LevelChanged -= OnIncomeUpgradeLevelChanged;
        }

        public void Init(PauseSetter pauseSetter, Wallet wallet)
        {
            OnIncomeUpgradeLevelChanged();
            _pauseSetter = pauseSetter;
            _wallet = wallet;
            _waitDisplayDelay = new WaitForSecondsRealtime(RewardAdDisplayDelay);
            _incomeUpgrade.LevelChanged += OnIncomeUpgradeLevelChanged;
        }

        public void WatchVideoAd()
        {
            _button.interactable = false;
            AdStarted?.Invoke();
            _pauseSetter.Enable();
            StartCoroutine(StartWatchVideoAd());
        }

        private IEnumerator StartWatchVideoAd()
        {
            yield return _waitDisplayDelay;

            VideoAd.Show(
            onRewardedCallback: () =>
            {
                _wallet.AddCoins(RewardCount);
                _isRewarded = true;
            },
            onCloseCallback: () =>
            {
                if (_isRewarded)
                {
                    _rewardReceived?.Invoke();
                    RewardReceived?.Invoke();
                }

                Enable();
            },
            onErrorCallback: error =>
            {
                Enable();
            });
        }

        private void Enable()
        {
            _isRewarded = false;
            _pauseSetter.Disable();
            _button.interactable = true;
            AdEnded?.Invoke();
        }

        private void OnIncomeUpgradeLevelChanged()
        {
            uint rewardCount = (uint)Mathf.CeilToInt(_defaultRewardCount * _incomeUpgrade.BonusValue);

            if (rewardCount < MinRewardCount)
                rewardCount = MinRewardCount;

            RewardCount = rewardCount;
            RewardChanged?.Invoke();
        }
    }
}