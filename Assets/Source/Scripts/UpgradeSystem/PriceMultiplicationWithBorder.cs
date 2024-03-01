using UnityEngine;

namespace Scripts.UpgradeSystem
{
    public class PriceMultiplicationWithBorder : PriceMultiplication
    {
        [SerializeField] private int _maxPrice;

        public override int GetChanged(int currentPrice)
        {
            int newPrice = base.GetChanged(currentPrice);

            if (newPrice > _maxPrice)
                return _maxPrice;
            else
                return newPrice;
        }
    }
}