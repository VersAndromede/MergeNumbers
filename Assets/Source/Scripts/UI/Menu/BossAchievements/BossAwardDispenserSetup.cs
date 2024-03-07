using Scripts.WalletSystem;
using UnityEngine;

namespace Scripts.UI.Menu.BossAchievements
{
    public class BossAwardDispenserSetup : MonoBehaviour
    {
        [SerializeField] private BossAwardButton _bossAwardButton;
        [SerializeField] private BossAwardButtonText _bossAwardButtonText;

        private BossAwardDispenserPresenter _bossAwardDispenserPresenter;

        private void OnDestroy()
        {
            _bossAwardDispenserPresenter.Disable();
        }

        public void Init(BossAward bossAward, Wallet wallet)
        {
            BossAwardDispenser bossAwardDispenser = new BossAwardDispenser(bossAward, wallet);
            _bossAwardDispenserPresenter = new BossAwardDispenserPresenter(bossAwardDispenser, _bossAwardButton, _bossAwardButtonText);
            _bossAwardDispenserPresenter.Enable();
            _bossAwardButton.Init(bossAward.CanBeTaken, bossAward.IsTaken);
        }
    }
}
