using UnityEngine;

public class FocusObserver : MonoBehaviour
{
    private PauseController _pauseController;
    private PauseButton _pauseButton;
    private IAd[] _ads;
    private bool _isAdRunning;

    private void OnApplicationFocus(bool focus)
    {
        if (focus && _isAdRunning == false && _pauseButton.IsPaused == false)
            _pauseController.SetPause(false);
        else
            _pauseController.SetPause(true);
    }

    public void Init(PauseController pauseController, PauseButton pauseButton, params IAd[] ads)
    {
        _pauseButton = pauseButton;
        _pauseController = pauseController;
        _ads = ads;
    }

    public void Enable()
    {
        foreach (IAd ad in _ads)
            ad.AdRunning += OnAdRunning;
    }

    public void Disable()
    {
        foreach (IAd ad in _ads)
            ad.AdRunning -= OnAdRunning;
    }

    private void OnAdRunning(bool isRunning)
    {
        _isAdRunning = isRunning;
    }
}