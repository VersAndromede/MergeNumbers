using System.Collections;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameMoves _gameMoves;
    [SerializeField] private DamageUpgrade _damageUpgrade;
    [SerializeField] private HealthSetup _bossHealthSetup;
    [SerializeField] public float _bossRechargeTime;

    private WaitForSeconds _waitTime;
    private Boss _boss;
    private Health _playerHealth;

    private BossHealth _bossHealth => _boss.BossHealth;

    private void OnDestroy()
    {
        _gameMoves.Ended -= Fight;
    }

    public void Init(Boss boss, Health playerHealth)
    {
        _boss = boss;
        _playerHealth = playerHealth;
        _waitTime = new WaitForSeconds(_bossRechargeTime);
        _gameMoves.Ended += Fight;
    }

    private void Fight()
    {
        StartCoroutine(StartFight());
    }

    private IEnumerator StartFight()
    {
        _player.UpgradeDamage(_damageUpgrade);
        _boss.Init(_bossHealthSetup, _player.Damage);
        StartCoroutine(_bossHealth.MakeVulnerable());
        yield return _waitTime;

        while (_playerHealth.IsDied == false && _playerHealth.Value >= 0 && _bossHealth.HealthCount> 0)
        {
            _playerHealth.TakeDamage(_boss.Data.Damage);
            yield return _waitTime;
        }
    }
}