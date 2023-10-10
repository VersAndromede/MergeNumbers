using UnityEngine;

public class IncomeUpgrade : Upgrade
{
    [SerializeField] private float _multiplierPrice;
    [SerializeField] private float _multiplierValue;

    public const int MinBonusValue = 25;

    public override int AffectValue()
    {
        if (BonusValue <= 0)
            return MinBonusValue;

        return Mathf.CeilToInt(BonusValue * _multiplierValue);
    }

    public override int AffectPrice()
    {
        return (int)Mathf.Round(Price * _multiplierPrice);
    }
}
