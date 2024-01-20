using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameMoves : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameStarter _gameStarter;
    [SerializeField] private MovesUpgrade _movesUpgrade; 
    [SerializeField] private Image _inputHandler;
    [SerializeField] private UnityEvent _ended;

    [field: SerializeField] public int Count { get; private set; }

    public event Action Changed;
    public event Action Ended;

    private void OnEnable()
    {
        _playerMovement.FinishedMoving += FinishMove;
        _gameStarter.GameStarted += UpdateCount;
    }

    private void OnDisable()
    {
        _playerMovement.FinishedMoving -= FinishMove;
        _gameStarter.GameStarted -= UpdateCount;
    }

    public void UpdateCount()
    {
        Count += _movesUpgrade.BonusValue;
    }

    private void FinishMove()
    {
        Count--;
        Changed?.Invoke();
        _inputHandler.raycastTarget = true;

        if (Count == 0)
        {
            Ended?.Invoke();
            _ended?.Invoke();
        }
    }
}
