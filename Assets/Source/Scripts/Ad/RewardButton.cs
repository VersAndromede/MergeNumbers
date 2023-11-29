using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardButton : MonoBehaviour, IAd
{
    [SerializeField] private IncomeUpgrade _incomeUpgrade;
    [SerializeField] private Button _button;
    [SerializeField] private float _defaulftRewardCount;
    [SerializeField] private UnityEvent _rewardGetted;

    private const uint MinRewardCount = 150;
    private const float RewardAdDisplayDalay = 0.04f;

    private Wallet _wallet;
    private PauseController _pauseController;

    public uint RewardCount { get; private set; }

    public event Action RewardGetted;
    public event Action RewardChanged;
    public event Action<bool> AdRunning;

    private void OnDestroy()
    {
        _incomeUpgrade.LevelChanged -= OnIncomeUpgradeLevelChanged;
    }

    public void Init(PauseController pauseController, Wallet wallet)
    {
        OnIncomeUpgradeLevelChanged();
        _pauseController = pauseController;
        _wallet = wallet;
        _incomeUpgrade.LevelChanged += OnIncomeUpgradeLevelChanged;
    }

    public void GetReward()
    {
        _button.interactable = false;
        AdRunning?.Invoke(true);
        _pauseController.SetPause(true);
        StartCoroutine(StartGetReward());
    }

    private IEnumerator StartGetReward()
    {
        yield return new WaitForSecondsRealtime(RewardAdDisplayDalay);

        VideoAd.Show(onCloseCallback: () =>
        {
            _wallet.AddCoins(RewardCount);
            RewardGetted?.Invoke();
            _rewardGetted?.Invoke();
            Enable();
        },
        onErrorCallback: error =>
        {
            Enable();
        });
    }

    private void OnIncomeUpgradeLevelChanged()
    {
        uint rewardCount = (uint)Mathf.CeilToInt(_defaulftRewardCount * _incomeUpgrade.BonusValue);

        if (rewardCount < MinRewardCount)
            rewardCount = MinRewardCount;

        RewardCount = rewardCount;
        RewardChanged?.Invoke();
    }

    private void Enable()
    {
        _pauseController.SetPause(false);
        _button.interactable = true;
        AdRunning?.Invoke(false);
    }
}
