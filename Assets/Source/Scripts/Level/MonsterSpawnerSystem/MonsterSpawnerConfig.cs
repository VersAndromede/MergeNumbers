using System;
using UnityEngine;

namespace Scripts.Level.MonsterSpawnerSystem
{
    [CreateAssetMenu(fileName = "Monster Spawner", menuName = "Data/Monster Spawner")]
    public class MonsterSpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public int NumberMovesToComplicate { get; private set; }

        [field: SerializeField] public int PowerProgression { get; private set; }

        [field: SerializeField] public int StartingMinPower { get; private set; }

        [field: SerializeField] public int StartingMaxPower { get; private set; }

        [field: SerializeField] public int DividerPower { get; private set; }

        [field: Range(0, 100)]
        [field: SerializeField] public int ProbabilityPositiveMonster { get; private set; }

        [field: Range(0, 100)]
        [field: SerializeField] public int ProbabilityDividerMonster { get; private set; }
    }
}