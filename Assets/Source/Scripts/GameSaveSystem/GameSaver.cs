using Scripts.Ad;
using Scripts.Audio;
using Scripts.Level.BossSystem;
using Scripts.Level.GameOver;
using Scripts.TrainingSystem;
using Scripts.UI.Menu.BossAchievements;
using Scripts.UpgradeSystem;
using Scripts.WalletSystem;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Scripts.GameSaveSystem
{
    public class GameSaver
    {
        private readonly SaveSystem _saveSystem = new SaveSystem();

        private readonly GameOverHandler _gameOverHandler;
        private readonly Wallet _wallet;
        private readonly BossLoader _bossLoader;
        private readonly IReadOnlyList<Upgrade> _upgrades;
        private readonly List<BossAward> _bossAwards;
        private readonly Button _bossMapExitButton;
        private readonly BossMapScroll _bossMapScroll;
        private readonly Training _training;
        private readonly RewardButton _rewardButton;
        private readonly AudioButton _soundButton;
        private readonly AudioButton _musicButton;

        public GameSaver(
            GameOverHandler gameOverHandler,
            Wallet wallet,
            BossLoader bossLoader,
            IReadOnlyList<Upgrade> upgrades,
            List<BossAward> bossAwards,
            Button bossMapExitButton,
            BossMapScroll bossMapScroll,
            Training training,
            RewardButton rewardButton,
            AudioButton soundButton,
            AudioButton musicButton)
        {
            _gameOverHandler = gameOverHandler;
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
            _gameOverHandler.GameOver += OnGameOver;
            _training.Viewed += OnTrainingViewed;
            _rewardButton.RewardReceived += OnRewardGetted;
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
            _gameOverHandler.GameOver -= OnGameOver;
            _training.Viewed -= OnTrainingViewed;
            _rewardButton.RewardReceived -= OnRewardGetted;
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
            _saveSystem.Save(data =>
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
            _saveSystem.Save(data =>
            {
                for (int i = 0; i < _upgrades.Count; i++)
                {
                    data.UpgradeDatas[i] = new()
                    {
                        Level = _upgrades[i].Level,
                        Price = _upgrades[i].Price,
                        BonusValue = _upgrades[i].BonusValue,
                    };
                }

                data.Coins = _wallet.Coins;
            });
        }

        private void OnBossAwardTaken(BossAward bossAward)
        {
            _saveSystem.Save(data =>
            {
                data.BossAwards[bossAward.Id] = bossAward;
                data.Coins = _wallet.Coins;
            });
        }

        private void OnBossMapExitButton()
        {
            _saveSystem.Save(data =>
            {
                data.BossMapContentYPosition = _bossMapScroll.YPosition;
            });
        }

        private void OnTrainingViewed()
        {
            _saveSystem.Save(data =>
            {
                data.TrainingIsViewed = true;
            });
        }

        private void OnRewardGetted()
        {
            _saveSystem.Save(data =>
            {
                data.Coins = _wallet.Coins;
            });
        }

        private void OnSoundButtonEnabledChanged()
        {
            _saveSystem.Save(data =>
            {
                data.IsSoundButtonEnabled = _soundButton.Enabled;
            });
        }

        private void OnMusicButtonEnabledChanged()
        {
            _saveSystem.Save(data =>
            {
                data.IsMusicButtonEnabled = _musicButton.Enabled;
            });
        }
    }
}