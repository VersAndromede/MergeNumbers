using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BossAudioSystem
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _hitClips;

    private const int MaxRepeatCount = 3;

    private AudioClip _currentAudioClip;
    private int _repeatCount;

    public void PlayHit()
    {
        AudioClip clip = _hitClips[UnityEngine.Random.Range(0, _hitClips.Count)];

        if (_currentAudioClip == clip)
        {
            _repeatCount++;

            if (_repeatCount == MaxRepeatCount)
            {
                PlayHit();
                return;
            }
        }

        _currentAudioClip = clip;
        _audioSource.PlayOneShot(clip);
    }
}