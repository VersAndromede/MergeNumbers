using UnityEngine;

public class DamageUpgrade : Upgrade
{
    [SerializeField] private float _multiplierValue;
    [SerializeField] private float _multiplierPrice;

    public override int AffectValue()
    {
        int newBonusValue = (int)Mathf.Round(BonusValue * _multiplierValue);
        int minNewBonusValue = 5;
        return Mathf.Clamp(newBonusValue, minNewBonusValue, int.MaxValue);
    }

    public override int AffectPrice()
    {
        return (int)Mathf.Round(Price * _multiplierPrice);
    }
}