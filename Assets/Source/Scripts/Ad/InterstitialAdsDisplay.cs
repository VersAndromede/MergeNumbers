using Agava.YandexGames;
using Pause;
using System;
using UnityEngine;

namespace Ad
{
    public class InterstitialAdsDisplay : MonoBehaviour, IAd
    {
        private PauseSetter _pauseSetter;

        public event Action<bool> AdRunning;

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
            AdRunning?.Invoke(true);

            InterstitialAd.Show(
                onCloseCallback: value => Enable(adOver));
        }

        private void Enable(Action adOver)
        {
            _pauseSetter.Disable();
            AdRunning?.Invoke(false);
            adOver?.Invoke();
        }
    }
}