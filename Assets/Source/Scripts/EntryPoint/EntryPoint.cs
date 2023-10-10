using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private SetSaveDataButton _setSaveDataButton;
    [SerializeField] private Button _bossMapExitButton;
    [SerializeField] private BossMapScroll _bossMapScroll;

    private GameSaver _saver;

    private void Awake()
    {
        StartCoroutine(Init());
    }

    private void OnDestroy()
    {
        _saver.Disable();
    }

    private IEnumerator Init()
    {
        yield return YandexGamesSdk.Initialize();
        InitLocalization();

        SaveSystem.Load(saveData =>
        {
            _setSaveDataButton.Init(saveData);
            Wallet wallet = new Wallet();
            _walletSetup.Init(wallet);
            wallet.Init(saveData.Coins);
            InitUpgrades(saveData.UpgradeDatas);
            InitUpgradeButtons(wallet);
            _bossMap.Init(saveData.BossAwards, saveData.BossDataIndex, wallet);
            _bossMapScroll.Init(saveData.BossMapContentYPosition);
            _saver = new GameSaver(_gameOverController, wallet, _bossLoader, _upgrades, saveData.BossAwards, _bossMapExitButton, _bossMapScroll);
            _saver.Enable();
            _bossLoader.Init(saveData.BossDataIndex);
            _gameOverController.Init(_bossLoader.CurrentBoss);
            _damageToCoinTranslator.Init(wallet, _bossLoader.CurrentBoss.BossHealth);
            BossAnimator bossAnimator = _bossLoader.CurrentBoss.GetComponent<BossAnimator>();
            bossAnimator.Init(_gameMoves);
            _bossHealthBar.Init(_bossLoader.CurrentBoss.BossHealth.Health);
            _bossDamageView.Init(_bossLoader.CurrentBoss);
            _battlefield.Init(_bossLoader.CurrentBoss);
            _playerHealthBar.Init(_playerHealth);
        });
    }

    private void InitLocalization()
    {
        LocalizationSetter localizationSetter = new LocalizationSetter();
        localizationSetter.Set(YandexGamesSdk.Environment.i18n.lang);
    }

    private void InitUpgrades(UpgradeData[] upgradesData)
    {
        for (int i = 0; i < _upgrades.Count; i++)
            _upgrades[i].Init(upgradesData[i].Level, upgradesData[i].Price, upgradesData[i].BonusValue);
    }

    private void InitUpgradeButtons(Wallet wallet)
    {
        for (int i = 0; i < _upgradeButtons.Count; i++)
            _upgradeButtons[i].Init(wallet);
    }
}
