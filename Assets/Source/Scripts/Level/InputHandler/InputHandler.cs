using MonsterSystem;
using MoveCounterSystem;
using System;
using TrainingSystem;
using UnityEngine;

namespace GameInput
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private InputGetter _inputGetter;
        [SerializeField] private PlayerMoverToMonster _playerMoverToMonster;
        [SerializeField] private MoveCounter _moveCounter;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Transform _player;

        private readonly MonsterFromRay _monsterFromRay = new MonsterFromRay();

        private IInput _input;
        private Training _training;
        private bool _isLockedMovement;

        private bool TrainingComplete => _training.IsViewed;

        private void OnDestroy()
        {
            _input.Received -= OnReceived;
            _moveCounter.Ended -= OnEnded;
        }

        public void Init(Training training)
        {
            _training = training;
            _input = _inputGetter.Get();

            _input.Received += OnReceived;
            _moveCounter.Ended += OnEnded;
        }

        private void OnReceived(Direction direction)
        {
            if (_isLockedMovement || TrainingComplete == false)
                return;

            Vector3 newDirection;

            switch (direction)
            {
                case Direction.Left:
                    newDirection = Vector3.left;
                    break;
                case Direction.Right:
                    newDirection = Vector3.right;
                    break;
                case Direction.Down:
                    newDirection = -Vector3.forward;
                    break;
                case Direction.Up:
                    newDirection = Vector3.forward;
                    break;
                default:
                    throw new ArgumentException();
            }

            if (_monsterFromRay.TryGet(_player.position, newDirection, _layerMask, out Monster monster))
                _playerMoverToMonster.Move(monster);
        }

        private void OnEnded()
        {
            _isLockedMovement = true;
        }
    }
}