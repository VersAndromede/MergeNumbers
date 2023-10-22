using Agava.YandexGames;
using System;
using UnityEngine;
using UnityEngine.Events;

public class RewardButton : MonoBehaviour
{
    [SerializeField] private IncomeUpgrade _incomeUpgrade;
    [SerializeField] private uint _defaulftRewardCount;
    [SerializeField] private UnityEvent _rewardGetted;

    private Wallet _wallet;

    public event Action RewardGetted;

    public void Init(Wallet wallet)
    {
        _defaulftRewardCount += (uint)_incomeUpgrade.BonusValue;
        _wallet = wallet;
    }

    public void GetReward()
    {
        VideoAd.Show(onRewardedCallback: () =>
        {
            _wallet.AddCoins(_defaulftRewardCount);
            RewardGetted?.Invoke();
            _rewardGetted?.Invoke();
        });
    }
}
