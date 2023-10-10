using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void SetPause(bool paused)
    {
        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
