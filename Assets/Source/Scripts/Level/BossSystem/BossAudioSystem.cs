using System;
using System.Collections.Generic;
using Plugins.Audio.Core;
using Plugins.Audio.Utils;
using UnityEngine;

namespace Scripts.Level.BossSystem
{
    [Serializable]
    public class BossAudioSystem
    {
        private const int MaxRepeatCount = 3;

        [SerializeField] private SourceAudio _audioSource;
        [SerializeField] private List<AudioDataProperty> _hitClips;

        private AudioDataProperty _currentAudioClip;
        private int _repeatCount;

        public void PlayHit()
        {
            AudioDataProperty clip = _hitClips[UnityEngine.Random.Range(0, _hitClips.Count)];

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
}