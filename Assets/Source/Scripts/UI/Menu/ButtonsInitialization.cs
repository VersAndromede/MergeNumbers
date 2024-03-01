using Scripts.Ad;
using Scripts.Audio;
using Scripts.FocusObserverSystem;
using Scripts.UI.Game;
using Scripts.Pause;
using UnityEngine;
using Scripts.WalletSystem;

namespace Scripts.UI.Menu
{
    public class ButtonsInitialization : MonoBehaviour
    {
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private FocusObserver _focusObserver;
        [SerializeField] private ReturnToMenuButton[] _returnToMenuButtons;

        [field: SerializeField] public RewardButton RewardButton { get; private set; }
        [field: SerializeField] public AudioButton SoundButton { get; private set; }
        [field: SerializeField] public AudioButton MusicButton { get; private set; }

        public PauseSetter PauseSetter { get; private set; }

        private void OnDestroy()
        {
            _focusObserver.Disable();
        }

        public void Init(bool isSoundButtonEnabled, bool IsMusicButtonEnabled, InterstitialAdsDisplay adsDisplay, Wallet wallet)
        {
            PauseSetter pauseSetter = new PauseSetter();
            _pauseButton.Init(pauseSetter);
            RewardButton.Init(pauseSetter, wallet);
            SoundButton.Init(isSoundButtonEnabled);
            MusicButton.Init(IsMusicButtonEnabled);
            _focusObserver.Init(pauseSetter, _pauseButton, RewardButton, adsDisplay);
            _focusObserver.Enable();

            for (int i = 0; i < _returnToMenuButtons.Length; i++)
                _returnToMenuButtons[i].Init();
        }
    }
}