using Scripts.Ad;
using Scripts.Audio;
using Scripts.FocusObserverSystem;
using Scripts.Pause;
using Scripts.UI.Game;
using UnityEngine;

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

        private void OnDestroy()
        {
            _focusObserver.Disable();
        }

        public void Init(
            bool isSoundButtonEnabled, 
            bool isMusicButtonEnabled, 
            InterstitialAdsDisplay adsDisplay, 
            VideoAdDisplay videoAdDisplay,
            PauseSetter pauseSetter)
        {
            _pauseButton.Init(pauseSetter);
            SoundButton.Init(isSoundButtonEnabled);
            MusicButton.Init(isMusicButtonEnabled);
            _focusObserver.Init(pauseSetter, _pauseButton, videoAdDisplay, adsDisplay);
            _focusObserver.Enable();

            for (int i = 0; i < _returnToMenuButtons.Length; i++)
                _returnToMenuButtons[i].Init();
        }
    }
}