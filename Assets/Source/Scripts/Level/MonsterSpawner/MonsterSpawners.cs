using UnityEngine;

public class MonsterSpawners : MonoBehaviour, IMonsterNegativeCounter
{
    private MonsterSpawner[] _monsterSpawners;

    public int AllCount { get; private set; }
    public int DividersCount { get; private set; }
    public int MaxAllCount => 4;
    public int MaxDividersCount => 2;

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

    public void OnMonsterSpawned(Monster monster, int power)
    {
        if (power < 0 || monster.Type == MonsterType.Divider)
            AllCount++;

        if (monster.Type == MonsterType.Divider)
        {
            DividersCount++;
            monster.Died += OnDied;
        }
    }

    public void Restart()
    {
        AllCount = 0;
    }

    private void OnDied(Monster monster)
    {
        monster.Died -= OnDied;
        DividersCount--;
    }
}
