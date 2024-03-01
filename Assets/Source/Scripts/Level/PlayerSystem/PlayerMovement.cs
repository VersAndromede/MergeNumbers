using Scripts.SmoothnessFunctions;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Level.PlayerSystem
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _travelTime;

        private Coroutine _moveJob;
        private Coroutine _endMoveJob;
        private WaitForSeconds _waitTime;
        private float _elapsedTime;

        public event Action StartedMoving;
        public event Action FinishedMoving;

        private void Start()
        {
            _waitTime = new WaitForSeconds(_travelTime);
        }

        public void MoveToMonster(Vector3 targetPosition)
        {
            if (_moveJob != null)
                return;

            _moveJob = StartCoroutine(StartMovement(targetPosition));
            StartedMoving?.Invoke();
        }

        private IEnumerator StartMovement(Vector3 targetPosition)
        {
            _elapsedTime = 0;
            Vector3 currentPosition = transform.position;
            targetPosition.y = currentPosition.y;

            if (_endMoveJob != null)
                StopCoroutine(_endMoveJob);

            _endMoveJob = StartCoroutine(EndMovement());

            while (Vector3.Distance(transform.position, targetPosition) > 0)
            {
                _elapsedTime += Time.deltaTime;
                float tLerp = SmoothnessFunction.GetEaseInOutCubic(_elapsedTime / _travelTime);
                Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, tLerp);
                _rigidbody.MovePosition(newPosition);
                yield return null;
            }
        }

        private IEnumerator EndMovement()
        {
            yield return _waitTime;
            StopCoroutine(_moveJob);
            _rigidbody.velocity = Vector3.zero;
            _moveJob = null;
            FinishedMoving?.Invoke();
        }
    }
}