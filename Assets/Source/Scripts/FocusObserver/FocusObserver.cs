using Ad;
using Agava.WebUtility;
using Pause;
using UnityEngine;

namespace GameFocusObserver
{
    public class FocusObserver : MonoBehaviour
    {
        private PauseSetter _pauseSetter;
        private PauseButton _pauseButton;
        private IAd[] _ads;
        private bool _isAdRunning;

        public void Init(PauseSetter pauseSetter, PauseButton pauseButton, params IAd[] ads)
        {
            _pauseButton = pauseButton;
            _pauseSetter = pauseSetter;
            _ads = ads;
        }

        public void Enable()
        {
            WebApplication.InBackgroundChangeEvent += OnBackgroundChangeEvent;

            foreach (IAd ad in _ads)
                ad.AdRunning += OnAdRunning;
        }

        public void Disable()
        {
            WebApplication.InBackgroundChangeEvent -= OnBackgroundChangeEvent;

            foreach (IAd ad in _ads)
                ad.AdRunning -= OnAdRunning;
        }

        private void OnAdRunning(bool isRunning)
        {
            _isAdRunning = isRunning;
        }

        private void OnBackgroundChangeEvent(bool inBackground)
        {
            if (inBackground == false && _isAdRunning == false && _pauseButton.IsPaused == false)
                _pauseSetter.Disable();
            else
                _pauseSetter.Enable();
        }
    }
}