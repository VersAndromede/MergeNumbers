using Plugins.Audio.Core;
using UnityEngine;

public class PauseController 
{
    public void SetPause(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0;
            AudioPauseHandler.Instance.PauseAudio();
            return;
        }

        Time.timeScale = 1;
        AudioPauseHandler.Instance.UnpauseAudio();
    }
}
