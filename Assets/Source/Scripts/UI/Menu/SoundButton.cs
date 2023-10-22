using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private Image _image;
    [SerializeField] private Color _disableColor;
    [SerializeField] private Color _enableColor;

    private const string MixerName = "SoundVolume";
    private const int MaxMixerVolume = 0;
    private const int MinMixerVolume = -80;

    public bool Enabled { get; private set; }

    public event Action EnabledChanged;

    private void Start()
    {
        if (Enabled)
            _audioMixerGroup.audioMixer.SetFloat(MixerName, MaxMixerVolume);
        else
            _audioMixerGroup.audioMixer.SetFloat(MixerName, MinMixerVolume);
    }

    public void Init(bool enabled)
    {
        Enabled = enabled;
        _image.color = enabled ? _enableColor : _disableColor;

        if (Enabled)
            _audioMixerGroup.audioMixer.SetFloat(MixerName, MaxMixerVolume);
        else
            _audioMixerGroup.audioMixer.SetFloat(MixerName, MinMixerVolume);
    }

    public void Switch()
    {
        if (Enabled)
            Switch(MinMixerVolume, false);
        else
            Switch(MaxMixerVolume, true);
    }

    private void Switch(float mixerValue, bool enabled)
    {
        _audioMixerGroup.audioMixer.SetFloat(MixerName, mixerValue);
        Enabled = enabled;
        _image.color = enabled ? _enableColor : _disableColor;
        EnabledChanged?.Invoke();
    }
}
