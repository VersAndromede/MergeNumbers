﻿using UnityEngine;
using Upgrades;

public class PlayerDamage : MonoBehaviour
{
    [field: SerializeField] public int Count { get; private set; }

    public void Upgrade(UpgradeWithMultiplicationValuePolicy damageUpgrade)
    {
        Count += damageUpgrade.BonusValue;
    }
}
