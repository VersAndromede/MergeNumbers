using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private PauseSetter _pauseSetter;

    public bool IsPaused { get; private set; }

    public void Init(PauseSetter pauseSetter)
    {
        _pauseSetter = pauseSetter;
    }

    public void Enable()
    {
        _pauseSetter.Enable();
        IsPaused = true;
    }

    public void Disable()
    {
        _pauseSetter.Disable();
        IsPaused = false;
    }
}
