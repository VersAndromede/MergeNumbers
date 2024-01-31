using Extensions;
using MonsterSystem;
using MoveCounterSystem;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MonsterSpawnerSystem
{
    public class MonsterSpawner : MonoBehaviour
    {
        [SerializeField] private MoveCounter _moveCounter;
        [SerializeField] private SpawnerObserver _observer;
        [SerializeField] private MonsterSpawnerConfig _config;
        [SerializeField] private MonsterFabric _monsterFabric;
        [SerializeField] private float _monsterYPositionOffset;

        [field: SerializeField] public bool HasPlayerAtStart { get; private set; }

        private Monster _currentMonster;
        private IMonsterNegativeCounter _monsterNegativeCounter;
        private int _currentMinPower;
        private int _currentMaxPower;

        public event Action<Monster, int> Spawned;
        public event Action CounterRestartRequired;

        private Vector3 MonsterPosition => _observer.transform.position + new Vector3(0, _monsterYPositionOffset, 0);
        private int MonsterPower => Random.Range(_currentMinPower, _currentMaxPower);

        private void OnDestroy()
        {
            _moveCounter.Changed -= OnGameMovesChanged;
            _moveCounter.Ended -= OnGameMovesEnded;
        }

        public void Init(IMonsterNegativeCounter monsterNegativeCounter)
        {
            _currentMinPower = _config.StartingMinPower;
            _currentMaxPower = _config.StartingMaxPower;
            _monsterNegativeCounter = monsterNegativeCounter;

            _moveCounter.Changed += OnGameMovesChanged;
            _moveCounter.Ended += OnGameMovesEnded;
        }

        public Monster InstantiateMonsterAdding()
        {
            Monster monster = _monsterFabric.GetAdding(MonsterPower, MonsterPosition);
            _currentMonster = monster;
            return monster;
        }

        private void RandomSpawn()
        {
            if (_observer.Monster != null || _observer.Player != null)
                return;

            if (IsNegativeMonsterCanBeSpawned() || TrySpawnAddingMonster() || TrySpawnDividerMonster())
                Spawned?.Invoke(_currentMonster, _currentMonster.PowerCount);
            else
                _currentMonster = _monsterFabric.GetSubtractive(MonsterPower, MonsterPosition);
        }

        private bool TrySpawnAddingMonster()
        {
            if (Randomizer.TryProbability(_config.ProbabilityPositiveMonster))
            {
                _currentMonster = InstantiateMonsterAdding();
                CounterRestartRequired?.Invoke();
                return true;
            }

            return false;
        }

        private bool TrySpawnDividerMonster()
        {
            if (_monsterNegativeCounter.DividersCount >= _monsterNegativeCounter.MaxDividersCount)
                return false;

            if (Randomizer.TryProbability(_config.ProbabilityDividerMonster))
            {
                _currentMonster = _monsterFabric.GetDivider(_config.DividerPower, MonsterPosition);
                return true;
            }

            return false;
        }

        private bool IsNegativeMonsterCanBeSpawned()
        {
            if (_monsterNegativeCounter.AllCount >= _monsterNegativeCounter.MaxAllCount)
            {
                InstantiateMonsterAdding();
                CounterRestartRequired?.Invoke();
                return true;
            }

            return false;
        }

        private void IncreaseDifficulty()
        {
            if (_moveCounter.Count % _config.NumberMovesToComplicate == 0)
            {
                _currentMinPower += _config.PowerProgression;
                _currentMaxPower += _config.PowerProgression;
            }
        }

        private void OnGameMovesChanged()
        {
            RandomSpawn();
            IncreaseDifficulty();
        }

        private void OnGameMovesEnded()
        {
            if (_currentMonster != null)
                _currentMonster.Die();
        }
    }
}