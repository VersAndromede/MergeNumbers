using Scripts.Pause;
using Scripts.UpgradeSystem;
using Scripts.WalletSystem;
using UnityEngine;

namespace Scripts.Ad
{
    public class VideoAdDisplaySetup : MonoBehaviour
    {
        [SerializeField] private RewardButton _rewardButton;
        [SerializeField] private UpgradeWithMultiplicationValuePolicy _incomeUpgrade;
        [SerializeField] private AdRewardCountDisplay _adRewardCountDisplay;

        private VideoAdDisplayPresenter _videoAdDisplayPresenter;
        private RewardForVideoAd _rewardForVideoAd;
        private VideoAdRewardDispenser _videoAdRewardDispenser;

        private void OnDestroy()
        {
            _videoAdDisplayPresenter.Disable();
            _rewardForVideoAd.Disable();
        }

        public void Init(VideoAdDisplay videoAdDisplay, Wallet wallet)
        {
            _rewardForVideoAd = new RewardForVideoAd(_incomeUpgrade);
            _videoAdRewardDispenser = new VideoAdRewardDispenser(_rewardForVideoAd, wallet);
            _videoAdDisplayPresenter = new VideoAdDisplayPresenter(videoAdDisplay, _videoAdRewardDispenser, _rewardButton);
            _videoAdDisplayPresenter.Enable();
            _rewardForVideoAd.Enable();

            _adRewardCountDisplay.Init(_rewardForVideoAd);
        }
    }
}