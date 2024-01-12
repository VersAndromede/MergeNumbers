using Agava.YandexGames;
using System;
using UnityEngine;

public class InterstitialAdsDisplay : MonoBehaviour, IAd
{
    private PauseController _pauseController;

    public event Action<bool> AdRunning;

    public void Init(PauseController pauseController)
    {
        _pauseController = pauseController;
    }

    public void TryShowAd(Action adOver)
    {
        if (Application.isEditor)
        {
            adOver?.Invoke();
            return;
        }

        _pauseController.SetPause(true);
        AdRunning?.Invoke(true);

        InterstitialAd.Show(
            onCloseCallback: value => Enable(adOver));
    }

    private void Enable(Action adOver)
    {
        _pauseController.SetPause(false);
        AdRunning?.Invoke(false);
        adOver?.Invoke();
    }
}