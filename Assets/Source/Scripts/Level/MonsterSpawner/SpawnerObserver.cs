using MonsterSystem;
using MoveCounterSystem;
using PlayerSystem;
using UnityEngine;

namespace MonsterSpawnerSystem
{
    public class SpawnerObserver : MonoBehaviour
    {
        [SerializeField] private MoveCounter _moveCounter;

        public Player Player { get; private set; }
        public Monster Monster { get; private set; }

        private void OnEnable()
        {
            _moveCounter.Ended += OnGameMovesEnded;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out Player player))
                Player = player;

            if (collision.TryGetComponent(out Monster monster))
                Monster = monster;
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                Player = null;
                Monster = null;
            }
        }

        private void OnDisable()
        {
            _moveCounter.Ended -= OnGameMovesEnded;
        }

        private void OnGameMovesEnded()
        {
            gameObject.SetActive(false);
        }
    }
}