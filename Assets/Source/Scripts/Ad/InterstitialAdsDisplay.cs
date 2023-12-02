using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

public class InterstitialAdsDisplay : MonoBehaviour, IAd
{
    [SerializeField] private int _waitTimeInSeconds;

    private int _currentTimeInSeconds;
    private Coroutine _startWaitTimerJob;
    private PauseController _pauseController;

    public event Action<bool> AdRunning;


    public void Init(PauseController pauseController)
    {
        _pauseController = pauseController;
        _startWaitTimerJob = StartCoroutine(StartWaitTimer());
    }

    public void TryShowAd()
    {
        if (Application.isEditor)
            return;

        if (_currentTimeInSeconds >= _waitTimeInSeconds)
        {
            StopCoroutine(_startWaitTimerJob);
            _currentTimeInSeconds = 0;
            AdRunning?.Invoke(true);
            _pauseController.SetPause(true);

            InterstitialAd.Show(
                onCloseCallback: value => Enable(),
                onErrorCallback: error => Enable());

            _startWaitTimerJob = StartCoroutine(StartWaitTimer());
        }
    }

    private IEnumerator StartWaitTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            _currentTimeInSeconds++;
        }
    }

    private void Enable()
    {
        _pauseController.SetPause(false);
        AdRunning?.Invoke(false);
    }
}