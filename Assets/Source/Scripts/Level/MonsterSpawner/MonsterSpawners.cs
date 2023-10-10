using UnityEngine;

public class MonsterSpawners : MonoBehaviour, IMonsterNegativeCounter
{
    private MonsterSpawner[] _monsterSpawners;

    public int Count { get; private set; }
    public int MaxCount => 4;

    private void Start()
    {
        _monsterSpawners = GetComponentsInChildren<MonsterSpawner>();

        foreach (MonsterSpawner monsterSpawner in _monsterSpawners)
        {
            monsterSpawner.Init(this);
            monsterSpawner.Spawned += OnMonsterSpawned;
            monsterSpawner.CounterRestartRequired += Restart;
        }
    }

    private void OnDestroy()
    {
        foreach (MonsterSpawner monsterSpawner in _monsterSpawners)
        {
            monsterSpawner.Spawned -= OnMonsterSpawned;
            monsterSpawner.CounterRestartRequired -= Restart;
        }
    }

    public void TrySpawnOnlyPositiveMonsters()
    {
        foreach (MonsterSpawner monsterSpawner in _monsterSpawners)
            if (monsterSpawner.HasPlayerAtStart == false)
                monsterSpawner.SpawnOnlyPositive();
    }

    public void OnMonsterSpawned(MonsterType type, int power)
    {
        if (power < 0 || type == MonsterType.Divider)
            Count++;
    }

    public void Restart()
    {
        Count = 0;
    }
}
