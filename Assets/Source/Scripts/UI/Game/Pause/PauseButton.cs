using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private PauseController _pauseController;

    public bool IsPaused { get; private set; }

    public void Init(PauseController pauseController)
    {
        _pauseController = pauseController;
    }

    public void SetPause(bool paused)
    {
        if (paused)
        {
            _pauseController.SetPause(true);
            IsPaused = true;
        }
        else
        {
            _pauseController.SetPause(false);
            IsPaused = false;
        }
    }
}
