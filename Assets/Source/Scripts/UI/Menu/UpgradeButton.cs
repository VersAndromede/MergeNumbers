using UnityEngine;
using UnityEngine.Events;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private UnityEvent _upgraded;

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
            _upgraded?.Invoke();
        }
    }
}