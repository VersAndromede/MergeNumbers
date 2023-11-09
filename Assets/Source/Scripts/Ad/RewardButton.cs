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
    [SerializeField] private uint _defaulftRewardCount;
    [SerializeField] private UnityEvent _rewardGetted;

    private const float RewardMultiplier = 1.6f;
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
        RewardCount = _defaulftRewardCount + (uint)(_incomeUpgrade.BonusValue * RewardMultiplier);
        RewardChanged?.Invoke();
    }

    private void Enable()
    {
        _pauseController.SetPause(false);
        _button.interactable = true;
        AdRunning?.Invoke(false);
    }
}
