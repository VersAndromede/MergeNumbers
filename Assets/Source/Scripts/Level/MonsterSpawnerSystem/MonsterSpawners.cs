using Scripts.Level.MonsterSystem;
using UnityEngine;

namespace Scripts.Level.MonsterSpawnerSystem
{
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
                monsterSpawner.CounterRestartRequired += OnCounterRestartRequired;
            }
        }

        private void OnDestroy()
        {
            foreach (MonsterSpawner monsterSpawner in _monsterSpawners)
            {
                monsterSpawner.Spawned -= OnMonsterSpawned;
                monsterSpawner.CounterRestartRequired -= OnCounterRestartRequired;
            }
        }

        public void FillFieldWithMonstersAdding()
        {
            foreach (MonsterSpawner monsterSpawner in _monsterSpawners)
            {
                if (monsterSpawner.HasPlayerAtStart == false)
                    monsterSpawner.InstantiateMonsterAdding();
            }
        }

        public void OnMonsterSpawned(Monster monster, int power)
        {
            if (monster is MonsterSubtractive)
                AllCount++;

            if (monster is MonsterDivider)
            {
                DividersCount++;
                AllCount++;
                monster.Died += OnMonsterDividerDied;
            }
        }

        public void OnCounterRestartRequired()
        {
            AllCount = 0;
        }

        private void OnMonsterDividerDied(Monster monster)
        {
            monster.Died -= OnMonsterDividerDied;
            DividersCount--;
        }
    }
}