using Agava.YandexGames;
using Plugins.Audio.Core;
using System.Collections.Generic;
using TrainingSystem;
using UnityEngine;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private WalletSetup _walletSetup;
    [SerializeField] private BossLoader _bossLoader;
    [SerializeField] private GameOverController _gameOverController;
    [SerializeField] private DamageToCoinsTranslator _damageToCoinTranslator;
    [SerializeField] private GameMoves _gameMoves;
    [SerializeField] private Battlefield _battlefield;
    [SerializeField] private HealthBar _playerHealthBar;
    [SerializeField] private HealthBar _bossHealthBar;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private BossDamageView _bossDamageView;
    [SerializeField] private List<Upgrade> _upgrades;
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

    private GameSaver _saver;

    private void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        _saver.Disable();
        _focusObserver.Disable();
    }

    private void Init()
    {
        SaveSystem.Load(saveData =>
        {
            InitLocalization();
            InitTraining(saveData.TrainingIsViewed, out Training training);
            InitWallet(saveData.Coins, out Wallet wallet);
            InitUpgrades(saveData.UpgradeDatas);
            InitUpgradeButtons(wallet);
            PauseController pauseController = new PauseController();
            InitReturnToMenuButtons();
            _pauseButton.Init(pauseController);
            _interstitialAdsDisplay.Init(pauseController);
            _rewardButton.Init(pauseController, wallet);
            _soundButton.Init(saveData.IsSoundButtonEnabled);
            _musicButton.Init(saveData.IsMusicButtonEnabled);
            _bossMap.Init(saveData.BossAwards, saveData.BossDataIndex, wallet);
            _bossMapScroll.Init(saveData.BossMapContentYPosition);
            _bossLoader.Init(saveData.BossDataIndex);
            _inputHandler.Init(training);
            _leaderboardUpdater.Init(wallet);
            _gameOverController.Init(_bossLoader.CurrentBoss);
            _damageToCoinTranslator.Init(wallet, _bossLoader.CurrentBoss.BossHealth);
            _attackReadinessIndicator.Init(_bossLoader.CurrentBoss.BossHealth);
            _trainingCursor.Init(_bossLoader.CurrentBoss.BossHealth, saveData.BossDataIndex);
            BossAnimator bossAnimator = _bossLoader.CurrentBoss.GetComponent<BossAnimator>();
            bossAnimator.Init(_gameMoves);
            _bossHealthBar.Init(_bossLoader.CurrentBoss.BossHealth.Health);
            _bossDamageView.Init(_bossLoader.CurrentBoss);
            _hitDamageCountSpawner.Init(_bossLoader.CurrentBoss.BossHealth);

            _battlefield.Init(_bossLoader.CurrentBoss);
            _playerHealthBar.Init(_playerHealth);
            _focusObserver.Init(pauseController, _pauseButton, _rewardButton, _interstitialAdsDisplay);

            _saver = new GameSaver(_gameOverController, wallet, _bossLoader, _upgrades, saveData.BossAwards, _bossMapExitButton, _bossMapScroll, training, _rewardButton, _soundButton, _musicButton);
            _saver.Enable();
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

    private void InitTraining(bool trainingIsViewed, out Training training)
    {
        training = new Training(trainingIsViewed, _pageContent);
        _trainingSetup.Init(training);
        _currentTrainingPageView.Init(training);
    }

    private void InitWallet(int coins, out Wallet wallet)
    {
        wallet = new Wallet();
        _walletSetup.Init(wallet);
        wallet.Init(coins);
    }

    private void InitUpgrades(UpgradeData[] upgradesData)
    {
        if (upgradesData[0] == null)
            for (int i = 0; i < upgradesData.Length; i++)
                upgradesData[i] = new UpgradeData();

        for (int i = 0; i < _upgrades.Count; i++)
            _upgrades[i].Init(upgradesData[i].Level, upgradesData[i].Price, upgradesData[i].BonusValue);
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
}
