namespace Scripts.Ad
{
    public class VideoAdDisplayPresenter
    {
        private readonly VideoAdDisplay _videoAdDisplay;
        private readonly VideoAdRewardDispenser _videoAdRewardDispenser;
        private readonly RewardButton _rewardButton;

        public VideoAdDisplayPresenter(VideoAdDisplay videoAdDisplay, VideoAdRewardDispenser videoAdRewardDispenser, RewardButton button)
        {
            _videoAdDisplay = videoAdDisplay;
            _videoAdRewardDispenser = videoAdRewardDispenser;
            _rewardButton = button;
        }

        public void Enable()
        {
            _rewardButton.Pressed += OnPressed;
            _videoAdDisplay.AdEnded += OnAdEnded;
            _videoAdDisplay.Viewed += OnViewed;
            _videoAdDisplay.RewardReceived += OnRewardReceived;
        }

        public void Disable()
        {
            _rewardButton.Pressed -= OnPressed;
            _videoAdDisplay.AdEnded -= OnAdEnded;
            _videoAdDisplay.Viewed += OnViewed;
            _videoAdDisplay.RewardReceived -= OnRewardReceived;
        }

        private void OnPressed()
        {
            _rewardButton.DisableInteractable();
            _videoAdDisplay.WatchVideoAd();
        }

        private void OnAdEnded()
        {
            _rewardButton.EnableInteractable();
        }

        private void OnViewed()
        {
            _videoAdRewardDispenser.Give();
        }

        private void OnRewardReceived()
        {
            _rewardButton.ReportViewedVideoAd();
        }
    }
}