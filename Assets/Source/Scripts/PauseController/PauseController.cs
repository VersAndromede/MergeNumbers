using UnityEngine;

public class PauseController 
{
    public void SetPause(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
            return;
        }

        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}
