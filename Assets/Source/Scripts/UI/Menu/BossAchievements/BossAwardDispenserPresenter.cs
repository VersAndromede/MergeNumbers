namespace Scripts.UI.Menu.BossAchievements
{
    public class BossAwardDispenserPresenter
    {
        private readonly BossAwardDispenser _bossAwardDispenser;
        private readonly BossAwardButton _bossAwardButton;
        private readonly BossAwardButtonText _bossAwardButtonText;

        public BossAwardDispenserPresenter(BossAwardDispenser bossAwardDispenser, BossAwardButton button, BossAwardButtonText buttonText)
        {
            _bossAwardDispenser = bossAwardDispenser;
            _bossAwardButton = button;
            _bossAwardButtonText = buttonText;
        }

        public void Enable()
        {
            _bossAwardDispenser.Received += OnReceived;
            _bossAwardButton.Initialized += OnInitialized;
            _bossAwardButton.Pressed += OnPressed;
        }

        public void Disable()
        {
            _bossAwardDispenser.Received -= OnReceived;
            _bossAwardButton.Initialized -= OnInitialized;
            _bossAwardButton.Pressed -= OnPressed;
        }

        private void UpdateButtonText()
        {
            if (_bossAwardDispenser.AwardTaken)
                _bossAwardButtonText.SetReceived();
            else
                _bossAwardButtonText.SetTake();
        }

        private void OnReceived()
        {
            _bossAwardButton.ReportReceipt();
            UpdateButtonText();
        }

        private void OnInitialized()
        {
            UpdateButtonText();
        }

        private void OnPressed()
        {
            _bossAwardButton.DisableInteractable();
            _bossAwardDispenser.Give();
        }
    }
}
