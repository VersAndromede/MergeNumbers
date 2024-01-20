using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameMoves _gameMoves;
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

    private Vector3 _monsterPosition => _observer.transform.position + new Vector3(0, _monsterYPositionOffset, 0);
    private int _monsterPower => Random.Range(_currentMinPower, _currentMaxPower);

    private void OnDestroy()
    {
        _gameMoves.Changed -= OnGameMovesChanged;
        _gameMoves.Ended -= OnGameMovesEnded;
    }

    public void Init(IMonsterNegativeCounter monsterNegativeCounter)
    {
        _currentMinPower = _config.StartingMinPower;
        _currentMaxPower = _config.StartingMaxPower;
        _monsterNegativeCounter = monsterNegativeCounter;

        _gameMoves.Changed += OnGameMovesChanged;
        _gameMoves.Ended += OnGameMovesEnded;
    }

    public Monster SpawnOnlyPositive()
    {
        Monster monster = _monsterFabric.GetAdding(_monsterPower, _monsterPosition);
        _currentMonster = monster;
        return monster;
    }

    private void TryRandomSpawn()
    {
        if (_observer.Monster != null || _observer.Player != null)
            return;

        if (IsNegativeMonsterCanBeSpawned() || TrySpawnAddingMonster() || TrySpawnDividerMonster())
            Spawned?.Invoke(_currentMonster, _currentMonster.PowerCount);
        else
            _currentMonster = _monsterFabric.GetSubtractive(_monsterPower, _monsterPosition);
    }

    private bool TrySpawnAddingMonster()
    {
        if (Randomizer.CheckProbability(_config.ProbabilityPositiveMonster))
        {
            _currentMonster = SpawnOnlyPositive();
            CounterRestartRequired?.Invoke();
            return true;
        }

        return false;
    }

    private bool TrySpawnDividerMonster()
    {
        if (_monsterNegativeCounter.DividersCount >= _monsterNegativeCounter.MaxDividersCount)
            return false;

        if (Randomizer.CheckProbability(_config.ProbabilityDividerMonster))
        {
            _currentMonster = _monsterFabric.GetDivider(_config.DividerPower, _monsterPosition);
            return true;
        }

        return false;
    }

    private bool IsNegativeMonsterCanBeSpawned()
    {
        if (_monsterNegativeCounter.AllCount >= _monsterNegativeCounter.MaxAllCount)
        {
            SpawnOnlyPositive();
            CounterRestartRequired?.Invoke();
            return true;
        }

        return false;
    }

    private void TryIncreaseDifficulty()
    {
        if (_gameMoves.Count % _config.NumberMovesToComplicate == 0)
        {
            _currentMinPower += _config.PowerProgression;
            _currentMaxPower += _config.PowerProgression;
        }
    }

    private void OnGameMovesChanged()
    {
        TryRandomSpawn();
        TryIncreaseDifficulty();
    }

    private void OnGameMovesEnded()
    {
        if (_currentMonster != null)
            _currentMonster.Die();
    }
}