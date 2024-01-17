using Plugins.Audio.Core;
using Plugins.Audio.Utils;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private SourceAudio _audioSource;
    [SerializeField] private AudioDataProperty _audioClip;
    [SerializeField] private PlayerMovement _playerMovement;

    private void OnEnable()
    {
        _playerMovement.StartedMoving += OnStartedMoving;
    }

    private void OnDisable()
    {
        _playerMovement.StartedMoving -= OnStartedMoving;
    }

    private void OnStartedMoving()
    {
        _audioSource.Play(_audioClip);
    }
}
