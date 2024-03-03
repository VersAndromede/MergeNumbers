using Agava.WebUtility;
using Scripts.Ad;
using Scripts.Pause;
using UnityEngine;

namespace Scripts.FocusObserverSystem
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
            {
                ad.AdStarted += OnAdStarted;
                ad.AdEnded += OnAdEnded;
            }
        }

        public void Disable()
        {
            WebApplication.InBackgroundChangeEvent -= OnBackgroundChangeEvent;

            foreach (IAd ad in _ads)
            {
                ad.AdStarted -= OnAdStarted;
                ad.AdEnded -= OnAdEnded;
            }
        }

        private void OnAdStarted()
        {
            _isAdRunning = true;
        }

        private void OnAdEnded()
        {
            _isAdRunning = false;
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