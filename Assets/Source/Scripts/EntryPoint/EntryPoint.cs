using Ad;
using Agava.YandexGames;
using Audio;
using BossAchievements;
using BossSystem;
using GameBattlefield;
using GameButtons;
using GameFocusObserver;
using GameInput;
using GameLocalizationSetter;
using GameOver;
using GameSaveSystem;
using HealthSystem;
using HitDamage;
using MainLeaderboard;
using MoveCounterSystem;
using Pause;
using PlayerSystem;
using PowerSystem;
using System.Collections.Generic;
using TrainingSystem;
using UnityEngine;
using UnityEngine.UI;
using Upgrades;
using WalletSystem;

namespace EntryPoints
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private WalletSetup _walletSetup;
        [SerializeField] private BossLoader _bossLoader;
        [SerializeField] private GameOverHandler _gameOverHandler;
        [SerializeField] private DamageToCoinsTranslator _damageToCoinTranslator;
        [SerializeField] private MoveCounter _moveCounter;
        [SerializeField] private Battlefield _battlefield;
        [SerializeField] private BossDamageView _bossDamageView;
        [SerializeField] private UpgradesInitializer _upgradesInitializer;
        [SerializeField] private List<UpgradeButton> _upgradeButtons;
        [SerializeField] private BossMap _bossMap;
        [SerializeField] private Button _bossMapExitButton;
        [SerializeField] private BossMapScroll _bossMapScroll;
        [SerializeField] private GameObject[] _pageContent;
        [SerializeField] private TrainingSetup _trainingSetup;
        [SerializeField] private CurrentTrainingPageView _currentTrainingPageView;
        [SerializeField] private TrainingCursor _trainingCursor;
        [SerializeField] private AudioButton _soundButton;
        [SerializeField] private AudioButton _musicButton;
        [SerializeField] private RewardButton _rewardButton;
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private ReturnToMenuButton[] _returnToMenuButtons;
        [SerializeField] private InterstitialAdsDisplay _interstitialAdsDisplay;
        [SerializeField] private FocusObserver _focusObserver;
        [SerializeField] private Image _lockPanel;
        [SerializeField] private LeaderboardUpdater _leaderboardUpdater;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private HitDamageCountSpawner _hitDamageCountSpawner;
        [SerializeField] private AttackReadinessIndicator _attackReadinessIndicator;
        [SerializeField] private Player _player;
        [SerializeField] private PowerSetup _powerSetup;
        [SerializeField] private PlayerMergeAbility _playerMergeAbility;

        private GameSaver _saver;

        private void OnDestroy()
        {
            _saver.Disable();
            _focusObserver.Disable();
        }

        private void Awake()
        {
            SaveSystem saveSystem = new SaveSystem();

            saveSystem.Load(saveData =>
            {
                InitLocalization();
                Training training = InitTraining(saveData.TrainingIsViewed);
                Wallet wallet = InitWallet(saveData.Coins);
                _upgradesInitializer.Init(saveData.UpgradeDatas);
                InitUpgradeButtons(wallet);
                Health playerHealth = new Health(0);
                Power power = InitPower(playerHealth);
                _playerMergeAbility.Init(power);
                PauseSetter pauseSetter = new PauseSetter();
                InitReturnToMenuButtons();
                _pauseButton.Init(pauseSetter);
                _interstitialAdsDisplay.Init(pauseSetter);
                _rewardButton.Init(pauseSetter, wallet);
                _soundButton.Init(saveData.IsSoundButtonEnabled);
                _musicButton.Init(saveData.IsMusicButtonEnabled);
                _bossMap.Fill(saveData.BossAwards, saveData.BossDataIndex, wallet);
                _bossMapScroll.Init(saveData.BossMapContentYPosition);
                _bossLoader.Init(saveData.BossDataIndex);
                _inputHandler.Init(training);
                _leaderboardUpdater.Init(wallet);
                _gameOverHandler.Init(_bossLoader.CurrentBoss, power, playerHealth);
                _damageToCoinTranslator.Init(wallet, _bossLoader.CurrentBoss.BossHealth);
                _attackReadinessIndicator.Init(_bossLoader.CurrentBoss.BossHealth);
                _trainingCursor.Init(_bossLoader.CurrentBoss.BossHealth, saveData.BossDataIndex);
                BossAnimator bossAnimator = _bossLoader.CurrentBoss.GetComponent<BossAnimator>();
                bossAnimator.Init(_moveCounter);
                _bossDamageView.Init(_bossLoader.CurrentBoss);
                _hitDamageCountSpawner.Init(_bossLoader.CurrentBoss.BossHealth);
                _battlefield.Init(_bossLoader.CurrentBoss, playerHealth);
                _focusObserver.Init(pauseSetter, _pauseButton, _rewardButton, _interstitialAdsDisplay);
                InitSaver(wallet, saveData.BossAwards, training);
                _focusObserver.Enable();
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

        private Training InitTraining(bool trainingIsViewed)
        {
            Training training = new Training(trainingIsViewed, _pageContent);
            _trainingSetup.Init(training);
            _currentTrainingPageView.Init(training);
            return training;
        }

        private Wallet InitWallet(int coins)
        {
            Wallet wallet = new Wallet();
            _walletSetup.Init(wallet);
            wallet.Init(coins);
            return wallet;
        }

        private void InitUpgradeButtons(Wallet wallet)
        {
            for (int i = 0; i < _upgradeButtons.Count; i++)
                _upgradeButtons[i].Init(wallet);
        }

        private void InitReturnToMenuButtons()
        {
            for (int i = 0; i < _returnToMenuButtons.Length; i++)
                _returnToMenuButtons[i].Init();
        }

        private Power InitPower(Health health)
        {
            Power power = new Power(0);
            _powerSetup.Init(power);
            _player.Init(health, power);
            return power;
        }

        private void InitSaver(Wallet wallet, List<BossAward> bossAwards, Training training)
        {
            _saver = new GameSaver(_gameOverHandler, wallet,
                _bossLoader, _upgradesInitializer.Upgrades,
                bossAwards, _bossMapExitButton,
                _bossMapScroll, training,
                _rewardButton, _soundButton,
                _musicButton);

            _saver.Enable();
        }
    }
}