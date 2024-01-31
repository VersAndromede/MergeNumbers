using PlayerSystem;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Upgrades;

namespace MoveCounterSystem
{
    public class MoveCounter : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private GameStarter _gameStarter;
        [SerializeField] private UpgradeWithAddedValuePolicy _movesUpgrade;
        [SerializeField] private Image _inputHandler;
        [SerializeField] private UnityEvent _ended;

        [field: SerializeField] public int Count { get; private set; }

        public event Action Changed;
        public event Action Ended;

        private void OnEnable()
        {
            _playerMovement.FinishedMoving += OnFinishMove;
            _gameStarter.GameStarted += OnGameStarted;
        }

        private void OnDisable()
        {
            _playerMovement.FinishedMoving -= OnFinishMove;
            _gameStarter.GameStarted -= OnGameStarted;
        }

        public void OnGameStarted()
        {
            Count += _movesUpgrade.BonusValue;
        }

        private void OnFinishMove()
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
}