using System.Collections.Generic;
using TrainingSystem;
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
    private readonly RewardButton _rewardButton;
    private readonly AudioButton _soundButton;
    private readonly AudioButton _musicButton;

    public GameSaver(GameOverController gameOverController, Wallet wallet, BossLoader bossLoader, 
        List<Upgrade> upgrades, List<BossAward> bossAwards, Button bossMapExitButton,
        BossMapScroll bossMapScroll, Training training, RewardButton rewardButton, AudioButton soundButton, AudioButton musicButton)
    {
        _gameOverController = gameOverController;
        _wallet = wallet;
        _bossLoader = bossLoader;
        _upgrades = upgrades;
        _bossAwards = bossAwards;
        _bossMapExitButton = bossMapExitButton;
        _bossMapScroll = bossMapScroll;
        _training = training;
        _rewardButton = rewardButton;
        _soundButton = soundButton;
        _musicButton = musicButton;
    }

    public void Enable()
    {
        _gameOverController.GameOver += OnGameOver;
        _training.Viewed += OnTrainingViewed;
        _rewardButton.RewardGetted += OnRewardGetted;
        _soundButton.EnabledChanged += OnSoundButtonEnabledChanged;
        _musicButton.EnabledChanged += OnMusicButtonEnabledChanged;
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
        _rewardButton.RewardGetted -= OnRewardGetted;
        _soundButton.EnabledChanged -= OnSoundButtonEnabledChanged;
        _musicButton.EnabledChanged -= OnMusicButtonEnabledChanged;
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

            if (data.BossDataIndex - 1 >= data.BossAwards.Count || winner != Winner.Player)
                return;

            data.BossAwards[data.BossDataIndex - 1].LetTake();
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

    private void OnRewardGetted()
    {
        SaveSystem.Save(data =>
        {
            data.Coins = _wallet.Coins;
        });
    }

    private void OnSoundButtonEnabledChanged()
    {
        SaveSystem.Save(data =>
        {
            data.IsSoundButtonEnabled = _soundButton.Enabled;
        });
    }

    private void OnMusicButtonEnabledChanged()
    {
        SaveSystem.Save(data =>
        {
            data.IsMusicButtonEnabled = _musicButton.Enabled;
        });
    }
}
