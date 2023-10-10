using UnityEngine;

public class DamageToCoinsTranslator : MonoBehaviour
{
    [SerializeField] private IncomeUpgrade _incomeUpgrade;

    private Wallet _wallet;
    private BossHealth _bossHealth;

    public void Init(Wallet wallet, BossHealth bossHealth)
    {
        _wallet = wallet;
        _bossHealth = bossHealth;
        _bossHealth.DamageReceived += OnDamageReceived;
    }

    private void OnDestroy()
    {
        _bossHealth.DamageReceived -= OnDamageReceived;
    }

    private void OnDamageReceived(int damageTaken)
    {
        uint bonusValue = (uint)_incomeUpgrade.BonusValue;

        if (bonusValue >= IncomeUpgrade.MinBonusValue)
            _wallet.AddCoins(bonusValue + IncomeUpgrade.MinBonusValue);
        else
            _wallet.AddCoins(IncomeUpgrade.MinBonusValue);
    }
}
