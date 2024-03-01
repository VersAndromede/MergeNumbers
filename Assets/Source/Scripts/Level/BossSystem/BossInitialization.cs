using Scripts.UI.Menu.BossAchievements;
using Scripts.GameSaveSystem;
using Scripts.HitDamageCountView;
using Scripts.Level.MoveCounterSystem;
using Scripts.WalletSystem;
using UnityEngine;

namespace Scripts.Level.BossSystem
{
    public class BossInitialization : MonoBehaviour
    {
        [SerializeField] private BossDamageView _bossDamageView;
        [SerializeField] private BossMap _bossMap;
        [SerializeField] private DamageToCoinsTranslator _damageToCoinTranslator;
        [SerializeField] private HitDamageCountSpawner _hitDamageCountSpawner;
        [SerializeField] private AttackReadinessIndicator _attackReadinessIndicator;

        [field: SerializeField] public BossLoader BossLoader { get; private set; }
        [field: SerializeField] public BossMapScroll BossMapScroll { get; private set; }

        public void Init(SaveData saveData, Wallet wallet, MoveCounter moveCounter)
        {
            _bossMap.Fill(saveData.BossAwards, saveData.BossDataIndex, wallet);
            BossMapScroll.Init(saveData.BossMapContentYPosition);
            BossLoader.Init(saveData.BossDataIndex);
            _damageToCoinTranslator.Init(wallet, BossLoader.CurrentBoss.BossHealth);
            _bossDamageView.Init(BossLoader.CurrentBoss);
            _attackReadinessIndicator.Init(BossLoader.CurrentBoss.BossHealth);
            _hitDamageCountSpawner.Init(BossLoader.CurrentBoss.BossHealth);

            BossAnimator bossAnimator = BossLoader.CurrentBoss.GetComponent<BossAnimator>();
            bossAnimator.Init(moveCounter);
        }
    }
}