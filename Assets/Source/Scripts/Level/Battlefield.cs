using Scripts.Level.BossSystem;
using Scripts.Level.HealthSystems;
using Scripts.Level.MoveCounterSystem;
using Scripts.Level.PlayerSystem;
using Scripts.UpgradeSystem;
using System.Collections;
using UnityEngine;

namespace Scripts.Level
{
    public class Battlefield : MonoBehaviour
    {
        [SerializeField] private PlayerDamage _playerDamage;
        [SerializeField] private MoveCounter _moveCounter;
        [SerializeField] private UpgradeWithMultiplicationValuePolicy _damageUpgrade;
        [SerializeField] private float _bossRechargeTime;

        private WaitForSeconds _waitTime;
        private Boss _boss;
        private Health _playerHealth;

        private BossHealth BossHealth => _boss.BossHealth;

        private void OnDestroy()
        {
            _moveCounter.Ended -= Fight;
        }

        public void Init(Boss boss, Health playerHealth)
        {
            _boss = boss;
            _playerHealth = playerHealth;
            _waitTime = new WaitForSeconds(_bossRechargeTime);
            _moveCounter.Ended += Fight;
        }

        private void Fight()
        {
            StartCoroutine(StartFight());
        }

        private IEnumerator StartFight()
        {
            _playerDamage.Upgrade(_damageUpgrade);
            _boss.BossHealth.SetDamageTaken(_playerDamage.Count);
            StartCoroutine(BossHealth.MakeVulnerable());
            yield return _waitTime;

            while (_playerHealth.IsDied == false && _playerHealth.Value >= 0 && BossHealth.HealthCount > 0)
            {
                _playerHealth.TakeDamage(_boss.Data.Damage);
                yield return _waitTime;
            }
        }
    }
}