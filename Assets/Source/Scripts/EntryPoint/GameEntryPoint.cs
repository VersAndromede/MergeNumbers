using Scripts.Ad;
using Scripts.Localization;
using Agava.YandexGames;
using Scripts.UI.Menu.BossAchievements;
using Scripts.GameSaveSystem;
using Scripts.MainLeaderboard;
using System.Collections.Generic;
using Scripts.TrainingSystem;
using UnityEngine;
using UnityEngine.UI;
using Scripts.UpgradeSystem;
using Scripts.WalletSystem;
using Scripts.Level.PowerSystem;
using Scripts.Level.BossSystem;
using Scripts.Level.GameInput;
using Scripts.Level.GameOver;
using Scripts.Level;
using Scripts.Level.MoveCounterSystem;
using Scripts.Level.HealthSystems;
using Scripts.UI.Menu;

namespace Scripts.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private WalletInitialization _walletInitialization;
        [SerializeField] private PowerInitialization _powerInitialization;
        [SerializeField] private BossInitialization _bossInitialization;
        [SerializeField] private ButtonsInitialization _buttonsInitialization;
        [SerializeField] private TrainingInitialization _trainingInitialization;
        [SerializeField] private UpgradesInitializion _upgradesInitializion;
        [SerializeField] private InterstitialAdsDisplay _interstitialAdsDisplay;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private LeaderboardUpdater _leaderboardUpdater;
        [SerializeField] private GameOverHandler _gameOverHandler;
        [SerializeField] private Battlefield _battlefield;
        [SerializeField] private MoveCounter _moveCounter;
        [SerializeField] private Button _bossMapExitButton;
        [SerializeField] private Image _lockPanel;

        private GameSaver _saver;

        private Wallet Wallet => _walletInitialization.Wallet;

        private void OnDestroy()
        {
            _saver.Disable();
        }

        private void Awake()
        {
            SaveSystem saveSystem = new SaveSystem();

            saveSystem.Load(saveData =>
            {
                InitLocalization();
                Health playerHealth = new Health(0);
                _walletInitialization.Init(saveData.Coins);
                _powerInitialization.Init(playerHealth);
                _bossInitialization.Init(saveData, Wallet, _moveCounter);
                _buttonsInitialization.Init(
                    saveData.IsSoundButtonEnabled,
                    saveData.IsMusicButtonEnabled,
                    _interstitialAdsDisplay,
                    Wallet);

                BossLoader bossLoader = _bossInitialization.BossLoader;
                BossHealth bossHealth = bossLoader.CurrentBoss.BossHealth;
                _trainingInitialization.Init(bossHealth, saveData.BossDataIndex, saveData.TrainingIsViewed);
                _upgradesInitializion.Init(saveData.UpgradeDatas, Wallet);
                _interstitialAdsDisplay.Init(_buttonsInitialization.PauseSetter);
                _inputHandler.Init(_trainingInitialization.Training);
                _leaderboardUpdater.Init(Wallet);
                _gameOverHandler.Init(bossLoader.CurrentBoss, _powerInitialization.Power, playerHealth);
                _battlefield.Init(bossLoader.CurrentBoss, playerHealth);
                InitSaver(Wallet, saveData.BossAwards, _trainingInitialization.Training);

                _lockPanel.raycastTarget = false;
            });
        }

        private void InitLocalization()
        {
            if (Application.isEditor)
                return;

            LocalizationSetter localizationSetter = new LocalizationSetter();
            localizationSetter.Set(YandexGamesSdk.Environment.i18n.lang);
        }

        private void InitSaver(Wallet wallet, List<BossAward> bossAwards, Training training)
        {
            _saver = new GameSaver(_gameOverHandler, 
                wallet,
                _bossInitialization.BossLoader, 
                _upgradesInitializion.Upgrades,
                bossAwards, 
                _bossMapExitButton,
                _bossInitialization.BossMapScroll, 
                training,
                _buttonsInitialization.RewardButton,
                _buttonsInitialization.SoundButton,
                _buttonsInitialization.MusicButton);

            _saver.Enable();
        }
    }
}