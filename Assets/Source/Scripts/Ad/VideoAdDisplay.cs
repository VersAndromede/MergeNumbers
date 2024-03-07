using System;
using System.Collections;
using Agava.YandexGames;
using Scripts.Pause;
using UnityEngine;

namespace Scripts.Ad
{
    public class VideoAdDisplay : IAd
    {
        private const float RewardAdDisplayDelay = 0.04f;

        private readonly PauseSetter _pauseSetter;

        private readonly MonoBehaviour _monoBehaviour;
        private readonly WaitForSecondsRealtime _waitDisplayDelay;

        private bool _isRewarded;

        public VideoAdDisplay(PauseSetter pauseSetter, MonoBehaviour monoBehaviour)
        {
            _pauseSetter = pauseSetter;
            _monoBehaviour = monoBehaviour;
            _waitDisplayDelay = new WaitForSecondsRealtime(RewardAdDisplayDelay);
        }

        public event Action Viewed;

        public event Action RewardReceived;

        public event Action AdStarted;

        public event Action AdEnded;

        public void WatchVideoAd()
        {
            AdStarted?.Invoke();
            _pauseSetter.Enable();
            _monoBehaviour.StartCoroutine(StartWatchVideoAd());
        }

        private IEnumerator StartWatchVideoAd()
        {
            yield return _waitDisplayDelay;

            VideoAd.Show(
            onRewardedCallback: () =>
            {
                Viewed?.Invoke();
                _isRewarded = true;
            },
            onCloseCallback: () =>
            {
                if (_isRewarded)
                    RewardReceived?.Invoke();

                Enable();
            },
            onErrorCallback: error =>
            {
                Enable();
            });
        }

        private void Enable()
        {
            _isRewarded = false;
            _pauseSetter.Disable();
            AdEnded?.Invoke();
        }
    }
}