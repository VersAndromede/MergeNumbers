using HealthSystem;
using System;
using UnityEngine;

namespace BossSystem
{
    public class Boss : MonoBehaviour
    {
        [field: SerializeField] public BossHealth BossHealth { get; private set; }

        public BossData Data { get; private set; }

        public void InitData(BossData bossData)
        {
            Data = bossData;
        }

        public void InitHealth(HealthSetup healthSetup, int damageTaken)
        {
            if (damageTaken < 0)
                throw new ArgumentOutOfRangeException();

            BossHealth.Init(healthSetup, Data.Health, damageTaken);
        }
    }
}