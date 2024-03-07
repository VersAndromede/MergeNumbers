using System.Collections;
using Scripts.Level.MoveCounterSystem;
using UnityEngine;

namespace Scripts.Level.PlayerSystem
{
    public class PlayerRotation : MonoBehaviour
    {
        private const float ForcedStopTime = 0.3f;

        [SerializeField] private MoveCounter _moveCounter;
        [SerializeField] private float _travelTime;

        private Coroutine _rotationJob;
        private Coroutine _endMovementJob;
        private WaitForSeconds _waitTime;
        private float _elapsedTime;

        private void Start()
        {
            _waitTime = new WaitForSeconds(ForcedStopTime);
            _moveCounter.Ended += OnGameMovesEnded;
        }

        private void OnDestroy()
        {
            _moveCounter.Ended -= OnGameMovesEnded;
        }

        public void StartRotationJob(Vector3 targetDirection)
        {
            if (_rotationJob != null)
            {
                StopCoroutine(_endMovementJob);
                StopCoroutine(_rotationJob);
            }

            _rotationJob = StartCoroutine(StartRotation(targetDirection));
        }

        private IEnumerator StartRotation(Vector3 targetDirection)
        {
            Vector3 currentDirection = transform.forward;
            _elapsedTime = 0;
            _endMovementJob = StartCoroutine(EndMovement());

            while (transform.forward.normalized != targetDirection.normalized)
            {
                _elapsedTime += Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(currentDirection, targetDirection, _elapsedTime / _travelTime, 0);
                transform.rotation = Quaternion.LookRotation(newDirection);
                yield return null;
            }
        }

        private IEnumerator EndMovement()
        {
            yield return _waitTime;

            if (_rotationJob != null)
                StopCoroutine(_rotationJob);

            _rotationJob = null;
        }

        private void OnGameMovesEnded()
        {
            StartRotationJob(Vector3.forward);
        }
    }
}