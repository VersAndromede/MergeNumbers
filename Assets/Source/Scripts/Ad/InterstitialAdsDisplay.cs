using System;
using Agava.YandexGames;
using Scripts.Pause;
using UnityEngine;

namespace Scripts.Ad
{
    public class InterstitialAdsDisplay : MonoBehaviour, IAd
    {
        private PauseSetter _pauseSetter;

        public event Action AdStarted;

        public event Action AdEnded;

        public void Init(PauseSetter pauseSetter)
        {
            _pauseSetter = pauseSetter;
        }

        public void ShowAd(Action adOver)
        {
            if (Application.isEditor)
            {
                adOver?.Invoke();
                return;
            }

            _pauseSetter.Enable();
            AdStarted?.Invoke();

            InterstitialAd.Show(
                onCloseCallback: value => Enable(adOver));
        }

        private void Enable(Action adOver)
        {
            _pauseSetter.Disable();
            AdEnded?.Invoke();
            adOver?.Invoke();
        }
    }
}