using System.Collections;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameMoves _gameMoves;
    [SerializeField] private DamageUpgrade _damageUpgrade;

    private WaitForSeconds _waitTime;
    private Boss _boss;

    private void OnEnable()
    {
        _gameMoves.Ended += Fight;
    }

    private void OnDisable()
    {
        _gameMoves.Ended -= Fight;
    }

    public void Init(Boss boss)
    {
        _boss = boss;
        _waitTime = new WaitForSeconds(_boss.RechargeTime);
    }

    private void Fight()
    {
        StartCoroutine(StartFight());
    }

    private IEnumerator StartFight()
    {
        _player.UpgradeDamage(_damageUpgrade);
        _boss.Init(_player.Damage);
        StartCoroutine(_boss.BossHealth.MakeVulnerable());
        yield return _waitTime;

        while (_player.Health.Value > 0 && _boss.BossHealth.Health.Value > 0)
        {
            _player.Health.TakeDamage(_boss.Data.Damage);
            yield return _waitTime;
        }
    }
}
