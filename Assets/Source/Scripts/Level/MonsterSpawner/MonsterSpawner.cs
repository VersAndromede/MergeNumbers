using System;
using System.Collections;
using System.Threading;
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

    private const float MinDaleyForNewSpawn = 0.3f;

    private Monster _currentMonster;
    private IMonsterNegativeCounter _monsterNegativeCounter;
    private WaitForSeconds _waitForMinDaleyForNewSpawn;
    private Coroutine _spawnJob;
    private bool _canSpawn = true;

    public event Action<Monster, int> Spawned;
    public event Action CounterRestartRequired;

    private void OnDestroy()
    {
        _gameMoves.Changed -= OnGameMovesChanged;
        _gameMoves.Ended -= OnGameMovesEnded;
    }

    public void Init(IMonsterNegativeCounter monsterNegativeCounter)
    {
        _monsterNegativeCounter = monsterNegativeCounter;
        _waitForMinDaleyForNewSpawn = new WaitForSeconds(MinDaleyForNewSpawn);
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
        if (_spawnJob != null)
            StopCoroutine(_spawnJob);

        _spawnJob = StartCoroutine(TryRandomSpawn());
        TryIncreaseDifficulty();
    }

    private IEnumerator TryRandomSpawn()
    {
        if (_canSpawn == false || _observer.Monster != null || _observer.Player != null)
            yield break;

        if (IsNegativeMonsterCanBeSpawned())
            yield break;

        _currentMonster = Instantiate(_monsterPrefab, transform);
        int power = Random.Range(_startingMinPower, _startingMaxPower);

        if (TrySpawnAddingMonster(power))
            yield break;

        if (TrySpawnDividerMonster())
            yield break;

        InitCurrentMonster(MonsterType.Adding, -power);
        _canSpawn = false;
        yield return _waitForMinDaleyForNewSpawn;
        _canSpawn = true;
    }

    private void InitCurrentMonster(MonsterType type, int power)
    {
        _currentMonster.Init(type, power);
        Spawned?.Invoke(_currentMonster, power);
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
        if (_monsterNegativeCounter.DividersCount >= _monsterNegativeCounter.MaxDividersCount)
            return false;

        if (Randomizer.CheckProbability(_probabilityDividerMonster))
        {
            InitCurrentMonster(MonsterType.Divider, _dividerPower);
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