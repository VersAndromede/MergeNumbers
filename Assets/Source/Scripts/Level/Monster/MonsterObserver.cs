using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterSystem
{
    public class MonsterObserver : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;

        private List<Monster> _monsters = new List<Monster>();

        public IReadOnlyList<Monster> Monsters => _monsters;

        private void OnEnable()
        {
            _movement.StartedMoving += OnStartedMoving;
        }

        private void OnDisable()
        {
            _movement.StartedMoving -= OnStartedMoving;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out Monster monster))
                _monsters.Add(monster);
        }

        private void OnStartedMoving()
        {
            _monsters = new List<Monster>();
        }
    }
}