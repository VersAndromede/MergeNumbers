using System;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [field: SerializeField] public BossHealth BossHealth { get; private set; }

    public BossData Data { get; private set; }

    public void Init(BossData bossData)
    {
        Data = bossData;
    }

    public void Init(HealthSetup healthSetup, int damageTaken)
    {
        if (damageTaken < 0)
            throw new ArgumentOutOfRangeException();

        BossHealth.Init(healthSetup, Data.Health, damageTaken);
    }
}