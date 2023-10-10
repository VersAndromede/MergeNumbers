using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    private Wallet _wallet;

    public void Init(Wallet wallet)
    {
        _wallet = wallet;
    }

    public void TryBuyUpgrade(Upgrade upgrade)
    {
        if (upgrade.CanImprove && _wallet.IsSolvent(upgrade.Price))
        {
            _wallet.RemoveCoins((uint)upgrade.Price);
            upgrade.Improve();
        }
    }
}