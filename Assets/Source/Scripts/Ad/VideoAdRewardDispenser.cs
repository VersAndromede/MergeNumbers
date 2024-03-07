using Scripts.WalletSystem;

namespace Scripts.Ad
{
    public class VideoAdRewardDispenser
    {
        private readonly RewardForVideoAd _rewardForVideoAd;
        private readonly Wallet _wallet;

        public VideoAdRewardDispenser(RewardForVideoAd rewardForVideoAd, Wallet wallet)
        {
            _rewardForVideoAd = rewardForVideoAd;
            _wallet = wallet;
        }

        public void Give()
        {
            _wallet.AddCoins(_rewardForVideoAd.Count);
        }
    }
}