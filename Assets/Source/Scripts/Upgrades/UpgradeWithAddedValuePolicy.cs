using UnityEngine;

namespace Upgrades
{
    public class UpgradeWithAddedValuePolicy : Upgrade
    {
        [SerializeField] private PriceMultiplicationWithBorder _priceMultiplicationWithBorder;
        [SerializeField] private DefaultValueChange _defaultValueChange;

        protected override void OnInit()
        {
            SetPriceChangeAlgorithm(_priceMultiplicationWithBorder);
            SetValueChangeAlgorithm(_defaultValueChange);
        }
    }
}
