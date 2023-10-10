using System;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [field: SerializeField] public BossHealth BossHealth { get; private set; }
    [field: SerializeField] public float RechargeTime { get; private set; }

    public BossData Data { get; private set; }

    public void Init(BossData bossData)
    {
        Data = bossData;
    }

    public void Init(int damageTaken)
    {
        if (damageTaken < 0)
            throw new ArgumentOutOfRangeException();

        BossHealth.Init(Data.Health, damageTaken);
    }
}