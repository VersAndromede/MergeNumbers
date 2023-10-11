using System.Collections.Generic;
using TrainingSystem;
using UnityEngine;
using UnityEngine.UI;

public class GameSaver
{
    private readonly GameOverController _gameOverController;
    private readonly Wallet _wallet;
    private readonly BossLoader _bossLoader;
    private readonly List<Upgrade> _upgrades;
    private readonly List<BossAward> _bossAwards;
    private readonly Button _bossMapExitButton;
    private readonly BossMapScroll _bossMapScroll;
    private readonly Training _training;

    public GameSaver(GameOverController gameOverController, Wallet wallet, BossLoader bossLoader, 
        List<Upgrade> upgrades, List<BossAward> bossAwards, Button bossMapExitButton,
        BossMapScroll bossMapScroll, Training training)
    {
        _gameOverController = gameOverController;
        _wallet = wallet;
        _bossLoader = bossLoader;
        _upgrades = upgrades;
        _bossAwards = bossAwards;
        _bossMapExitButton = bossMapExitButton;
        _bossMapScroll = bossMapScroll;
        _training = training;
    }

    public void Enable()
    {
        _gameOverController.GameOver += OnGameOver;
        _training.Viewed += OnTrainingViewed;
        _bossMapExitButton.onClick.AddListener(OnBossMapExitButton);

        foreach (Upgrade upgrade in _upgrades)
            upgrade.LevelChanged += OnUpgradeLevelChanged;

        foreach (BossAward bossAward in _bossAwards)
            bossAward.Taken += OnBossAwardTaken;
    }

    public void Disable()
    {
        _gameOverController.GameOver -= OnGameOver;
        _training.Viewed -= OnTrainingViewed;
        _bossMapExitButton.onClick.RemoveListener(OnBossMapExitButton);

        foreach (Upgrade upgrade in _upgrades)
            upgrade.LevelChanged -= OnUpgradeLevelChanged;

        foreach (BossAward bossAward in _bossAwards)
            bossAward.Taken -= OnBossAwardTaken;
    }

    private void OnGameOver(Winner winner)
    {
        SaveSystem.Save(data =>
        {
            data.Coins = _wallet.Coins;
            data.BossDataIndex = _bossLoader.BossDataIndex;

            if (data.BossAwards.Count == 0)
                data.BossAwards = _bossAwards;

            if (data.BossDataIndex >= data.BossAwards.Count || winner != Winner.Player)
                return;

            data.BossAwards[data.BossDataIndex - 1].LetTake();
            Debug.Log(JsonUtility.ToJson(data, true));
        });
    }

    private void OnUpgradeLevelChanged()
    {
        SaveSystem.Save(data =>
        {
            for (int i = 0; i < _upgrades.Count; i++)
            {
                data.UpgradeDatas[i] = new()
                {
                    Level = _upgrades[i].Level,
                    Price = _upgrades[i].Price,
                    BonusValue = _upgrades[i].BonusValue
                };
            }

            data.Coins = _wallet.Coins;
        });
    }

    private void OnBossAwardTaken(BossAward bossAward)
    {
        SaveSystem.Save(data =>
        {
            data.BossAwards[bossAward.Id] = bossAward;
            data.Coins = _wallet.Coins;
        });
    }

    private void OnBossMapExitButton()
    {
        SaveSystem.Save(data =>
        {
            data.BossMapContentYPosition = _bossMapScroll.YPosition;
        });
    }

    private void OnTrainingViewed()
    {
        SaveSystem.Save(data =>
        {
            data.TrainingIsViewed = true;
        });
    }
}
