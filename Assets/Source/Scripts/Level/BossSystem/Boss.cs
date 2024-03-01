using Scripts.Level.HealthSystems;
using UnityEngine;

namespace Scripts.Level.BossSystem
{
    public class Boss : MonoBehaviour
    {
        [field: SerializeField] public BossHealth BossHealth { get; private set; }

        public BossData Data { get; private set; }

        public void Init(HealthSetup healthSetup, BossData bossData)
        {
            Data = bossData;
            BossHealth.Init(healthSetup, Data.Health);
        }
    }
}