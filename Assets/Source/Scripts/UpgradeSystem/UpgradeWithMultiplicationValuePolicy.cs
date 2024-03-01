using UnityEngine;

namespace Scripts.UpgradeSystem
{
    public class UpgradeWithMultiplicationValuePolicy : Upgrade
    {
        [SerializeField] private PriceMultiplication _priceMultiplication;
        [SerializeField] private ValueMultiplicationWithMinBorder _valueMultiplication;

        public uint MinBonusValue => (uint)_valueMultiplication.MinBorder;

        protected override void OnInit()
        {
            SetPriceChangeAlgorithm(_priceMultiplication);
            SetValueChangeAlgorithm(_valueMultiplication);
        }
    }
}