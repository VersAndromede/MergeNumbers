﻿using UnityEngine;

namespace Upgrades
{
    public class PriceMultiplication : MonoBehaviour, IPriceChangeAlgorithm
    {
        [SerializeField] private float _multiplier;

        public virtual int GetChanged(int currentPrice)
        {
            return (int)Mathf.Round(currentPrice * _multiplier);
        }
    }
}