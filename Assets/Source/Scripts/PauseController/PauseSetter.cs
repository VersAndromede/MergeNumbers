using Plugins.Audio.Core;
using UnityEngine;

public class PauseSetter 
{
    public void Enable()
    {
        Time.timeScale = 0;
        AudioPauseHandler.Instance.PauseAudio();
    }

    public void Disable() 
    {
        Time.timeScale = 1;
        AudioPauseHandler.Instance.UnpauseAudio();
    }
}
