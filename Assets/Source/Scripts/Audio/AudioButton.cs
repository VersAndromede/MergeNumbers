using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Scripts.Audio
{
    public class AudioButton : MonoBehaviour
    {
        private const int MaxMixerVolume = 0;
        private const int MinMixerVolume = -80;

        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private string _mixerName;

        public event Action EnabledChanged;

        public bool Enabled { get; private set; }

        private void Start()
        {
            if (Enabled)
                _audioMixerGroup.audioMixer.SetFloat(_mixerName, MaxMixerVolume);
            else
                _audioMixerGroup.audioMixer.SetFloat(_mixerName, MinMixerVolume);
        }

        public void Init(bool enabled)
        {
            Enabled = enabled;
            EnabledChanged?.Invoke();
        }

        public void Enable()
        {
            _audioMixerGroup.audioMixer.SetFloat(_mixerName, MinMixerVolume);
            Enabled = true;
            EnabledChanged?.Invoke();
        }

        public void Disable()
        {
            _audioMixerGroup.audioMixer.SetFloat(_mixerName, MaxMixerVolume);
            Enabled = false;
            EnabledChanged?.Invoke();
        }
    }
}