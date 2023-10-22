using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
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
        _audioSource.Play();
    }
}
