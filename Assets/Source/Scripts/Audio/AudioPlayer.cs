using Plugins.Audio.Core;
using Plugins.Audio.Utils;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private SourceAudio _audioSource;
    [SerializeField] private AudioDataProperty _audioClip;
    [SerializeField] private bool _playOnStart;

    private void Start()
    {
        if (_playOnStart)
            Play();
    }

    public void Play()
    {
        _audioSource.Play(_audioClip);
    }
}
