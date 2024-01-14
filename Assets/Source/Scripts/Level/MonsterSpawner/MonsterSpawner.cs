using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Monster _monsterPrefab;
    [SerializeField] private GameMoves _gameMoves;
    [SerializeField] private SpawnerObserver _observer;
    [SerializeField] private int _numberMovesToComplicate;
    [SerializeField] private int _powerProgression;
    [SerializeField] private int _startingMinPower;
    [SerializeField] private int _startingMaxPower;
    [SerializeField] private int _dividerPower;
    [SerializeField, Range(0, 100)] private int _probabilityPositiveMonster;
    [SerializeField, Range(0, 100)] private int _probabilityDividerMonster;

    [field: SerializeField] public bool HasPlayerAtStart { get; private set; }

    private Monster _currentMonster;
    private IMonsterNegativeCounter _monsterNegativeCounter;

    public event Action<MonsterType, int> Spawned;
    public event Action CounterRestartRequired;

    private void OnDestroy()
    {
        _gameMoves.Changed -= OnGameMovesChanged;
        _gameMoves.Ended -= OnGameMovesEnded;
    }

    public void Init(IMonsterNegativeCounter monsterNegativeCounter)
    {
        _monsterNegativeCounter = monsterNegativeCounter;
        _gameMoves.Changed += OnGameMovesChanged;
        _gameMoves.Ended += OnGameMovesEnded;
    }

    public void SpawnOnlyPositive()
    {
        _currentMonster = Instantiate(_monsterPrefab, transform);
        int power = Random.Range(_startingMinPower, _startingMaxPower);
        InitCurrentMonster(MonsterType.Adding, power);
    }

    private void OnGameMovesChanged()
    {
        TryRandomSpawn();
        TryIncreaseDifficulty();
    }

    private void TryRandomSpawn()
    {
        if (_observer.Monster != null || _observer.Player != null)
            return;

        if (IsNegativeMonsterCanBeSpawned())
            return;

        _currentMonster = Instantiate(_monsterPrefab, transform);
        int power = Random.Range(_startingMinPower, _startingMaxPower);

        if (TrySpawnAddingMonster(power))
            return;

        if (TrySpawnDividerMonster())
            return;

        InitCurrentMonster(MonsterType.Adding, -power);
    }

    private void InitCurrentMonster(MonsterType type, int power)
    {
        _currentMonster.Init(type, power);
        Spawned?.Invoke(type, power);
    }

    private bool TrySpawnAddingMonster(int power)
    {
        if (Randomizer.CheckProbability(_probabilityPositiveMonster))
        {
            InitCurrentMonster(MonsterType.Adding, power);
            CounterRestartRequired?.Invoke();
            return true;
        }

        return false;
    }

    private bool TrySpawnDividerMonster()
    {
        if (Randomizer.CheckProbability(_probabilityDividerMonster))
        {
            InitCurrentMonster(MonsterType.Divider, _dividerPower);
            return true;
        }

        return false;
    }

    private bool IsNegativeMonsterCanBeSpawned()
    {
        if (_monsterNegativeCounter.Count >= _monsterNegativeCounter.MaxCount)
        {
            SpawnOnlyPositive();
            CounterRestartRequired?.Invoke();
            return true;
        }

        return false;
    }

    private void TryIncreaseDifficulty()
    {
        if (_gameMoves.Count % _numberMovesToComplicate == 0)
        {
            _startingMinPower += _powerProgression;
            _startingMaxPower += _powerProgression;
        }
    }

    private void OnGameMovesEnded()
    {
        if (_currentMonster != null)
            _currentMonster.Die();
    }
}