using UnityEngine;

public class MovesUpgrade : Upgrade
{
    [SerializeField] private int _addedValue;
    [SerializeField] private int _maxPrice;

    public override int AffectValue()
    {
        return BonusValue + _addedValue;
    }

    public override int AffectPrice()
    {
        int newPrice = Mathf.CeilToInt(Price * 2.5f);

        if (newPrice > _maxPrice)
            return _maxPrice;
        else
            return newPrice;
    }
}
